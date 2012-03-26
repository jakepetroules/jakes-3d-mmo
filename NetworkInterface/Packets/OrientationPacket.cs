namespace MMO3D.NetworkInterface
{
    using System;
    using Microsoft.Xna.Framework;
    using Petroules.Synteza.Networking;
    using MMO3D.Engine;

    /// <summary>
    /// Packet containing the position and rotation of a player.
    /// </summary>
    [Serializable]
    public sealed class OrientationPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the OrientationPacket class.
        /// </summary>
        /// <param name="player">The player object to obtain the username, position and rotation from.</param>
        public OrientationPacket(Player player)
            : this(player.UserName, player.Position, player.RotationDegrees, player.Scaling)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OrientationPacket class.
        /// </summary>
        /// <param name="userName">The username of the player whose orientation this packet contains.</param>
        /// <param name="position">The position of the player.</param>
        /// <param name="rotation">The rotation of the player.</param>
        /// <param name="scaling">The scaling of the player.</param>
        public OrientationPacket(string userName, Vector3 position, Vector3 rotation, Vector3 scaling)
            : base()
        {
            this.UserName = userName;
            this.Position = position;
            this.Rotation = rotation;
            this.Scaling = scaling;
        }

        /// <summary>
        /// Gets the username of the player whose orientation this packet contains.
        /// </summary>
        /// <value>See summary.</value>
        public string UserName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the rotation of the player.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 Position
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the rotation of the player.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 Rotation
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the scaling of the player.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 Scaling
        {
            get;
            private set;
        }
    }
}
