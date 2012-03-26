namespace MMO3D.Engine
{
    using System.IO;
    using Petroules.Synteza.IO;

    /// <summary>
    /// Provides the base functionality for reading pack files.
    /// </summary>
    public class PackFileReader
    {
        /// <summary>
        /// Cached instance of the pack file's index.
        /// </summary>
        private PackFileIndex packFileIndex;

        /// <summary>
        /// Initializes a new instance of the PackFileReader class.
        /// </summary>
        /// <param name="stream">The stream used to read data.</param>
        public PackFileReader(BinaryReader stream)
        {
            this.Stream = stream;
            this.packFileIndex = this.ReadIndex();
        }

        /// <summary>
        /// Initializes a new instance of the PackFileReader class.
        /// </summary>
        /// <param name="fileName">The filename of the file to read from.</param>
        /// <exception cref="System.ArgumentException">
        /// The stream does not support reading, the stream is null,
        /// or the stream is already closed. -or- path is an empty string (""),
        /// contains only white space, or contains one or more invalid characters.
        /// -or- path refers to a non-file device, such as "con:", "com1:", "lpt1:",
        /// etc. in an NTFS environment.
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// path refers to a non-file device, such as "con:", "com1:", "lpt1:", etc.
        /// in a non-NTFS environment.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">path is null.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.IO.FileNotFoundException">
        /// The file cannot be found, such as when mode is FileMode.Truncate or FileMode.Open,
        /// and the file specified by path does not exist. The file must already exist
        /// in these modes.
        /// </exception>
        /// <exception cref="System.IO.IOException">
        /// An I/O error occurs, such as specifying FileMode.CreateNew and the file specified
        ///  by path already exists. -or- The stream has been closed.
        /// </exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined maximum
        /// length. For example, on Windows-based platforms, paths must be less than
        /// 248 characters, and file names must be less than 260 characters.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">mode contains an invalid value.</exception>
        public PackFileReader(string fileName)
            : this(new BinaryReader(new FileStream(fileName, FileMode.Open)))
        {
        }

        /// <summary>
        /// Gets a copy of the pack file's index.
        /// </summary>
        /// <value>See summary.</value>
        public PackFileIndex PackFileIndex
        {
            get { return this.packFileIndex.Clone() as PackFileIndex; }
        }

        /// <summary>
        /// Gets the stream used to read data.
        /// </summary>
        /// <value>See summary.</value>
        protected BinaryReader Stream
        {
            get;
            private set;
        }

        /// <summary>
        /// Reads a file as a byte array from the pack file.
        /// </summary>
        /// <param name="fileName">The file name of file to read.</param>
        /// <returns>See summary.</returns>
        public byte[] ReadFile(string fileName)
        {
            // If the pack file contained the file with the requested name...
            if (this.packFileIndex.Entries.ContainsKey(fileName))
            {
                // Get the file info
                PackFileEntry tpi;
                this.packFileIndex.Entries.TryGetValue(fileName, out tpi);

                // Save stream offset and set the stream to the location of the file data
                long streamOffset = this.Stream.BaseStream.Position;
                this.Stream.BaseStream.Position = tpi.Offset;

                // Read the appropriate number of bytes and decompress them
                byte[] patch = SimpleCompression.Decompress(this.Stream.ReadBytes(tpi.Length));

                // Restore stream offset
                this.Stream.BaseStream.Position = streamOffset;

                return patch;
            }
            else
            {
                throw new IOException(fileName + " does not exist in pack file.");
                // TODO TODO
            }

            // Otherwise if a patch was not found, return null
            return null;
        }

        /// <summary>
        /// Reads the file index of the pack file.
        /// </summary>
        /// <returns>See summary.</returns>
        private PackFileIndex ReadIndex()
        {
            // Save current offset and go to the beginning of the file   
            long streamOffset = this.Stream.BaseStream.Position;
            this.Stream.BaseStream.Position = 0;

            // Make sure the header is valid - if it's not a pack file we shan't bother reading it!
            if (PackFile.IsValidHeader(this.Stream.ReadBytes(PackFile.GetFileHeader().Length)))
            {
                // Get the number of virtual files in the file
                int filesStored = this.Stream.ReadInt32();

                // Create an array to store the packed file index
                PackFileIndex patchIndex = new PackFileIndex();

                // Read the pack file index, getting the file ID offsets and lengths of the packed files
                for (int i = 0; i < filesStored; i++)
                {
                    string fileName = this.Stream.ReadString();
                    long offset = this.Stream.ReadInt64();
                    int length = this.Stream.ReadInt32();

                    patchIndex.Entries.Add(fileName, new PackFileEntry(offset, length));
                }

                // Restore stream offset
                this.Stream.BaseStream.Position = streamOffset;

                return patchIndex;
            }

            return null;
        }
    }
}
