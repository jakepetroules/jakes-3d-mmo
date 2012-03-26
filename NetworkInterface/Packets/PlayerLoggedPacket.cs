namespace MMO3D.NetworkInterface
{
    using System;
    using Petroules.Synteza.Networking;
    using MMO3D.Engine;

    /// <summary>
    /// The player logged on/off packet.
    /// </summary>
    [Serializable]
    public sealed class PlayerLoggedPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the PlayerLoggedPacket class.
        /// </summary>
        /// <param name="player">The player who logged on or off.</param>
        /// <param name="onOff">A value indicating whether the player logged on or off.</param>
        public PlayerLoggedPacket(Player player, bool onOff)
            : base()
        {
            this.OnOff = onOff;
            this.Orientation = new OrientationPacket(player);
        }

        /// <summary>
        /// Gets the user name of the player that logged on or off.
        /// Shortcut for Orientation.Username.
        /// </summary>
        /// <value>See summary.</value>
        public string UserName
        {
            get { return this.Orientation.UserName; }
        }

        /// <summary>
        /// Gets a value indicating whether the player logged on or off.
        /// </summary>
        /// <value>See summary.</value>
        public bool OnOff
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the orientation of the player.
        /// </summary>
        /// <value>See summary.</value>
        public OrientationPacket Orientation
        {
            get;
            private set;
        }
    }
}
