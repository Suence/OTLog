using OTLog.Core.Constants;
using OTLog.Core.StaticObjects;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTLog.Home.ViewModels
{
    public class HomePageViewModel : BindableBase, INavigationAware
    {
        #region private 
        private readonly IRegionManager _regionManager;
        #endregion
        
        public DelegateCommand<string> GoToTargetViewCommand { get; }
        private async void GoToTargetView(string viewName)
        {
            await Task.Delay(150);
            _regionManager.RequestNavigate(RegionNames.HomeRegion, viewName);
        }

        public HomePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            GoToTargetViewCommand = new DelegateCommand<string>(GoToTargetView);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
