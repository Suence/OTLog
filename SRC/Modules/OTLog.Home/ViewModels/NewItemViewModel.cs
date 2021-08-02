using OTLog.Core.Constants;
using Prism.Commands;
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
        #endregion

        public DelegateCommand CancelCommand { get; }
        private void Cancel()
        {
            _regionManager.Regions[RegionNames.MessageRegion].RemoveAll();
        }

        public NewItemViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            CancelCommand = new DelegateCommand(Cancel);
        }
    }
}
