using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;

namespace OTLog.Home.Views
{
    /// <summary>
    /// Interaction logic for About
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
        }

        private void GoToTheOfficialWebsite(object sender, System.Windows.RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            var ps = new ProcessStartInfo(link.NavigateUri.AbsoluteUri)
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        private void SendEmail(object sender, System.Windows.RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            var ps = new ProcessStartInfo("mailto:Suence.Online@Outlook.com")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        private void GoToGitHub(object sender, System.Windows.RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            var ps = new ProcessStartInfo("https://github.com/Suence")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        private void ShowWeChatQR(object sender, System.Windows.RoutedEventArgs e)
        {
            WeChatPopup.IsOpen = !WeChatPopup.IsOpen;
        }
    }
}
