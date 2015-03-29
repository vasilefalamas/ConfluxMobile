using System;

namespace Conflux.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsYesterday(this DateTime dateTime)
        {
            var supposedYesterday = GetTrimmedDateTime(dateTime);
            var today = GetTrimmedDateTime(DateTime.Now);

            return today.Equals(supposedYesterday.AddDays(1));
        }

        public static bool IsToday(this DateTime dateTime)
        {
            var supposedToday = GetTrimmedDateTime(dateTime);
            var today = GetTrimmedDateTime(DateTime.Now);
            
            return today.Equals(supposedToday);
        }

        public static bool IsTomorrow(this DateTime dateTime)
        {
            var supposedTomorrow = GetTrimmedDateTime(dateTime);
            var today = GetTrimmedDateTime(DateTime.Now);

            return today.AddDays(1).Equals(supposedTomorrow);
        }

        private static DateTime GetTrimmedDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }
}
