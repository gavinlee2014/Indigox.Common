using System;

namespace Indigox.Common.Utilities
{
    public static class StringUtil
    {
        private static bool TrimBlanks = true;

        /// <summary>
        /// Determines whether [is null or empty] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if [is null or empty] [the specified text]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(string text)
        {
            if (text == null)
                return true;
            if (text == String.Empty)
                return true;
            if (TrimBlanks && text.Trim() == String.Empty)
                return true;
            return false;
        }

        /// <summary>
        /// Determines whether [is null or empty] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="trimBlanks">if set to <c>true</c> [trim blanks].</param>
        /// <returns>
        /// 	<c>true</c> if [is null or empty] [the specified text]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(string text, bool trimBlanks)
        {
            if (text == null)
                return true;
            if (text == String.Empty)
                return true;
            if (trimBlanks && text.Trim() == String.Empty)
                return true;
            return false;
        }

        /// <summary>
        /// Determines whether the specified text is int, not contains decimal.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if the specified text is int; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInt(string text)
        {
            int i = 0;
            return int.TryParse(text, out i);
        }

        /// <summary>
        /// Determines whether the specified text is number, can contains decimal.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if the specified text is number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumber(string text)
        {
            double d = 0d;
            return double.TryParse(text, out d);
        }

        /// <summary>
        /// Determines whether the specified text is number, can contains decimal.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if the specified text is number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDateTime(string text)
        {
            DateTime d = DateTime.MinValue;
            return DateTime.TryParse(text, out d);
        }
    }
}
