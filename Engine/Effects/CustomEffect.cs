namespace MMO3D.Engine
{
    using System;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Provides base functionality for custom effects.
    /// </summary>
    public abstract class CustomEffect : Effect
    {
        /// <summary>
        /// The 'Projection' effect parameter.
        /// </summary>
        private readonly EffectParameter projection;

        /// <summary>
        /// The 'View' effect parameter.
        /// </summary>
        private readonly EffectParameter view;

        /// <summary>
        /// The 'World' effect parameter.
        /// </summary>
        private readonly EffectParameter world;

        /// <summary>
        /// The 'Ambient' effect parameter.
        /// </summary>
        private readonly EffectParameter ambient;

        /// <summary>
        /// The 'LightDirection' effect parameter.
        /// </summary>
        private readonly EffectParameter lightDirection;

        /// <summary>
        /// The 'LightingEnabled' effect parameter.
        /// </summary>
        private readonly EffectParameter lightingEnabled;

        /// <summary>
        /// The 'Transparency' effect parameter.
        /// </summary>
        private readonly EffectParameter transparency;

        /// <summary>
        /// Initializes a new instance of the CustomEffect class.
        /// </summary>
        /// <param name="device">The graphics device that will create the effect.</param>
        /// <param name="cloneSource">The effect to clone.</param>
        /// <param name="technique">The technique run by this effect.</param>
        protected CustomEffect(GraphicsDevice device, Effect cloneSource, string technique)
            : base(device, cloneSource)
        {
            this.CurrentTechnique = this.Techniques[technique];

            this.projection = this.Parameters["Projection"];
            this.view = this.Parameters["View"];
            this.world = this.Parameters["World"];

            this.ambient = this.Parameters["Ambient"];
            this.lightDirection = this.Parameters["LightDirection"];
            this.lightingEnabled = this.Parameters["EnableLighting"];

            this.transparency = this.Parameters["Transparency"];
        }

        /// <summary>
        /// Gets or sets the projection matrix.
        /// </summary>
        /// <value>The projection matrix.</value>
        public Matrix Projection
        {
            get { return this.projection.GetValueMatrix(); }
            set { this.projection.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the view matrix.
        /// </summary>
        /// <value>The view matrix.</value>
        public Matrix View
        {
            get { return this.view.GetValueMatrix(); }
            set { this.view.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the world matrix.
        /// </summary>
        /// <value>The world matrix.</value>
        public Matrix World
        {
            get { return this.world.GetValueMatrix(); }
            set { this.world.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the amount of ambient lighting.
        /// </summary>
        /// <value>See summary.</value>
        public float Ambient
        {
            get { return this.ambient.GetValueSingle(); }
            set { this.ambient.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the light direction.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 LightDirection
        {
            get { return this.lightDirection.GetValueVector3(); }
            set { this.lightDirection.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable lighting for this effect.
        /// </summary>
        /// <value>True to enable lighting; false otherwise.</value>
        public bool LightingEnabled
        {
            get { return this.lightingEnabled.GetValueBoolean(); }
            set { this.lightingEnabled.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating the transparency of the object.
        /// 0 specifies fully transparent and 1 specifies fully opaque.
        /// </summary>
        /// <value>See summary.</value>
        public float Transparency
        {
            get { return this.transparency.GetValueSingle(); }
            set { this.transparency.SetValue(value); }
        }

        /// <summary>
        /// Generates a <see cref="Microsoft.Xna.Framework.Graphics.Effect"/> from HLSL source code. Returns null on error.
        /// </summary>
        /// <param name="device">The graphics device that will create the effect.</param>
        /// <param name="shaderSource">The HLSL source code used to create the effect.</param>
        /// <returns>See summary.</returns>
        public static Effect GetEffect(GraphicsDevice device, byte[] shaderSource)
        {
            return CustomEffect.GetEffect(device, Encoding.UTF8.GetString(shaderSource));
        }

        /// <summary>
        /// Generates a <see cref="Microsoft.Xna.Framework.Graphics.Effect"/> from HLSL source code. Returns null on error.
        /// </summary>
        /// <param name="device">The graphics device that will create the effect.</param>
        /// <param name="shaderSource">The HLSL source code used to create the effect.</param>
        /// <returns>See summary.</returns>
        public static Effect GetEffect(GraphicsDevice device, string shaderSource)
        {
            CompiledEffect compiledEffect = Effect.CompileEffectFromSource(shaderSource, null, null, CompilerOptions.None, TargetPlatform.Windows);
            if (compiledEffect.Success)
            {
                try
                {
                    return new Effect(device, compiledEffect.GetEffectCode(), CompilerOptions.None, null);
                }
                catch (ArgumentNullException)
                {
                }
            }

            return null;
        }
    }
}
