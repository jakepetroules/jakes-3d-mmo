namespace Petroules.Synteza
{
    using System;
    using System.Drawing.Imaging;
    using Petroules.Synteza.Properties;

    /// <summary>
    /// Provides extensions to the <see cref="ImageFormat"/> class.
    /// </summary>
    public static class ImageFormatExtensions
    {
        /// <summary>
        /// Gets the name of the specified image file format.
        /// </summary>
        /// <param name="format">The image format to get the name of.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="format"/> is <see cref="ImageFormat.MemoryBmp"/> or is not recognized.</exception>
        public static string GetFormatName(this ImageFormat format)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            if (format.Guid == ImageFormat.Bmp.Guid)
            {
                return Resources.ImageFormatNameWindowsBitmap;
            }
            else if (format.Guid == ImageFormat.Emf.Guid)
            {
                return Resources.ImageFormatNameEnhancedMetafile;
            }
            else if (format.Guid == ImageFormat.Exif.Guid)
            {
                return Resources.ImageFormatNameExchangeableImageFileFormat;
            }
            else if (format.Guid == ImageFormat.Gif.Guid)
            {
                return Resources.ImageFormatNameGraphicsInterchangeFormat;
            }
            else if (format.Guid == ImageFormat.Icon.Guid)
            {
                return Resources.ImageFormatNameWindowsIcon;
            }
            else if (format.Guid == ImageFormat.Jpeg.Guid)
            {
                return Resources.ImageFormatNameJointPhotographicExpertsGroup;
            }
            else if (format.Guid == ImageFormat.Png.Guid)
            {
                return Resources.ImageFormatNamePortableNetworkGraphics;
            }
            else if (format.Guid == ImageFormat.Tiff.Guid)
            {
                return Resources.ImageFormatNameTaggedImageFileFormat;
            }
            else if (format.Guid == ImageFormat.Wmf.Guid)
            {
                return Resources.ImageFormatNameWindowsMetafile;
            }
            else
            {
                throw new ArgumentException(Resources.ImageFormatNameUnrecognized);
            }
        }

        /// <summary>
        /// Gets the file extension of the specified <see cref="ImageFormat"/>, without the dot.
        /// </summary>
        /// <param name="format">The image format to get the file extension of.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="format"/> is <see cref="ImageFormat.MemoryBmp"/> or is not recognized.</exception>
        public static string GetExtension(this ImageFormat format)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            if (format.Guid == ImageFormat.Bmp.Guid)
            {
                return "bmp";
            }
            else if (format.Guid == ImageFormat.Emf.Guid)
            {
                return "emf";
            }
            else if (format.Guid == ImageFormat.Exif.Guid)
            {
                return "exif";
            }
            else if (format.Guid == ImageFormat.Gif.Guid)
            {
                return "gif";
            }
            else if (format.Guid == ImageFormat.Icon.Guid)
            {
                return "ico";
            }
            else if (format.Guid == ImageFormat.Jpeg.Guid)
            {
                return "jpg";
            }
            else if (format.Guid == ImageFormat.Png.Guid)
            {
                return "png";
            }
            else if (format.Guid == ImageFormat.Tiff.Guid)
            {
                return "tif";
            }
            else if (format.Guid == ImageFormat.Wmf.Guid)
            {
                return "wmf";
            }
            else
            {
                throw new ArgumentException(Resources.ImageFormatNameUnrecognized);
            }
        }
    }
}
