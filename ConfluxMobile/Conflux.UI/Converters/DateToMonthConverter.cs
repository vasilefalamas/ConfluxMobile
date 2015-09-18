using System;
using Windows.UI.Xaml.Data;

namespace Conflux.UI.Converters
{
    public class DateToMonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateValue = (DateTime?) value;

            if (dateValue.HasValue)
            {
                return dateValue.Value.ToString("MMM");
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new object();
        }
    }
}
