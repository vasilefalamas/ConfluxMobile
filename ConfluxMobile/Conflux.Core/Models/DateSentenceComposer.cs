using System;
using System.Runtime.Serialization;
using Conflux.Core.Extensions;

namespace Conflux.Core.Models
{
    public class DateSentenceComposer
    {
        private const string DateFormat = "MM-dd-yyyy, dddd";

        //TODO : ComposeBegin & ComposeEnd look similar - refactor them.
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

            if (startDateValue.IsTomorrow())
            {
                return string.Format("Will start tomorrow.");
            }

            return string.Format("Starts on {0}.", startDateValue.ToString(DateFormat));
        }

        public static string ComposeEndSentence(DateTime? endDate)
        {
            if (!endDate.HasValue)
            {
                return null;
            }

            var endDateValue = endDate.Value;

            if (endDateValue.IsYesterday())
            {
                return string.Format("Has ended yesterday.");
            }

            if (endDateValue.IsTomorrow())
            {
                return string.Format("Will end tomorrow.");
            }

            return string.Format("Ends on {0}.", endDate.Value.ToString(DateFormat));
        }
    }
}
