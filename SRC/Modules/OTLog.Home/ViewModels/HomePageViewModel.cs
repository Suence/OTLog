using Microsoft.Win32;
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
        private DateTime? _lastUnlockTime;
        #endregion

        public DateTime? LastUnlockTime
        {
            get => _lastUnlockTime;
            set => SetProperty(ref _lastUnlockTime, value);
        }

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
            SystemEvents.SessionSwitch += UpdateScreenLockTime;
        }

        private void UpdateScreenLockTime(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                return;
            }

            if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                LastUnlockTime = DateTime.Now;
                // do nothing
            }
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
