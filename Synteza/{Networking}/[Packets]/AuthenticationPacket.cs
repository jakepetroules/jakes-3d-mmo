namespace Petroules.Synteza.Networking
{
    using System;

    /// <summary>
    /// Represents a set of credentials used to authenticate a client with a server.
    /// </summary>
    [Serializable]
    public class AuthenticationPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationPacket"/> class.
        /// </summary>
        /// <param name="userId">The user ID to authenticate with.</param>
        /// <param name="password">The password to authenticate with.</param>
        public AuthenticationPacket(string userId, string password)
        {
            this.UserId = userId;
            this.Password = password;
        }

        /// <summary>
        /// Gets the user ID to authenticate with.
        /// </summary>
        public string UserId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the password to authenticate with.
        /// </summary>
        public string Password
        {
            get;
            private set;
        }
    }
}
