using OTLog.Core.Constants;
using OTLog.Core.Events;
using OTLog.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OTLog.Core.Utils;
using OTLog.Core.Extensions;

namespace OTLog.Home.ViewModels
{
    public class NoticeViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        #region private
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private OTRecordTodo _record;
        private DateTime? _beginDate;
        private DateTime? _beginTime;
        private DateTime? _endTime;
        private string _remark;
        private bool _finished;
        #endregion 

        public bool Finished
        {
            get => _finished;
            set => SetProperty(ref _finished, value);
        }


        public DateTime? BeginDate
        {
            get => _beginDate;
            set => SetProperty(ref _beginDate, value);
        }

        public DateTime? BeginTime
        {
            get => _beginTime;
            set
            {
                SetProperty(ref _beginTime, value);
                RaisePropertyChanged(nameof(TotalTime));
                RaisePropertyChanged(nameof(CanCreate));
                RaisePropertyChanged(nameof(IsNextDay));
            }
        }
        public DateTime? EndTime
        {
            get => _endTime;
            set
            {
                SetProperty(ref _endTime, value);
                RaisePropertyChanged(nameof(TotalTime));
                RaisePropertyChanged(nameof(CanCreate));
                RaisePropertyChanged(nameof(IsNextDay));
            }
        }

        public string Remark
        {
            get => _remark;
            set => SetProperty(ref _remark, value);
        }

        public TimeSpan? TotalTime
            => EndTime < BeginTime
               ? EndTime.Value.AddDays(1) - BeginTime
               : EndTime - BeginTime;

        public bool CanCreate => BeginDate != null && BeginTime != null && EndTime != null;
        public bool IsNextDay => EndTime?.TimeOfDay < BeginTime?.TimeOfDay;

        public DelegateCommand NeglectCommand { get; }
        private void Neglect()
        {
            Record.Status = Core.Enums.TodoStatus.Negligible;
            SaveRecord();
            Finished = true;
        }
        public DelegateCommand ResetRecordCommand { get; }
        private void ResetRecord()
        {
            Record = null;
        }
        public DelegateCommand ConfirmCommand { get; }
        private void Confirm()
        {
            OTRecord newRecord = new OTRecord
            {
                Id = Guid.NewGuid(),
                BeginTime = BeginDate.Value + BeginTime.Value.TimeOfDay,
                EndTime = EndTime < BeginTime
                          ? BeginDate.Value.AddDays(1) + EndTime.Value.TimeOfDay
                          : BeginDate.Value + EndTime.Value.TimeOfDay,
                Remark = Remark
            };

            List<OTRecord> records = AppFileHelper.GetOTRecords();
            OTRecord conflictRecord = records.FirstOrDefault(r => r.CheckIsCoincidence(newRecord));
            if (conflictRecord != null)
            {
                (DateTime? beginTime, DateTime? endTime) = conflictRecord.CoincidenceInterval(newRecord);
                string errorMessage = $"{beginTime.Value.Month} 月 {beginTime.Value.Day} 日 {beginTime:HH:mm:ss} - {endTime.Value.Month} 月 {endTime.Value.Day} 日 {endTime:HH:mm:ss} 已存在记录，添加失败。";
                _regionManager.RequestNavigate(
                    RegionNames.ErrorRegion,
                    ViewNames.ErrorTips,
                    new NavigationParameters
                    {
                        { "Message", errorMessage }
                    });
                return;
            }
            records.Add(newRecord);
            AppFileHelper.SaveOTRecords(records);

            Record.Status = Core.Enums.TodoStatus.Negligible;
            SaveRecord();

            Finished = true;
        }

        private void SaveRecord()
            => _eventAggregator.GetEvent<OTRecordTodoChangedEvent>().Publish();

        public OTRecordTodo Record
        {
            get => _record;
            set => SetProperty(ref _record, value);
        }

        public NoticeViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            NeglectCommand = new DelegateCommand(Neglect);
            ConfirmCommand = new DelegateCommand(Confirm);

            ResetRecordCommand = new DelegateCommand(ResetRecord);
        }

        public bool KeepAlive => false;

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Record = navigationContext.Parameters["Todo"] as OTRecordTodo;

            if (Record != null)
            {
                BeginDate = Record.BeginTime.Date;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
