using System;
using Windows.UI.Xaml.Data;
using Conflux.Core.Models;

namespace Conflux.UI.Converters
{
    public class DateSentenceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateTime = value as DateTime?;

            if (!dateTime.HasValue)
            {
                return null;
            }

            if (parameter.Equals("Start"))
            {
                return DateSentenceComposer.ComposeBeginSentence(dateTime.Value);
            }

            if (parameter.Equals("End"))
            {
                return DateSentenceComposer.ComposeEndSentence(dateTime.Value);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
