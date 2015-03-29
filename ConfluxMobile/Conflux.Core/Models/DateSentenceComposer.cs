using System;
using System.Globalization;
using Conflux.Core.Extensions;

namespace Conflux.Core.Models
{
    public class DateSentenceComposer
    {
        private const string DateFormat = "dd MMMM yyyy, dddd";

        private const string StartPastOnDate = "Started on {0}.";
        private const string StartPastYesterday = "Started yesterday.";
        private const string StartPresentOnDate = "Starts today.";
        private const string StartFutureTomorrow = "Will start tomorrow.";
        private const string StartFutureOnDate = "Will start on {0}.";


        private const string EndPastOnDate = "Ended on {0}.";
        private const string EndPastYesterday = "Has ended yesterday.";
        private const string EndPresentOnDate = "Ends today.";
        private const string EndFutureTomorrow = "Will end tomorrow.";
        private const string EndFutureOnDate = "Will end on {0}.";

        //TODO : ComposeBegin & ComposeEnd look similar - refactor them.
        public static string ComposeBeginSentence(DateTime startDate)
        {
            if (startDate.Date < DateTime.Now.Date)
            {
                return GetBeginSentenceForPastDate(startDate);
            }

            if (startDate.Date > DateTime.Now.Date)
            {
                return GetBeginSentenceForFutureDate(startDate);
            }

            return GetBeginSentenceForPresentDate();
        }

        public static string ComposeEndSentence(DateTime endDate)
        {
            if (endDate.Date < DateTime.Now.Date)
            {
                return GetEndSentenceForPastDate(endDate);
            }

            if (endDate.Date > DateTime.Now.Date)
            {
                return GetEndSentenceForFutureDate(endDate);
            }

            return GetEndSentenceForPresentDate();
        }

        private static string GetBeginSentenceForPastDate(DateTime date)
        {
            if (date.IsYesterday())
            {
                return StartPastYesterday;
            }

            return string.Format(StartPastOnDate, date.ToString(DateFormat, CultureInfo.CurrentCulture));
        }

        private static string GetBeginSentenceForPresentDate()
        {
            return string.Format(StartPresentOnDate);
        }

        private static string GetBeginSentenceForFutureDate(DateTime date)
        {
            if (date.IsTomorrow())
            {
                return StartFutureTomorrow;
            }

            return string.Format(StartFutureOnDate, date.ToString(DateFormat, CultureInfo.CurrentCulture));
        }

        private static string GetEndSentenceForPastDate(DateTime date)
        {
            if (date.IsYesterday())
            {
                return EndPastYesterday;
            }

            return string.Format(EndPastOnDate, date.ToString(DateFormat, CultureInfo.CurrentCulture));
        }

        private static string GetEndSentenceForPresentDate()
        {
            return string.Format(EndPresentOnDate);
        }

        private static string GetEndSentenceForFutureDate(DateTime date)
        {
            if (date.IsTomorrow())
            {
                return EndFutureTomorrow;
            }

            return string.Format(EndFutureOnDate, date.ToString(DateFormat, CultureInfo.CurrentCulture));
        }
    }
}
