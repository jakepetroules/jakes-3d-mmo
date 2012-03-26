namespace MMO3D.Engine
{
    using System;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// Class to compile X and FBX models into XNB format.
    /// </summary>
    public static class ModelBuilder
    {
        /// <summary>
        /// The ContentBuilder object to transform our source files into XNB files.
        /// </summary>
        private static ContentBuilder contentBuilder = new ContentBuilder();

        /// <summary>
        /// Gets the ModelBuilder's output directory, which will contain the generated .xnb files.
        /// </summary>
        /// <value>See summary.</value>
        public static string OutputDirectory
        {
            get { return ModelBuilder.contentBuilder.OutputDirectory; }
        }

        /// <summary>
        /// Builds an X or FBX model into an XNB file and returns the filename of the XNB file.
        /// </summary>
        /// <param name="fileName">The source file.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="System.InvalidOperationException">A build error occurred.</exception>
        public static string BuildModel(string fileName)
        {
            string safeFileName = ModelBuilder.GetAssetFriendlyName(fileName);

            ModelBuilder.contentBuilder.Clear();
            ModelBuilder.contentBuilder.Add(fileName, safeFileName, null, "ModelProcessor");

            string buildError = ModelBuilder.contentBuilder.Build();

            if (!string.IsNullOrEmpty(buildError))
            {
                throw new InvalidOperationException(buildError);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}\\{1}.xnb", ModelBuilder.contentBuilder.OutputDirectory, safeFileName);
            }
        }

        /// <summary>
        /// Builds a number of X and/or FBX models into XNB files and returns the filenames of the XNB files.
        /// </summary>
        /// <param name="fileNames">The source files.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="System.InvalidOperationException">A build error occurred.</exception>
        public static string[] BuildModels(string[] fileNames)
        {
            ModelBuilder.contentBuilder.Clear();

            string[] destFileNames = new string[fileNames.Length];

            for (int i = 0; i < fileNames.Length; i++)
            {
                string safeFileName = ModelBuilder.GetAssetFriendlyName(fileNames[i]);

                ModelBuilder.contentBuilder.Add(fileNames[i], safeFileName, null, "ModelProcessor");
                destFileNames[i] = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}.xnb", ModelBuilder.contentBuilder.OutputDirectory, safeFileName);
            }

            string buildError = ModelBuilder.contentBuilder.Build();

            if (!string.IsNullOrEmpty(buildError))
            {
                throw new InvalidOperationException(buildError);
            }
            else
            {
                return destFileNames;
            }
        }

        /// <summary>
        /// Gets the asset friendly name of a file from its full path and name.
        /// </summary>
        /// <param name="fileName">The full path and name of the file.</param>
        /// <returns>See summary.</returns>
        public static string GetAssetFriendlyName(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }
    }
}
