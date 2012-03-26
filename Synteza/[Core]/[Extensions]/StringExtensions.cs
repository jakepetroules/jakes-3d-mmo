namespace Petroules.Synteza
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides extensions to the <see cref="string"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether the string specified by <paramref name="match"/> contains any of the
        /// keywords in <paramref name="pattern"/>, where each keyword is separated with a space.
        /// </summary>
        /// <param name="match">The string to check for matches.</param>
        /// <param name="pattern">The pattern to test against <paramref name="match"/>.</param>
        /// <param name="ignoreCase">Whether to ignore casing rules. The default is <c>true</c>.</param>
        /// <returns>Whether <paramref name="match"/> contained one of the keywords in <paramref name="pattern"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="match"/> or <paramref name="pattern"/> is <c>null</c>.</exception>
        public static bool ContainsAnyOf(this string match, string pattern, bool ignoreCase)
        {
            if (match == null || pattern == null)
            {
                throw new ArgumentNullException(match == null ? "match" : "pattern");
            }

            return match.ContainsAnyOf(pattern.Split(' '), ignoreCase);
        }

        /// <summary>
        /// Determines whether the string specified by <paramref name="match"/> contains any of the elements of the <paramref name="array"/> array.
        /// </summary>
        /// <param name="match">The string to check for matches.</param>
        /// <param name="array">The array to test against <paramref name="match"/>.</param>
        /// <param name="ignoreCase">Whether to ignore casing rules. The default is <c>true</c>.</param>
        /// <returns>Whether <paramref name="match"/> contained one of the elements of <paramref name="array"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="match"/> or <paramref name="array"/> is <c>null</c>.</exception>
        public static bool ContainsAnyOf(this string match, IList<string> array, bool ignoreCase)
        {
            if (match == null || array == null)
            {
                throw new ArgumentNullException(match == null ? "match" : "array");
            }

            return array.Any(p => match.IndexOf(p, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) > -1);
        }
    }
}
