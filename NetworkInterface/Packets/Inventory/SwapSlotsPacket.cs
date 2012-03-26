namespace MMO3D.NetworkInterface
{
    using System;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// Contains the the indices of two slots a user wishes to swap, in his inventory.
    /// </summary>
    [Serializable]
    public sealed class SwapSlotsPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the SwapSlotsPacket class.
        /// </summary>
        /// <param name="swapIndex1">The index of the first item to swap.</param>
        /// <param name="swapIndex2">The index of the second item to swap.</param>
        public SwapSlotsPacket(int swapIndex1, int swapIndex2)
            : base()
        {
            this.SwapIndex1 = swapIndex1;
            this.SwapIndex2 = swapIndex2;
        }

        /// <summary>
        /// Gets the index of the first item to swap.
        /// </summary>
        /// <value>See summary.</value>
        public int SwapIndex1
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the index of the second item to swap.
        /// </summary>
        /// <value>See summary.</value>
        public int SwapIndex2
        {
            get;
            private set;
        }
    }
}
