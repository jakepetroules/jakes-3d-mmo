namespace Petroules.Synteza.Text
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a collection of filter strings to form a complete filter string.
    /// </summary>
    public sealed class FilterStringCollection : List<FilterString>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterStringCollection"/> class that is empty.
        /// </summary>
        public FilterStringCollection()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterStringCollection"/> class as a wrapper for the specified list.
        /// </summary>
        /// <param name="list">The list that is wrapped by the new collection.</param>
        public FilterStringCollection(IList<FilterString> list)
            : base(list)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterStringCollection"/> class as a wrapper for the specified list.
        /// </summary>
        /// <param name="list">The list that is wrapped by the new collection.</param>
        public FilterStringCollection(params FilterString[] list)
            : base(list)
        {
        }

        /// <summary>
        /// Combines the two filter strings into a <see cref="FilterStringCollection"/>.
        /// </summary>
        /// <param name="left">The left-hand operand.</param>
        /// <param name="right">The right-hand operand.</param>
        /// <returns>A <see cref="FilterStringCollection"/> representing the combined filter strings.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="left"/> is <c>null</c> -or- <paramref name="right"/> is <c>null</c>.</exception>
        public static FilterStringCollection operator +(FilterStringCollection left, FilterStringCollection right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException(left == null ? "left" : "right");
            }

            var collection = new FilterStringCollection();
            left.ForEach(p => collection.Add(p));
            right.ForEach(p => collection.Add(p));
            return collection;
        }

        /// <summary>
        /// Combines the two filter strings into a <see cref="FilterStringCollection"/>.
        /// </summary>
        /// <param name="left">The left-hand operand.</param>
        /// <param name="right">The right-hand operand.</param>
        /// <returns>A <see cref="FilterStringCollection"/> representing the combined filter strings.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="left"/> is <c>null</c> -or- <paramref name="right"/> is <c>null</c>.</exception>
        public static FilterStringCollection operator +(FilterStringCollection left, FilterString right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException(left == null ? "left" : "right");
            }

            var collection = new FilterStringCollection();
            left.ForEach(p => collection.Add(p));
            collection.Add(right);
            return collection;
        }

        /// <summary>
        /// Combines the two filter strings into a <see cref="FilterStringCollection"/>.
        /// </summary>
        /// <param name="left">The left-hand operand.</param>
        /// <param name="right">The right-hand operand.</param>
        /// <returns>A <see cref="FilterStringCollection"/> representing the combined filter strings.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="left"/> is <c>null</c> -or- <paramref name="right"/> is <c>null</c>.</exception>
        public static FilterStringCollection operator +(FilterString left, FilterStringCollection right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException(left == null ? "left" : "right");
            }

            var collection = new FilterStringCollection();
            collection.Add(left);
            right.ForEach(p => collection.Add(p));
            return collection;
        }

        /// <summary>
        /// Converts the specified <see cref="FilterStringCollection"/> to a <see cref="System.String"/>.
        /// </summary>
        /// <param name="filterStringCollection">The <see cref="FilterStringCollection"/> to convert.</param>
        /// <returns>See summary.</returns>
        public static implicit operator string(FilterStringCollection filterStringCollection)
        {
            return filterStringCollection.ToString();
        }

        /// <summary>
        /// Converts the collection of <see cref="FilterString"/>s to an actual filter string.
        /// </summary>
        /// <returns>See summary.</returns>
        public override string ToString()
        {
            return string.Join("|", this.Select(p => p.ToString()).ToArray());
        }
    }
}
