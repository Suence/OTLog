using OTLog.Core.Constants;
using OTLog.Core.Enums;
using OTLog.Core.Events;
using OTLog.Core.Extensions;
using OTLog.Core.Models;
using OTLog.Core.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OTLog.Home.ViewModels
{
    public class EditItemViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        #region private
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private DateTime? _beginTime;
        private DateTime? _endTime;
        private string _remark;
        private DateTime? _beginDate;
        private DialogResult _dialogResult;
        #endregion

        public DialogResult DialogResult
        {
            get => _dialogResult;
            set => SetProperty(ref _dialogResult, value);
        }

        public Guid Id { get; set; }

        public DateTime? BeginDate
        {
            get => _beginDate;
            set
            {
                SetProperty(ref _beginDate, value);
                RaisePropertyChanged(nameof(CanEdit));
            }
        }

        public DateTime? BeginTime
        {
            get => _beginTime;
            set
            {
                SetProperty(ref _beginTime, value);
                RaisePropertyChanged(nameof(TotalTime));
                RaisePropertyChanged(nameof(CanEdit));
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
                RaisePropertyChanged(nameof(CanEdit));
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

        public OTRecord NewRecord => new()
        {
            BeginTime = BeginDate.Value + BeginTime.Value.TimeOfDay,
            EndTime = EndTime?.TimeOfDay < BeginTime?.TimeOfDay
                                      ? BeginDate.Value.AddDays(1) + EndTime.Value.TimeOfDay
                                      : BeginDate.Value + EndTime.Value.TimeOfDay,
            Id = Id,
            Remark = Remark
        };

        public bool CanEdit => BeginDate != null && BeginTime != null && EndTime != null;
        public bool IsNextDay => EndTime?.TimeOfDay < BeginTime?.TimeOfDay;

        public DelegateCommand CancelCommand { get; }
        private void Cancel() => DialogResult = DialogResult.Cancelled;

        public DelegateCommand ConfirmCommand { get; }
        private void Confirm()
        {
            List<OTRecord> records = AppFileHelper.GetOTRecords();
            OTRecord conflictRecord = records.FirstOrDefault(r => r.CheckIsCoincidence(NewRecord));
            if (conflictRecord != null)
            {
                (DateTime? beginTime, DateTime? endTime) = conflictRecord.CoincidenceInterval(NewRecord);
                string errorMessage = $"{beginTime.Value.Month} 月 {beginTime.Value.Day} 日 {beginTime:HH:mm:ss} - {endTime.Value.Month} 月 {endTime.Value.Day} 日 {endTime:HH:mm:ss} 已存在记录，添加失败。";

                _regionManager.Regions[RegionNames.ErrorRegion].RemoveAll();
                _regionManager.RequestNavigate(
                    RegionNames.ErrorRegion,
                    ViewNames.ErrorTips,
                    new NavigationParameters
                    {
                        { "Message", errorMessage }
                    });
                return;
            }

            DialogResult = DialogResult.Confirmed;
        }

        public DelegateCommand FinishedCommand { get; }
        private void Finished()
        {
            if (DialogResult == DialogResult.Confirmed)
            {
                _eventAggregator.GetEvent<OTRecordChangedEvent>().Publish(NewRecord);
            }

            _regionManager.Regions[RegionNames.ErrorRegion].RemoveAll();
            _regionManager.Regions[RegionNames.MessageRegion].RemoveAll();
        }

        public EditItemViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            ConfirmCommand = new DelegateCommand(Confirm);
            CancelCommand = new DelegateCommand(Cancel);
            FinishedCommand = new DelegateCommand(Finished);
        }

        public bool KeepAlive => false;

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            OTRecord otRecord = navigationContext.Parameters["Record"] as OTRecord;

            Id = otRecord.Id;
            BeginDate = otRecord.BeginTime.Value.Date;
            BeginTime = otRecord.BeginTime;
            EndTime = otRecord.EndTime;
            Remark = otRecord.Remark;
        }
    }
}
