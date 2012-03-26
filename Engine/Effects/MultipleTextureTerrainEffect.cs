namespace MMO3D.Engine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines an effect that renders multi-textured terrain.
    /// </summary>
    public sealed class MultipleTextureTerrainEffect : CustomEffect
    {
        /// <summary>
        /// The 'CursorPosition' effect parameter.
        /// </summary>
        private readonly EffectParameter cursorPosition;

        /// <summary>
        /// The 'CursorSize' effect parameter.
        /// </summary>
        private readonly EffectParameter cursorSize;

        /// <summary>
        /// The 'ShowCursor' effect parameter.
        /// </summary>
        private readonly EffectParameter showCursor;

        /// <summary>
        /// The 'PatchIdX' effect parameter.
        /// </summary>
        private readonly EffectParameter patchIdX;

        /// <summary>
        /// The 'PatchIdY' effect parameter.
        /// </summary>
        private readonly EffectParameter patchIdY;

        /// <summary>
        /// The 'PatchIdZ' effect parameter.
        /// </summary>
        private readonly EffectParameter patchIdZ;

        /// <summary>
        /// The 'PatchSize' effect parameter.
        /// </summary>
        private readonly EffectParameter patchSize;

        /// <summary>
        /// The 'Texture' effect parameter.
        /// </summary>
        private readonly EffectParameter texture;

        /// <summary>
        /// Initializes a new instance of the MultipleTextureTerrainEffect class.
        /// </summary>
        /// <param name="device">The graphics device that will create the effect.</param>
        public MultipleTextureTerrainEffect(GraphicsDevice device)
            : base(device, CustomEffect.GetEffect(device, Resources.MultiTexturedTerrain), "MultiTexturedTerrain")
        {
            this.Transparency = 1;

            this.cursorPosition = this.Parameters["CursorPosition"];
            this.cursorSize = this.Parameters["CursorSize"];
            this.showCursor = this.Parameters["ShowCursor"];

            this.patchIdX = this.Parameters["PatchIdX"];
            this.patchIdY = this.Parameters["PatchIdY"];
            this.patchIdZ = this.Parameters["PatchIdZ"];
            this.patchSize = this.Parameters["PatchSize"];

            this.texture = this.Parameters["Texture"];
        }

        /// <summary>
        /// Gets or sets the terrain cursor position in world space.
        /// </summary>
        /// <value>See summary.</value>
        public Vector2 CursorPosition
        {
            get { return this.cursorPosition.GetValueVector2(); }
            set { this.cursorPosition.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the terrain cursor size.
        /// </summary>
        /// <value>See summary.</value>
        public int CursorSize
        {
            get { return this.cursorSize.GetValueInt32(); }
            set { this.cursorSize.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the terrain cursor.
        /// </summary>
        /// <value>See summary.</value>
        public bool ShowCursor
        {
            get { return this.showCursor.GetValueBoolean(); }
            set { this.showCursor.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the patch ID of the terrain patch being rendered.
        /// </summary>
        /// <value>See summary.</value>
        public Point3D PatchId
        {
            get
            {
                return new Point3D(this.patchIdX.GetValueInt32(), this.patchIdY.GetValueInt32(), this.patchIdZ.GetValueInt32());
            }

            set
            {
                this.patchIdX.SetValue(value.X);
                this.patchIdY.SetValue(value.Y);
                this.patchIdZ.SetValue(value.Z);
            }
        }

        /// <summary>
        /// Gets or sets the size of terrain patches. This value should be constant.
        /// </summary>
        /// <value>See summary.</value>
        public int PatchSize
        {
            get { return this.patchSize.GetValueInt32(); }
            set { this.patchSize.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the current terrain texture.
        /// </summary>
        /// <value>See summary.</value>
        public Texture2D Texture
        {
            get { return this.texture.GetValueTexture2D(); }
            set { this.texture.SetValue(value); }
        }
    }
}
