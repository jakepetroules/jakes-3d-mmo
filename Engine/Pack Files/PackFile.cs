namespace MMO3D.Engine
{
    using System;

    /// <summary>
    /// Contains base functionality for a packed file containing multiple files.
    /// By default files are compressed before being written and decompressed
    /// before being read.
    /// </summary>
    public static class PackFile
    {
        /// <summary>
        /// Gets the header of the file type.
        /// </summary>
        /// <returns>See summary.</returns>
        /// <remarks>
        /// Asides from the ASCII characters, PCK, which stand for 'pack', all
        /// the bytes exist for the same purposes as they do in the PNG file format.
        /// </remarks>
        public static byte[] GetFileHeader()
        {
            return new byte[] { 137, (byte)'P', (byte)'C', (byte)'K', (byte)'\r', (byte)'\n', 26, (byte)'\n' };
        }

        /// <summary>
        /// Compares a byte array to the pack file format header and determines if they are equal.
        /// </summary>
        /// <param name="array">The bytes to compare.</param>
        /// <returns>See summary.</returns>
        public static bool IsValidHeader(byte[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (array.Length != PackFile.GetFileHeader().Length)
            {
                return false;
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != PackFile.GetFileHeader()[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
