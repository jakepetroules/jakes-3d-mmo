namespace MMO3D.Server
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    //using Petroules.Synteza.MySQL;
    using MMO3D.Engine;
    using System.Linq;

    /// <summary>
    /// The main class of the server.
    /// </summary>
    public sealed class GameServer
    {
        /// <summary>
        /// Maximum number of players that can be connected to the server at one time.
        /// </summary>
        public const int MaximumConnections = 500;

        /// <summary>
        /// List of IProcessable classes that run the game.
        /// </summary>
        private Collection<IGameProcess> processers;

        /// <summary>
        /// The client listener object.
        /// </summary>
        private PlayerListener clientListener;

        /// <summary>
        /// The query listener object.
        /// </summary>
        private QueryListener queryListener;

        /// <summary>
        /// Initializes a new instance of the GameServer class.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Attempt was made to create multiple instances of this class.</exception>
        public GameServer()
        {
            // If the instance isn't null, blow up
            if (GameServer.Instance != null)
            {
                throw new InvalidOperationException("Only one instance of GameServer may be created at one time; make sure to call Dispose() when finished with one.");
            }

            GameServer.Instance = this;

            //this.Engine = new GameEngine();
            //this.Engine.Initialize();

            this.processers = new Collection<IGameProcess>();
            this.Clients = new Collection<PlayerHandler>();
        }

        /// <summary>
        /// Gets a reference to the active instance of <see cref="GameServer"/>.
        /// </summary>
        /// <value>See summary.</value>
        public static GameServer Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the database manager used to connect to the database.
        /// </summary>
        /// <value>See summary.</value>
        public MainDatabaseDataContext DbManager
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the game engine used to provide access to engine components.
        /// </summary>
        /// <value>See summary.</value>
        public GameEngine Engine
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the list of clients connected to the server.
        /// </summary>
        /// <value>See summary.</value>
        public Collection<PlayerHandler> Clients
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the server's GUI display.
        /// </summary>
        /// <value>See summary.</value>
        public ServerGui ServerGui
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the server is currently running.
        /// </summary>
        /// <remarks>
        /// This is set to <code>false</code> by <code>exit()</code>
        /// to clean up and shut down the server.
        /// </remarks>
        /// <value>See summary.</value>
        public bool Running
        {
            get;
            private set;
        }

        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <param name="args">The command-line arguments passed to the program.</param>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //DatabaseManager databaseManager = ProgramLauncher.GetLogOnInfo(args);
            //if (databaseManager != null)
            {
                GameServer server = new GameServer();

                try
                {
                    Console.SetOut(new StreamWriter("log.txt", true, Encoding.UTF8));
                    Thread.CurrentThread.Name = "Main Server Processing Thread";
                    server.Run(/*null*/);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error", "FATAL ERROR. Server will now exit.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, server.ServerGui.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
                    new ThreadExceptionDialog(e).ShowDialog();
                }
            }
        }

        /// <summary>
        /// Starts/runs the server.
        /// </summary>
        /// <param name="databaseManager">The database manager used to interact with the master game database.</param>
        public void Run(/*DatabaseManager databaseManager*/)
        {
            this.ServerGui = ServerGui.CreateGui(this);

            this.ServerGui.AddStatusText(Resources.DatabaseManagerConnectingToDatabase);

            try
            {
                this.DbManager = new MainDatabaseDataContext();
                this.ServerGui.AddStatusText(Resources.DatabaseManagerConnectedToDatabase);

                ChatFilter.Initialize();
                PacketSender.Initialize(this);

                //// TODO: Add all the IProcessables
                //// this.processers.Add(...

                this.clientListener = new PlayerListener();
                this.clientListener.Start();

                this.queryListener = new QueryListener();
                this.queryListener.Start();

                // Server is now running!
                this.Running = true;

                // Run the processing loop...
                while (this.Running)
                {
                    this.CleanDisconnectedPlayers();

                    for (int i = 0; i < this.processers.Count; i++)
                    {
                        this.processers[i].Process();
                    }

                    // TODO: Do all processing here
                    Thread.Sleep(1);
                }

                // Shut down the connection listeners
                this.clientListener.Stop();
                this.queryListener.Stop();
                this.ServerGui.AddStatusText(Resources.NetworkListenersShutDown);

                // Disconnect all clients
                while (this.Clients.Count > 0)
                {
                    this.Clients[0].Dispose();
                    this.Clients.RemoveAt(0);
                    this.ServerGui.UpdatePlayerCount(this.Clients.Count);
                }

                // Close database connection
                // TODO: We didn't actually close anything...
                this.ServerGui.AddStatusText(Resources.DatabaseManagerConnectionCloseSuccess);

                // Successfully shut down everything
                this.ServerGui.AddStatusText(Resources.ServerSuccessfulShutdown);
            }
            catch (InvalidOperationException)
            {
                this.ServerGui.AddStatusText(Resources.DatabaseManagerConnectionFailed);
            }

            // Wait 3 seconds, destroy the GUI then fully exit the environment
            Thread.Sleep(3000);
            this.ServerGui.Dispose();
            Environment.Exit(Environment.ExitCode);
        }

        /// <summary>
        /// Terminates the server - should be called to clean up before exiting the program.
        /// </summary>
        public void Exit()
        {
            this.Running = false;
        }

        /// <summary>
        /// Gets a list of banned IPs from the database that should be blocked.
        /// </summary>
        /// <returns>See summary.</returns>
        public ReadOnlyCollection<long> GetBannedIps()
        {
            var bannedAddresses = from p in this.DbManager.BlockedAddresses select p;
            Collection<long> bannedLong = new Collection<long>();
            foreach (var banned in bannedAddresses)
            {
                bannedLong.Add(banned.Address);
            }

            return new ReadOnlyCollection<long>(bannedLong);
        }

        /// <summary>
        /// Removes all disconnected players from the list of connected ones.
        /// </summary>
        private void CleanDisconnectedPlayers()
        {
            Collection<PlayerHandler> removePlayers = new Collection<PlayerHandler>();
            for (int i = 0; i < this.Clients.Count; i++)
            {
                if (this.Clients[i].Disposed)
                {
                    removePlayers.Add(this.Clients[i]);
                }
            }

            for (int i = 0; i < removePlayers.Count; i++)
            {
                this.Clients.Remove(removePlayers[i]);
                this.ServerGui.UpdatePlayerCount(this.Clients.Count);
            }
        }
    }
}
