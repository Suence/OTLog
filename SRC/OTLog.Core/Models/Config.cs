using OTLog.Core.Enums;

namespace OTLog.Core.Models
{
    public class Config
    {
        public bool OpenAtBoot { get; set; }
        public bool NotificationAfterMin { get; set; }
        public string ThemeColor { get; set; }
        public Theme Theme { get; set; }
    }
}
