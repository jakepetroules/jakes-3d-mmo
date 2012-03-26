namespace MMO3D.Server
{
    using Microsoft.Xna.Framework;
    using MMO3D.Engine;

    /// <summary>
    /// Defines any sort of gateway a player can pass through.
    /// </summary>
    /// <remarks>
    /// Something that will move the player from one map to another;
    /// this will be for portals or cave entrances, etc. These are simple.
    /// If a player walk into them, the player moves into another area.
    /// Suggested is a 10 second timer before the player can use another gateway.
    /// This prevents entering a cave and accidentally walking back out again instantly.
    /// </remarks>
    public sealed class Gateway : GameObjectBase
    {
        /// <summary>
        /// Initializes a new instance of the Gateway class.
        /// </summary>
        /// <param name="model">The model to use for this game object.</param>
        /// <param name="destinationPosition">The position the gateway teleports the player to.</param>
        /// <param name="destinationRotation">The rotation the gateway sets the player to after teleportation.</param>
        public Gateway(ExtendedModel model, Vector3 destinationPosition, Vector3 destinationRotation)
            : base(model)
        {
            this.DestinationPosition = destinationPosition;
            this.DestinationRotation = destinationRotation;
        }

        /// <summary>
        /// Gets or sets the position the gateway teleports the player to.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 DestinationPosition
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rotation the gateway sets the player to after teleportation.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 DestinationRotation
        {
            get;
            set;
        }

        /// <summary>
        /// Teleports the object using the gateway's destination position and rotation.
        /// </summary>
        /// <param name="gob">The object to teleport.</param>
        public void Teleport(GameObjectBase gob)
        {
            gob.Position = this.DestinationPosition;
            gob.RotationDegrees = this.DestinationRotation;
        }
    }
}
