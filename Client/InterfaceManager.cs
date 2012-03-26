namespace MMO3D.Client
{
    using System;
    using System.Windows.Forms;
    using MMO3D.Engine;
using MMO3D.NetworkInterface;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// The GUI management section of the MMO3D class.
    /// </summary>
    public class InterfaceManager
    {
        /// <summary>
        /// Whether the GUI has been initialized.
        /// </summary>
        private bool guiInitialized;

        /// <summary>
        /// The control that is the parent of the user interface.
        /// </summary>
        private GameWindow parentControl;

        /// <summary>
        /// Initializes a new instance of the InterfaceManager class.
        /// </summary>
        /// <param name="gameEngine">The game engine associated with this instance.</param>
        /// <param name="network">The network connection manager used to connect to the network.</param>
        /// <param name="parentControl">The control that is the parent of the user interface.</param>
        public InterfaceManager(GameEngine gameEngine, NetworkClient network, GameWindow parentControl)
        {
            this.GameEngine = gameEngine;
            this.Network = network;
            this.parentControl = parentControl;

            // Create the login screen
            this.parentControl.Controls.Add(this.LogOnWindow = new LogOnWindow(this.GameEngine, this.Network));

            // Set up the control buttons panel
            this.parentControl.Controls.Add(this.ControlButtonsPanel = new ControlButtonsPanel(this.GameEngine, this.Network));

            // Hook button events for control buttons panel
            this.ControlButtonsPanel.ButtonInventory.Click += new EventHandler(this.ControlButton_Click);
            this.ControlButtonsPanel.ButtonBelt.Click += new EventHandler(this.ControlButton_Click);
            this.ControlButtonsPanel.ButtonArts.Click += new EventHandler(this.ControlButton_Click);
            this.ControlButtonsPanel.ButtonCharacterDetails.Click += new EventHandler(this.ControlButton_Click);
            this.ControlButtonsPanel.ButtonQuestJournal.Click += new EventHandler(this.ControlButton_Click);
            this.ControlButtonsPanel.ButtonParty.Click += new EventHandler(this.ControlButton_Click);
            this.ControlButtonsPanel.ButtonSettings.Click += new EventHandler(this.ControlButton_Click);
            this.ControlButtonsPanel.ButtonQuit.Click += new EventHandler(this.ControlButtonQuit_Click);

            // Create inventory window
            this.parentControl.Controls.Add(this.InventoryWindow = new InventoryWindow(this.GameEngine, this.Network));
            this.ControlButtonsPanel.ButtonInventory.Tag = this.InventoryWindow;

            // Create magic window
            this.parentControl.Controls.Add(this.ArtsWindow = new ArtsWindow(this.GameEngine, this.Network));
            this.ControlButtonsPanel.ButtonArts.Tag = this.ArtsWindow;

            // Create settings window
            this.parentControl.Controls.Add(this.SettingsWindow = new SettingsWindow(this));
            this.ControlButtonsPanel.ButtonSettings.Tag = this.SettingsWindow;

            // Set up the shortcut buttons panel
            this.parentControl.Controls.Add(this.ShortcutButtonsPanel = new ShortcutButtonsPanel(this.GameEngine, this.Network));

            // Set up the message area panel
            this.parentControl.Controls.Add(this.MessageAreaPanel = new MessageAreaPanel(this.GameEngine, this.Network));

            // Set the main GUI to be visible
            this.MainGuiVisible = true;

            // Show the login screen when we've disconnected
            this.Network.Authenticated += this.Network_LoggedIn;
            this.Network.Disconnected += this.Network_Disconnected;

            // Note that we've finished initializing the GUI,
            // and update the windows' positions on the screen
            this.guiInitialized = true;
            this.UpdateGuiPositions();
        }

        /// <summary>
        /// Gets the game engine associated with this instance.
        /// </summary>
        /// <value>See summary.</value>
        public GameEngine GameEngine
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the network connection manager used to connect to the network.
        /// </summary>
        /// <value>See summary.</value>
        public NetworkClient Network
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the log-on window.
        /// </summary>
        /// <value>See summary.</value>
        public LogOnWindow LogOnWindow
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the control buttons (top) panel.
        /// </summary>
        /// <value>See summary.</value>
        public ControlButtonsPanel ControlButtonsPanel
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the shortcut buttons (bottom-right) panel.
        /// </summary>
        /// <value>See summary.</value>
        public ShortcutButtonsPanel ShortcutButtonsPanel
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the message area (bottom-left) panel.
        /// </summary>
        /// <value>See summary.</value>
        public MessageAreaPanel MessageAreaPanel
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the inventory window.
        /// </summary>
        /// <value>See summary.</value>
        public InventoryWindow InventoryWindow
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the arts window.
        /// </summary>
        /// <value>See summary.</value>
        public ArtsWindow ArtsWindow
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the settings window.
        /// </summary>
        /// <value>See summary.</value>
        public SettingsWindow SettingsWindow
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the main GUI is visible.
        /// </summary>
        /// <value>See summary.</value>
        public bool MainGuiVisible
        {
            get
            {
                return this.ControlButtonsPanel.Visible &&
                    this.ShortcutButtonsPanel.Visible &&
                    this.MessageAreaPanel.Visible;
            }

            set
            {
                this.ControlButtonsPanel.Visible = value;
                this.ShortcutButtonsPanel.Visible = value;
                this.MessageAreaPanel.Visible = value;
            }
        }

        /// <summary>
        /// Adds a line of text to the message history box.
        /// </summary>
        /// <param name="message">The type of message to add.</param>
        /// <param name="messageText">The text to add.</param>
        public void AddMessage(MessageType message, string messageText)
        {
            this.MessageAreaPanel.AppendText(messageText);
        }

        /// <summary>
        /// Updates the positions of the static control panels on the screen.
        /// </summary>
        public void UpdateGuiPositions()
        {
            if (this.guiInitialized)
            {
                this.ControlButtonsPanel.AlignWindow(WindowAlignment.TopCenter);
                this.ShortcutButtonsPanel.AlignWindow(WindowAlignment.BottomRight);
                this.MessageAreaPanel.AlignWindow(WindowAlignment.BottomLeft);

                this.InventoryWindow.AlignWindow(WindowAlignment.MiddleCenter);
                this.ArtsWindow.AlignWindow(WindowAlignment.MiddleCenter);
                this.SettingsWindow.AlignWindow(WindowAlignment.MiddleCenter);
            }
        }

        /// <summary>
        /// Logged in event handler. Hides the login screen.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Network_LoggedIn(object sender, EventArgs e)
        {
            this.parentControl.ShouldDraw = true;

            if (!this.LogOnWindow.IsDisposed)
            {
                this.LogOnWindow.Hide();
            }
        }

        /// <summary>
        /// Network connection terminated event handler. Shows the login screen.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Network_Disconnected(object sender, EventArgs e)
        {
            this.parentControl.ShouldDraw = false;

            if (!this.LogOnWindow.IsDisposed)
            {
                this.LogOnWindow.Show();
            }
        }

        /// <summary>
        /// Event handler for the control buttons' click events.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ControlButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                Window win = btn.Tag as Window;
                if (win != null)
                {
                    win.AlignWindow(WindowAlignment.MiddleCenter);
                    win.Visible = true;
                }
            }
        }

        /// <summary>
        /// Event handler for the quit button's click event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ControlButtonQuit_Click(object sender, EventArgs e)
        {
            this.Network.Disconnect();
        }
    }
}
