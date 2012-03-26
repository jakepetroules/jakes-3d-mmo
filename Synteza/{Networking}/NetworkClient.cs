namespace Petroules.Synteza.Networking
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Security;
    using System.Threading;
    using Petroules.Synteza.Security;

    /// <summary>
    /// Defines a class that acts a client that can connect to a server over TCP.
    /// </summary>
    public class NetworkClient : IDisposable
    {
        /// <summary>
        /// The stream object used to read data from the server.
        /// </summary>
        private BinaryReader binaryReader;

        /// <summary>
        /// The stream object used to write data to the server.
        /// </summary>
        private BinaryWriter binaryWriter;

        /// <summary>
        /// Reference to the thread the listener is running on.
        /// </summary>
        private Thread listenerThread;

        /// <summary>
        /// The client's connection to the server.
        /// </summary>
        private TcpClient connection;

        /// <summary>
        /// <c>true</c> if the user terminated the connection; <c>false</c> if the connection to the server was lost.
        /// </summary>
        private bool userDisconnected = false;

        /// <summary>
        /// The client RSA encryption object used for secure communication.
        /// </summary>
        private RsaEncryption clientRsa = new RsaEncryption(2048);

        /// <summary>
        /// The server RSA encryption object used for secure communication.
        /// </summary>
        private RsaEncryption serverRsa;

        /// <summary>
        /// Initializes a new instance of the NetworkClient class.
        /// </summary>
        public NetworkClient()
        {
        }

        /// <summary>
        /// Raised when data has been received by the server.
        /// </summary>
        public event EventHandler<PacketEventArgs> PacketReceived = delegate { };

        /// <summary>
        /// Raised when the connection to the server has been initiated.
        /// </summary>
        public event EventHandler Connected = delegate { };

        /// <summary>
        /// Raised when the connection to the server has been terminated.
        /// </summary>
        public event EventHandler<DisconnectedEventArgs> Disconnected = delegate { };

        /// <summary>
        /// Raised when the user has authenticated with the server and thus fully established the connection.
        /// </summary>
        public event EventHandler Authenticated = delegate { };

        /// <summary>
        /// Gets a value indicating whether the client is connected to a server.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (this.connection != null && this.connection.Client != null)
                {
                    return this.connection.Connected;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is logged in to the server.
        /// </summary>
        /// <value>See summary.</value>
        public bool IsLoggedIn
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the client's connection to the server.
        /// </summary>
        /// <value>See summary.</value>
        protected TcpClient Connection
        {
            get { return this.connection; }
        }

        /// <summary>
        /// This method will initialize the TCP/IP connection to the server.
        /// </summary>
        /// <param name="host">The IP address or hostname to connect to.</param>
        /// <param name="port">The TCP/IP port to make the connection on.</param>
        /// <returns>A connection result structure.</returns>
        public ConnectionStatus Connect(string host, int port)
        {
            lock (this)
            {
                if (this.IsConnected)
                {
                    return ConnectionStatus.AlreadyConnected;
                }

                try
                {
                    // Reset this variable
                    this.userDisconnected = false;

                    (this.connection = new TcpClient()).Connect(host, port);
                    this.connection.NoDelay = true;
                    this.connection.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                    this.binaryReader = new BinaryReader(this.connection.GetStream());
                    this.binaryWriter = new BinaryWriter(this.connection.GetStream());

                    this.OnConnected();

                    return ConnectionStatus.Connected;
                }
                catch (ArgumentException e)
                {
                    // hostNameOrAddress is an invalid IP address
                    // The length of hostNameOrAddress is greater than 126 characters
                    this.Disconnect(ConnectionTerminationType.FailedToConnect, e);
                    return ConnectionStatus.Failed;
                }
                catch (SocketException e)
                {
                    this.Disconnect(ConnectionTerminationType.FailedToConnect, e);
                    return ConnectionStatus.Failed;
                }
                catch (ObjectDisposedException e)
                {
                    this.Disconnect(ConnectionTerminationType.FailedToConnect, e);
                    return ConnectionStatus.Failed;
                }
                catch (InvalidOperationException e)
                {
                    this.Disconnect(ConnectionTerminationType.FailedToConnect, e);
                    return ConnectionStatus.Failed;
                }
                catch (SecurityException e)
                {
                    this.Disconnect(ConnectionTerminationType.FailedToConnect, e);
                    return ConnectionStatus.Failed;
                }
            }
        }

        /// <summary>
        /// Authenticates the user with the specified ID and password.
        /// </summary>
        /// <param name="userId">The ID of the user to authenticate.</param>
        /// <param name="password">The password of the user to authenticate.</param>
        public AuthenticationResult Authenticate(string userId, string password)
        {
            lock (this)
            {
                if (this.IsConnected)
                {
                    if (!this.IsLoggedIn)
                    {
                        ServerInitPacket serverInit = this.ReceivePacket() as ServerInitPacket;
                        if (serverInit != null)
                        {
                            // Send OUR key to the server
                            this.SendPacket(new ClientInitPacket(this.clientRsa.PublicKey));

                            // We are now SECURE
                            this.serverRsa = new RsaEncryption(serverInit.PublicKey);

                            // Send the login information to the server
                            this.SendPacket(new AuthenticationPacket(userId, password));

                            // Expecting an authentication result packet, we just fail out if that's not the response
                            AuthenticationResultPacket packet = this.ReceivePacket() as AuthenticationResultPacket;
                            if (packet != null)
                            {
                                switch (packet.Result)
                                {
                                    case AuthenticationResult.Success:
                                        // If we successfully connected, start the listener thread
                                        this.IsLoggedIn = true;
                                        this.StartListener();

                                        this.OnAuthenticated();

                                        return packet.Result;
                                    default:
                                        // In any other case we kill the connection and return whatever the login result was
                                        this.DisconnectAuthFailed(packet.Result);
                                        return packet.Result;
                                }
                            }
                            else
                            {
                                this.DisconnectAuthFailed(AuthenticationResult.FailedUnknown);
                                return AuthenticationResult.FailedUnknown;
                            }
                        }
                        else
                        {
                            this.DisconnectAuthFailed(AuthenticationResult.FailedUnknown);
                            return AuthenticationResult.FailedUnknown;
                        }
                    }
                    else
                    {
                        return AuthenticationResult.AlreadyAuthenticated;
                    }
                }
                else
                {
                    this.DisconnectAuthFailed(AuthenticationResult.FailedNotConnected);
                    return AuthenticationResult.FailedNotConnected;
                }
            }
        }

        /// <summary>
        /// Disconnects from the server and closes all connections and resources.
        /// </summary>
        public void Disconnect()
        {
            this.Disconnect(ConnectionTerminationType.UserTermination, null);
        }

        /// <summary>
        /// Sends a packet to the server.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        /// <returns>Whether the sending succeeded.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="packet"/> is <c>null</c>.</exception>
        public bool SendPacket(Packet packet)
        {
            if (packet == null)
            {
                throw new ArgumentNullException("packet");
            }

            lock (this.binaryWriter)
            {
                if (this.serverRsa != null)
                {
                    return packet.WriteToStream(this.binaryWriter, this.serverRsa);
                }
                else
                {
                    return packet.WriteToStream(this.binaryWriter);
                }
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            this.Disconnect();

            if (disposing)
            {
                if (this.connection != null)
                {
                    this.connection.Close();
                }

                if (this.binaryReader != null)
                {
                    this.binaryReader.Close();
                }

                if (this.binaryWriter != null)
                {
                    this.binaryWriter.Close();
                }

                if (this.clientRsa != null)
                {
                    this.clientRsa.Dispose();
                }

                if (this.serverRsa != null)
                {
                    this.serverRsa.Dispose();
                }
            }

            this.connection = null;
            this.binaryReader = null;
            this.binaryWriter = null;
            this.clientRsa = null;
            this.serverRsa = null;
        }

        /// <summary>
        /// Reads a packet coming from the server. The packet can be null on error.
        /// </summary>
        /// <returns>See summary.</returns>
        protected Packet ReceivePacket()
        {
            lock (this)
            {
                if (this.serverRsa != null)
                {
                    return Packet.ReadFromStream(this.binaryReader, this.clientRsa);
                }
                else
                {
                    return Packet.ReadFromStream(this.binaryReader);
                }
            }
        }

        /// <summary>
        /// Disconnects from the server and closes all connections and resources, indicating a failed authentication.
        /// </summary>
        /// <param name="authenticationResult">The authentication result.</param>
        private void DisconnectAuthFailed(AuthenticationResult authenticationResult)
        {
            this.Disconnect(ConnectionTerminationType.FailedAuthentication, authenticationResult);
        }

        /// <summary>
        /// Disconnects from the server and closes all connections and resources.
        /// </summary>
        /// <param name="type">The type of connection termination that occurred.</param>
        /// <param name="exception">The exception detailing the reason for the connection termination, if any.</param>
        private void Disconnect(ConnectionTerminationType type, Exception exception)
        {
            this.DisconnectCore();
            this.OnDisconnected(type, exception);
        }

        /// <summary>
        /// Disconnects from the server and closes all connections and resources.
        /// </summary>
        /// <param name="type">The type of connection termination that occurred.</param>
        /// <param name="authenticationResult">The authentication result, if any.</param>
        private void Disconnect(ConnectionTerminationType type, AuthenticationResult authenticationResult)
        {
            this.DisconnectCore();
            this.OnDisconnected(type, authenticationResult);
        }

        /// <summary>
        /// Performs the core work of disconnecting from the server and closing all connections and resources.
        /// </summary>
        private void DisconnectCore()
        {
            this.userDisconnected = true;
            this.IsLoggedIn = false;

            // Destroy the server RSA encryption object so we don't have errors
            // trying to reconnect after a disconnection; we'll get a new one if
            // we want to authenticate again anyways
            this.serverRsa = null;

            // If the connection objects exist
            if (this.connection != null)
            {
                if (this.connection.Client != null)
                {
                    this.connection.Client.Close();
                }

                this.connection.Close();
            }
        }

        /// <summary>
        /// Listens for data received from the server, asynchronously.
        /// </summary>
        private void StartListener()
        {
            if (this.listenerThread == null)
            {
                (this.listenerThread = new Thread(this.StartListener)).Start();
                return;
            }

            Thread.CurrentThread.Name = "NetworkClient Listener";

            // If we've still got a connection...
            while (this.IsConnected)
            {
                // Read some data from the server, parse it into a packet, and raise the data received event
                Packet packet = this.ReceivePacket();
                if (packet != null)
                {
                    this.OnPacketReceived(packet);
                }
                else
                {
                    // We're nullifying the reference to the currently running thread - the current instance will die momentarily
                    this.listenerThread = null;

                    if (!this.userDisconnected)
                    {
                        this.Disconnect(ConnectionTerminationType.ServerOffline, null);
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// Fires the <see cref="PacketReceived"/> event.
        /// </summary>
        /// <param name="packet">The packet that was received.</param>
        private void OnPacketReceived(Packet packet)
        {
            new Thread(new ThreadStart(delegate { this.PacketReceived(this, new PacketEventArgs(packet)); })).Start();
        }

        /// <summary>
        /// Fires the <see cref="Connected"/> event on a new thread.
        /// </summary>
        private void OnConnected()
        {
            this.Connected(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fires the <see cref="Disconnected"/> event on a new thread.
        /// </summary>
        /// <param name="type">The type of connection termination that occurred.</param>
        /// <param name="exception">The exception detailing the reason for the connection termination, if any.</param>
        private void OnDisconnected(ConnectionTerminationType type, Exception exception)
        {
            this.Disconnected(this, new DisconnectedEventArgs(type, exception));
        }

        /// <summary>
        /// Fires the <see cref="Disconnected"/> event on a new thread.
        /// </summary>
        /// <param name="type">The type of connection termination that occurred.</param>
        /// <param name="authenticationResult">The authentication result, if any.</param>
        private void OnDisconnected(ConnectionTerminationType type, AuthenticationResult authenticationResult)
        {
            this.Disconnected(this, new DisconnectedEventArgs(type, authenticationResult));
        }

        /// <summary>
        /// Fires the <see cref="Authenticated"/> event on a new thread.
        /// </summary>
        private void OnAuthenticated()
        {
            this.Authenticated(this, EventArgs.Empty);
        }
    }
}
