using OTLog.Core.Constants;
using OTLog.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OTLog.Home.ViewModels
{
    public class NewItemViewModel : BindableBase
    {
        #region private
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private DateTime? _beginDate;
        private DateTime? _beginTime;
        private DateTime? _endTime;
        private string _remark;
        #endregion 

        public DateTime? BeginDate
        {
            get => _beginDate;
            set
            {
                SetProperty(ref _beginDate, value);
                RaisePropertyChanged(nameof(CanCreate));
            }
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

        public DelegateCommand CancelCommand { get; }
        private void Cancel()
        {
            _regionManager.Regions[RegionNames.MessageRegion].RemoveAll();
        }

        public DelegateCommand ConfirmCommand { get; }
        private void Confirm()
        {
            _eventAggregator.GetEvent<NewOTRecordEvent>().Publish(new Core.Models.OTRecord
            {
                BeginTime = BeginDate.Value + BeginTime.Value.TimeOfDay,
                EndTime = EndTime < BeginTime 
                          ? BeginDate.Value.AddDays(1) + EndTime.Value.TimeOfDay 
                          : BeginDate.Value + EndTime.Value.TimeOfDay,
                Remark = Remark
            });
        }

        public NewItemViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            ConfirmCommand = new DelegateCommand(Confirm);
            CancelCommand = new DelegateCommand(Cancel);
        }
    }
}
