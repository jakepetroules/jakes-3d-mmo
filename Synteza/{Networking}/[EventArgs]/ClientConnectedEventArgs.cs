namespace Petroules.Synteza.Networking
{
    using System;
    using System.Net.Sockets;

    /// <summary>
    /// Provides data for the <see cref="NetworkServerListener.ClientConnected"/> event.
    /// </summary>
    public class ClientConnectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConnectedEventArgs"/> class.
        /// </summary>
        /// <param name="client">The client that connected to the server.</param>
        /// <exception cref="ArgumentNullException"><paramref name="client"/> is <c>null</c>.</exception>
        public ClientConnectedEventArgs(NetworkServerHandler client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }

            this.Client = client;
        }

        /// <summary>
        /// Gets the client that connected to the server.
        /// </summary>
        public NetworkServerHandler Client
        {
            get;
            private set;
        }
    }
}
