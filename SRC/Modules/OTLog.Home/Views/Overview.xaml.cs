using System.Windows.Controls;

namespace OTLog.Home.Views
{
    /// <summary>
    /// Interaction logic for Overview
    /// </summary>
    public partial class Overview : UserControl
    {
        public Overview()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            DeleteRecordPopup.DataContext = button.DataContext;
            DeleteRecordPopup.IsOpen = true;
        }

        private void HideDeleteRecordPopup(object sender, System.Windows.RoutedEventArgs e)
        {
            DeleteRecordPopup.IsOpen = false;
        }
    }
}
