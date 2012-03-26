namespace MMO3D.CommonCode
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using Microsoft.Xna.Framework.Graphics;
    using Sys = System.Drawing;
    using Xna = Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Contains XNA texture and color-related functions.
    /// </summary>
    public static class TextureManipulation
    {
        /// <summary>
        /// Generates a Microsoft.Xna.Framework.Graphics.Texture2D from a System.Drawing.Bitmap.
        /// </summary>
        /// <param name="bitmap">The Bitmap whose color data is used to generate the Texture2D.</param>
        /// <param name="device">The GraphicsDevice used to display the texture.</param>
        /// <returns>See summary.</returns>
        public static Texture2D Texture2DFromBitmap(this Image bitmap, GraphicsDevice device)
        {
            TextureCreationParameters parameters = new TextureCreationParameters();
            parameters.Filter = FilterOptions.Triangle;
            parameters.Format = SurfaceFormat.Color;

            return TextureManipulation.Texture2DFromBitmap(bitmap, device, parameters);
        }

        /// <summary>
        /// Generates a Microsoft.Xna.Framework.Graphics.Texture2D from a System.Drawing.Bitmap.
        /// </summary>
        /// <param name="bitmap">The Bitmap whose color data is used to generate the Texture2D.</param>
        /// <param name="device">The GraphicsDevice used to display the texture.</param>
        /// <param name="parameters">The parameters used to create the texture.</param>
        /// <returns>See summary.</returns>
        public static Texture2D Texture2DFromBitmap(this Image bitmap, GraphicsDevice device, TextureCreationParameters parameters)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);
                return Texture2D.FromFile(device, stream, parameters);
            }
        }

        /// <summary>
        /// Gets the maximum number of mip levels for an image.
        /// </summary>
        /// <param name="image">The image to get the maximum mip levels of.</param>
        /// <returns>See summary.</returns>
        public static int GetMaximumMipLevels(this Image image)
        {
            return TextureManipulation.GetMaximumMipLevels(image.Width, image.Height);
        }

        /// <summary>
        /// Gets the maximum number of mip levels for an texture.
        /// </summary>
        /// <param name="texture">The texture to get the maximum mip levels of.</param>
        /// <returns>See summary.</returns>
        public static int GetMaximumMipLevels(this Texture2D texture)
        {
            return TextureManipulation.GetMaximumMipLevels(texture.Width, texture.Height);
        }

        /// <summary>
        /// Gets the maximum number of mip levels for an image or texture.
        /// </summary>
        /// <param name="width">The width of the image or texture.</param>
        /// <param name="height">The height of the image or texture.</param>
        /// <returns>See summary.</returns>
        public static int GetMaximumMipLevels(int width, int height)
        {
            int larger = Math.Max(width, height);
            int mipLevels = 1;

            while (larger > 1)
            {
                larger /= 2;
                mipLevels++;
            }

            return mipLevels;
        }

        /// <summary>
        /// Converts a <see cref="Microsoft.Xna.Framework.Graphics.Color"/> to a <see cref="System.Drawing.Color"/>.
        /// </summary>
        /// <param name="color">The <see cref="Microsoft.Xna.Framework.Graphics.Color"/> to convert.</param>
        /// <returns>See summary.</returns>
        public static Sys.Color XnaColorToSystemColor(this Xna.Color color)
        {
            return Sys.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Converts a <see cref="System.Drawing.Color"/> to a <see cref="Microsoft.Xna.Framework.Graphics.Color"/>.
        /// </summary>
        /// <param name="color">The <see cref="System.Drawing.Color"/> to convert.</param>
        /// <returns>See summary.</returns>
        public static Xna.Color SystemColorToXnaColor(this Sys.Color color)
        {
            return new Xna.Color(color.R, color.G, color.B, color.A);
        }
    }
}
