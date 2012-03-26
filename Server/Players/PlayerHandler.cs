namespace MMO3D.Server
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Transactions;
    using System.Net.Sockets;
    using System.Threading;
    using Microsoft.Xna.Framework;
    using Petroules.Synteza.Networking;
    using MMO3D.CommonCode;
    using MMO3D.Engine;
    using MMO3D.NetworkInterface;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;

    /// <summary>
    /// Defines a class to handle a player client.
    /// </summary>
    public sealed class PlayerHandler : Engine.Player, IDisposable
    {
        /// <summary>
        /// Number of seconds to wait before automatically disconnecting the client after initiating the connection.
        /// </summary>
        private const int LoginPromptTimeout = 60;

        /// <summary>
        /// Number of ticks a client must wait before logging in if they have failed to login X times in a row.
        /// A tick is 1/10,000,000th of a second.
        /// </summary>
        private const long LoginWaitTime = 600L * 1000 * 10000;

        /// <summary>
        /// The maximum number of incorrect login tries before the <strong>IP</strong> is locked for X minutes.
        /// </summary>
        private const int MaxAttemptsIp = 3;

        /// <summary>
        /// The maximum number of incorrect login tries before the <strong>account</strong> is locked indefinitely.
        /// </summary>
        private const int MaxAttemptsAll = 75;

        /// <summary>
        /// The time that the client connected to the server.
        /// </summary>
        private readonly long connectionTimeUTC;

        /// <summary>
        /// A reference to the server object.
        /// </summary>
        private GameServer server;

        /// <summary>
        /// A value indicating whether or not the client has logged in.
        /// </summary>
        private volatile bool loggedIn = false;

        /// <summary>
        /// Whether the object's listener thread is running.
        /// </summary>
        private volatile bool running = false;

        /// <summary>
        /// Initializes a new instance of the PlayerHandler class.
        /// </summary>
        /// <param name="server">A reference to the server object.</param>
        /// <param name="connection">The TCP client object describing the client's binding to the server.</param>
        public PlayerHandler(GameServer server, NetworkServerHandler connection)
            : base()
        {
            lock (this)
            {
                this.server = server;
                this.Connection = connection;
                this.PlayerHostName = connection.Connection.Client.RemoteEndPoint.ToString();
                this.connectionTimeUTC = DateTime.UtcNow.Ticks;

                this.Id = -1;
                this.PasswordHash = string.Empty;
                this.UserLevel = UserPermissionLevel.Regular;
                this.Inventory = new PlayerInventory();
                this.ObjectsSeen = new Collection<GameObjectBase>();

                new Thread(this.Run).Start();
            }
        }

        /// <summary>
        /// Gets the object describing the client's binding to the server.
        /// </summary>
        /// <value>See summary.</value>
        public NetworkServerHandler Connection
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the host name of the client.
        /// </summary>
        /// <value>See summary.</value>
        public string PlayerHostName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether or not the client has logged in.
        /// </summary>
        /// <value>See summary.</value>
        public bool LoggedIn
        {
            get
            {
                return this.loggedIn;
            }

            private set
            {
                this.loggedIn = value;

                if (value)
                {
                    this.Save();
                    this.HadLoggedIn = value;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the client had logged in at all.
        /// </summary>
        /// <value>See summary.</value>
        public bool HadLoggedIn
        {
            get;
            private set;
        }

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
        /// Gets the list of the game objects seen by this player.
        /// The client will build up the same list.
        /// </summary>
        /// <value>See summary.</value>
        public Collection<GameObjectBase> ObjectsSeen
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the player's unique user ID.
        /// </summary>
        /// <value>See summary.</value>
        public long Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the player's encrypted password.
        /// </summary>
        /// <value>See summary.</value>
        public string PasswordHash
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user's rank.
        /// </summary>
        /// <value>See summary.</value>
        public UserPermissionLevel UserLevel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reason for the user's account being disabled, if it is.
        /// </summary>
        /// <value>See summary.</value>
        public DisableReason DisableReason
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the UTC time that the user last tried to log on.
        /// </summary>
        /// <value>See summary.</value>
        public long LastLogOnAttempt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the UTC time that the user last successfully logged on.
        /// </summary>
        /// <value>See summary.</value>
        public long LastLogOnSuccess
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of attempted logins since the last successful login.
        /// </summary>
        /// <value>See summary.</value>
        public int AttemptedLogOnCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last IP the user tried to log on from.
        /// </summary>
        /// <value>See summary.</value>
        public long LastLogOnAttemptIP
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last IP the user successfully logged in from.
        /// </summary>
        /// <value>See summary.</value>
        public long LastLogOnSuccessIP
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the player's inventory.
        /// </summary>
        /// <value>See summary.</value>
        public PlayerInventory Inventory
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the player's password.
        /// </summary>
        /// <param name="password">The password, in plain text, to set.</param>
        public void SetPassword(string password)
        {
            this.PasswordHash = SimpleCryptography.MD5(password);
        }

        /// <summary>
        /// Sends a packet to the client.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        /// <returns>Whether the sending succeeded.</returns>
        public bool SendPacket(Packet packet)
        {
            return this.Connection.SendPacket(packet);
        }

        /// <summary>
        /// Obtains information about a player.
        /// If the information could not be retrieved, false is returned.
        /// </summary>
        /// <param name="id">The ID of the player to obtain information about.</param>
        /// <returns>True if the player information was set successfully, false if not.</returns>
        public bool Load(long id)
        {
            var schema = (from p in GameServer.Instance.DbManager.Players where p.ID == id select p).SingleOrDefault();
            if (schema != null)
            {
                try
                {
                    this.UserName = schema.UserName;
                    this.PasswordHash = schema.Password;
                    this.UserLevel = (UserPermissionLevel)schema.UserLevel;
                    this.DisableReason = (DisableReason)schema.DisableReason;

                    // Do not use the property, it sets the HadLoggedIn property to true and we don't want to do that yet
                    this.loggedIn = schema.LoggedIn;

                    this.LastLogOnAttempt = schema.LastLogOnAttempt;
                    this.LastLogOnSuccess = schema.LastLogOnSuccess;
                    this.LastLogOnAttemptIP = schema.LastLogOnAttemptIP;
                    this.LastLogOnSuccessIP = schema.LastLogOnSuccessIP;
                    this.AttemptedLogOnCount = schema.LogOnAttempts;
                    this.Position = new Vector3(schema.PositionX, schema.PositionY, schema.PositionZ);
                    this.RotationDegrees = new Vector3(schema.RotationX, schema.RotationY, schema.RotationZ);

                    /*ReadOnlyCollection<InstanceItemsSchema> itemSchema = GameServer.Instance.DbManager.Tables.InstanceItemsTable.SelectAllFor(id);
                    if (itemSchema != null)
                    {
                        this.Inventory = new PlayerInventory();
                        
                        // TODO: We must load inventory using our special system
                        this.Inventory = null;
                    }
                    else
                    {
                        return false;
                    }*/
                }
                catch (KeyNotFoundException knf)
                {
                    Console.WriteLine(knf);
                    return false;
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe);
                    return false;
                }

                // This we do last just to show that if anything went wrong,
                // we don't have a valid ID so it may be easier to catch errors
                this.Id = id;

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Obtains information about a player.
        /// If the information could not be retrieved, false is returned.
        /// </summary>
        /// <param name="userName">The user name of the player to obtain information about.</param>
        /// <returns>True if the player information was set successfully, false if not.</returns>
        public bool Load(string userName)
        {
            var pid = (from p in GameServer.Instance.DbManager.Players where p.UserName == userName select p).SingleOrDefault();

            if (pid != null && pid.ID > -1)
            {
                return this.Load(pid.ID);
            }

            return false;
        }

        /// <summary>
        /// Logs into a player account. If the player is already signed in, this simply returns true.
        /// </summary>
        /// <param name="userName">The user name of the client player.</param>
        /// <param name="password">The password of the client player.</param>
        /// <returns>Whether the login was successful.</returns>
        public bool ClientLogOn(string userName, string password)
        {
            lock (this)
            {
                bool loadedData = this.Load(userName);

                // Only try to login if the player hasn't already
                if (!this.LoggedIn)
                {
                    // If a player data file was not successfully loaded, destroy the connection
                    if (!loadedData)
                    {
                        PacketSender.Send(AuthenticationResultPacket.FailedInvalidUserName, this);
                        this.Dispose();
                        return false;
                    }

                    // Get the MD5 of the player's password
                    string passwordHash = SimpleCryptography.MD5(password);

                    // If X minutes have passed, reset the attempted login count
                    if ((DateTime.UtcNow.Ticks - this.LastLogOnAttempt) > PlayerHandler.LoginWaitTime)
                    {
                        this.AttemptedLogOnCount = 0;
                    }

                    // Set last login attempt time to now
                    this.LastLogOnAttempt = DateTime.UtcNow.Ticks;
                    this.LastLogOnAttemptIP = NetworkUtilities.GetRemoteIP(this.Connection.Connection.Client);

                    // If there's not been too many wrong logins in a row and player is not banned
                    if (this.AttemptedLogOnCount < PlayerHandler.MaxAttemptsIp && this.UserLevel != UserPermissionLevel.Disabled)
                    {
                        // If the password supplied by the login is equal to the password stored in the database, continue
                        if (passwordHash.ToUpperInvariant() == this.PasswordHash.ToUpperInvariant())
                        {
                            // Set the player logged in to true, player is now logged in!
                            this.LoggedIn = true;

                            // Reset the attempted logins since player is now authenticated
                            if ((DateTime.UtcNow.Ticks - this.LastLogOnAttempt) > PlayerHandler.LoginWaitTime)
                            {
                                this.AttemptedLogOnCount = 0;
                            }

                            // Set the login success time and IP
                            this.LastLogOnSuccess = DateTime.UtcNow.Ticks;
                            this.LastLogOnSuccessIP = NetworkUtilities.GetRemoteIP(this.Connection.Connection.Client);

                            // Tell the client his or her login was successful and tell other users he or she logged in
                            PacketSender.Send(AuthenticationResultPacket.Success, this);

                            InventoryPacket inv = new InventoryPacket(this.Inventory);
                            OrientationPacket ori = new OrientationPacket(this);
                            PacketSender.Send(new IntroductionPacket(inv, ori, this.server.Clients.ToArray()), this);

                            PacketSender.SendToAll(new PlayerLoggedPacket(this, true));
                        }
                        else
                        {
                            // Wrong password, increment login attempts and send message informing the
                            // player that an incorrect password has been supplied, then terminate the connection
                            this.AttemptedLogOnCount++;
                            PacketSender.Send(AuthenticationResultPacket.FailedInvalidPassword, this);
                            this.Dispose();
                            return false;
                        }
                    }
                    else
                    {
                        // Too many wrong logins or player banned... increment attempted logins
                        this.AttemptedLogOnCount++;

                        if (this.AttemptedLogOnCount >= PlayerHandler.MaxAttemptsAll)
                        {
                            // If the player attempted to login far too many times set their user level
                            // to banned (if it wasn't already), send message telling player that their
                            // account has been disabled, and destroy the connection
                            this.UserLevel = UserPermissionLevel.Disabled;
                            this.DisableReason = DisableReason.ExcessLogins;
                            PacketSender.Send(AuthenticationResultPacket.FailedAccountDisabled, this);
                            this.Dispose();
                            return false;
                        }
                        else if (this.UserLevel == UserPermissionLevel.Disabled)
                        {
                            // The player is banned - deny 'em
                            PacketSender.Send(AuthenticationResultPacket.FailedAccountDisabled, this);
                            this.Dispose();
                            return false;
                        }
                        else if (this.AttemptedLogOnCount >= PlayerHandler.MaxAttemptsIp)
                        {
                            // Otherwise, if they only attempted to login a few too many times,
                            // tell them that they have tried to login too many times and will
                            // have to wait a while before trying again
                            PacketSender.Send(AuthenticationResultPacket.FailedTooManyIncorrectLogins, this);
                            this.Dispose();
                            return false;
                        }
                    }
                }
                else
                {
                    // Tell the client he's already logged in
                    PacketSender.Send(AuthenticationResultPacket.AlreadyLoggedIn, this);
                }

                return this.LoggedIn;
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// Disconnects client, first saving all their data,
        /// then destroying the connection and client handler object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// Disconnects client, first saving all their data,
        /// then destroying the connection and client handler object.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        private void Dispose(bool disposing)
        {
            lock (this)
            {
                // If the client has not already been destroyed...
                if (!this.Disposed)
                {
                    // Only "log out" if the client actually did log in in the first place...
                    // otherwise duplicate logins will be allowed
                    if (this.HadLoggedIn)
                    {
                        this.LoggedIn = false;
                    }

                    // Save player data
                    this.Save();

                    // Destroy the connection connection and send a message to the server GUI
                    if (disposing)
                    {
                        try
                        {
                            this.Connection.Dispose();
                            this.server.ServerGui.AddStatusText(string.Format(CultureInfo.CurrentCulture, Resources.ClientHandlerConnectionTerminated, this.PlayerHostName));
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(Resources.ClientHandlerCloseSocketFailed);
                            Console.WriteLine(e);
                        }
                    }

                    // If the player had signed on at all, tell other players he or she has signed off
                    if (this.HadLoggedIn)
                    {
                        PacketSender.SendToAll(new PlayerLoggedPacket(this, false));
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

                Thread.CurrentThread.Name = string.Format(CultureInfo.CurrentCulture, "{0} Listener Thread", this.PlayerHostName);

                // If the client isn't signed on,
                // check to see if LOGIN_PROMPT_TIMEOUT seconds have passed since the login request was sent,
                // and if so destroy the connection
                Thread timeoutManager = new Thread(new ThreadStart(this.TimeOut));
                timeoutManager.Name = string.Format(CultureInfo.CurrentCulture, "{0} Timeout Manager Thread", this.PlayerHostName);
                timeoutManager.Start();

                this.Connection.PacketReceived += delegate(object sender, PacketEventArgs e)
                {
                    try
                    {
                        PacketProcessor.Process(this.server, this, e.Packet);
                    }
                    catch (ArgumentNullException)
                    {
                        this.Dispose();
                        return;
                    }
                };
            }
        }

        /// <summary>
        /// Saves the player data object to the database and returns whether it was saved successfully.
        /// </summary>
        /// <returns>See summary.</returns>
        private bool Save()
        {
            // Saves the player's data if it was loaded in the first place
            if (this.Id > -1)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        var schema = (from p in GameServer.Instance.DbManager.Players where p.ID == this.Id select p).Single();
                        schema.ID = this.Id;
                        schema.UserName = this.UserName;
                        schema.Password = this.PasswordHash;
                        schema.UserLevel = (int)this.UserLevel;
                        schema.DisableReason = (int)this.DisableReason;
                        schema.LoggedIn = this.LoggedIn;
                        schema.LastLogOnAttempt = this.LastLogOnAttempt;
                        schema.LastLogOnSuccess = this.LastLogOnSuccess;
                        schema.LastLogOnAttemptIP = this.LastLogOnAttemptIP;
                        schema.LastLogOnSuccessIP = this.LastLogOnSuccessIP;
                        schema.LogOnAttempts = this.AttemptedLogOnCount;
                        schema.PositionX = this.Position.X;
                        schema.PositionY = this.Position.Y;
                        schema.PositionZ = this.Position.Z;
                        schema.RotationX = this.RotationDegrees.X;
                        schema.RotationY = this.RotationDegrees.Y;
                        schema.RotationZ = this.RotationDegrees.Z;

                        GameServer.Instance.DbManager.SubmitChanges();
                        // TODO: We must save the player inventory using our special system

                        ts.Complete();

                        return true;
                    }
                }
                catch (SqlException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Waits for an appropriate amount of time,
        /// and if the client has not logged in,
        /// disconnects him or her.
        /// </summary>
        private void TimeOut()
        {
            Thread.Sleep(PlayerHandler.LoginPromptTimeout * 1000);

            if (this.running && !this.Disposed && !this.LoggedIn)
            {
                lock (this)
                {
                    this.Dispose();
                }
            }
        }
    }
}
