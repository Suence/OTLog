using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace OTLog.UI.Core.Converters
{
    [ValueConversion(typeof(TimeSpan?), typeof(SolidColorBrush))]
    public class OTSeverityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = value as TimeSpan?;

            if (timeSpan == null) 
                return new SolidColorBrush(Colors.Transparent);

            if (timeSpan < TimeSpan.FromHours(3))
                return new SolidColorBrush(Colors.Green);

            if (timeSpan < TimeSpan.FromHours(5))
                return new SolidColorBrush(Colors.Orange);

            return new SolidColorBrush(Colors.OrangeRed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
