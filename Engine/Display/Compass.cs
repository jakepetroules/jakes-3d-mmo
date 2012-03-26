namespace MMO3D.Engine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MMO3D.CommonCode;

    /// <summary>
    /// Represents a compass that can be drawn on the screen.
    /// </summary>
    public class Compass
    {
        /// <summary>
        /// The rotation of the compass, in radians.
        /// </summary>
        /// <remarks>
        /// Note that this is WORLD rotation, not texture rotation.
        /// This means that 90&#176; or &#189;&#960; radians
        /// is subtracted from this value when used
        /// in the call to SpriteBatch.Draw.
        /// </remarks>
        private float rotation;

        /// <summary>
        /// Initializes a new instance of the Compass class with the default texture and the engine's graphics device.
        /// </summary>
        public Compass()
        {
            TextureCreationParameters parameters = new TextureCreationParameters();
            parameters.Filter = FilterOptions.Linear;
            parameters.Format = SurfaceFormat.Color;
            parameters.MipFilter = FilterOptions.Linear;

            this.Texture = TextureManipulation.Texture2DFromBitmap(Resources.Compass, EngineManager.Engine.GraphicsDevice, parameters);
            this.Visible = true;
        }

        /// <summary>
        /// Initializes a new instance of the Compass class.
        /// </summary>
        /// <param name="texture">The texture used to display the compass.</param>
        public Compass(Texture2D texture)
        {
            this.Texture = texture;
            this.Visible = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the compass is visible.
        /// If this value is false, the compass will not be drawn.
        /// The default is true.
        /// </summary>
        /// <value>See summary.</value>
        public bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the screen position of the upper-left corner of the compass.
        /// </summary>
        /// <value>See summary.</value>
        public Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the world rotation of the compass in degrees.
        /// </summary>
        /// <value>See summary.</value>
        /// <remarks>
        /// Note that this is WORLD rotation, not texture rotation.
        /// This means that 90&#176; is subtracted from this value
        /// when used in the call to SpriteBatch.Draw.
        /// </remarks>
        public float RotationDegrees
        {
            get { return MathHelper.ToDegrees(this.rotation); }
            set { this.rotation = MathHelper.ToRadians(value); }
        }

        /// <summary>
        /// Gets or sets the world rotation of the compass in radians.
        /// </summary>
        /// <value>See summary.</value>
        /// <remarks>
        /// Note that this is WORLD rotation, not texture rotation.
        /// This means that &#189;&#960; radians is subtracted from
        /// this value when used in the call to SpriteBatch.Draw.
        /// </remarks>
        public float RotationRadians
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        /// <summary>
        /// Gets the texture used to display the compass.
        /// </summary>
        /// <value>See summary.</value>
        public Texture2D Texture
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the width of the compass.
        /// </summary>
        /// <value>See summary.</value>
        public int Width
        {
            get { return this.Texture.Width; }
        }

        /// <summary>
        /// Gets the height of the compass.
        /// </summary>
        /// <value>See summary.</value>
        public int Height
        {
            get { return this.Texture.Height; }
        }

        /// <summary>
        /// Gets the origin of the compass. This is always its center.
        /// </summary>
        /// <value>See summary.</value>
        private Vector2 Origin
        {
            get { return new Vector2(this.Width / 2, this.Height / 2); }
        }

        /// <summary>
        /// Draws the compass.
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> used to draw the compass.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Visible)
            {
                spriteBatch.Draw(this.Texture, this.Position + this.Origin, null, Color.White, this.RotationRadians - MathHelper.ToRadians(90), this.Origin, 1, SpriteEffects.None, 0);
            }
        }
    }
}
