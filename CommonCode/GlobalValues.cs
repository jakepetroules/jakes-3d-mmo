namespace MMO3D.CommonCode
{
    using System.Drawing;

    /// <summary>
    /// Provides a central accessor for all sorts of values in MMO3D.
    /// </summary>
    public static class GlobalValues
    {
        /// <summary>
        /// Gets the size of the thumbnail image of an item.
        /// </summary>
        /// <value>See summary.</value>
        public static Size ItemThumbnailSize
        {
            get { return new Size(32, 32); }
        }
    }
}
