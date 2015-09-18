using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Conflux.UI.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        private static readonly SolidColorBrush ReadEventColor = new SolidColorBrush(Colors.Black);

        private static readonly SolidColorBrush UnreadEventColor = new SolidColorBrush(Colors.Teal);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var boolValue = (bool) value;
            return boolValue ? ReadEventColor : UnreadEventColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (SolidColorBrush)value != ReadEventColor;
        }
    }
}
