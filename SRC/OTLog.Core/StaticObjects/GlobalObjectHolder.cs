using OTLog.Core.Enums;
using OTLog.Core.Models;
using OTLog.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace OTLog.Core.StaticObjects
{
    public static class GlobalObjectHolder
    {
        public static Config Config { get; set; }

        static GlobalObjectHolder()
        {
            InitConfig();

            ThemeColors = new List<ThemeColor>
            {
                new ThemeColor(Color.FromRgb(255, 185, 0), "黄金色"),
                new ThemeColor(Color.FromRgb(255, 140, 0), "金色"),
                new ThemeColor(Color.FromRgb(247, 99, 12), "亮橙黄色"),
                new ThemeColor(Color.FromRgb(202, 80, 16), "深橙黄色"),
                new ThemeColor(Color.FromRgb(218, 59, 1), "铁锈色"),
                new ThemeColor(Color.FromRgb(239, 135, 80), "浅铁锈色"),
                new ThemeColor(Color.FromRgb(209, 52, 56), "砖红色"),
                new ThemeColor(Color.FromRgb(255, 67, 67), "中等红色"),
                new ThemeColor(Color.FromRgb(231, 72, 86), "浅红色"),
                new ThemeColor(Color.FromRgb(232, 17, 35), "红色"),
                new ThemeColor(Color.FromRgb(234, 0, 94), "亮玫红"),
                new ThemeColor(Color.FromRgb(195, 0, 82), "玫瑰红"),
                new ThemeColor(Color.FromRgb(227, 0, 140), "浅紫红色"),
                new ThemeColor(Color.FromRgb(191, 0, 119), "玫红色"),
                new ThemeColor(Color.FromRgb(194, 57, 179), "浅兰花紫"),
                new ThemeColor(Color.FromRgb(154, 0, 137), "兰花紫色"),
                new ThemeColor(Color.FromRgb(0, 120, 212), "蓝色"),
                new ThemeColor(Color.FromRgb(0, 99, 177), "深蓝色"),
                new ThemeColor(Color.FromRgb(142, 140, 216), "紫影色"),
                new ThemeColor(Color.FromRgb(107, 105, 214), "深紫影色"),
                new ThemeColor(Color.FromRgb(135, 100, 184), "柔和彩虹色"),
                new ThemeColor(Color.FromRgb(116, 77, 169), "鸢尾花色"),
                new ThemeColor(Color.FromRgb(177, 70, 194), "浅紫红色"),
                new ThemeColor(Color.FromRgb(136, 23, 152), "紫红色"),
                new ThemeColor(Color.FromRgb(0, 153, 188), "亮酷蓝色"),
                new ThemeColor(Color.FromRgb(45, 125, 154), "酷蓝色"),
                new ThemeColor(Color.FromRgb(0, 183, 195), "海沫绿"),
                new ThemeColor(Color.FromRgb(3, 131, 135), "蓝绿色"),
                new ThemeColor(Color.FromRgb(0, 178, 148), "浅薄荷色"),
                new ThemeColor(Color.FromRgb(1, 133, 116), "深薄荷色"),
                new ThemeColor(Color.FromRgb(0, 204, 106), "浅绿色"),
                new ThemeColor(Color.FromRgb(16, 137, 62), "运动绿"),
                new ThemeColor(Color.FromRgb(122, 117, 116), "灰色"),
                new ThemeColor(Color.FromRgb(93, 90, 88), "灰棕色"),
                new ThemeColor(Color.FromRgb(104, 118, 138), "钢蓝色"),
                new ThemeColor(Color.FromRgb(81, 92, 107), "金属蓝色"),
                new ThemeColor(Color.FromRgb(86, 124, 115), "浅苔藓色"),
                new ThemeColor(Color.FromRgb(72, 104, 96), "苔藓色"),
                new ThemeColor(Color.FromRgb(73, 130, 5), "草绿色"),
                new ThemeColor(Color.FromRgb(16, 124, 16), "绿色"),
                new ThemeColor(Color.FromRgb(118, 118, 118), "天灰色"),
                new ThemeColor(Color.FromRgb(76, 74, 72), "雾灰色"),
                new ThemeColor(Color.FromRgb(105, 121, 126), "蓝灰色"),
                new ThemeColor(Color.FromRgb(74, 84, 89), "深灰色"),
                new ThemeColor(Color.FromRgb(100, 124, 100), "艳绿色"),
                new ThemeColor(Color.FromRgb(82, 94, 84), "鼠尾草色"),
                new ThemeColor(Color.FromRgb(132, 117, 69), "沙漠迷彩"),
                new ThemeColor(Color.FromRgb(126, 115, 95), "保护色"),
            };
        }

        public static List<ThemeColor> ThemeColors { get; }

        private static void InitConfig()
        {
            if (!File.Exists(AppFileHelper.SettingsFileFullPath))
            {
                Config = new Config
                {
                    NotificationAfterMin = true,
                    OpenAtBoot = true,
                    ThemeColor = Color.FromRgb(0, 120, 212),
                    Theme = Theme.DarkTheme,
                    //UserName = WindowsIdentity.GetCurrent().Name;
                    UserName = Environment.UserName
                };

                using (File.Create(AppFileHelper.SettingsFileFullPath)) { }

                AppFileHelper.SaveAppConfig(Config);
                return;
            }
            Config = AppFileHelper.LoadAppConfig();
        }
    }
}
