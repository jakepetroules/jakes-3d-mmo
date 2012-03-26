namespace MMO3D.Server
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using Petroules.Synteza.Networking;
    using MMO3D.NetworkInterface;

    /// <summary>
    /// Defines a listener to receive and respond to queries to the server.
    /// </summary>
    public class QueryListener : NetworkServerListener
    {
        /// <summary>
        /// Initializes a new instance of the QueryListener class.
        /// </summary>
        public QueryListener()
            : base(false)
        {
            this.Address = IPAddress.Any;
            this.Port = Network.DefaultQueryPort;

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
            NetworkStream stream = null;
            StreamReader reader = null;
            BinaryWriter writer = null;

            try
            {
                stream = e.Client.Connection.GetStream();
                reader = new StreamReader(stream, Encoding.ASCII);
                writer = new BinaryWriter(stream, Encoding.ASCII);

                switch (reader.ReadLine())
                {
                    case "PlayerCount":
                        writer.Write(GameServer.Instance.Clients.Count);
                        break;
                }

                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }

                if (reader != null)
                {
                    reader.Dispose();
                }

                if (writer != null)
                {
                    writer.Close();
                }

                this.CloseConnection(e.Client.Connection);
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
        /// Shows that the query listener was started, on the GUI.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ClientListener_ListenerStarted(object sender, EventArgs e)
        {
            GameServer.Instance.ServerGui.AddStatusText(string.Format(CultureInfo.CurrentCulture, Resources.QueryListenerSuccessfulStart, this.Port));
        }

        /// <summary>
        /// Shows that the query listener was started, on the GUI.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ClientListener_ListenerFailedStart(object sender, EventArgs e)
        {
            GameServer.Instance.ServerGui.AddStatusText(string.Format(CultureInfo.CurrentCulture, Resources.QueryListenerCouldNotStart, this.Port));
        }
    }
}
