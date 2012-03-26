namespace MMO3D.Engine
{
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines a sky which is managed inside the terrain class.
    /// </summary>
    public sealed class Sky
    {
        /// <summary>
        /// Initializes a new instance of the Sky class.
        /// </summary>
        public Sky()
        {
            this.SkyType = SkyType.Cloudy;
        }

        /// <summary>
        /// Gets or sets the type of sky to display.
        /// </summary>
        /// <value>See summary.</value>
        public SkyType SkyType
        {
            get;
            set;
        }

        /// <summary>
        /// Draws the sky.
        /// </summary>
        /// <param name="camera">The camera being used to draw the 3D world.</param>
        public void Draw(Camera camera)
        {
            Texture2D skyTexture = this.SkyType.GetTexture();
            if (skyTexture != null && false)
            {
                // TODO: Draw skydome
            }
        }
    }
}
