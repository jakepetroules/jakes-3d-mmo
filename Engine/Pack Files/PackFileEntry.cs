namespace MMO3D.Engine
{
    using System.Globalization;

    /// <summary>
    /// Stores an offset and length for a pack file entry.
    /// </summary>
    public struct PackFileEntry
    {
        /// <summary>
        /// A pre-initialized default instance of this class.
        /// </summary>
        public static readonly PackFileEntry Null = new PackFileEntry();

        /// <summary>
        /// Initializes a new instance of the PackFileEntry struct.
        /// </summary>
        /// <param name="offset">The offset of the terrain patch in the TPC file.</param>
        /// <param name="length">The length of the compressed terrain patch data.</param>
        public PackFileEntry(long offset, int length)
            : this()
        {
            this.Offset = offset;
            this.Length = length;
        }

        /// <summary>
        /// Gets the offset of the terrain patch in the TPC file.
        /// </summary>
        /// <value>See summary.</value>
        public long Offset
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the length of the compressed terrain patch data.
        /// </summary>
        /// <value>See summary.</value>
        public int Length
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the specified instances are equal.
        /// </summary>
        /// <param name="packFileEntry1">The first instance.</param>
        /// <param name="packFileEntry2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator ==(PackFileEntry packFileEntry1, PackFileEntry packFileEntry2)
        {
            return packFileEntry1.Offset == packFileEntry2.Offset && packFileEntry1.Length == packFileEntry2.Length;
        }

        /// <summary>
        /// Determines whether the specified instances are inequal.
        /// </summary>
        /// <param name="packFileEntry1">The first instance.</param>
        /// <param name="packFileEntry2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator !=(PackFileEntry packFileEntry1, PackFileEntry packFileEntry2)
        {
            return !(packFileEntry1 == packFileEntry2);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>True if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return (PackFileEntry)obj == this;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return (int)(this.Offset >> 32) ^ this.Length;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> representing the current <see cref="MMO3D.Engine.PackFileEntry"/>.
        /// </summary>
        /// <returns>See summary.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{{Offset: {0} Length: {1}}}", this.Offset, this.Length);
        }
    }
}
