namespace MMO3D.Client
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Net;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using Petroules.Synteza.Networking;
    using Petroules.Synteza.Windows.Forms;
    using MMO3D.CommonCode;
    using MMO3D.Engine;
    using MMO3D.NetworkInterface;

    /// <summary>
    /// Defines the MMO3D log-on window.
    /// </summary>
    public partial class LogOnWindow : UserControl
    {
        /// <summary>
        /// A reference to the game engine associated with this instance.
        /// </summary>
        private GameEngine engine;

        /// <summary>
        /// The network connection manager used to connect to the network.
        /// </summary>
        private NetworkClient network;

        /// <summary>
        /// Network connection thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// Initializes a new instance of the LogOnWindow class.
        /// </summary>
        /// <param name="engine">A reference to the game engine associated with this instance.</param>
        /// <param name="network">The network connection manager used to connect to the network.</param>
        public LogOnWindow(GameEngine engine, NetworkClient network)
        {
            this.InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.engine = engine;
            this.network = network;

            this.PopulateServerList();
        }

        /// <summary>
        /// Displays the control to the user. This method is thread-safe.
        /// </summary>
        public new void Show()
        {
            this.SetLoginStatus(string.Empty);

            if (this.InvokeRequired)
            {
                this.Invoke((Action)base.Show);
            }
            else
            {
                base.Show();
            }
        }

        /// <summary>
        /// Conceals the control from the user. This method is thread-safe.
        /// </summary>
        public new void Hide()
        {
            this.SetLoginStatus(string.Empty);

            if (this.InvokeRequired)
            {
                this.Invoke((Action)base.Hide);
            }
            else
            {
                base.Hide();
            }

            this.BlankPassword();
        }

        /// <summary>
        /// Login screen resized event handler. Centers the login panel in the middle of the login screen.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void LoginScreen_Resize(object sender, EventArgs e)
        {
            this.panel.Location = new Point((this.Size.Width - this.panel.Width) / 2, (this.Height - this.panel.Height) / 2);
        }

        /// <summary>
        /// Login button event handler. Attempts to connect to the server and log in.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NoFocusButtonLogin_Click(object sender, EventArgs e)
        {
            if (this.thread == null || (!this.thread.IsAlive && this.thread.ThreadState == ThreadState.Stopped))
            {
                this.EnableControls(false);

                (this.thread = new Thread(new ThreadStart(this.Connect))).Start();
                this.thread.Name = "Logon Screen Network Thread";
            }
        }

        /// <summary>
        /// Exit button event handler. Exits the game.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NoFocusButtonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Refresh server list link label event handler. Refreshes the server list.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void LinkLabelRefreshServerList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.PopulateServerList();
        }

        /// <summary>
        /// Does the actual work of attempting to connect to the server and logging in.
        /// </summary>
        private void Connect()
        {
            lock (this)
            {
                try
                {
                    if (this.dataGridViewServers.SelectedRows.Count == 1)
                    {
                        if (this.dataGridViewServers.SelectedRows[0].Cells["ColumnStatus"].Value.ToString() == "Offline")
                        {
                            this.SetLoginStatus(Resources.CannotConnectOfflineServer);
                            return;
                        }

                        this.SetLoginStatus(Resources.ConnectionStatusConnecting);

                        string serverAddress = (string)this.dataGridViewServers.Invoke((Func<string>)(() => this.dataGridViewServers.SelectedRows[0].Cells["ColumnServerAddress"].Value.ToString()));

                        ConnectionStatus result = this.network.Connect(serverAddress, Network.DefaultPort);
                        switch (result)
                        {
                            case ConnectionStatus.Connected:
                                {
                                    this.SetLoginStatus(Resources.ConnectionStatusConnected);

                                    string username = this.textBoxUsername.GetText();
                                    string password = this.maskedTextBoxPassword.GetText();

                                    AuthenticationResult status = this.network.Authenticate(username, password);
                                    switch (status)
                                    {
                                        case AuthenticationResult.Success:
                                            this.SetLoginStatus(Resources.LogOnResultSuccess);
                                            break;
                                        case AuthenticationResult.AlreadyAuthenticated:
                                            this.SetLoginStatus(Resources.LogOnResultAlreadyLoggedIn);
                                            break;
                                        case AuthenticationResult.FailedInvalidUserName:
                                            this.SetLoginStatus(Resources.LogOnResultFailedInvalidUserName);
                                            break;
                                        case AuthenticationResult.FailedInvalidPassword:
                                            this.SetLoginStatus(Resources.LogOnResultFailedInvalidPassword);
                                            break;
                                        case AuthenticationResult.FailedExceededAuthenticationAttempts:
                                            this.SetLoginStatus(Resources.LogOnResultFailedTooManyIncorrectLogins);
                                            break;
                                        case AuthenticationResult.FailedAccountDisabled:
                                            this.SetLoginStatus(Resources.LogOnResultFailedAccountDisabled);
                                            break;
                                        case AuthenticationResult.FailedUnknown:
                                            this.SetLoginStatus(Resources.LogOnResultFailedUnknown);
                                            break;
                                    }
                                }

                                break;
                            case ConnectionStatus.Failed:
                                this.SetLoginStatus(Resources.ConnectionStatusFailed);
                                break;
                            case ConnectionStatus.AlreadyConnected:
                                this.SetLoginStatus(Resources.ConnectionStatusAlreadyConnected);
                                break;
                        }
                    }
                    else
                    {
                        this.SetLoginStatus(Resources.ConnectionStatusSelectAServer);
                    }
                }
                finally
                {
                    this.EnableControls(true);
                }
            }
        }

        /// <summary>
        /// Populates the server list with the available servers.
        /// </summary>
        private void PopulateServerList()
        {
            try
            {
                this.dataGridViewServers.Rows.Clear();

                XmlDocument document = new XmlDocument();
                document.LoadXml(new WebClient().DownloadString(WebResourceUrls.ServerListUrl));

                for (int i = 0; i < document.ChildNodes.Count; i++)
                {
                    for (int j = 0; j < document.ChildNodes[i].ChildNodes.Count; j++)
                    {
                        DataGridViewRow row = this.dataGridViewServers.Rows[this.dataGridViewServers.Rows.Add()];

                        for (int k = 0; k < document.ChildNodes[i].ChildNodes[j].ChildNodes.Count; k++)
                        {
                            XmlNode node = document.ChildNodes[i].ChildNodes[j].ChildNodes[k];

                            string cell = string.Empty;

                            switch (node.Name)
                            {
                                case "image":
                                    row.Cells["ColumnFlag"].Value = this.imageListFlags.Images[node.InnerText];
                                    break;
                                case "location":
                                    cell = "ColumnLocation";
                                    break;
                                case "players":
                                    cell = "ColumnPlayers";
                                    break;
                                case "number":
                                    row.Cells["ColumnServer"].Value = "Server " + node.InnerText;
                                    break;
                                case "address":
                                    cell = "ColumnServerAddress";
                                    break;
                                case "status":
                                    row.Cells["ColumnStatus"].Value = Convert.ToInt32(node.InnerText, CultureInfo.InvariantCulture) == 1 ? "Online" : "Offline";
                                    break;
                            }

                            if (!string.IsNullOrEmpty(cell))
                            {
                                row.Cells[cell].Value = node.InnerText;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.dataGridViewServers.Rows.Clear();
            }
        }

        /// <summary>
        /// Blanks out the user's password. This method is thread-safe.
        /// </summary>
        private void BlankPassword()
        {
            this.maskedTextBoxPassword.ClearSafe();
        }

        /// <summary>
        /// Synchronously sets the current login status. This method is thread-safe.
        /// </summary>
        /// <param name="text">The text to show on the connection status label.</param>
        private void SetLoginStatus(string text)
        {
            this.labelConnectionStatus.SetText(text);
        }

        /// <summary>
        /// Enables or disables all controls that receive user input.
        /// </summary>
        /// <param name="enable">True to enable all controls, false to disable all controls.</param>
        private void EnableControls(bool enable)
        {
            Control[] controls = new Control[] { this.textBoxUsername, this.maskedTextBoxPassword, this.dataGridViewServers, this.noFocusButtonLogin, this.noFocusButtonExit };

            for (int i = 0; i < controls.Length; i++)
            {
                controls[i].SetEnabled(enable);
            }
        }
    }
}
