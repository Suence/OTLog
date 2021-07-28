using System.Windows;

namespace OTLog.Themes
{
    public partial class LightTheme : ResourceDictionary
    {
        private LightTheme() => InitializeComponent();
        public static LightTheme Instance { get; } = new LightTheme();
    }
}
