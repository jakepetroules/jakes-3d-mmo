namespace Petroules.Synteza.Imaging
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Petroules.Synteza.Native;

    /// <summary>
    /// Provides methods that create <see cref="Cursor"/> objects from <see cref="Bitmap"/>s.
    /// </summary>
    public static class CursorCreator
    {
        /// <summary>
        /// Initializes static members of the <see cref="CursorCreator"/> class.
        /// </summary>
        /// <exception cref="UnsupportedPlatformException">The current platform is not supported.</exception>
        static CursorCreator()
        {
            PlatformUtilities.ThrowIfUnsupported(PlatformID.Win32NT);
        }

        /// <summary>
        /// Creates a cursor from two cursors, with the first cursor on the left and the second on the right.
        /// </summary>
        /// <param name="cursor">The first/left cursor.</param>
        /// <param name="secondaryCursor">The second/right cursor.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cursor"/> is <c>null</c> -or- <paramref name="secondaryCursor"/> is <c>null</c>.</exception>
        public static Cursor CreateDualCursor(this Cursor cursor, Cursor secondaryCursor)
        {
            if (cursor == null || secondaryCursor == null)
            {
                throw new ArgumentNullException(cursor == null ? "cursor" : "secondaryCursor");
            }

            using (Bitmap bmp = new Bitmap(cursor.Size.Width + secondaryCursor.Size.Width, Math.Max(cursor.Size.Height, secondaryCursor.Size.Height)))
            using (Bitmap cursor1 = cursor.CreateBitmap())
            using (Bitmap cursor2 = secondaryCursor.CreateBitmap())
            {
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImageUnscaled(cursor1, Point.Empty);
                g.DrawImageUnscaled(cursor2, new Point(cursor.Size.Width, Math.Abs(cursor.Size.Height - secondaryCursor.Size.Height) / 2));

                return bmp.CreateCursor(cursor.HotSpot.X, cursor.HotSpot.Y);
            }
        }

        /// <summary>
        /// Creates a bitmap from a cursor.
        /// </summary>
        /// <param name="cursor">The cursor to create the bitmap from.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cursor"/> is <c>null</c>.</exception>
        public static Bitmap CreateBitmap(this Cursor cursor)
        {
            if (cursor == null)
            {
                throw new ArgumentNullException("cursor");
            }

            return Icon.FromHandle(cursor.CopyHandle()).ToBitmap();
        }

        /// <summary>
        /// Creates a cursor from a bitmap. The hotspot is set to the center of the image.
        /// </summary>
        /// <param name="bitmap">The bitmap to create the cursor from.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bitmap"/> is <c>null</c>.</exception>
        public static Cursor CreateCursor(this Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            return CursorCreator.CreateCursor(bitmap, bitmap.Width / 2, bitmap.Height / 2);
        }

        /// <summary>
        /// Creates a cursor from a bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap to create the cursor from.</param>
        /// <param name="hotspotX">Specifies the x-coordinate of a cursor's hot spot.</param>
        /// <param name="hotspotY">Specifies the y-coordinate of a cursor's hot spot.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bitmap"/> is <c>null</c>.</exception>
        public static Cursor CreateCursor(this Bitmap bitmap, int hotspotX, int hotspotY)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            NativeMethods.IconInfo iconInfo = new NativeMethods.IconInfo(hotspotX, hotspotY, false);
            NativeMethods.GetIconInfo(bitmap.GetHicon(), ref iconInfo);

            return new Cursor(NativeMethods.CreateIconIndirect(ref iconInfo));
        }
    }
}
