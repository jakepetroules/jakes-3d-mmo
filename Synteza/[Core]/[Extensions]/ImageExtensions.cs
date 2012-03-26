namespace Petroules.Synteza
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Security;
    using Petroules.Synteza.Imaging;

    /// <summary>
    /// Provides extensions to the <see cref="Image"/> class.
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Gets the number of unique colors in an image.
        /// </summary>
        /// <param name="image">The image to count the unique colors of.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="image"/> is <c>null</c>.</exception>
        [SecurityCritical]
        public static int CountColors(this Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            HashSet<int> colors = new HashSet<int>();

            FastBitmap fastBitmap = new FastBitmap(image);
            fastBitmap.Lock();

            // Loop through all the pixels of the image
            // and add the colors to the HashSet - duplicates
            // are automatically ignored by that collection type
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    // We use ToArgb because Colors are compared on more than their ARGB values - see Color class documentation on MSDN
                    colors.Add(fastBitmap.GetPixel(x, y).ToArgb());
                }
            }

            fastBitmap.Unlock();

            return colors.Count;
        }

        /// <summary>
        /// Gets the number of pixels of the specified color in an image.
        /// </summary>
        /// <param name="image">The image to count the unique colors of.</param>
        /// <param name="color">The color to get the occurrence count of.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="image"/> is <c>null</c>.</exception>
        [SecurityCritical]
        public static int CountColors(this Image image, Color color)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            int count = 0;

            FastBitmap fastBitmap = new FastBitmap(image);
            fastBitmap.Lock();

            // Loop through all the pixels of the image
            // and add the colors to the HashSet - duplicates
            // are automatically ignored by that collection type
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    // We use ToArgb because Colors are compared on more than their ARGB values - see Color class documentation on MSDN
                    if (fastBitmap.GetPixel(x, y).ToArgb() == color.ToArgb())
                    {
                        count++;
                    }
                }
            }

            fastBitmap.Unlock();

            return count;
        }

        /// <summary>
        /// Replaces all instances of <paramref name="oldColor"/> in the image with <paramref name="newColor"/> and returns a new instance.
        /// </summary>
        /// <param name="image">Reference to the <see cref="Image"/> instance.</param>
        /// <param name="oldColor">The color to replace.</param>
        /// <param name="newColor">The color to replace <paramref name="oldColor"/> with.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="image"/> is <c>null</c>.</exception>
        [SecurityCritical]
        public static Image ReplaceColor(this Image image, Color oldColor, Color newColor)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            Image clone = image.Clone() as Image;

            // If they try to replace a color with itself (sneaky, sneaky!) then just return our cloned image immediately
            if (oldColor.ToArgb() == newColor.ToArgb())
            {
                return clone;
            }

            FastBitmap fastBitmap = new FastBitmap(clone);
            fastBitmap.Lock();

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    // We use ToArgb because Colors are compared on more than their ARGB values - see Color class documentation on MSDN
                    if (fastBitmap.GetPixel(x, y).ToArgb() == oldColor.ToArgb())
                    {
                        fastBitmap.SetPixel(x, y, newColor);
                    }
                }
            }

            fastBitmap.Unlock();

            return fastBitmap.Image;
        }

        /// <summary>
        /// Compares <paramref name="image"/> with <paramref name="other"/> to see if they are logically equivalent (their sizes and colors match exactly).
        /// </summary>
        /// <param name="image">The image to compare.</param>
        /// <param name="other">The other image to compare.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="image"/> is <c>null</c> -or- <paramref name="other"/> is <c>null</c>.</exception>
        [SecurityCritical]
        public static bool CompareColors(this Image image, Image other)
        {
            if (image == null || other == null)
            {
                throw new ArgumentNullException(image == null ? "image" : "other");
            }

            if (image.Size != other.Size)
            {
                return false;
            }

            FastBitmap fastImage = new FastBitmap(image);
            FastBitmap fastOther = new FastBitmap(other);

            try
            {
                fastImage.Lock();
                fastOther.Lock();

                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        // We use ToArgb because Colors are compared on more than their ARGB values - see Color class documentation on MSDN
                        if (fastImage.GetPixel(x, y).ToArgb() != fastOther.GetPixel(x, y).ToArgb())
                        {
                            return false;
                        }
                    }
                }
            }
            finally
            {
                fastImage.Unlock();
                fastOther.Unlock();
            }

            return true;
        }

        /// <summary>
        /// Scales a image using high quality mode.
        /// </summary>
        /// <param name="image">The image to scale.</param>
        /// <param name="scaleFactorX">Scale factor on the X axis.</param>
        /// <param name="scaleFactorY">Scale factor on the Y axis.</param>
        /// <returns>The scaled image.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="image"/> is <c>null</c>.</exception>
        public static Image ScaleBitmap(this Image image, float scaleFactorX, float scaleFactorY)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            int scaledWidth = (int)System.Math.Max(image.Width * scaleFactorX, 1.0f);
            int scaledHeight = (int)System.Math.Max(image.Height * scaleFactorY, 1.0f);

            Image scaledImage = new Bitmap(scaledWidth, scaledHeight);

            using (Graphics graphics = Graphics.FromImage(scaledImage))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphics.DrawImage(image, new Rectangle(0, 0, scaledWidth, scaledHeight), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }

            foreach (PropertyItem propertyItem in image.PropertyItems)
            {
                scaledImage.SetPropertyItem(propertyItem);
            }

            return scaledImage;
        }
    }
}
