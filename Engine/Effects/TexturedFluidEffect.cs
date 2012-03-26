namespace MMO3D.Engine
{
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines an effect that renders textured fluids.
    /// </summary>
    public sealed class TexturedFluidEffect : CustomEffect
    {
        /// <summary>
        /// The 'Texture' parameter.
        /// </summary>
        private readonly EffectParameter texture;

        /// <summary>
        /// Initializes a new instance of the TexturedFluidEffect class.
        /// </summary>
        /// <param name="device">The graphics device that will create the effect.</param>
        public TexturedFluidEffect(GraphicsDevice device)
            : base(device, CustomEffect.GetEffect(device, Resources.TexturedFluid), "TexturedFluid")
        {
            this.texture = this.Parameters["Texture"];
        }

        /// <summary>
        /// Gets or sets the texture of the fluid.
        /// </summary>
        /// <value>See summary.</value>
        public Texture2D Texture
        {
            get { return this.texture.GetValueTexture2D(); }
            set { this.texture.SetValue(value); }
        }
    }
}
