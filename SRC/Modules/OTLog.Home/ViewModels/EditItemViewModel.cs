using OTLog.Core.Constants;
using OTLog.Core.Events;
using OTLog.Core.Models;
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
        #endregion 


        public Guid Id { get; set; }

        public DateTime? BeginTime
        {
            get => _beginTime;
            set
            {
                SetProperty(ref _beginTime, value);
                RaisePropertyChanged(nameof(TotalTime));
            }
        }
        public DateTime? EndTime
        {
            get => _endTime;
            set
            {
                SetProperty(ref _endTime, value);
                RaisePropertyChanged(nameof(TotalTime));
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
                BeginTime = BeginTime,
                EndTime = EndTime,
                Id = Id,
                Remark = Remark
            });
            _regionManager.Regions[RegionNames.MessageRegion].RemoveAll();
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
            BeginTime = otRecord.BeginTime;
            EndTime = otRecord.EndTime;
            Remark = otRecord.Remark;
        }
    }
}
