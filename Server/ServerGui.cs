namespace MMO3D.Server
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;
    using Petroules.Synteza.Windows.Forms;

    /// <summary>
    /// A GUI to easily view status information about the server and perform various commands on it.
    /// </summary>
    public partial class ServerGui : Form
    {
        /// <summary>
        /// A static reference to this object.
        /// </summary>
        private static ServerGui gui;

        /// <summary>
        /// A reference to the server object.
        /// </summary>
        private GameServer server;

        /// <summary>
        /// Initializes a new instance of the ServerGui class.
        /// </summary>
        /// <param name="server">The server object this instance should store a reference to.</param>
        private ServerGui(GameServer server)
        {
            this.server = server;

            if (this.InvokeRequired)
            {
                this.Invoke((Action)this.InitializeComponent);
            }
            else
            {
                this.InitializeComponent();
            }
        }

        /// <summary>
        /// Creates an instance of ServerGui and begins running a standard application method loop,
        /// then returns a reference to the created instance.
        /// </summary>
        /// <param name="server">The server object this instance should store a reference to.</param>
        /// <returns>See summary.</returns>
        public static ServerGui CreateGui(GameServer server)
        {
            ServerGui.gui = new ServerGui(server);
            new Thread(new ThreadStart(ServerGui.Run)).Start();

            // Block until the GUI's handles have been created to prevent
            // invalid cross-thread control access
            while (!ServerGui.gui.Visible)
            {
                Thread.Sleep(1);
            }

            return ServerGui.gui;
        }

        /// <summary>
        /// Begins running the standard message loop.
        /// </summary>
        public static void Run()
        {
            Thread.CurrentThread.Name = "Server GUI Message Loop";
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(ServerGui.gui);
        }

        /// <summary>
        /// Adds text to the GUI status window.
        /// </summary>
        /// <param name="text">The text to add.</param>
        public void AddStatusText(string text)
        {
            string statusText = string.Format(CultureInfo.InvariantCulture, Resources.GuiOutputMessage, DateTime.UtcNow.ToLongDateString(), DateTime.UtcNow.ToLongTimeString(), text) + System.Environment.NewLine;

            this.richTextBoxMessageLog.AppendTextSafe(statusText);
            this.richTextBoxMessageLog.SetSelectionStart(this.richTextBoxMessageLog.GetText().Length);
            this.richTextBoxMessageLog.ScrollToCaretSafe();
        }

        /// <summary>
        /// Updates the numbers of players connected, on the GUI.
        /// </summary>
        /// <param name="players">The new number of players currently connected.</param>
        public void UpdatePlayerCount(int players)
        {
            this.labelPlayers.SetText(string.Format(CultureInfo.InvariantCulture, Resources.GuiPlayersConnected, players));
        }

        /// <summary>
        /// Prevents the program from being closed "normally" by properly cleaning up resources before closing.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ServerGui_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.ButtonShutDown_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Shuts down the server from the gui.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonShutDown_Click(object sender, EventArgs e)
        {
            // If the server is running, shut it down and disable this button
            if (this.server.Running)
            {
                this.buttonShutDown.Enabled = false;
                this.server.Exit();
            }
        }
    }
}
