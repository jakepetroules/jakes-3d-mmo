namespace Petroules.Synteza
{
    using System;
    using System.Collections;

    /// <summary>
    /// Provides extensions to the <see cref="IList"/> interface.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Swaps the positions of the items at the specified indices.
        /// </summary>
        /// <param name="list">Reference to the <see cref="IList"/> instance.</param>
        /// <param name="index1">The index of the first item to swap.</param>
        /// <param name="index2">The index of the second item to swap.</param>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is <c>null</c>.</exception>
        public static void Swap(this IList list, int index1, int index2)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            object temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}
