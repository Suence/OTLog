using OTLog.Core.Constants;
using OTLog.Core.Events;
using OTLog.Core.Models;
using OTLog.Core.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OTLog.Home.ViewModels
{
    public class OverviewViewModel : BindableBase, IRegionMemberLifetime
    {
        #region private
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<OTRecord> _otRecords;
        private ObservableCollection<OTRecord> _searchResult;
        private DateTime? _beginDate;
        private DateTime? _endDate;
        private string _remark;
        #endregion

        public ObservableCollection<OTRecord> SearchResult
        {
            get => _searchResult;
            set => SetProperty(ref _searchResult, value);
        }

        public DateTime? BeginDate
        {
            get => _beginDate;
            set => SetProperty(ref _beginDate, value);
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public string Remark
        {
            get => _remark;
            set => SetProperty(ref _remark, value);
        }


        public ObservableCollection<OTRecord> OTRecords
        {
            get => _otRecords;
            set => SetProperty(ref _otRecords, value);
        }

        public DelegateCommand ResetCommand { get; }
        private void Reset()
        {
            BeginDate = null;
            EndDate = null;
            Remark = null;
            Search();
        }

        public int MildTimes=> OTRecords.Count(r => r.OTTime.Value.Hours < 3);
        public int ModerateTimes => OTRecords.Count(r => r.OTTime.Value.Hours < 5);
        public int SevereTimes => OTRecords.Count(r => r.OTTime.Value.Hours >= 5);
        public double AllTime => OTRecords.Select(r => r.OTTime ?? new TimeSpan()).Aggregate(new TimeSpan(), (left, right) => left + right).TotalHours;

        public DelegateCommand AddNewItemCommand { get; }
        private void AddNewItem()
        {
            _regionManager.RequestNavigate(
                RegionNames.MessageRegion,
                ViewNames.NewItem);
        }

        public DelegateCommand SearchCommand { get; }
        private void Search()
        {
            SearchResult = 
                new ObservableCollection<OTRecord>(
                    OTRecords.Where(r => r.BeginTime >= (BeginDate ?? DateTime.MinValue)  &&
                                         r.EndTime <= (EndDate?.AddDays(1) ?? DateTime.MaxValue) &&
                                         (r.Remark ?? String.Empty).Contains(Remark ?? String.Empty)));
        }

        public DelegateCommand<OTRecord> EditRecordCommand { get; }
        private void EditRecord(OTRecord record)
        {
            _regionManager.RequestNavigate(
                RegionNames.MessageRegion,
                ViewNames.EditItem,
                new NavigationParameters
                {
                    { "Record", record }
                });
        }

        public DelegateCommand<OTRecord> DeleteRecordCommand { get; }
        private void DeleteRecord(OTRecord record)
        {
            OTRecords.Remove(record);
            SearchResult.Remove(record);
            UpdateStatisticalInfo();
            AppFileHelper.SaveOTRecords(OTRecords.ToList());
        }
        public OverviewViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            AddNewItemCommand = new DelegateCommand(AddNewItem);
            EditRecordCommand = new DelegateCommand<OTRecord>(EditRecord);
            DeleteRecordCommand = new DelegateCommand<OTRecord>(DeleteRecord);
            SearchCommand = new DelegateCommand(Search);
            ResetCommand = new DelegateCommand(Reset);

            _eventAggregator.GetEvent<NewOTRecordEvent>().Subscribe(NewOTRecord);
            _eventAggregator.GetEvent<OTRecordChangedEvent>().Subscribe(OnOTRecordChanged);
            Load();
        }

        private void UpdateStatisticalInfo()
        {
            RaisePropertyChanged(nameof(MildTimes));
            RaisePropertyChanged(nameof(ModerateTimes));
            RaisePropertyChanged(nameof(SevereTimes));
            RaisePropertyChanged(nameof(AllTime));
        }

        private void NewOTRecord(OTRecord newRecord)
        {
            OTRecords.Add(newRecord);
            SearchResult.Add(newRecord);
            UpdateStatisticalInfo();
            AppFileHelper.SaveOTRecords(OTRecords.ToList());
        }


        private void OnOTRecordChanged(OTRecord record)
        {
            OTRecord targetRecord = OTRecords.First(r => r.Id == record.Id);
            targetRecord.BeginTime = record.BeginTime;
            targetRecord.EndTime = record.EndTime;
            targetRecord.Remark = record.Remark;
            UpdateStatisticalInfo();
            AppFileHelper.SaveOTRecords(OTRecords.ToList());
        }

        private void Load()
        {
            var data = AppFileHelper.GetOTRecords();
            OTRecords = new ObservableCollection<OTRecord>(data);
            SearchResult = new ObservableCollection<OTRecord>(OTRecords);
            UpdateStatisticalInfo();
        }

        public bool KeepAlive => false;
    }
}
