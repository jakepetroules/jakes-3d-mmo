namespace MMO3D.Engine
{
    using System;
    using System.IO;
    using Microsoft.Xna.Framework.Content;

    /// <summary>
    /// Extends the Content Manager to read from pack files.
    /// </summary>
    public sealed class PackFileContentManager : ContentManager
    {
        /// <summary>
        /// The delimiter separating the file ID portion of the asset name from the file name of the pack file.
        /// </summary>
        public const string PackedFileDelimiter = "->";

        /// <summary>
        /// Initializes a new instance of the PackFileContentManager class using the properties of the passed ContentManager.
        /// </summary>
        /// <param name="content">The ContentManager whose properties are used to create this instance.</param>
        public PackFileContentManager(ContentManager content)
            : base(content.ServiceProvider, content.RootDirectory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PackFileContentManager class.
        /// </summary>
        /// <param name="serviceProvider">The service provider the ContentManager should use to locate services.</param>
        public PackFileContentManager(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PackFileContentManager class.
        /// </summary>
        /// <param name="serviceProvider">The service provider the ContentManager should use to locate services.</param>
        /// <param name="rootDirectory">The root directory to search for content.</param>
        public PackFileContentManager(IServiceProvider serviceProvider, string rootDirectory)
            : base(serviceProvider, rootDirectory)
        {
        }

        /// <summary>
        /// Opens a stream for reading the specified asset. This method will read from pack files.
        /// </summary>
        /// <param name="assetName">The name of the asset being read.</param>
        /// <returns>The opened stream.</returns>
        protected override Stream OpenStream(string assetName)
        {
            if (assetName.Contains(PackFileContentManager.PackedFileDelimiter) || assetName.StartsWith("Textures/", StringComparison.Ordinal) || assetName.StartsWith("Textures\\", StringComparison.Ordinal))
            {
                if (assetName.StartsWith("Textures", StringComparison.Ordinal))
                {
                    assetName = "models.mc->" + assetName;
                }

                string[] split = assetName.Split(new string[] { PackFileContentManager.PackedFileDelimiter }, StringSplitOptions.RemoveEmptyEntries);

                string file = split[0];
                string packFileName = split[1];

                return new MemoryStream(new PackFileReader(new BinaryReader(base.OpenStream(file))).ReadFile(packFileName));
            }
            else
            {
                return base.OpenStream(assetName);
            }
        }
    }
}
