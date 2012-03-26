namespace MMO3D.NetworkInterface
{
    using System;
    using Microsoft.Xna.Framework;
    using Petroules.Synteza.Networking;
    using MMO3D.CommonCode;

    /// <summary>
    /// The packet containing player movement information.
    /// </summary>
    [Serializable]
    public sealed class MovementPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the MovementPacket class.
        /// </summary>
        /// <param name="movementDirection">The direction in which the player wishes to move.</param>
        /// <param name="rotationDegrees">The player's rotation, in degrees.</param>
        public MovementPacket(Direction movementDirection, Vector3 rotationDegrees)
            : base()
        {
            this.MovementDirection = movementDirection;
            this.RotationDegrees = rotationDegrees;
        }

        /// <summary>
        /// Gets the direction in which the player wishes to move.
        /// </summary>
        /// <value>See summary.</value>
        public Direction MovementDirection
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the player's rotation, in degrees.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 RotationDegrees
        {
            get;
            private set;
        }
    }
}
