namespace Petroules.Synteza.Networking
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using Petroules.Synteza.Security;

    /// <summary>
    /// Represents a listener that allows clients to connect to it. This class can be used as-is or overridden to provide additional functionality.
    /// </summary>
    public class NetworkServerListener : IDisposable
    {
        /// <summary>
        /// The IP address to listen on.
        /// </summary>
        private IPAddress address;

        /// <summary>
        /// The port to listen on.
        /// </summary>
        private int port;

        /// <summary>
        /// The thread to listen on.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// The server's connection.
        /// </summary>
        private TcpListener serverSocket;

        /// <summary>
        /// <c>true</c> if the user programmatically terminated the listener; <c>false</c> if the listener was terminated due to an exception.
        /// </summary>
        private bool userDisconnected = false;

        /// <summary>
        /// The server RSA encryption object used for secure communication.
        /// </summary>
        private RsaEncryption rsa = new RsaEncryption(2048);

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkServerListener"/> class.
        /// </summary>
        /// <param name="authenticationRequired">Whether authentication is required for connecting clients.</param>
        public NetworkServerListener(bool authenticationRequired)
        {
            this.AuthenticationRequired = authenticationRequired;
        }

        /// <summary>
        /// Raised when the listener is started.
        /// </summary>
        public event EventHandler ListenerStarted = delegate { };

        /// <summary>
        /// Raised if the listener fails to start.
        /// </summary>
        public event EventHandler<ListenerStartFailedEventArgs> ListenerFailedStart = delegate { };

        /// <summary>
        /// Raised when teh listener is stopped.
        /// </summary>
        public event EventHandler<ListenerStoppedEventArgs> ListenerStopped = delegate { };

        /// <summary>
        /// Raised when a client connects.
        /// </summary>
        public event EventHandler<ClientConnectedEventArgs> ClientConnected = delegate { };

        /// <summary>
        /// Gets a value indicating whether authentication is required for connecting clients.
        /// </summary>
        public bool AuthenticationRequired
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the IP address the listener is running on.
        /// </summary>
        /// <exception cref="InvalidOperationException">Attempted to set the listener address while the listener was running.</exception>
        public IPAddress Address
        {
            get
            {
                return this.address;
            }

            set
            {
                if (this.Running)
                {
                    throw new InvalidOperationException("Cannot set listener address while listener is running.");
                }

                this.address = value;
            }
        }

        /// <summary>
        /// Gets or sets the port the listener is running on.
        /// </summary>
        /// <exception cref="InvalidOperationException">Attempted to set the listener port while the listener was running.</exception>
        public int Port
        {
            get
            {
                return this.port;
            }

            set
            {
                if (this.Running)
                {
                    throw new InvalidOperationException("Cannot set listener port while listener is running.");
                }

                this.port = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the listener is running.
        /// </summary>
        public bool Running
        {
            get;
            private set;
        }

        /// <summary>
        /// Starts the listener.
        /// </summary>
        /// <exception cref="InvalidOperationException">Listener is already running.</exception>
        public void Start()
        {
            if (this.Running)
            {
                throw new InvalidOperationException("Listener is already running.");
            }

            // Make sure the old thread's dead
            if (this.thread != null)
            {
                this.thread.Join();
            }

            // Reset the variable
            this.userDisconnected = false;

            (this.thread = new Thread(this.Run)).Start();
        }

        /// <summary>
        /// Safely shuts down the listener.
        /// </summary>
        public void Stop()
        {
            if (this.Running && this.serverSocket != null)
            {
                this.userDisconnected = true;

                this.serverSocket.Stop();

                this.OnListenerStopped(ListenerStopType.Manual, null);
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
            if (disposing)
            {
                if (this.rsa != null)
                {
                    this.rsa.Dispose();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the specified client should be allowed to connect to the server.
        /// Override this method to implement filtering rules such as those based on IP addresses. This default
        /// implementation simply returns true.
        /// </summary>
        /// <param name="socket">The socket representing the connection to the client.</param>
        protected virtual bool AllowConnection(TcpClient socket)
        {
            return true;
        }

        /// <summary>
        /// Destroys the connection to a client.
        /// </summary>
        /// <param name="client">The client to destroy the connection to.</param>
        protected virtual void CloseConnection(TcpClient client)
        {
            if (client != null)
            {
                if (client.Client != null)
                {
                    client.Client.Close();
                }

                client.Close();
            }
        }

        /// <summary>
        /// Runs the client listener.
        /// </summary>
        private void Run()
        {
            try
            {
                Thread.CurrentThread.Name = "Client Listener";

                (this.serverSocket = new TcpListener(this.address, this.port)).Start();
                this.OnListenerStarted();
            }
            catch (ArgumentNullException e)
            {
                this.OnListenerFailedStart(e);
                return;
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.OnListenerFailedStart(e);
                return;
            }
            catch (SocketException e)
            {
                this.OnListenerFailedStart(e);
                return;
            }
            catch (IOException e)
            {
                this.OnListenerFailedStart(e);
                return;
            }

            this.HandleClientConnections();
        }

        /// <summary>
        /// Handles incoming client connections.
        /// </summary>
        private void HandleClientConnections()
        {
            while (true)
            {
                try
                {
                    TcpClient socket = this.serverSocket.AcceptTcpClient();
                    socket.NoDelay = true;
                    socket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                    // Only accept the client if they haven't been blocked
                    if (this.AllowConnection(socket))
                    {
                        this.OnClientConnected(socket);
                    }
                    else
                    {
                        this.CloseConnection(socket);
                    }
                }
                catch (ObjectDisposedException e)
                {
                    if (!this.userDisconnected)
                    {
                        this.OnListenerStopped(ListenerStopType.Exception, e);
                    }

                    break;
                }
                catch (SocketException e)
                {
                    if (!this.userDisconnected)
                    {
                        this.OnListenerStopped(ListenerStopType.Exception, e);
                    }

                    break;
                }
                catch (IOException e)
                {
                    if (!this.userDisconnected)
                    {
                        this.OnListenerStopped(ListenerStopType.Exception, e);
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// Fires the <see cref="ListenerStarted"/> event.
        /// </summary>
        private void OnListenerStarted()
        {
            this.Running = true;
            this.ListenerStarted(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fires the <see cref="ListenerFailedStart"/> event.
        /// </summary>
        /// <param name="exception">The exception that caused the listener not to start.</param>
        private void OnListenerFailedStart(Exception exception)
        {
            this.Running = false;
            this.ListenerFailedStart(this, new ListenerStartFailedEventArgs(exception));
        }

        /// <summary>
        /// Fires the <see cref="ListenerStopped"/> event.
        /// </summary>
        /// <param name="type">The reason the listener was stopped.</param>
        /// <param name="exception">The exception that caused the listener to stop, if any.</param>
        private void OnListenerStopped(ListenerStopType type, Exception exception)
        {
            this.Running = false;
            this.ListenerStopped(this, new ListenerStoppedEventArgs(type, exception));
        }

        /// <summary>
        /// Fires the <see cref="ClientConnected"/> event.
        /// </summary>
        /// <param name="socket">The socket of the connected client.</param>
        private void OnClientConnected(TcpClient socket)
        {
            this.ClientConnected(this, new ClientConnectedEventArgs(new NetworkServerHandler(socket, this.rsa, this.AuthenticationRequired)));
        }
    }
}
