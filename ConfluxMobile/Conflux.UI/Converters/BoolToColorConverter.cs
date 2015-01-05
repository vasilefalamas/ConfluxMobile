using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Conflux.UI.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((bool) value) ? new SolidColorBrush(Colors.White) : Application.Current.Resources["PhoneAccentBrush"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (SolidColorBrush) Application.Current.Resources["PhoneAccentBrush"] != (SolidColorBrush) value;
        }
    }
}
