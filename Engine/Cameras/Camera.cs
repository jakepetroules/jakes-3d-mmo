namespace MMO3D.Engine
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Defines a camera used to view the 3D world.
    /// </summary>
    public abstract class Camera
    {
        /// <summary>
        /// Initializes a new instance of the Camera class.
        /// </summary>
        /// <param name="position">The position of the camera.</param>
        /// <param name="target">The look-target of the camera.</param>
        protected Camera(Vector3 position, Vector3 target)
        {
            this.Position = position;
            this.Target = target;

            this.NearPlaneDistance = 0.2f;
            this.FarPlaneDistance = 3000;
        }

        /// <summary>
        /// Gets the aspect ratio of the graphics device's viewport.
        /// </summary>
        /// <value>See summary.</value>
        public static float AspectRatio
        {
            get { return EngineManager.Engine.GraphicsDevice.Viewport.AspectRatio; }
        }

        /// <summary>
        /// Gets the field of view of a camera.
        /// </summary>
        /// <value>See summary.</value>
        public static float FieldOfView
        {
            get { return MathHelper.PiOver4; }
        }

        /// <summary>
        /// Gets the up vector of a camera.
        /// </summary>
        /// <value>See summary.</value>
        public static Vector3 UpVector
        {
            get { return Vector3.UnitZ; }
        }

        /// <summary>
        /// Gets or sets the position of the camera.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the look-target of the camera.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 Target
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the distance to the near plane. The default is 0.2.
        /// </summary>
        /// <value>See summary.</value>
        public float NearPlaneDistance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the distance to the far plane. The default is 3000.
        /// </summary>
        /// <value>See summary.</value>
        public float FarPlaneDistance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current view matrix.
        /// </summary>
        /// <value>See summary.</value>
        public Matrix ViewMatrix
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current projection matrix.
        /// </summary>
        /// <value>See summary.</value>
        public Matrix ProjectionMatrix
        {
            get;
            private set;
        }

        /// <summary>
        /// Updates the matrices of the camera.
        /// Derived classes should almost always
        /// call this base implementation after
        /// their code.
        /// </summary>
        public virtual void Update()
        {
            this.ViewMatrix = Matrix.CreateLookAt(this.Position, this.Target, Camera.UpVector);
            this.ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(Camera.FieldOfView, Camera.AspectRatio, this.NearPlaneDistance, this.FarPlaneDistance);
        }
    }
}
