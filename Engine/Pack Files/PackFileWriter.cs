namespace MMO3D.Engine
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Petroules.Synteza.IO;

    /// <summary>
    /// Provides the base functionality for writing pack files.
    /// </summary>
    public abstract class PackFileWriter
    {
        /// <summary>
        /// The keys (target file names) of the files to be written to the pack file.
        /// </summary>
        private string[] keys;

        /// <summary>
        /// The values (source file names) of the files to be written to the pack file.
        /// This is only used if fileData is null.
        /// </summary>
        private string[] filenames;

        /// <summary>
        /// The raw file data to be written to the pack file.
        /// </summary>
        private byte[][] filesData;

        /// <summary>
        /// The stream offsets of each file's stream offset data in the index.
        /// This is needed because strings are of variable length, so we can't
        /// dynamically measure the position, we must remember it when we write it.
        /// </summary>
        private long[] memoryIndex;

        /// <summary>
        /// Initializes a new instance of the PackFileWriter class.
        /// </summary>
        /// <param name="stream">The stream used to write data.</param>
        protected PackFileWriter(BinaryWriter stream)
        {
            this.Stream = stream;
        }

        /// <summary>
        /// Gets the stream used to write data.
        /// </summary>
        /// <value>See summary.</value>
        protected BinaryWriter Stream
        {
            get;
            private set;
        }

        /// <summary>
        /// Writes all the data to the pack file.
        /// </summary>
        public void Write()
        {
            this.WriteHeaderAndIndex();

            if (this.filesData != null)
            {
                for (int i = 0; i < this.filesData.Length; i++)
                {
                    this.WriteFile(this.filesData[i], i);
                }
            }
            else if (this.filenames != null)
            {
                for (int i = 0; i < this.filenames.Length; i++)
                {
                    this.WriteFile(File.ReadAllBytes(this.filenames[i]), i);
                }
            }
            else
            {
                throw new InvalidOperationException("The Initialize method must be called before this method can execute.");
            }
        }

        /// <summary>
        /// Initializes the pack file, preparing it for writing.
        /// </summary>
        /// <param name="files">Dictionary containing the destination and source file names of the files to be written.</param>
        protected void Initialize(Dictionary<string, string> files)
        {
            this.keys = new string[files.Count];
            this.filenames = new string[files.Count];

            files.Keys.CopyTo(this.keys, 0);
            files.Values.CopyTo(this.filenames, 0);

            this.memoryIndex = new long[files.Count];
        }

        /// <summary>
        /// Initializes the pack file, preparing it for writing.
        /// </summary>
        /// <param name="files">Dictionary containing the destination file names and raw byte data of the files to be written.</param>
        protected void Initialize(Dictionary<string, byte[]> files)
        {
            this.keys = new string[files.Count];
            this.filesData = new byte[files.Count][];

            files.Keys.CopyTo(this.keys, 0);
            files.Values.CopyTo(this.filesData, 0);

            this.memoryIndex = new long[files.Count];
        }

        /// <summary>
        /// Writes the file header and patch index of the pack file.
        /// </summary>
        private void WriteHeaderAndIndex()
        {
            // Write pack file header
            this.Stream.Write(PackFile.GetFileHeader(), 0, PackFile.GetFileHeader().Length);

            // Write the number of files stored (int)
            this.Stream.Write(this.keys.Length);

            // For each file, we need:
            //     variable bytes for the file name
            //     8 bytes for the offset in the pack file, of the packed file (long)
            //     4 bytes for the length of the data (int)
            // >12+ bytes total
            for (int i = 0; i < this.keys.Length; i++)
            {
                this.Stream.Write(this.keys[i]);

                this.memoryIndex[i] = this.Stream.BaseStream.Position;

                this.Stream.Write(0L);
                this.Stream.Write(0);
            }
        }

        /// <summary>
        /// Writes a file to the pack file.
        /// </summary>
        /// <param name="fileData">The file data to write.</param>
        /// <param name="index">The index of the file being written.</param>
        private void WriteFile(byte[] fileData, int index)
        {
            // Get compressed version of terrain patch...
            byte[] compressedBytes = SimpleCompression.Compress(fileData);

            // Store current position in stream (this is where we write the terrain patch)
            long position = this.Stream.BaseStream.Position;

            // Seek to the proper location to write this patch's location in the file
            this.Stream.BaseStream.Position = this.memoryIndex[index];

            // Write patch's X and Y coordinates, offset and length
            this.Stream.Write(position);
            this.Stream.Write(compressedBytes.Length);

            // Go back to where the terrain patch should be written
            this.Stream.BaseStream.Position = position;

            // Write the terrain patch
            this.Stream.Write(compressedBytes, 0, compressedBytes.Length);
        }
    }
}
