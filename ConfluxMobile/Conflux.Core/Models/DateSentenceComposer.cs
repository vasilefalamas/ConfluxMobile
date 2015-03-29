using System;
using Conflux.Core.Extensions;

namespace Conflux.Core.Models
{
    public class DateSentenceComposer
    {
        public static string ComposeBeginSentence(DateTime? startDate)
        {
            if (!startDate.HasValue)
            {
                return null;
            }

            var startDateValue = startDate.Value;

            if (startDateValue.IsYesterday())
            {
                return string.Format("Has started yesterday.");
            }

            return string.Format("Started on {0}.", startDateValue);
        }

        public static string ComposeEndSentence(DateTime? endDate)
        {
            if (!endDate.HasValue)
            {
                return null;
            }

            return string.Format("Ended on {0}.", endDate.Value);
        }
    }
}
