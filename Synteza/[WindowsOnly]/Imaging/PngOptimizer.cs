namespace Petroules.Synteza.Imaging
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using Petroules.Synteza.IO;
    using Petroules.Synteza.Native;
    using Petroules.Synteza.Properties;

    /// <summary>
    /// Provides a .NET interface to the program <c>pngcrush</c>.
    /// </summary>
    public sealed class PngOptimizer : IDisposable
    {
        /// <summary>
        /// The process used to run <c>pngcrush</c>.
        /// </summary>
        private Process process = new Process();

        /// <summary>
        /// Initializes a new instance of the <see cref="PngOptimizer"/> class.
        /// </summary>
        /// <exception cref="UnsupportedPlatformException">The current platform is not supported.</exception>
        public PngOptimizer()
        {
            PlatformUtilities.ThrowIfUnsupported(PlatformID.Win32NT);

            this.PngCrushFile = PngOptimizer.DefaultPngCrushFile;
        }

        /// <summary>
        /// Raised when the <c>pngcrush</c> process returns a line of output.
        /// </summary>
        public event DataReceivedEventHandler OutputReceived
        {
            add { this.process.OutputDataReceived += value; }
            remove { this.process.OutputDataReceived -= value; }
        }

        /// <summary>
        /// Gets the default location of the <c>pngcrush</c> executable.
        /// </summary>
        public static string DefaultPngCrushFile
        {
            get { return PathExtensions.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Petroules Enterprises", "NetUtilities", "PngCrush.exe"); }
        }

        /// <summary>
        /// Gets or sets the location of the <c>pngcrush</c> executable.
        /// This can be changed if it is undesired to use the version of
        /// <c>pngcrush</c> that is packages with NetUtilities.
        /// </summary>
        public string PngCrushFile
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the optimizer is running.
        /// </summary>
        public bool Running
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the specified file is a PNG image.
        /// </summary>
        /// <param name="fileName">The name of the file to check.</param>
        /// <returns>See summary.</returns>
        public static bool IsPngImageFile(string fileName)
        {
            try
            {
                using (Image image = Image.FromFile(fileName))
                {
                    if (image.RawFormat.Guid != ImageFormat.Png.Guid)
                    {
                        return false;
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Optimizes a PNG image.
        /// </summary>
        /// <param name="sourceFile">The source PNG file to optimize.</param>
        /// <param name="destinationFile">The destination file to write the optimized PNG file to.</param>
        public void Optimize(string sourceFile, string destinationFile)
        {
            try
            {
                this.Running = true;

                // Define the algorithm we will use for verification
                const ChecksumAlgorithm VerificationAlgorithm = ChecksumAlgorithm.MD5;

                // Only perform this checking step if the default pngcrush executable is being used
                if (this.PngCrushFile == PngOptimizer.DefaultPngCrushFile)
                {
                    // If the png crush file does not exist or its checksum fails, rewrite it to disk before using it
                    if (!File.Exists(PngOptimizer.DefaultPngCrushFile) || !ChecksumUtilities.PerformChecksum(PngOptimizer.DefaultPngCrushFile, ChecksumUtilities.GetChecksumBytes(new MemoryStream(Resources.PngCrush), VerificationAlgorithm), VerificationAlgorithm))
                    {
                        // Make sure the containing directory exists!
                        Directory.CreateDirectory(Path.GetDirectoryName(PngOptimizer.DefaultPngCrushFile));

                        // Then write the file
                        File.WriteAllBytes(PngOptimizer.DefaultPngCrushFile, Resources.PngCrush);
                    }
                }

                // Set some start info parameters
                this.process.StartInfo.Arguments = string.Format(CultureInfo.InvariantCulture, "-brute -rem gAMA -rem iCCP -rem sRGB \"{0}\" \"{1}\"", sourceFile, destinationFile);
                this.process.StartInfo.CreateNoWindow = true;
                this.process.StartInfo.FileName = this.PngCrushFile;
                this.process.StartInfo.RedirectStandardOutput = true;
                this.process.StartInfo.UseShellExecute = false;
                this.process.StartInfo.WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath);

                // Start the process and begin reading its output
                this.process.Start();
                this.process.BeginOutputReadLine();

                // Wait for it to exit
                this.process.WaitForExit();
            }
            finally
            {
                this.Running = false;
            }
        }

        /// <summary>
        /// Aborts any currently running <c>pngcrush</c> process.
        /// </summary>
        public void Abort()
        {
            if (this.Running && !this.process.HasExited)
            {
                this.process.Kill();
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.process != null)
                {
                    this.process.Dispose();
                }
            }
        }
    }
}
