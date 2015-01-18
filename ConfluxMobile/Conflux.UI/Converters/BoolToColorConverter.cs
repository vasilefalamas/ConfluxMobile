using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Conflux.UI.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        private static readonly SolidColorBrush accentColor = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

        private static readonly SolidColorBrush staticColor = Application.Current.Resources["MidBrush"] as SolidColorBrush;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((bool)value) ? staticColor : accentColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (SolidColorBrush)value != accentColor;
        }
    }
}
