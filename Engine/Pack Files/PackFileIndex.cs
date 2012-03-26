namespace MMO3D.Engine
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Encapsulates the index of a pack file in memory.
    /// </summary>
    public sealed class PackFileIndex : ICloneable
    {
        /// <summary>
        /// Initializes a new instance of the PackFileIndex class.
        /// </summary>
        public PackFileIndex()
        {
            this.Entries = new Dictionary<string, PackFileEntry>();
        }

        /// <summary>
        /// Gets a dictionary containing the file name and PackFileEntry of the files in the pack file.
        /// </summary>
        /// <value>See summary.</value>
        public Dictionary<string, PackFileEntry> Entries
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        /// <returns>See summary.</returns>
        public object Clone()
        {
            PackFileIndex pfi = new PackFileIndex();

            string[] keys = new string[this.Entries.Count];
            this.Entries.Keys.CopyTo(keys, 0);

            PackFileEntry[] values = new PackFileEntry[this.Entries.Count];
            this.Entries.Values.CopyTo(values, 0);

            for (int i = 0; i < this.Entries.Count; i++)
            {
                pfi.Entries.Add(keys[i], values[i]);
            }

            return pfi;
        }
    }
}
