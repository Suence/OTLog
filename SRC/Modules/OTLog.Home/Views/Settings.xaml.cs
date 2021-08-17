using System.Windows.Controls;
using System.Windows.Media;
using ModernWpf;

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
    }
}
