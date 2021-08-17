using OTLog.Core.Enums;
using System.Windows.Media;

namespace OTLog.Core.Models
{
    public class Config
    {
        public bool OpenAtBoot { get; set; }
        public bool NotificationAfterMin { get; set; }
        public Color ThemeColor { get; set; }
        public Theme Theme { get; set; }
    }
}
