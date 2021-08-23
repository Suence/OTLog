using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ModernWpf;
using OTLog.Core.StaticObjects;
using Windows.ApplicationModel;

namespace OTLog.Home.Views
{
    /// <summary>
    /// Interaction logic for Settings
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ThemeManager.Current.AccentColor = (Color)((sender as RadioButton).Tag);
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var _otLogStartupTask = await StartupTask.GetAsync(GlobalObjectHolder.StartupTaskId);

            switch (_otLogStartupTask.State)
            {
                case StartupTaskState.Disabled:
                    // Task is disabled but can be enabled.
                    // StartupTaskState newState = await _otLogStartupTask.RequestEnableAsync(); // ensure that you are on a UI thread when you call RequestEnableAsync()
                    StartupCheckBox.IsChecked = false;
                    StartupCheckBox.Content = "随系统启动";
                    break;
                case StartupTaskState.DisabledByUser:
                    StartupCheckBox.IsChecked = false;
                    StartupCheckBox.IsEnabled = false;
                    StartupCheckBox.Content = "已在任务管理器中禁用启动";
                    break;
                case StartupTaskState.DisabledByPolicy:
                    StartupCheckBox.IsChecked = false;
                    StartupCheckBox.IsEnabled = false;
                    StartupCheckBox.Content = "启动被组策略禁用，或在此设备上不受支持";
                    break;
                case StartupTaskState.Enabled:
                    StartupCheckBox.IsChecked = true;
                    StartupCheckBox.Content = "随系统启动";
                    break;
            }
        }

        private async void StartupCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var otLogStartupTask = await StartupTask.GetAsync(GlobalObjectHolder.StartupTaskId);
            switch (otLogStartupTask.State)
            {
                case StartupTaskState.Disabled:
                    StartupCheckBox.IsChecked = false;
                    StartupCheckBox.Content = "随系统启动";
                    break;
                case StartupTaskState.DisabledByUser:
                    StartupCheckBox.IsChecked = false;
                    StartupCheckBox.IsEnabled = false;
                    StartupCheckBox.Content = "已在任务管理器中禁用启动";
                    break;
                case StartupTaskState.DisabledByPolicy:
                    StartupCheckBox.IsChecked = false;
                    StartupCheckBox.IsEnabled = false;
                    StartupCheckBox.Content = "启动被组策略禁用，或在此设备上不受支持";
                    break;
                case StartupTaskState.Enabled:
                    otLogStartupTask.Disable();
                    StartupCheckBox.IsChecked = false;
                    StartupCheckBox.Content = "随系统启动";
                    break;
            }
        }

        private async void StartupCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var otLogStartupTask = await StartupTask.GetAsync(GlobalObjectHolder.StartupTaskId);
            switch (otLogStartupTask.State)
            {
                case StartupTaskState.Disabled:
                    await otLogStartupTask.RequestEnableAsync();
                    StartupCheckBox.IsChecked = true;
                    StartupCheckBox.Content = "随系统启动";
                    break;
                case StartupTaskState.DisabledByUser:
                    StartupCheckBox.IsChecked = false;
                    StartupCheckBox.IsEnabled = false;
                    StartupCheckBox.Content = "已在任务管理器中禁用启动";
                    break;
                case StartupTaskState.DisabledByPolicy:
                    StartupCheckBox.IsChecked = false;
                    StartupCheckBox.IsEnabled = false;
                    StartupCheckBox.Content = "启动被组策略禁用，或在此设备上不受支持";
                    break;
                case StartupTaskState.Enabled:
                    StartupCheckBox.IsChecked = true;
                    StartupCheckBox.Content = "随系统启动";
                    break;
            }
        }
    }
}
