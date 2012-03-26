namespace MMO3D.Engine
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Creates a SpriteFont dynamically, at runtime.
    /// </summary>
    public sealed class SpriteFontCreator : IDisposable
    {
        /// <summary>
        /// The content builder used to build fonts.
        /// </summary>
        private ContentBuilder builder = new ContentBuilder();

        /// <summary>
        /// The content manager used to load built fonts.
        /// </summary>
        private ContentManager content;

        /// <summary>
        /// Initializes a new instance of the SpriteFontCreator class.
        /// </summary>
        /// <param name="services">
        /// The service provider used to create the internal content manager.
        /// This can be retrieved from an existing ContentManager's Services property.
        /// </param>
        /// <exception cref="System.ArgumentNullException">Service provider is null.</exception>
        public SpriteFontCreator(IServiceProvider services)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services", "Service provider cannot be null.");
            }

            this.content = new ContentManager(services, this.builder.OutputDirectory);
        }

        /// <summary>
        /// Creates a SpriteFont from a Font.
        /// </summary>
        /// <param name="font">A font object containing the details of the font being created.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="System.InvalidOperationException">A build error occurred.</exception>
        public SpriteFont CreateFont(Font font)
        {
            return this.CreateFont(new FontDefinition(font));
        }

        /// <summary>
        /// Creates a SpriteFont from a font definition.
        /// </summary>
        /// <param name="fontDefinition">The details of the font being created.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="System.InvalidOperationException">A build error occurred.</exception>
        public SpriteFont CreateFont(FontDefinition fontDefinition)
        {
            const string BuiltName = "Font";

            string fileName = Path.Combine(this.builder.OutputDirectory, Path.GetRandomFileName() + ".spritefont");
            fontDefinition.ToXmlFile(fileName);

            this.builder.Clear();
            this.builder.Add(fileName, BuiltName, null, "FontDescriptionProcessor");

            string buildError = this.builder.Build();

            if (!string.IsNullOrEmpty(buildError))
            {
                throw new InvalidOperationException(buildError);
            }
            else
            {
                return this.content.Load<SpriteFont>(string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", this.builder.OutputDirectory, BuiltName));
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
                this.builder.Dispose();
                this.content.Dispose();
            }
        }
    }
}
