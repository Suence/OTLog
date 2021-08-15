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

namespace OTLog.Home.ViewModels
{
    public class NoticeViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        #region private

        private DateTime? _beginDate;
        private DateTime? _beginTime;
        private DateTime? _endTime;
        private string _remark;
        #endregion 


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
            records.Add(newRecord);
            AppFileHelper.SaveOTRecords(records);

            Record.Status = Core.Enums.TodoStatus.Negligible;
            SaveRecord();
        }

        private void SaveRecord()
            => _eventAggregator.GetEvent<OTRecordTodoChangedEvent>().Publish();
        #region private
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private OTRecordTodo _record;
        private string _userName;
        #endregion
        
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

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

            //UserName = WindowsIdentity.GetCurrent().Name;
            UserName = Environment.UserName;
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
