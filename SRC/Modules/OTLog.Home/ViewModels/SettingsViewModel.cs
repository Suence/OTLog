using ModernWpf;
using OTLog.Core.Enums;
using OTLog.Core.Events;
using OTLog.Core.Models;
using OTLog.Core.StaticObjects;
using OTLog.Core.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;

namespace OTLog.Home.ViewModels
{
    public class SettingsViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        #region private
        private readonly IEventAggregator _eventAggregator;
        private Config _config;

        private ObservableCollection<ThemeColor> _colors;
        private ObservableCollection<Theme> _themes;
        #endregion

        public ObservableCollection<Theme> Themes
        {
            get => _themes;
            set => SetProperty(ref _themes, value);
        }

        public ObservableCollection<ThemeColor> Colors
        {
            get => _colors;
            set => SetProperty(ref _colors, value);
        }

        public DelegateCommand<Color?> ChangeThemeColorCommand { get; }
        private void ChangeThemeColor(Color? color)
        {
            GlobalObjectHolder.Config.ThemeColor = (Color)color;
            ThemeManager.Current.AccentColor = color;
        }

        public DelegateCommand ChangeThemeColorSchemeCommand { get; }
        private void ChangeThemeColorScheme()
        {
            if (GlobalObjectHolder.Config.UseSystemThemeColorScheme)
            {
                _eventAggregator.GetEvent<ThemeColorSchemeChangedEvent>().Publish();
                return;
            }
            ThemeManager.Current.AccentColor = GlobalObjectHolder.Config.ThemeColor;
        }

        public DelegateCommand<Theme?> ChangeThemeCommand { get; }
        private void ChangeTheme(Theme? theme)
        {
            _eventAggregator.GetEvent<ThemeChangedEvent>().Publish((Theme)theme);
        }

        public SettingsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            ChangeThemeColorSchemeCommand = new DelegateCommand(ChangeThemeColorScheme);
            ChangeThemeColorCommand = new DelegateCommand<Color?>(ChangeThemeColor);
            ChangeThemeCommand = new DelegateCommand<Theme?>(ChangeTheme);
            Colors = new ObservableCollection<ThemeColor>(GlobalObjectHolder.ThemeColors);
            Themes = new ObservableCollection<Theme>((Theme[])Enum.GetValues(typeof(Theme)));
        }

        public bool KeepAlive => false;

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            AppFileHelper.SaveAppConfigAsync(GlobalObjectHolder.Config);

            //if (!GlobalObjectHolder.Config.OpenAtBoot)
            //{
            //    File.Delete(AppFileHelper.LinkFileFullPath);
            //    return;
            //}

            //if (!File.Exists(AppFileHelper.LinkFileFullPath))
            //{
            //    AppFileHelper.CreateShortcut(AppFileHelper.LinkFileFullPath, "--nowindow");
            //}
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
