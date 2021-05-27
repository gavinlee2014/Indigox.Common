using System;
using System.Globalization;

namespace Indigox.Common.Utilities
{
    public static class DateTimeUtil
    {
        private static readonly string DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
        private static readonly string DateFormat = "yyyy/MM/dd";

        private static readonly IFormatProvider DefaultFormatProvider = CultureInfo.CurrentCulture;

        /// <summary>
        /// Converts to date string, use format "yyyy/MM/dd".
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static string ConvertToDateString(DateTime datetime)
        {
            return datetime.ToString(DateFormat, DefaultFormatProvider);
        }

        /// <summary>
        /// Converts to date time string, use format "yyyy/MM/dd HH:mm:ss".
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static string ConvertToDateTimeString(DateTime datetime)
        {
            return datetime.ToString(DateTimeFormat, DefaultFormatProvider);
        }
    }
}
