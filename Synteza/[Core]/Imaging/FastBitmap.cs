namespace Petroules.Synteza.Imaging
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using System.Security;
    using Petroules.Synteza.Properties;

    /// <summary>
    /// Provides fast access to a bitmap while providing an interface much like the <see cref="System.Drawing.Bitmap"/> class.
    /// </summary>
    public sealed class FastBitmap : IDisposable
    {
        /// <summary>
        /// Reference to the attributes of a bitmap image.
        /// </summary>
        private BitmapData bitmapData;

        /// <summary>
        /// An array containing the bitmap data.
        /// </summary>
        private byte[] baseData;

        /// <summary>
        /// Initializes a new instance of the <see cref="FastBitmap"/> class.
        /// </summary>
        /// <param name="image">The image to encapsulate.</param>
        public FastBitmap(Image image)
        {
            this.Image = (Bitmap)image;
        }

        /// <summary>
        /// Gets a value indicating whether the bitmap is locked.
        /// </summary>
        public bool IsLocked
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the encapsulated image.
        /// </summary>
        internal Bitmap Image
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the color of the pixel at the specified coordinates.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="System.InvalidOperationException">The <see cref="Lock"/> method was not called.</exception>
        public Color GetPixel(int x, int y)
        {
            if (!this.IsLocked)
            {
                throw new InvalidOperationException(Resources.MustLockBeforeAccessingPixelData);
            }

            int pixelIndex = this.GetPixelStartIndex(x, y);
            return Color.FromArgb(this.baseData[pixelIndex + 3], this.baseData[pixelIndex + 2], this.baseData[pixelIndex + 1], this.baseData[pixelIndex]);
        }

        /// <summary>
        /// Sets the color of the pixel at the specified coordinates.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="color">The color to set the pixel to.</param>
        /// <exception cref="System.InvalidOperationException">The <see cref="Lock"/> method was not called.</exception>
        public void SetPixel(int x, int y, Color color)
        {
            if (!this.IsLocked)
            {
                throw new InvalidOperationException(Resources.MustLockBeforeAccessingPixelData);
            }

            int pixelIndex = this.GetPixelStartIndex(x, y);
            this.baseData[pixelIndex] = color.B;
            this.baseData[pixelIndex + 1] = color.G;
            this.baseData[pixelIndex + 2] = color.R;
            this.baseData[pixelIndex + 3] = color.A;
        }

        /// <summary>
        /// Locks the internal bitmap into system memory.
        /// </summary>
        [SecurityCritical]
        public void Lock()
        {
            // We only want to run the lock routine if it is unlocked, otherwise we'll get some state corruption ;)
            if (!this.IsLocked)
            {
                // Get the bounds of the image
                Rectangle bounds = new Rectangle(Point.Empty, this.Image.Size);

                // Lock the image and get the pointer to the beginning of the pixel data
                this.bitmapData = this.Image.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                // Initialize our data array to hold the data of the bitmap and copy it there
                this.baseData = new byte[this.bitmapData.Stride * this.Image.Height];
                Marshal.Copy(this.bitmapData.Scan0, this.baseData, 0, this.baseData.Length);

                // Locked and ready!
                this.IsLocked = true;
            }
        }

        /// <summary>
        /// Unlocks the internal bitmap from system memory.
        /// </summary>
        [SecurityCritical]
        public void Unlock()
        {
            // We only want to run the unlock routine if it is locked, otherwise we'll get some state corruption ;)
            if (this.IsLocked)
            {
                // Copy our data, with whatever modifications we may have made, back to the Bitmap object, and unlock it
                Marshal.Copy(this.baseData, 0, this.bitmapData.Scan0, this.baseData.Length);
                this.Image.UnlockBits(this.bitmapData);

                // No longer locked...
                this.IsLocked = false;

                // Clear the data reference
                this.baseData = null;
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
                this.Unlock();
                this.Image.Dispose();
            }

            this.Image = null;
        }

        /// <summary>
        /// Gets the starting index of the pixel with the specified coordinates, in the internal data array.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>The starting index of the pixel.</returns>
        private int GetPixelStartIndex(int x, int y)
        {
            // Just to document the occurence of '4' so it doesn't look like a magic number...
            // 4 is the number of bytes in a color, one byte each for red, blue, green and alpha
            return (y * this.bitmapData.Stride) + (x * 4);
        }
    }
}
