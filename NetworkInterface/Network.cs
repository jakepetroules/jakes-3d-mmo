namespace MMO3D.NetworkInterface
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// Controls the network connection to the game server.
    /// </summary>
    public sealed class Network : NetworkClient, IDisposable
    {
        /// <summary>
        /// The default port to use for network connections.
        /// </summary>
        public const int DefaultPort = 9798;

        /// <summary>
        /// The default port to use for queries.
        /// </summary>
        public const int DefaultQueryPort = 9799;

        /// <summary>
        /// Initializes a new instance of the Network class.
        /// </summary>
        public Network()
            : base()
        {
        }
    }
}
