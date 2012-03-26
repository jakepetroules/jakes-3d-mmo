namespace MMO3D.Engine
{
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a chase camera which inherits the default functionality of a <see cref="Camera"/>.
    /// </summary>
    public sealed class ChaseCamera : Camera
    {
        /// <summary>
        /// Initializes a new instance of the ChaseCamera class.
        /// </summary>
        /// <param name="chaseTarget">The object being chased by the camera. This value can be null.</param>
        public ChaseCamera(GameObjectBase chaseTarget)
            : base(Vector3.Zero, chaseTarget != null ? chaseTarget.Position : Vector3.Zero)
        {
            this.ChaseTarget = chaseTarget;

            this.CameraDistance = 15;
            this.HeightDifference = 6;
        }

        /// <summary>
        /// Gets or sets the object being chased by the camera.
        /// </summary>
        /// <value>See summary.</value>
        public GameObjectBase ChaseTarget
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the distance of the camera from the object. The default is 15.
        /// </summary>
        /// <value>See summary.</value>
        public float CameraDistance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating how much higher the camera's position is, than the object's. The default is 6.
        /// </summary>
        /// <value>See summary.</value>
        public float HeightDifference
        {
            get;
            set;
        }

        /// <summary>
        /// Updates the state of the camera. If the chase target is null,
        /// the camera will retain its previous position and look-at target.
        /// </summary>
        public override void Update()
        {
            if (this.ChaseTarget != null)
            {
                // Gets the object rotation on the Z axis (left-right)
                float objectRotation = MathHelper.ToRadians(this.ChaseTarget.RotationDegrees.Z);

                // Sets the camera position at the specified distance behind the object
                this.Position = new Vector3((float)(this.ChaseTarget.Position.X - (this.CameraDistance * MathExtensions.GetRotationX(objectRotation))), (float)(this.ChaseTarget.Position.Y - (this.CameraDistance * MathExtensions.GetRotationY(objectRotation))), this.ChaseTarget.Position.Z + this.HeightDifference);

                // Updates the camera look vector to the object's position
                this.Target = this.ChaseTarget.Position;
            }

            // Update the matrices
            base.Update();
        }
    }
}
