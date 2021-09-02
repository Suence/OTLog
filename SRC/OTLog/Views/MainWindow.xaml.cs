using Microsoft.Toolkit.Uwp.Notifications;
using OTLog.Core.Enums;
using OTLog.Core.StaticObjects;
using OTLog.Core.Utils;
using System;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel;
using Windows.Foundation.Collections;

namespace OTLog.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        public void BringToForeground()
        {
            if (WindowState == WindowState.Minimized || Visibility == Visibility.Hidden)
            {
                Show();
                WindowState = WindowState.Normal;
            }

            // According to some sources these steps gurantee that an app will be brought to foreground.
            Activate();
            Topmost = true;
            Topmost = false;
            Focus();
        }

        public static void ShowToast()
        {
            int conversationId = 384928;
            new ToastContentBuilder().AddArgument("conversationId", conversationId)
                                     .AddText("程序已最小化到系统托盘")
                                     .AddText("可转到个性化界面关闭推送通知")
                                     .Show();
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            if (!GlobalObjectHolder.Config.NotificationAfterMin) return;

            ShowToast();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var otLogStartupTask = await StartupTask.GetAsync(GlobalObjectHolder.StartupTaskId);
            if (GlobalObjectHolder.Config.StartupScheme == StartupScheme.NoWindow &&
                otLogStartupTask.State == StartupTaskState.Enabled)
            {
                GlobalObjectHolder.Config.StartupScheme = StartupScheme.Normal;
                AppFileHelper.SaveAppConfig(GlobalObjectHolder.Config);
                await Task.Delay(10);
                Hide();
            }

            // 点击通知时, 激活程序(即使程序已关闭)
            // Listen to notification activation
            ToastNotificationManagerCompat.OnActivated += toastArgs => Dispatcher.Invoke(BringToForeground);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowRestoreButton.Visibility = Visibility.Visible;
                return;
            }
            WindowRestoreButton.Visibility = Visibility.Collapsed;
        }
    }
}
