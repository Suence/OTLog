using OTLog.Core.Constants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OTLog.Home.ViewModels
{
    public class OverviewViewModel : BindableBase, IRegionMemberLifetime
    {
        #region private
        private readonly IRegionManager _regionManager;
        #endregion

        public DelegateCommand AddNewItemCommand { get; }
        private void AddNewItem()
        {
            _regionManager.RequestNavigate(
                RegionNames.MessageRegion,
                ViewNames.NewItem);
        }


        public OverviewViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            AddNewItemCommand = new DelegateCommand(AddNewItem);
        }

        public bool KeepAlive => false;
    }
}
