namespace MMO3D.WorldEditor
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using MMO3D.CommonCode;

    /// <summary>
    /// Contains various helper methods.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Gets a Size object from a screen resolution string.
        /// </summary>
        /// <param name="resolution">The screen resolution string to convert into a Size object.</param>
        /// <returns>The Size object from the screen resolution string. Returns a Size of {0, 0} if conversion was unsuccessful.</returns>
        public static Size ResolutionFromString(string resolution)
        {
            try
            {
                string[] array = resolution.Split(new char[] { '×', 'x', 'X', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                return new Size(Convert.ToInt32(array[0], CultureInfo.InvariantCulture), Convert.ToInt32(array[1], CultureInfo.InvariantCulture));
            }
            catch (FormatException)
            {
                return Size.Empty;
            }
        }

        /// <summary>
        /// Gets the file name of a terrain patch with particular coordinates.
        /// </summary>
        /// <param name="patchId">The patch ID of the terrain patch to get the file name of.</param>
        /// <returns>See summary.</returns>
        public static string GetPatchFileName(Point3D patchId)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2},{3},{4}.pat", Application.StartupPath, "GameData/Terrain", patchId.X, patchId.Y, patchId.Z);
        }

        /// <summary>
        /// Ensure that directories required for operations are created.
        /// </summary>
        public static void EnsureDirectories()
        {
            Directory.CreateDirectory(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", Application.StartupPath, "GameData/Terrain"));
            Directory.CreateDirectory(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", Application.StartupPath, "GameData/Models/Textures"));
        }

        /// <summary>
        /// Properly stops all running threads and terminates the application.
        /// Note that this should not be called until all data has first been saved.
        /// </summary>
        public static void ExitProgram()
        {
            Application.Exit();
        }
    }
}
