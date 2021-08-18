using OTLog.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTLog.Core.Extensions
{
    public static class ThemeExtensions
    {
        public static string ToFriendlyText(this Theme @this)
        {
            var textMap = new Dictionary<Theme, string>
            {
                [Theme.DarkTheme] = "深色",
                [Theme.LightTheme] = "浅色",
                [Theme.SystemTheme] = "跟随系统"
            };
            return textMap[@this];
        }
    }
}
