namespace MMO3D.Engine
{
    using System.IO;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;

    /// <summary>
    /// Provides a simple interface for reading terrain pack files.
    /// </summary>
    public sealed class TerrainPackFileReader : PackFileReader
    {
        /// <summary>
        /// Initializes a new instance of the TerrainPackFileReader class.
        /// </summary>
        /// <param name="stream">The stream used to read data.</param>
        public TerrainPackFileReader(BinaryReader stream)
            : base(stream)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TerrainPackFileReader class.
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
        public TerrainPackFileReader(string fileName)
            : base(fileName)
        {
        }

        /// <summary>
        /// Reads a terrain patch from the pack file.
        /// </summary>
        /// <param name="patchId">The terrain patch ID of the terrain patch to read.</param>
        /// <returns>See summary.</returns>
        public TerrainPatch ReadTerrainPatch(Point3D patchId)
        {
            return TerrainPatch.FromByteArray(this.ReadFile(patchId.ToString()));
        }
    }
}
