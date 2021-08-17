using System.Windows.Controls;
using System.Windows.Media;
using ModernWpf;
namespace OTLog.Home.Views
{
    /// <summary>
    /// Interaction logic for HomePage
    /// </summary>
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void OverviewButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ActiveModuleName.Text = "总览";
        }

        private void SettingsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ActiveModuleName.Text = "个性化";
        }

        private void AboutButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ActiveModuleName.Text = "关于";
        }

        private void NotificationButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ActiveModuleName.Text = "通知";
        }

        private void FoldSliderBarButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenSliderBarButton.Visibility = System.Windows.Visibility.Visible;
            FoldSliderBarButton.Visibility = System.Windows.Visibility.Hidden;
        }

        private void OpenSliderBarButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenSliderBarButton.Visibility = System.Windows.Visibility.Hidden;
            FoldSliderBarButton.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
