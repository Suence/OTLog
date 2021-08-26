using OTLog.Core.Constants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTLog.Home.ViewModels
{
    public class StartPageViewModel : BindableBase
    {
        #region private
        private readonly IRegionManager _regionManager;
        #endregion

        public DelegateCommand GoToHomePageCommand { get; }
        private void GoToHomePage()
        {
            _regionManager.RequestNavigate(RegionNames.MainRegion, ViewNames.HomePage);
        }

        public StartPageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            GoToHomePageCommand = new DelegateCommand(GoToHomePage);
            LoadData();
        }

        private async void LoadData()
        {
            await Task.Delay(4500);
            GoToHomePage();
        }
    }
}
