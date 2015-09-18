using System;
using Windows.UI.Xaml.Data;

namespace Conflux.UI.Converters
{
    public class DateToDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateValue = (DateTime?) value;

            if (dateValue.HasValue)
            {
                return dateValue.Value.Day.ToString();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
