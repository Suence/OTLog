using OTLog.Core.Constants;
using OTLog.Core.Models;
using Prism.Commands;
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
        private OTRecord _record;
        #endregion 

        public OTRecord OTRecord
        {
            get => _record;
            set => SetProperty(ref _record, value);
        }

        public DelegateCommand CancelCommand { get; }
        private void Cancel()
        {
            _regionManager.Regions[RegionNames.MessageRegion].RemoveAll();
        }

        public EditItemViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            CancelCommand = new DelegateCommand(Cancel);
        }

        public bool KeepAlive => false;

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            OTRecord = navigationContext.Parameters["Record"] as OTRecord;
        }
    }
}
