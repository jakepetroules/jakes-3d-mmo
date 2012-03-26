//-----------------------------------------------------------------------------
// ModelViewerControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
namespace MMO3D.WorldEditor
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using WinFormsGraphicsDevice;

    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, and displays
    /// a spinning 3D model. The main form class is responsible for loading
    /// the model: this control just displays it.
    /// </summary>
    public class ModelViewerControl : GraphicsDeviceControl
    {
        /// <summary>
        /// The current model displayed by the control.
        /// </summary>
        private Model model;

        /// <summary>
        /// Cached model bone transformations.
        /// </summary>
        private Matrix[] boneTransforms;

        /// <summary>
        /// Cached model center.
        /// </summary>
        private Vector3 modelCenter;

        /// <summary>
        /// Cached model radius.
        /// </summary>
        private float modelRadius;

        /// <summary>
        /// Timer to control the rotation speed.
        /// </summary>
        private Stopwatch timer;

        /// <summary>
        /// Initializes a new instance of the ModelViewerControl class.
        /// </summary>
        public ModelViewerControl()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the current model displayed by the control.
        /// </summary>
        /// <value>See summary.</value>
        public Model Model
        {
            get
            {
                return this.model;
            }

            set
            {
                this.model = value;

                if (this.model != null)
                {
                    this.MeasureModel();
                }
            }
        }

        /// <summary>
        /// Initializes the timer and starts the draw loop.
        /// </summary>
        protected override void Initialize()
        {
            // Start the animation timer.
            this.timer = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { this.Invalidate(); };
        }

        /// <summary>
        /// Draws the model.
        /// </summary>
        protected override void Draw()
        {
            // Clear to the default control background color.
            this.GraphicsDevice.Clear(new Color(this.BackColor.R, this.BackColor.G, this.BackColor.B));

            if (this.Model != null)
            {
                // Compute camera matrices.
                float rotation = (float)this.timer.Elapsed.TotalSeconds;

                Vector3 eyePosition = this.modelCenter;

                eyePosition.Z += this.modelRadius;
                eyePosition.Y += this.modelRadius * 2;

                float aspectRatio = this.GraphicsDevice.Viewport.AspectRatio;

                float nearClip = this.modelRadius / 100;
                float farClip = this.modelRadius * 100;

                Matrix world = Matrix.CreateRotationZ(rotation);
                Matrix view = Matrix.CreateLookAt(eyePosition, this.modelCenter, Vector3.UnitZ);
                Matrix projection = Matrix.CreatePerspectiveFieldOfView(1, aspectRatio, nearClip, farClip);

                // Draw the model...
                foreach (ModelMesh mesh in this.Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = this.boneTransforms[mesh.ParentBone.Index] * world;
                        effect.View = view;
                        effect.Projection = projection;

                        effect.EnableDefaultLighting();
                        effect.PreferPerPixelLighting = true;
                        effect.SpecularPower = 16;
                    }

                    mesh.Draw();
                }
            }
        }

        /// <summary>
        /// Whenever a new model is selected, we examine it to see how big
        /// it is and where it is centered. This lets us automatically zoom
        /// the display, so we can correctly handle models of any scale.
        /// </summary>
        private void MeasureModel()
        {
            // Look up the absolute bone transforms for this model.
            this.boneTransforms = new Matrix[this.Model.Bones.Count];

            this.Model.CopyAbsoluteBoneTransformsTo(this.boneTransforms);

            // Compute an (approximate) model center position by
            // averaging the center of each mesh bounding sphere.
            this.modelCenter = Vector3.Zero;

            foreach (ModelMesh mesh in this.Model.Meshes)
            {
                BoundingSphere meshBounds = mesh.BoundingSphere;
                Matrix transform = this.boneTransforms[mesh.ParentBone.Index];
                Vector3 meshCenter = Vector3.Transform(meshBounds.Center, transform);

                this.modelCenter += meshCenter;
            }

            this.modelCenter /= this.Model.Meshes.Count;

            // Now we know the center point, we can compute the model radius
            // by examining the radius of each mesh bounding sphere.
            this.modelRadius = 0;

            foreach (ModelMesh mesh in this.Model.Meshes)
            {
                BoundingSphere meshBounds = mesh.BoundingSphere;
                Matrix transform = this.boneTransforms[mesh.ParentBone.Index];
                Vector3 meshCenter = Vector3.Transform(meshBounds.Center, transform);

                float transformScale = transform.Forward.Length();

                float meshRadius = (meshCenter - this.modelCenter).Length() +
                                   (meshBounds.Radius * transformScale);

                this.modelRadius = Math.Max(this.modelRadius, meshRadius);
            }
        }
    }
}