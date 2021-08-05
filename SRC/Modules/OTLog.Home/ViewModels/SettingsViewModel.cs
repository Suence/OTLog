using OTLog.Core.Utils;
using Prism.Mvvm;
using Prism.Regions;
using System.IO;

namespace OTLog.Home.ViewModels
{
    public class SettingsViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        #region private
        private bool _openAtBoot;
        private bool _notificationAfterMin;
        #endregion

        public bool OpenAtBoot
        {
            get => _openAtBoot;
            set => SetProperty(ref _openAtBoot, value);
        }

        public bool NotificationAfterMin
        {
            get => _notificationAfterMin;
            set => SetProperty(ref _notificationAfterMin, value);
        }

        public SettingsViewModel()
        {
        }

        public bool KeepAlive => false;

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            AppFileHelper.WriteAutoOpenStatus(OpenAtBoot);

            if (OpenAtBoot)
            {
                AppFileHelper.CreateShortcut(AppFileHelper.LinkFileFullPath, "--nowindow");
                return;
            }

            File.Delete(AppFileHelper.LinkFileFullPath);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            OpenAtBoot = AppFileHelper.ReadAutoOpenStatus();
        }
    }
}
