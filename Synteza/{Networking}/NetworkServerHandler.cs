namespace Petroules.Synteza.Networking
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Threading;
    using Petroules.Synteza.Security;

    /// <summary>
    /// Represents the client on the server side.
    /// </summary>
    public sealed class NetworkServerHandler : IDisposable
    {
        /// <summary>
        /// The TCP client object describing the client's binding to the server.
        /// </summary>
        private TcpClient connection;

        /// <summary>
        /// The stream object used to read data from the client.
        /// </summary>
        private BinaryReader binaryReader;

        /// <summary>
        /// The stream object used to write data to the client.
        /// </summary>
        private BinaryWriter binaryWriter;

        /// <summary>
        /// Whether the object's listener thread is running.
        /// </summary>
        private volatile bool running = false;

        /// <summary>
        /// The server RSA encryption object used for secure communication.
        /// </summary>
        private RsaEncryption serverRsa;

        /// <summary>
        /// The client RSA encryption object used for secure communication.
        /// </summary>
        private RsaEncryption clientRsa;

        /// <summary>
        /// Whether authentication is required.
        /// </summary>
        private bool authenticationRequired;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkServerHandler"/> class.
        /// </summary>
        /// <param name="connection">The TCP client object describing the client's binding to the server.</param>
        /// <param name="serverRsa">The server's RSA encryption utility instance.</param>
        /// <param name="authenticationRequired">Whether authentication is required.</param>
        /// <exception cref="ArgumentException">The stream does not support reading, the stream is null, or the stream is already closed.</exception>
        /// <exception cref="InvalidOperationException">The client is not connected to a remote host or has been disposed.</exception>
        internal NetworkServerHandler(TcpClient connection, RsaEncryption serverRsa, bool authenticationRequired)
        {
            this.connection = connection;
            this.serverRsa = serverRsa;

            try
            {
                this.binaryReader = new BinaryReader(connection.GetStream());
                this.binaryWriter = new BinaryWriter(connection.GetStream());
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }

            // Send the server's public key to the client
            this.SendPacket(new ServerInitPacket(this.serverRsa.PublicKey, this.authenticationRequired = authenticationRequired));

            new Thread(this.Run).Start();
        }

        /// <summary>
        /// Raised when the client is authenticating.
        /// </summary>
        public event EventHandler<ClientAuthenticatingEventArgs> ClientAuthenticating = delegate { };

        /// <summary>
        /// Raised when a packet is received from the client.
        /// </summary>
        public event EventHandler<PacketEventArgs> PacketReceived = delegate { };

        /// <summary>
        /// Gets a value indicating whether the object has been destroyed.
        /// </summary>
        /// <value>See summary.</value>
        public bool Disposed
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the underlying client connection.
        /// </summary>
        public TcpClient Connection
        {
            get { return this.connection; }
        }

        /// <summary>
        /// Gets a value indicating whether the client is still connected to the server.
        /// </summary>
        public bool Connected
        {
            get { return this.connection != null && this.connection.Connected; }
        }

        /// <summary>
        /// Sends a packet to the client.
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
                if (this.clientRsa != null)
                {
                    return packet.WriteToStream(this.binaryWriter, this.clientRsa);
                }
                else
                {
                    return packet.WriteToStream(this.binaryWriter);
                }
            }
        }

        /// <summary>
        /// Disconnects the client by closing the connection.
        /// </summary>
        public void Dispose()
        {
            lock (this)
            {
                // If the client has not already been destroyed...
                if (!this.Disposed)
                {
                    // Destroy the connection connection and send a message to the server GUI
                    if (this.connection != null)
                    {
                        try
                        {
                            this.connection.Close();
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                    // Tell the client object it has been destroyed and that its listener is no longer running
                    this.Disposed = true;
                    this.running = false;
                }
            }
        }

        /// <summary>
        /// Begins the infinite loop of the client's data listener.
        /// </summary>
        private void Run()
        {
            Monitor.Enter(this);

            // Only run if not already running and object hasn't been destroyed
            if (!this.running && !this.Disposed)
            {
                this.running = true;

                Monitor.Exit(this);

                Thread.CurrentThread.Name = "Listener Thread";

                while (this.running && !this.Disposed && this.connection.Connected)
                {
                    try
                    {
                        Packet packet = this.clientRsa != null ? Packet.ReadFromStream(this.binaryReader, this.serverRsa) : Packet.ReadFromStream(this.binaryReader);

                        ClientInitPacket init = packet as ClientInitPacket;
                        if (init != null)
                        {
                            this.clientRsa = new RsaEncryption(init.PublicKey);
                            continue;
                        }

                        AuthenticationPacket auth = packet as AuthenticationPacket;
                        if (auth != null)
                        {
                            ClientAuthenticatingEventArgs e = new ClientAuthenticatingEventArgs(auth.UserId, auth.Password);
                            this.ClientAuthenticating(this, e);

                            this.SendPacket(new AuthenticationResultPacket(e.AuthenticationResult));

                            continue;
                        }

                        this.PacketReceived(this, new PacketEventArgs(this, packet));
                    }
                    catch (Exception e)
                    {
                        // If there was an exception, disconnect the client
                        Console.WriteLine(e);
                        this.Dispose();
                        return;
                    }
                }

                this.Dispose();
            }
        }
    }
}
