namespace MMO3D.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Represents a strongly-typed collection of values.
    /// </summary>
    /// <typeparam name="T">The type of the items contained in the list.</typeparam>
    [DebuggerDisplay("{Count} items")]
    public sealed class Vector<T>
    {
        /// <summary>
        /// The items contained in the <see cref="Vector&lt;T&gt;"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T[] items = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector&lt;T&gt;"/> class that is empty and has the default initial capacity.
        /// </summary>
        public Vector()
            : this(Vector<T>.BaseArraySize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector&lt;T&gt;"/> class that is empty and has the specified capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the <see cref="Vector&lt;T&gt;"/> can initialy store.</param>
        public Vector(int capacity)
        {
            this.Clear(capacity);
        }
        
        /// <summary>
        /// Gets the minimal size of an array if no size is specified.
        /// </summary>
        /// <value>See summary.</value>
        public static int BaseArraySize
        {
            get { return 8; }
        }

        /// <summary>
        /// Gets the number of elements actually contained in the <see cref="Vector&lt;T&gt;"/>.
        /// </summary>
        /// <value>See summary.</value>
        public int Count
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the value at the specified index.
        /// </summary>
        /// <param name="index">The index of the value to get or set.</param>
        /// <returns>See summary.</returns>
        public T this[int index]
        {
            get { return this.items[index]; }
            set { this.items[index] = value; }
        }

        /// <summary>
        /// Adds an object to the end of the <see cref="Vector&lt;T&gt;"/>.
        /// </summary>
        /// <param name="item">The object to be added to the end of the <see cref="Vector&lt;T&gt;"/>. The value can be null for reference types.</param>
        public void Add(T item)
        {
            this.items[this.Count] = item;
            this.Count++;

            if (this.Count >= this.items.Length)
            {
                Array.Resize<T>(ref this.items, this.items.Length << 1);
            }

            return;
        }

        /// <summary>
        /// Removes all elements from the <see cref="Vector&lt;T&gt;"/>.
        /// </summary>
        public void Clear()
        {
            this.Clear(Vector<T>.BaseArraySize);
        }

        /// <summary>
        /// Removes all elements from the <see cref="Vector&lt;T&gt;"/>.
        /// </summary>
        /// <param name="capacity">The number of elements that the <see cref="Vector&lt;T&gt;"/> can store after reset.</param>
        public void Clear(int capacity)
        {
            this.items = new T[capacity];
            this.Count = 0;
        }

        /// <summary>
        /// Determines whether an element is in the <see cref="Vector&lt;T&gt;"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="Vector&lt;T&gt;"/>. The value can be null for reference types.</param>
        /// <returns>See summary.</returns>
        public bool Contains(T item)
        {
            if (item == null)
            {
                for (int j = 0; j < this.Count; j++)
                {
                    if (this.items[j] == null)
                    {
                        return true;
                    }
                }

                return false;
            }

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < this.Count; i++)
            {
                if (comparer.Equals(this.items[i], item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Searches for the specified index and returns an index of the first occurence.
        /// </summary>
        /// <param name="item">The item to find the index of.</param>
        /// <returns>See summary.</returns>
        public int IndexOf(T item)
        {
            return Array.IndexOf(this.items, item);
        }

        /// <summary>
        /// Removes the element at the specified index from the <see cref="Vector&lt;T&gt;"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            this.Count--;

            if (index < this.Count)
            {
                Array.Copy(this.items, index + 1, this.items, index, this.Count - index);
            }

            this.items[this.Count] = default(T);
        }

        /// <summary>
        /// Removes the fist occurence of a specific object from the <see cref="Vector&lt;T&gt;"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="Vector&lt;T&gt;"/>. The value can be null for reference type.</param>
        /// <returns>Whether an item was removed.</returns>
        public bool Remove(T item)
        {
            int index = this.IndexOf(item);

            if (index >= 0)
            {
                this.RemoveAt(index);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Copies the elements of the <see cref="Vector&lt;T&gt;"/> to a new array.
        /// </summary>
        /// <returns>See summary.</returns>
        public T[] ToArray()
        {
            if (this.Count == this.items.Length)
            {
                return this.items;
            }

            T[] newArray = new T[this.Count];
            Array.Copy(this.items, newArray, this.Count);

            return newArray;
        }
    }
}
