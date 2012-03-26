namespace MMO3D.NetworkInterface
{
    using System;
    using Petroules.Synteza.Networking;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a packet to send the contents of a player's inventory.
    /// </summary>
    [Serializable]
    public sealed class InventoryPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the InventoryPacket class.
        /// </summary>
        /// <param name="inventory">The inventory object being transferred.</param>
        public InventoryPacket(PlayerInventory inventory)
            : base()
        {
            this.Inventory = inventory;
        }

        /// <summary>
        /// Gets the inventory object being transferred.
        /// </summary>
        /// <value>See summary.</value>
        public PlayerInventory Inventory
        {
            get;
            private set;
        }
    }
}
