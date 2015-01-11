using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Documents;

namespace Conflux.UI.Text
{
    public class TextFormatter
    {
        private const string Pattern = @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[\-;:&=\+\$,\w]+@)?[A-Za-z0-9\.\-]+|(?:www\.|[\-;:&=\+\$,\w]+@)[A-Za-z0-9\.\-]+)((?:\/[\+~%\/\.\w\-_]*)?\??(?:[\-\+=&;%@\.\w_]*)#?(?:[\.\!\/\\\w]*))?)";

        public IList<Inline> FormatToRichText(string plainText)
        {
            var runsResult = new List<Inline>();

            int index = 0;

            Regex regex = new Regex(Pattern);
             
            var matches = regex.Matches(plainText);

            foreach (Match match in matches)
            {
                //Add a regular text match
                int matchIndex = match.Index;
                var text = plainText.Substring(index, matchIndex - index);

                var textRun = new Run
                {
                    Text = text
                };
                runsResult.Add(textRun);
                
                //Add a hyperlink match
                var hyperlinkText = match.Value;
                var hyperlinkRun = new Run
                {
                    Text = hyperlinkText
                };

                var hyperlinkNavigateUri = hyperlinkText;

                if (!hyperlinkNavigateUri.Contains("@") && !hyperlinkNavigateUri.StartsWith("http"))
                {
                    hyperlinkNavigateUri = @"http://" + hyperlinkNavigateUri;
                }

                if (hyperlinkNavigateUri.Contains("@") && !hyperlinkNavigateUri.StartsWith("mailto"))
                {
                    hyperlinkNavigateUri = @"mailto://" + hyperlinkNavigateUri;
                }

                var hyperlink = new Hyperlink
                {
                    NavigateUri = new Uri(hyperlinkNavigateUri)
                };
                hyperlink.Inlines.Add(hyperlinkRun);

                runsResult.Add(hyperlink);
                
                index = matchIndex + match.Length;
            }

            var lastRegularText = plainText.Substring(index, plainText.Length - index);
            runsResult.Add(new Run { Text = lastRegularText });

            return runsResult;
        }
    }
}
