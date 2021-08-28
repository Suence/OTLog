using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OTLog.UI.Core.Converters
{
    [ValueConversion(typeof(TimeSpan?), typeof(String))]
    public class OTTimeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = value as TimeSpan?;

            if (timeSpan == null)
                return null;

            return $"{timeSpan.Value.Hours:#小时;;#} {timeSpan.Value.Minutes:#分钟;;#} {timeSpan.Value.Seconds:#秒;;#}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
