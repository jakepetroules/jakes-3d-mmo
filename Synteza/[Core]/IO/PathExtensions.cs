namespace Petroules.Synteza.IO
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Provides additional methods related to those in the <see cref="System.IO.Path"/> class.
    /// </summary>
    public static class PathExtensions
    {
        /// <summary>
        /// Returns the file path specified by <paramref name="path"/> enclosed in quotation marks.
        /// </summary>
        /// <param name="path">The path to quote.</param>
        /// <returns>The specified path with quotation marks at the beginning and end of the path.</returns>
        public static string Quote(string path)
        {
            return string.Format(CultureInfo.InvariantCulture, "\"{0}\"", path);
        }

        /// <summary>
        /// Combines a number of path strings.
        /// </summary>
        /// <param name="paths">The path strings to combine.</param>
        /// <returns>A path representing the combined path strings.</returns>
        public static string Combine(params string[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException("paths");
            }

            return paths.Aggregate(Path.Combine);
        }
    }
}
