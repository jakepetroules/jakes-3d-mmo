namespace Petroules.Synteza
{
    using System.Windows.Forms;

    /// <summary>
    /// Provides extensions to the <see cref="Screen"/> class.
    /// </summary>
    public static class ScreenExtensions
    {
        /// <summary>
        /// Gets the index of the <see cref="Screen"/>; -1 if not found.
        /// </summary>
        /// <param name="screen">The <see cref="Screen"/> to get the index of.</param>
        /// <returns>The index of the specified <see cref="Screen"/>; -1 if not found.</returns>
        public static int GetIndex(this Screen screen)
        {
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                if (Screen.AllScreens[i] == screen)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
