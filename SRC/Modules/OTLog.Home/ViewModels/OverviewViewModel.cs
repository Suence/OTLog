using OTLog.Core.Constants;
using OTLog.Core.Models;
using Prism.Commands;
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
        private ObservableCollection<OTRecord> _otRecords;
        #endregion

        public ObservableCollection<OTRecord> OTRecords
        {
            get => _otRecords;
            set => SetProperty(ref _otRecords, value);
        }

        public DelegateCommand AddNewItemCommand { get; }
        private void AddNewItem()
        {
            _regionManager.RequestNavigate(
                RegionNames.MessageRegion,
                ViewNames.NewItem);
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

        }
        public OverviewViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            
            AddNewItemCommand = new DelegateCommand(AddNewItem);
            EditRecordCommand = new DelegateCommand<OTRecord>(EditRecord);
            DeleteRecordCommand = new DelegateCommand<OTRecord>(DeleteRecord);

            Load();
        }

        private void Load()
        {
            _otRecords = new ObservableCollection<OTRecord>
            {
                new OTRecord { BeginTime = new DateTime(2021, 7, 25, 19, 34, 20), EndTime = new DateTime(2021, 7, 25, 22, 10, 41), Remark = "这是一段备注" },
                new OTRecord { BeginTime = new DateTime(2021, 7, 26, 18, 3, 0), EndTime = new DateTime(2021, 7, 26, 23, 20, 03), Remark = "这是一段备注" },
                new OTRecord { BeginTime = new DateTime(2021, 7, 27, 19, 4, 10), EndTime = new DateTime(2021, 7, 27, 22, 14, 0), Remark = "这是一段备注"},
                new OTRecord { BeginTime = new DateTime(2021, 7, 28, 19, 20, 33), EndTime = new DateTime(2021, 7, 28, 22, 30, 0), Remark = "这是一段备注" },
                new OTRecord { BeginTime = new DateTime(2021, 7, 29, 17, 10, 0), EndTime = new DateTime(2021, 7, 29, 23, 55, 24), Remark = "这是一段备注" },
            };
        }

        public bool KeepAlive => false;
    }
}
