using System;
using Windows.UI.Xaml.Data;

namespace Conflux.UI.Converters
{
    public class BoolToSoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? System.Convert.ToDouble(parameter) : 1;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !System.Convert.ToDouble(parameter).Equals(1);
        }
    }
}
