namespace Petroules.Synteza.IO
{
    using System;
    using System.IO;
    using System.IO.Compression;

    /// <summary>
    /// Provides methods for simple compression using GZIP.
    /// </summary>
    public static class SimpleCompression
    {
        /// <summary>
        /// Compresses an array of bytes using the GZIP algorithm.
        /// </summary>
        /// <param name="data">The array of bytes to compress.</param>
        /// <returns>The compressed byte array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <c>null</c>.</exception>
        public static byte[] Compress(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            // Create a memory stream to write our compressed data to
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Create a GZIP stream and write our byte array to compress it
                using (GZipStream gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzip.Write(data, 0, data.Length);
                }

                // Read compressed bytes back
                byte[] compressed = memoryStream.ToArray();

                // Create a new array the same as the length of the compressed data plus 4 bytes for the size
                byte[] buffer = new byte[compressed.Length + sizeof(int)];

                // Copy the compressed bytes we got earlier into our new array at offset 4
                Buffer.BlockCopy(compressed, 0, buffer, sizeof(int), compressed.Length);

                // Get the length of the compressed data array, making sure to adjust for endianness
                byte[] length = BitConverter.GetBytes(data.Length);
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(length);
                }

                // Copy the length of the compressed data array into the first 4 bytes our our new array
                Buffer.BlockCopy(length, 0, buffer, 0, sizeof(int));

                return buffer;
            }
        }

        /// <summary>
        /// Decompresses an array of bytes using the GZIP algorithm.
        /// </summary>
        /// <param name="data">The array of bytes to decompress.</param>
        /// <returns>The decompressed byte array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <c>null</c>.</exception>
        /// <exception cref="IndexOutOfRangeException">The length of <paramref name="data"/> was less than 4 bytes.</exception>
        public static byte[] Decompress(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (data.Length < 4)
            {
                throw new IndexOutOfRangeException("The length of data must be at least 4 bytes.");
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Get decompressed length
                byte[] length = new byte[sizeof(int)];
                Buffer.BlockCopy(data, 0, length, 0, sizeof(int));
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(length);
                }

                int decompressedLength = BitConverter.ToInt32(length, 0);

                // Write the compressed data to our memory stream
                memoryStream.Write(data, sizeof(int), data.Length - sizeof(int));
                memoryStream.Position = 0;

                // Create buffer to hold decompressed bytes and read them out
                byte[] buffer = new byte[decompressedLength];
                using (GZipStream gzip = new GZipStream(memoryStream, CompressionMode.Decompress, true))
                {
                    gzip.Read(buffer, 0, buffer.Length);
                }

                return buffer;
            }
        }
    }
}
