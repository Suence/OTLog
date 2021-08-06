using OTLog.Core.Models;
using OTLog.Core.StaticObjects;
using OTLog.Core.Utils;
using Prism.Mvvm;
using Prism.Regions;
using System.IO;

namespace OTLog.Home.ViewModels
{
    public class SettingsViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        #region private
        private Config _config;
        #endregion

        public Config Config
        {
            get => _config;
            set => SetProperty(ref _config, value);
        }

        public SettingsViewModel()
        {
        }

        public bool KeepAlive => false;

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            AppFileHelper.SaveAppConfig(Config);

            if (!Config.OpenAtBoot)
            {
                File.Delete(AppFileHelper.LinkFileFullPath);
                return;
            }

            if (!File.Exists(AppFileHelper.LinkFileFullPath))
            {
                AppFileHelper.CreateShortcut(AppFileHelper.LinkFileFullPath, "--nowindow");
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Config = GlobalObjectHolder.Config;
        }
    }
}
