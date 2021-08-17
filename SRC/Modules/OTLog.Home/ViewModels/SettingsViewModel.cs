using ModernWpf;
using OTLog.Core.Models;
using OTLog.Core.StaticObjects;
using OTLog.Core.Utils;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;

namespace OTLog.Home.ViewModels
{
    public class SettingsViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        #region private
        private Config _config;
        private ObservableCollection<ThemeColor> _colors;
        #endregion

        public ObservableCollection<ThemeColor> Colors
        {
            get => _colors;
            set => SetProperty(ref _colors, value);
        }
        public Config Config
        {
            get => _config;
            set => SetProperty(ref _config, value);
        }

        public DelegateCommand<Color?> ChangeThemeColorCommand { get; }
        private void ChangeThemeColor(Color? color)
        {
            Config.ThemeColor = (Color)color;
            ThemeManager.Current.AccentColor = color;
        }

        public SettingsViewModel()
        {
            ChangeThemeColorCommand = new DelegateCommand<Color?>(ChangeThemeColor);
            Colors = new ObservableCollection<ThemeColor>(GlobalObjectHolder.ThemeColors);
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
