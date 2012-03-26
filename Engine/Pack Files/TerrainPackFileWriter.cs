namespace MMO3D.Engine
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.IO;

    /// <summary>
    /// Provides a simple interface for writing terrain pack files.
    /// </summary>
    public sealed class TerrainPackFileWriter : PackFileWriter
    {
        /// <summary>
        /// Initializes a new instance of the TerrainPackFileWriter class.
        /// </summary>
        /// <param name="stream">The stream used to write data.</param>
        private TerrainPackFileWriter(BinaryWriter stream)
            : base(stream)
        {
        }

        /// <summary>
        /// Writes a pack file to the specified stream, with the specified terrain patches.
        /// </summary>
        /// <param name="stream">The stream used to write data.</param>
        /// <param name="patches">The terrain patches to write.</param>
        public static void CreateFile(BinaryWriter stream, ReadOnlyCollection<TerrainPatch> patches)
        {
            TerrainPackFileWriter writer = new TerrainPackFileWriter(stream);

            Dictionary<string, byte[]> array = new Dictionary<string, byte[]>();

            foreach (TerrainPatch patch in patches)
            {
                array.Add(patch.PatchId.ToString(), patch.ToByteArray());
            }

            writer.Initialize(array);
            writer.Write();
        }

        /// <summary>
        /// Writes a pack file to the specified stream, with the specified file names to read terrain patches from.
        /// </summary>
        /// <param name="stream">The stream used to write data.</param>
        /// <param name="patches">The file names of the terrain patches to write.</param>
        public static void CreateFile(BinaryWriter stream, StringCollection patches)
        {
            TerrainPackFileWriter writer = new TerrainPackFileWriter(stream);

            Dictionary<string, string> array = new Dictionary<string, string>();

            foreach (string patch in patches)
            {
                array.Add(TerrainPatch.FromByteArray(File.ReadAllBytes(patch)).PatchId.ToString(), patch);
            }

            writer.Initialize(array);
            writer.Write();
        }
    }
}
