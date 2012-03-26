namespace MMO3D.Server
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Net;
    using Petroules.Synteza.Networking;
    using MMO3D.NetworkInterface;
    using System.Net.Sockets;

    /// <summary>
    /// Defines a listener for players connecting to the server.
    /// </summary>
    public class PlayerListener : NetworkServerListener
    {
        /// <summary>
        /// Initializes a new instance of the PlayerListener class.
        /// </summary>
        public PlayerListener()
            : base(true)
        {
            this.Address = IPAddress.Any;
            this.Port = Network.DefaultPort;

            this.ListenerStarted += this.ClientListener_ListenerStarted;
            this.ListenerFailedStart += this.ClientListener_ListenerFailedStart;
            this.ClientConnected += this.ClientListener_ClientConnected;
        }

        /// <summary>
        /// Processes an accepted client.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void ClientListener_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            lock (GameServer.Instance.Clients)
            {
                if (GameServer.Instance.Clients.Count < GameServer.MaximumConnections)
                {
                    PlayerHandler handler = new PlayerHandler(GameServer.Instance, e.Client);
                    GameServer.Instance.Clients.Add(handler);

                    GameServer.Instance.ServerGui.AddStatusText(string.Format(CultureInfo.CurrentCulture, Resources.ClientListenerConnectionAccepted, e.Client.Connection.Client.RemoteEndPoint));

                    // Update the player count
                    GameServer.Instance.ServerGui.UpdatePlayerCount(GameServer.Instance.Clients.Count);
                }
            }
        }

        /// <summary>
        /// Returns a value indicating whether the client's IP is not in the blocked IP list.
        /// </summary>
        /// <param name="socket">The socket representing the client to check.</param>
        protected override bool AllowConnection(TcpClient socket)
        {
            return GameServer.Instance.GetBannedIps().Contains(NetworkUtilities.GetRemoteIP(socket.Client));
        }

        /// <summary>
        /// Shows that the client listener was started, on the GUI.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ClientListener_ListenerStarted(object sender, EventArgs e)
        {
            GameServer.Instance.ServerGui.AddStatusText(string.Format(CultureInfo.CurrentCulture, Resources.ClientListenerSuccessfulStart, this.Port));
        }

        /// <summary>
        /// Shows that the client listener was started, on the GUI.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ClientListener_ListenerFailedStart(object sender, EventArgs e)
        {
            GameServer.Instance.ServerGui.AddStatusText(string.Format(CultureInfo.CurrentCulture, Resources.ClientListenerCouldNotStart, this.Port));
        }
    }
}
