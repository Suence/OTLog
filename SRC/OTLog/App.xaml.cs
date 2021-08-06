using Hardcodet.Wpf.TaskbarNotification;
using ModernWpf;
using OTLog.Core.StaticObjects;
using OTLog.Core.Utils;
using OTLog.Home;
using OTLog.Themes;
using OTLog.ViewModels;
using OTLog.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace OTLog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private TaskbarIcon _notifyIcon;
        public static List<string> AppArgs;

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
            //return null;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        private ResourceDictionary GetThemeResource(Core.Enums.Theme theme)
            => new[]
            {
                DarkTheme.Instance as ResourceDictionary,
                DarkTheme.Instance as ResourceDictionary,
                LightTheme.Instance as ResourceDictionary
            }[(int)theme];

        protected override void OnStartup(StartupEventArgs e)
        {
            AppArgs = e.Args.ToList();

            AppFileHelper.ValidateApplicationFiles();
            Resources.MergedDictionaries[0] = GetThemeResource(GlobalObjectHolder.Config.Theme);

            base.OnStartup(e);

            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            _notifyIcon.DataContext = Container.Resolve<NotifyIconViewModel>();

            ThemeManager.Current.AccentColor = Colors.DarkMagenta;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<HomeModule>();
        }
    }
}
