using Hardcodet.Wpf.TaskbarNotification;
using OTLog.Home;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            AppArgs = e.Args.ToList();

            base.OnStartup(e);

            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            _notifyIcon.DataContext = Container.Resolve<NotifyIconViewModel>();

            var systemStartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string linkFileFullPath = Path.Combine(systemStartupFolder, "OTLog.lnk");

            // 如果快捷方式已经存在, 则不进行任何操作
            if (File.Exists(linkFileFullPath)) return;

            CreateShortcut(linkFileFullPath, "--nowindow");
        }

        // 创建快捷方式
        private void CreateShortcut(string lnkFilePath, string args = "")
        {
            var shellType = Type.GetTypeFromProgID("WScript.Shell");
            dynamic shell = Activator.CreateInstance(shellType);
            var shortcut = shell.CreateShortcut(lnkFilePath);
            shortcut.TargetPath = Assembly.GetEntryAssembly().Location;
            shortcut.Arguments = args;
            shortcut.WorkingDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            shortcut.Save();
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
