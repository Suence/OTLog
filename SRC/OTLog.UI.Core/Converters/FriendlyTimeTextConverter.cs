using OTLog.Core.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace OTLog.UI.Core.Converters
{
    public class FriendlyTimeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            if (dateTime.Year != DateTime.Now.Year)
                return $"{dateTime:yyyy年MM月dd日}";

            TimeSpan TimeDifference = DateTime.Now.Date - dateTime.Date;
            TimeSpan[] timeReferences =
            {
                TimeSpan.FromDays(1),
                TimeSpan.FromDays(2),
                TimeSpan.FromDays(3),
                TimeSpan.FromDays(7),
                TimeSpan.MaxValue
            };
            string[] values =
            {
                "今天",
                "昨天",
                "前天",
                $"{dateTime:M} {dateTime.DayOfWeek.ToFriendlyText()}",
                $"{dateTime:M}"
            };
            return GetValue();

            string GetValue(int index = 0)
                => TimeDifference < timeReferences[index]
                ? values[index]
                : GetValue(index + 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
