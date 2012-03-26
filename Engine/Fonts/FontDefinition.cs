namespace MMO3D.Engine
{
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Web;

    /// <summary>
    /// Contains the definition for a font.
    /// </summary>
    public sealed class FontDefinition
    {
        /// <summary>
        /// The name of the font to import.
        /// </summary>
        private string fontName;

        /// <summary>
        /// The size of the font, in points.
        /// </summary>
        private float size;

        /// <summary>
        /// The style of the font.
        /// </summary>
        private FontStyle style;

        /// <summary>
        /// The characters to be included in the compiled font.
        /// </summary>
        private Collection<CharacterRegion> characterRegions = new Collection<CharacterRegion>();

        /// <summary>
        /// Initializes a new instance of the FontDefinition class with the default settings.
        /// The font is Microsoft Sans Serif, Regular, 10pt size with kerning enabled and
        /// characters 32 (SPACE) to 126 (TILDE).
        /// </summary>
        public FontDefinition()
        {
            this.fontName = "Microsoft Sans Serif";
            this.Size = 10;
            this.Style = FontStyle.Regular;
            this.UseKerning = true;
            this.CharacterRegions.Add(new CharacterRegion(' ', '~'));
        }

        /// <summary>
        /// Initializes a new instance of the FontDefinition class.
        /// </summary>
        /// <param name="font">The font object whose properties to use to create the SpriteFont.</param>
        /// <exception cref="System.ArgumentNullException">Font is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// The specified font is not installed on the system. -or-
        /// The font size is not a valid number greater than zero, or is equal to infinity. -or-
        /// Font style is not Regular, Bold, Italic, or Bold + Italic.
        /// </exception>
        public FontDefinition(Font font)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font", "Font cannot be null.");
            }

            this.FontName = font.Name;
            this.Size = font.SizeInPoints;
            this.Style = font.Style;
        }

        /// <summary>
        /// Initializes a new instance of the FontDefinition class.
        /// </summary>
        /// <param name="fontName">The name of the font to import.</param>
        /// <param name="size">The size of the font, in points.</param>
        /// <param name="style">The style of the font.</param>
        /// <exception cref="System.ArgumentException">
        /// The specified font is not installed on the system. -or-
        /// The font size is not a valid number greater than zero, or is equal to infinity. -or-
        /// Font style is not Regular, Bold, Italic, or Bold + Italic.
        /// </exception>
        public FontDefinition(string fontName, float size, FontStyle style)
        {
            this.FontName = fontName;
            this.Size = size;
            this.Style = style;
        }

        /// <summary>
        /// Gets or sets the name of the font to import. The default is Microsoft Sans Serif.
        /// </summary>
        /// <value>See summary.</value>
        /// <exception cref="System.ArgumentNullException">The font name is null.</exception>
        /// <exception cref="System.ArgumentException">The specified font is not installed on the system.</exception>
        public string FontName
        {
            get
            {
                return this.fontName;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "Font name cannot be null.");
                }

                if (value != new Font(value, this.size).Name)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The specified font, {0}, is not installed on the system.", value), "value");
                }

                this.fontName = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the font, in points. The default is 10.
        /// </summary>
        /// <value>See summary.</value>
        /// <exception cref="System.ArgumentException">The font size must be a valid number greater than zero, and not equal to infinity.</exception>
        public float Size
        {
            get
            {
                return this.size;
            }

            set
            {
                if (float.IsNaN(value) || float.IsInfinity(value) || value <= 0)
                {
                    throw new ArgumentException("The font size must be a valid number greater than zero, and not equal to infinity.");
                }

                this.size = value;
            }
        }

        /// <summary>
        /// Gets or sets the amount of spacing in between characters. The default is 0.
        /// </summary>
        /// <value>See summary.</value>
        public float Spacing
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use kerning information when placing characters. The default is true.
        /// </summary>
        /// <value>See summary.</value>
        public bool UseKerning
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the style of the font; valid entries are Regular, Bold, Italic, and Bold + Italic. The default is Regular.
        /// </summary>
        /// <value>See summary.</value>
        /// <exception cref="System.ArgumentException">Font style is not Regular, Bold, Italic, or Bold + Italic.</exception>
        public FontStyle Style
        {
            get
            {
                return this.style;
            }

            set
            {
                switch (value)
                {
                    case FontStyle.Regular:
                    case FontStyle.Bold:
                    case FontStyle.Italic:
                    case FontStyle.Bold | FontStyle.Italic:
                        this.style = value;
                        break;
                    default:
                        throw new ArgumentException("Font style is not Regular, Bold, Italic, or Bold + Italic.");
                }
            }
        }

        /// <summary>
        /// Gets the CharacterRegions that control what letters are available in the font. Every
        /// character from Start to End will be built and made available for drawing. The default
        /// range is from 32, (ASCII space), to 126, ('~'), covering the basic Latin character set.
        /// The characters are ordered according to the Unicode standard.
        /// </summary>
        /// <value>See summary.</value>
        public Collection<CharacterRegion> CharacterRegions
        {
            get { return this.characterRegions; }
        }

        /// <summary>
        /// Gets the data of the FontDefinition as a string of XML.
        /// </summary>
        /// <returns>See summary.</returns>
        public string ToXmlString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.CharacterRegions.Count; i++)
            {
                builder.Append(this.CharacterRegions[i].ToXmlString());
            }

            return string.Format(
                CultureInfo.InvariantCulture,
                Resources.SpriteFontXmlFile,
                HttpUtility.HtmlEncode(this.FontName),
                this.Size,
                this.Spacing,
                this.UseKerning.ToString().ToLowerInvariant(),
                this.GetFontStyleString(),
                builder.ToString());
        }

        /// <summary>
        /// Writes the data of the FontDefinition to an XML string.
        /// </summary>
        /// <param name="fileName">The filename of the file to write to.</param>
        public void ToXmlFile(string fileName)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            File.WriteAllText(fileName, this.ToXmlString());
        }

        /// <summary>
        /// Gets the font style as a string appropriate for use in the XML file.
        /// </summary>
        /// <returns>See summary.</returns>
        private string GetFontStyleString()
        {
            switch (this.Style)
            {
                case FontStyle.Bold | FontStyle.Italic:
                    return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", FontStyle.Bold, FontStyle.Italic);
                default:
                    return this.Style.ToString();
            }
        }
    }
}
