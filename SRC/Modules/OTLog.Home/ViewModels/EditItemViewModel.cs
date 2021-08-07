using OTLog.Core.Constants;
using OTLog.Core.Events;
using OTLog.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

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
        #endregion 


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

        public bool CanEdit => BeginDate != null && BeginTime != null && EndTime != null;
        public bool IsNextDay => EndTime?.TimeOfDay < BeginTime?.TimeOfDay;

        public DelegateCommand CancelCommand { get; }
        private void Cancel()
        {
            _regionManager.Regions[RegionNames.MessageRegion].RemoveAll();
        }

        public DelegateCommand ConfirmCommand { get; }
        private void Confirm()
        {
            _eventAggregator.GetEvent<OTRecordChangedEvent>().Publish(new OTRecord
            {
                BeginTime = BeginDate.Value + BeginTime.Value.TimeOfDay,
                EndTime = EndTime?.TimeOfDay < BeginTime?.TimeOfDay
                          ? BeginDate.Value.AddDays(1) + EndTime.Value.TimeOfDay
                          : BeginDate.Value + EndTime.Value.TimeOfDay,
                Id = Id,
                Remark = Remark
            });
        }

        public EditItemViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            ConfirmCommand = new DelegateCommand(Confirm);
            CancelCommand = new DelegateCommand(Cancel);
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
