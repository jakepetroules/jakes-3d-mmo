namespace Petroules.Synteza.Networking
{
    using System;

    /// <summary>
    /// Provides data for the <see cref="NetworkClient.PacketReceived"/> and <see cref="NetworkServerHandler.PacketReceived"/> event.
    /// </summary>
    public class PacketEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PacketEventArgs"/> class.
        /// </summary>
        /// <param name="packet">The network packet to encapsulate, which can be <c>null</c>.</param>
        public PacketEventArgs(Packet packet)
        {
            this.Packet = packet;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketEventArgs"/> class.
        /// </summary>
        /// <param name="client">The object representing the client that sent the packet.</param>
        /// <param name="packet">The network packet to encapsulate, which can be <c>null</c>.</param>
        public PacketEventArgs(NetworkServerHandler client, Packet packet)
        {
            this.Client = client;
            this.Packet = packet;
        }

        /// <summary>
        /// Gets the object representing the client that sent the packet.
        /// </summary>
        public NetworkServerHandler Client
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the stored network packet.
        /// </summary>
        public Packet Packet
        {
            get;
            private set;
        }
    }
}
