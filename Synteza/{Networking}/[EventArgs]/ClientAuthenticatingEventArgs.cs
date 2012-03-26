namespace Petroules.Synteza.Networking
{
    using System;

    /// <summary>
    /// Provides data for the <see cref="NetworkServerHandler.ClientAuthenticating"/> event.
    /// </summary>
    public sealed class ClientAuthenticatingEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientAuthenticatingEventArgs"/> class.
        /// </summary>
        /// <param name="userId">The user ID of the authenticating client.</param>
        /// <param name="password">The password of the authenticating client.</param>
        public ClientAuthenticatingEventArgs(string userId, string password)
        {
            this.UserId = userId;
            this.Password = password;
        }

        /// <summary>
        /// Gets the user ID of the authenticating client.
        /// </summary>
        public string UserId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the password of the authenticating client.
        /// </summary>
        public string Password
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating the authentication status of the client.
        /// </summary>
        public AuthenticationResult AuthenticationResult
        {
            get;
            set;
        }
    }
}
