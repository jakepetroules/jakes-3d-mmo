namespace MMO3D.Engine
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Provides methods for writing pack files containing XNB files containing ExtendedModels.
    /// Models can only be read using the ExtendedContentManager.
    /// </summary>
    public sealed class ModelPackFileWriter : PackFileWriter
    {
        /// <summary>
        /// Initializes a new instance of the ModelPackFileWriter class.
        /// </summary>
        /// <param name="stream">The stream used to write data.</param>
        private ModelPackFileWriter(BinaryWriter stream)
            : base(stream)
        {
        }

        /// <summary>
        /// Writes a pack file to the specified stream,
        /// with the specified dictionary of destination
        /// file names, and source file names to read from.
        /// </summary>
        /// <param name="stream">The stream used to write data.</param>
        /// <param name="fileNames">The dictionary of destination file names (keys), and source file names to read from (values).</param>
        public static void CreateFile(BinaryWriter stream, Dictionary<string, string> fileNames)
        {
            ModelPackFileWriter writer = new ModelPackFileWriter(stream);
            writer.Initialize(fileNames);
            writer.Write();
        }
    }
}
