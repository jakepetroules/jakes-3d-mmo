namespace MMO3D.NetworkInterface
{
    using System;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// Contains the index of an item in the player's inventory to discard.
    /// </summary>
    [Serializable]
    public sealed class DiscardPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the DiscardPacket class.
        /// </summary>
        /// <param name="discardIndex">The index of the item to discard.</param>
        public DiscardPacket(int discardIndex)
            : base()
        {
            this.DiscardIndex = discardIndex;
        }

        /// <summary>
        /// Gets the index of the item to discard.
        /// </summary>
        /// <value>See summary.</value>
        public int DiscardIndex
        {
            get;
            private set;
        }
    }
}
