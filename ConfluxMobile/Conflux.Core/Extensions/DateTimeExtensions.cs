using System;

namespace Conflux.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsYesterday(this DateTime dateTime)
        {
            var currentDate = DateTime.Now;

            var today = GetTrimmedDateTime(currentDate);
            var lessPreciseDate = GetTrimmedDateTime(dateTime);

            return today.Equals(lessPreciseDate.AddDays(1));
        }

        public static bool IsToday(this DateTime dateTime)
        {
            var currentDate = DateTime.Now;

            var today = GetTrimmedDateTime(currentDate);
            var lessPreciseDate = GetTrimmedDateTime(dateTime);

            return today.Equals(lessPreciseDate);
        }

        public static bool IsTomorrow(this DateTime dateTime)
        {
            var currentDate = DateTime.Now;

            var today = GetTrimmedDateTime(currentDate);
            var lessPreciseDate = GetTrimmedDateTime(dateTime);

            return today.AddDays(1).Equals(lessPreciseDate);
        }

        private static DateTime GetTrimmedDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }
}
