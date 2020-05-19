using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SikonConferenceSystem.Converter
{
    public class TimeSpanToFormattedString:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return $"{(TimeSpan) value:hh\\:mm}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
