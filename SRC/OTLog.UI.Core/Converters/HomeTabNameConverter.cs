using OTLog.Core.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OTLog.UI.Core.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class HomeTabNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var viewNameMap = new Dictionary<string, string>
            {
                [ViewNames.About] = "关于",
                [ViewNames.Notice] = "通知",
                [ViewNames.Settings] = "个性化",
                [ViewNames.Overview] = "总览"
            };

            return viewNameMap[value.ToString()];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
