namespace Petroules.Synteza.Networking
{
    using System;

    /// <summary>
    /// Represents the client initialization packet containing the client's public key.
    /// </summary>
    [Serializable]
    public sealed class ClientInitPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientInitPacket"/> class.
        /// </summary>
        /// <param name="publicKey">The client's public encryption key.</param>
        public ClientInitPacket(string publicKey)
        {
            this.PublicKey = publicKey;
        }

        /// <summary>
        /// Gets the client's public encryption key.
        /// </summary>
        public string PublicKey
        {
            get;
            private set;
        }
    }
}
