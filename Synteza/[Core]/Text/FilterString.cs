namespace Petroules.Synteza.Text
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Represents a file filter string.
    /// </summary>
    public sealed class FilterString
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterString"/>
        /// class with the specified title and file types.
        /// </summary>
        /// <param name="title">The title of the filter string to be shown to the user.</param>
        /// <param name="types">The file types constituting the filter string, e.g. "avi".</param>
        /// <exception cref="ArgumentNullException"><paramref name="title"/> is <c>null</c> or white space -or- <paramref name="types"/> is <c>null</c>.</exception>
        public FilterString(string title, params string[] types)
        {
            if (string.IsNullOrEmpty(title) || types == null)
            {
                throw new ArgumentNullException(string.IsNullOrEmpty(title) ? "title" : "types");
            }

            this.Title = title;
            this.ShowTypes = true;
            this.FileTypes = new StringCollection();

            for (int i = 0; i < types.Length; i++)
            {
                this.FileTypes.Add(types[i]);
            }
        }

        /// <summary>
        /// Gets a filter string for all files.
        /// </summary>
        public static FilterString AllFiles
        {
            get { return new FilterString("All Files", "*"); }
        }

        /// <summary>
        /// Gets a filter string for AVI files.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Avi", Justification = "Refers to AVI files; not a mis-spelling or Hungarian.")]
        public static FilterString AviFiles
        {
            get { return new FilterString("AVI Files", "avi"); }
        }

        /// <summary>
        /// Gets a filter string for Bitmap images.
        /// </summary>
        public static FilterString BmpImages
        {
            get { return new FilterString("Bitmap Images", "bmp"); }
        }

        /// <summary>
        /// Gets a filter string for DIVX media files.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Divx", Justification = "Refers to DIVX files; not a mis-spelling or Hungarian.")]
        public static FilterString DivxMediaFiles
        {
            get { return new FilterString("DivX media files", "divx"); }
        }

        /// <summary>
        /// Gets a filter string for Flash video files.
        /// </summary>
        public static FilterString FlashVideoFiles
        {
            get { return new FilterString("Flash video files", "flv"); }
        }

        /// <summary>
        /// Gets a filter string for GIF images.
        /// </summary>
        public static FilterString GifImages
        {
            get { return new FilterString("GIF Images", "gif"); }
        }

        /// <summary>
        /// Gets a filter string for HTML files.
        /// </summary>
        public static FilterString HtmlFiles
        {
            get { return new FilterString("HTML/XHTML Files", "html", "htm"); }
        }

        /// <summary>
        /// Gets a filter string for JPEG images.
        /// </summary>
        public static FilterString JpegImages
        {
            get { return new FilterString("JPEG Images", "jpg", "jpeg"); }
        }

        /// <summary>
        /// Gets a filter string for MP4 video files.
        /// </summary>
        public static FilterString Mp4VideoFiles
        {
            get { return new FilterString("MP4 video files", "mp4"); }
        }

        /// <summary>
        /// Gets a filter string for PNG images.
        /// </summary>
        public static FilterString PngImages
        {
            get { return new FilterString("PNG Images", "png"); }
        }

        /// <summary>
        /// Gets a filter string for Windows Media video files.
        /// </summary>
        public static FilterString WindowsMediaVideoFiles
        {
            get { return new FilterString("Windows Media video files", "wmv"); }
        }

        /// <summary>
        /// Gets or sets the title of the filter string.
        /// </summary>
        /// <value>See summary.</value>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a collection of the file types belonging to the filter string.
        /// </summary>
        /// <value>See summary.</value>
        public StringCollection FileTypes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the
        /// user what file types are in the filter string.
        /// </summary>
        /// <value>See summary.</value>
        public bool ShowTypes
        {
            get;
            set;
        }

        /// <summary>
        /// Combines the two filter strings into a <see cref="FilterStringCollection"/>.
        /// </summary>
        /// <param name="left">The left-hand operand.</param>
        /// <param name="right">The right-hand operand.</param>
        /// <returns>A <see cref="FilterStringCollection"/> representing the combined filter strings.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="left"/> is <c>null</c> -or- <paramref name="right"/> is <c>null</c>.</exception>
        public static FilterStringCollection operator +(FilterString left, FilterString right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException(left == null ? "left" : "right");
            }

            return new FilterStringCollection(left, right);
        }

        /// <summary>
        /// Converts the specified <see cref="FilterString"/> to a <see cref="System.String"/>.
        /// </summary>
        /// <param name="filterString">The <see cref="FilterString"/> to convert.</param>
        /// <returns>See summary.</returns>
        public static implicit operator string(FilterString filterString)
        {
            return filterString.ToString();
        }

        /// <summary>
        /// Converts the instance of <see cref="FilterString"/> to an actual string.
        /// </summary>
        /// <returns>See summary.</returns>
        public override string ToString()
        {
            StringBuilder fileTypes = new StringBuilder();
            for (int i = 0; i < this.FileTypes.Count; i++)
            {
                if (i > 0)
                {
                    fileTypes.Append(";");
                }

                fileTypes.AppendFormat(CultureInfo.InvariantCulture, "*.{0}", this.FileTypes[i]);
            }

            string showTypes = string.Empty;
            if (this.ShowTypes)
            {
                showTypes = " (" + fileTypes.ToString() + ")";
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}{1}|{2}", this.Title, showTypes, fileTypes.ToString());
        }
    }
}
