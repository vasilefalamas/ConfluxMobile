using System;

namespace Conflux.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsYesterday(this DateTime dateTime)
        {
            var supposedYesterday = dateTime.Date;
            var today = DateTime.Now.Date;

            return today.Equals(supposedYesterday.AddDays(1));
        }

        public static bool IsToday(this DateTime dateTime)
        {
            var supposedToday = dateTime.Date;
            var today = DateTime.Now.Date;
            
            return today.Equals(supposedToday);
        }

        public static bool IsTomorrow(this DateTime dateTime)
        {
            var supposedTomorrow = dateTime.Date;
            var today = DateTime.Now.Date;

            return today.AddDays(1).Equals(supposedTomorrow);
        }

        /// <summary>
        /// Gets the start of the current week.
        /// </summary>
        /// <returns></returns>
        public static DateTime StartOfWeek()
        {
            System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
            DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

            var today = DateTime.Today;

            return today.AddDays(-(today.DayOfWeek - firstDayOfWeek));
        }
    }
}
