using Hardcodet.Wpf.TaskbarNotification;
using ModernWpf;
using OTLog.Core.Enums;
using OTLog.Core.Events;
using OTLog.Core.StaticObjects;
using OTLog.Core.Utils;
using OTLog.Home;
using OTLog.Themes;
using OTLog.ViewModels;
using OTLog.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Windows.UI.ViewManagement;

namespace OTLog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private TaskbarIcon _notifyIcon;
        public static List<string> AppArgs;
        private UISettings uISettings = new UISettings();

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
            //return null;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        private void SetTheme(Core.Enums.Theme theme)
        {
            if (theme == Theme.DarkTheme)
            {
                Resources.MergedDictionaries[1] = DarkTheme.Instance;
                (Resources.MergedDictionaries[2] as ThemeResources).RequestedTheme = ApplicationTheme.Dark;
                return;
            }

            if (theme == Theme.LightTheme)
            {
                Resources.MergedDictionaries[1] = LightTheme.Instance;
                (Resources.MergedDictionaries[2] as ThemeResources).RequestedTheme = ApplicationTheme.Light;
                return;
            }

            var color = uISettings.GetColorValue(UIColorType.Background);
            if (color.ToString() == "#FF000000")
            {
                Resources.MergedDictionaries[1] = DarkTheme.Instance;
                (Resources.MergedDictionaries[2] as ThemeResources).RequestedTheme = ApplicationTheme.Dark;
                return;
            }

            Resources.MergedDictionaries[1] = LightTheme.Instance;
            (Resources.MergedDictionaries[2] as ThemeResources).RequestedTheme = ApplicationTheme.Light;
            return;
        }

       
        protected override void OnStartup(StartupEventArgs e)
        {
            AppOnStartup();
            AppArgs = e.Args.ToList();

            AppFileHelper.ValidateApplicationFiles();
            SetTheme(GlobalObjectHolder.Config.Theme);
            base.OnStartup(e);

            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            _notifyIcon.DataContext = Container.Resolve<NotifyIconViewModel>();

            ThemeManager.Current.AccentColor = GlobalObjectHolder.Config.ThemeColor;

            uISettings.ColorValuesChanged += 
                (sender, para) => SyncToSystemTheme(uISettings.GetColorValue(UIColorType.Background));
            Container.Resolve<IEventAggregator>().GetEvent<ThemeChangedEvent>().Subscribe(SetTheme);
        }

        private void SyncToSystemTheme(Windows.UI.Color color)
        //=> Resources.MergedDictionaries[1] = 
        //    color.ToString() == "#FF000000" 
        //    ? (ResourceDictionary)DarkTheme.Instance 
        //    : LightTheme.Instance;
        {
            SetTheme(color.ToString() == "#FF000000" ? Theme.DarkTheme : Theme.LightTheme);
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

        #region Constants and Fields

        /// <summary>The event mutex name.</summary>
        private const string UniqueEventName = "6531c085-e1f3-4e6c-907e-63bb695d9dea";

        /// <summary>The unique mutex name.</summary>
        private const string UniqueMutexName = "a2ad1b11-6051-48ff-a8f4-8f86f5cea8f5";

        /// <summary>The event wait handle.</summary>
        private EventWaitHandle eventWaitHandle;

        /// <summary>The mutex.</summary>
        private Mutex mutex;

        #endregion

        #region Methods

        /// <summary>The app on startup.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void AppOnStartup()
        {
            bool isOwned;
            this.mutex = new Mutex(true, UniqueMutexName, out isOwned);
            this.eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, UniqueEventName);

            // So, R# would not give a warning that this variable is not used.
            GC.KeepAlive(this.mutex);

            if (isOwned)
            {
                // Spawn a thread which will be waiting for our event
                var thread = new Thread(
                    () =>
                    {
                        while (this.eventWaitHandle.WaitOne())
                        {
                            Current.Dispatcher.BeginInvoke(
                                (Action)(() => ((MainWindow)Current.MainWindow).BringToForeground()));
                        }
                    });

                // It is important mark it as background otherwise it will prevent app from exiting.
                thread.IsBackground = true;

                thread.Start();
                return;
            }

            // Notify other instance so it could bring itself to foreground.
            this.eventWaitHandle.Set();

            // Terminate this instance.
            this.Shutdown();
        }

        #endregion
    }
}
