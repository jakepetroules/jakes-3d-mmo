namespace MMO3D.NetworkInterface
{
    using System;
    using System.Collections.ObjectModel;
    using Petroules.Synteza.Networking;
    using MMO3D.Engine;

    /// <summary>
    /// Defines a packet which the server will send to the client to initialize itself.
    /// </summary>
    [Serializable]
    public sealed class IntroductionPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the IntroductionPacket class.
        /// </summary>
        /// <param name="inventoryPacket">The inventory packet which is part of the initialization.</param>
        /// <param name="orientationPacket">The orientation packet which is part of the initialization.</param>
        /// <param name="otherPlayers">The players to generate orientation packets from. Will automatically exclude an element if it's Username property is equal to that of orientationPacket.</param>
        public IntroductionPacket(InventoryPacket inventoryPacket, OrientationPacket orientationPacket, params Player[] otherPlayers)
            : base()
        {
            this.InventoryPacket = inventoryPacket;
            this.OrientationPacket = orientationPacket;

            Collection<OrientationPacket> otherOrientations = new Collection<OrientationPacket>();
            for (int i = 0; i < otherPlayers.Length; i++)
            {
                if (otherPlayers[i].UserName != orientationPacket.UserName)
                {
                    otherOrientations.Add(new OrientationPacket(otherPlayers[i]));
                }
            }

            this.OtherPlayersOrientation = new ReadOnlyCollection<OrientationPacket>(otherOrientations);
        }

        /// <summary>
        /// Initializes a new instance of the IntroductionPacket class.
        /// </summary>
        /// <param name="inventoryPacket">The inventory packet which is part of the initialization.</param>
        /// <param name="orientationPacket">The orientation packet which is part of the initialization.</param>
        /// <param name="otherPlayersOrientation">The orientation packets representing the orientation of other players.</param>
        public IntroductionPacket(InventoryPacket inventoryPacket, OrientationPacket orientationPacket, params OrientationPacket[] otherPlayersOrientation)
            : base()
        {
            this.InventoryPacket = inventoryPacket;
            this.OrientationPacket = orientationPacket;
            this.OtherPlayersOrientation = new ReadOnlyCollection<OrientationPacket>(otherPlayersOrientation);
        }

        /// <summary>
        /// Gets the username of the player.
        /// </summary>
        /// <value>See summary.</value>
        public string UserName
        {
            get { return this.OrientationPacket.UserName; }
        }

        /// <summary>
        /// Gets the inventory packet which is part of the initialization.
        /// </summary>
        /// <value>See summary.</value>
        public InventoryPacket InventoryPacket
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the orientation packet which is part of the initialization.
        /// </summary>
        /// <value>See summary.</value>
        public OrientationPacket OrientationPacket
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the orientation packets representing the orientation of other players.
        /// </summary>
        /// <value>See summary.</value>
        public ReadOnlyCollection<OrientationPacket> OtherPlayersOrientation
        {
            get;
            private set;
        }
    }
}
