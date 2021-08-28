using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace OTLog.UI.Core.Converters
{

    [ValueConversion(typeof(TimeSpan?), typeof(SolidColorBrush))]
    public class OTSeverityBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => new SolidColorBrush(OTSeverityColorConverter.TimeSpanToColor(value as TimeSpan?));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(TimeSpan?), typeof(Color))]
    public class OTSeverityColorConverter : IValueConverter
    {
        internal static Color TimeSpanToColor(TimeSpan? timeSpan)
        {
            if (timeSpan == null)
                return Colors.Transparent;

            if (timeSpan <= TimeSpan.FromHours(3))
                return Colors.Green;

            if (timeSpan <= TimeSpan.FromHours(5))
                return Colors.Orange;

            return Colors.OrangeRed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => TimeSpanToColor(value as TimeSpan?);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
