namespace MMO3D.Engine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MMO3D.CommonCode;

    /// <summary>
    /// Base class for representing all game objects that have a virtual physical representation.
    /// </summary>
    public abstract class GameObjectBase
    {
        /// <summary>
        /// The 3D model representing the game object.
        /// </summary>
        private ExtendedModel model;

        /// <summary>
        /// The model's absolute bone transformations.
        /// </summary>
        private Matrix[] transforms;

        /// <summary>
        /// Initializes a new instance of the GameObjectBase class.
        /// This is for use by the Player class. Do not extend otherwise.
        /// </summary>
        protected GameObjectBase()
        {
            this.Scaling = Vector3.One;
            this.Transparency = 255;
            this.Collides = true;
        }

        /// <summary>
        /// Initializes a new instance of the GameObjectBase class.
        /// </summary>
        /// <param name="model">The model to use for this game object.</param>
        protected GameObjectBase(ExtendedModel model)
            : this()
        {
            this.Model = model;
        }

        /// <summary>
        /// Gets or sets the 3D model representing the game object.
        /// </summary>
        /// <value>See summary.</value>
        public ExtendedModel Model
        {
            get
            {
                return this.model;
            }

            set
            {
                this.model = value;

                if (value != null)
                {
                    // Create the bone transformations
                    this.transforms = new Matrix[value.Bones.Count];
                    value.CopyAbsoluteBoneTransformsTo(this.transforms);
                }
            }
        }

        /// <summary>
        /// Gets or sets the position of the object in world space.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rotation of the object, in radians.
        /// </summary>
        /// <value>See summary.</value>
        /// <remarks>Roll, pitch and yaw constitute rotation around X, Y, and Z, respectively.</remarks>
        public Vector3 RotationRadians
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rotation of the object, in degrees.
        /// </summary>
        /// <value>See summary.</value>
        /// <remarks>Roll, pitch and yaw constitute rotation around X, Y, and Z, respectively.</remarks>
        public Vector3 RotationDegrees
        {
            get
            {
                return MathExtensions.Modulate(new Vector3(MathHelper.ToDegrees(this.RotationRadians.X), MathHelper.ToDegrees(this.RotationRadians.Y), MathHelper.ToDegrees(this.RotationRadians.Z)), 360);
            }

            set
            {
                value = MathExtensions.Modulate(value, 360);
                this.RotationRadians = new Vector3(MathHelper.ToRadians(value.X), MathHelper.ToRadians(value.Y), MathHelper.ToRadians(value.Z));
            }
        }

        /// <summary>
        /// Gets or sets the scaling of the object relative to 1 (1, 1, 1 is original size).
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 Scaling
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the object's width (using its bounding box, on the X axis).
        /// </summary>
        /// <value>See summary.</value>
        public float Width
        {
            get
            {
                if (this.Model != null)
                {
                    return this.Model.BoundingBox.Max.X - this.Model.BoundingBox.Min.X;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the object's depth (using its bounding box, on the Y axis).
        /// </summary>
        /// <value>See summary.</value>
        public float Depth
        {
            get
            {
                if (this.Model != null)
                {
                    return this.Model.BoundingBox.Max.Y - this.Model.BoundingBox.Min.Y;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the object's height (using its bounding box, on the Z axis).
        /// </summary>
        /// <value>See summary.</value>
        public float Height
        {
            get
            {
                if (this.Model != null)
                {
                    return this.Model.BoundingBox.Max.Z - this.Model.BoundingBox.Min.Z;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the name of the object to display (especially during picking operations).
        /// </summary>
        /// <value>See summary.</value>
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the transparency of the 3D model (0 = invisible, 255 = solid).
        /// </summary>
        /// <value>See summary.</value>
        public byte Transparency
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the object responds to collisions.
        /// </summary>
        /// <value>See summary.</value>
        public bool Collides
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a world transformation matrix.
        /// </summary>
        /// <param name="position">The position of the object in world space.</param>
        /// <param name="rotationRadians">The rotation of the object in world space, in radians.</param>
        /// <param name="scaling">The scaling of the object.</param>
        /// <returns>See summary.</returns>
        public static Matrix GetWorldTransform(Vector3 position, Vector3 rotationRadians, Vector3 scaling)
        {
            return Matrix.CreateScale(scaling) * Matrix.CreateFromYawPitchRoll(rotationRadians.Y, rotationRadians.X, rotationRadians.Z) * Matrix.CreateTranslation(position);
        }

        /// <summary>
        /// Gets the world transformation matrix of this object.
        /// </summary>
        /// <returns>See summary.</returns>
        public Matrix GetWorldTransform()
        {
            return GameObjectBase.GetWorldTransform(this.Position, this.RotationRadians, this.Scaling);
        }

        /// <summary>
        /// Sets the position, rotation and scaling of the game object.
        /// </summary>
        /// <param name="position">The position of the object in world space.</param>
        /// <param name="rotationDegrees">The rotation of the object, in degrees.</param>
        /// <param name="scaling">The scaling of the object, relative to 1 (1, 1, 1 is original size).</param>
        public void SetOrientation(Vector3 position, Vector3 rotationDegrees, Vector3 scaling)
        {
            this.Position = position;
            this.RotationDegrees = rotationDegrees;
            this.Scaling = scaling;
        }

        /// <summary>
        /// Moves the object the specified amount and direction along the map, optionally snapping to the terrain height after movement.
        /// </summary>
        /// <param name="gameObject">The game object to move forwards or backwards.</param>
        /// <param name="amount">The distance to move.</param>
        /// <param name="direction">The direction to move in.</param>
        /// <param name="snapToTerrainHeight">Whether to snap the object to the terrain's height after movement.</param>
        public void MoveForwardBack(GameObjectBase gameObject, float amount, Direction direction, bool snapToTerrainHeight)
        {
            if (direction == Direction.None)
            {
                return;
            }

            // Multiply by 1 if going forward, or -1 if going backwards
            amount *= (direction == Direction.Forward) ? 1 : -1;

            float positionX = gameObject.Position.X + (amount * MathExtensions.GetRotationX(gameObject.RotationRadians.Z));
            float positionY = gameObject.Position.Y + (amount * MathExtensions.GetRotationY(gameObject.RotationRadians.Z));

            Vector3 oldPosition = gameObject.Position;
            gameObject.Position = new Vector3(positionX, positionY, gameObject.Position.Z);

            // TODO: We only want to check collisions if we are NOT in editor mode
            /*if (!EngineManager.Engine.InEditorMode)
            {
                if (!EngineManager.Engine.Terrain.IsTraversable(gameObject.Position))
                {
                    gameObject.Position = new Vector3(positionX, oldPosition.Y, gameObject.Position.Z);

                    if (!EngineManager.Engine.Terrain.IsTraversable(gameObject.Position))
                    {
                        gameObject.Position = new Vector3(oldPosition.X, positionY, gameObject.Position.Z);

                        if (!EngineManager.Engine.Terrain.IsTraversable(gameObject.Position))
                        {
                            gameObject.Position = oldPosition;
                        }
                    }
                }
            }

            if (snapToTerrainHeight)
            {
                gameObject.SnapToTerrainHeight();
            }*/
        }

        /// <summary>
        /// Snaps the object to the terrain's height.
        /// </summary>
        public void SnapToTerrainHeight()
        {
            this.SnapToTerrainHeight(true);
        }

        /// <summary>
        /// Snaps the object to the terrain's height.
        /// </summary>
        /// <param name="addHalfHeight">
        /// Whether to add half the object's height to the Z position.
        /// Since objects are modeled with their absolute center
        /// at the origin, setting this value to true will raise
        /// the object the appropriate amount to appear to be sitting
        /// on top of the terrain. Setting this value to false will
        /// leave the object center at the terrain height so it will
        /// appear embedded into the terrain. The default is true.
        /// </param>
        public void SnapToTerrainHeight(bool addHalfHeight)
        {
            this.Position = new Vector3(this.Position.X, this.Position.Y, (EngineManager.Engine.Terrain.CurrentHeightLevel * TerrainManager.HeightLevelDifference) + EngineManager.Engine.Terrain.GetTerrainElevation(this.Position) + (addHalfHeight ? this.Height / 2 : 0));
        }

        /// <summary>
        /// Draws the object in the 3D world. If the object does not have a model, this method does nothing.
        /// </summary>
        /// <param name="cam">The camera object used for viewing.</param>
        public void Draw(Camera cam)
        {
            if (this.Model != null)
            {
                // Create the world transformation matrix
                Matrix worldMatrix = this.GetWorldTransform();

                EngineManager.Engine.SetDefaultRenderState();

                // Loop through each ModelMesh in the mesh
                foreach (ModelMesh mesh in this.Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.Begin(SaveStateMode.SaveState);

                        effect.AmbientLightColor = Vector3.One;
                        effect.PreferPerPixelLighting = true;
                        effect.World = this.transforms[mesh.ParentBone.Index] * worldMatrix;
                        effect.View = cam.ViewMatrix;
                        effect.Projection = cam.ProjectionMatrix;
                        effect.Alpha = (float)this.Transparency / (float)byte.MaxValue;

                        effect.End();
                    }

                    // Finally, draw the ModelMesh
                    mesh.Draw(SaveStateMode.SaveState);
                }
            }
        }
    }
}
