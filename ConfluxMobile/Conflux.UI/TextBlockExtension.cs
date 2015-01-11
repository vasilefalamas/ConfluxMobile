using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TextFormatter = Conflux.UI.Text.TextFormatter;

namespace Conflux.UI
{
    public static class TextBlockExtension
    {
        public static readonly DependencyProperty FormattedTextProperty =
            DependencyProperty.Register("FormattedText", typeof(string), typeof(TextBlockExtension), new PropertyMetadata(string.Empty,
                (sender, e) =>
                {
                    var text = e.NewValue as string;
                    var textBlock = (sender) as TextBlock;

                    if (textBlock != null)
                    {
                        textBlock.Inlines.Clear();

                        var formatter = new TextFormatter();

                        var inlines = formatter.FormatToRichText(text);

                        foreach (var inline in inlines)
                        {
                            textBlock.Inlines.Add(inline);
                        }
                    }
                }));

        public static string GetFormattedText(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(FormattedTextProperty);
        }

        public static void SetFormattedText(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(FormattedTextProperty, value);
        }
    }
}
