using OTLog.Core.Enums;
using OTLog.Core.Models;
using OTLog.Core.Utils;
using System.IO;

namespace OTLog.Core.StaticObjects
{
    public static class GlobalObjectHolder
    {
        public static Config Config { get; set; }

        static GlobalObjectHolder()
        {
            InitConfig();
        }

        private static void InitConfig()
        {
            if (!File.Exists(AppFileHelper.SettingsFileFullPath))
            {
                Config = new Config
                {
                    NotificationAfterMin = true,
                    OpenAtBoot = true,
                    ThemeColor = "DarkMagenta",
                    Theme = Theme.Default
                };

                using (File.Create(AppFileHelper.SettingsFileFullPath)) { }
                
                AppFileHelper.SaveAppConfig(Config);
                return;
            }
            Config = AppFileHelper.LoadAppConfig();
        }
    }
}
