using System;
using System.Collections.Generic;

namespace OTLog.Core.Extensions
{
    public static class DayOfWeekExtensions
    {
        public static string ToFriendlyText(this DayOfWeek @this)
        {
            var textMap = new Dictionary<DayOfWeek, string>
            { 
                [DayOfWeek.Monday] = "周一",
                [DayOfWeek.Tuesday] = "周二",
                [DayOfWeek.Wednesday] = "周三",
                [DayOfWeek.Thursday] = "周四",
                [DayOfWeek.Friday] = "周五",
                [DayOfWeek.Saturday] = "周六",
                [DayOfWeek.Sunday] = "周日",
            };

            return textMap[@this];
        }
    }
}
