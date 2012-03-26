namespace Petroules.Synteza.Networking
{
    using System;

    /// <summary>
    /// Represents the server initialization packet containing the server's public key and whether it requires authentication.
    /// </summary>
    [Serializable]
    public sealed class ServerInitPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerInitPacket"/> class.
        /// </summary>
        /// <param name="publicKey">The server's public encryption key.</param>
        /// <param name="authenticationRequired">Whether authentication is required.</param>
        public ServerInitPacket(string publicKey, bool authenticationRequired)
        {
            this.PublicKey = publicKey;
            this.AuthenticationRequired = authenticationRequired;
        }

        /// <summary>
        /// Gets the server's public encryption key.
        /// </summary>
        public string PublicKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether authentication is required.
        /// </summary>
        public bool AuthenticationRequired
        {
            get;
            private set;
        }
    }
}
