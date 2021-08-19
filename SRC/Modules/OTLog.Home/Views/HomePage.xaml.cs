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
