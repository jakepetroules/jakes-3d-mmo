namespace MMO3D.Client
{
    using System;
    using System.Threading;
    using OpenTK.Graphics;
    using MMO3D.NetworkInterface;
    using OpenTK;

    /// <summary>
    /// The settings window of the MMO3D client GUI.
    /// </summary>
    public partial class SettingsWindow : Window
    {
        /// <summary>
        /// The interface manager associated with this instance.
        /// </summary>
        private InterfaceManager interfaceManager;

        /// <summary>
        /// Initializes a new instance of the SettingsWindow class.
        /// </summary>
        /// <param name="interfaceManager">The interface manager associated with this instance.</param>
        public SettingsWindow(InterfaceManager interfaceManager)
            : base(interfaceManager.GameEngine, interfaceManager.Network)
        {
            this.InitializeComponent();
            this.Text = "Settings";

            this.interfaceManager = interfaceManager;

            DisplayResolution[] res = DisplayDevice.Default.AvailableResolutions;
            for (int i = 0; i < res.Length; i++)
            {
                this.comboBoxResolutions.Items.Add(res[i]);
            }

            this.comboBoxResolutions.SelectedIndex = 0;
        }

        /// <summary>
        /// Event handler for clicking the windowed button.
        /// Goes into windowed mode.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonWindowed_Click(object sender, EventArgs e)
        {
            this.buttonWindowed.Enabled = false;
            this.buttonFullscreen.Enabled = true;

            if (!this.Engine.GoFakeWindowed())
            {
                this.interfaceManager.AddMessage(MessageType.ImportantSystemMessage, "Failed to properly restore original screen resolution. Please restart the program.");
            }
        }

        /// <summary>
        /// Event handler for clicking the fullscreen button.
        /// Goes into fullscreen mode at the resolution chosen in the combo box.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonFullscreen_Click(object sender, EventArgs e)
        {
            this.buttonWindowed.Enabled = true;
            this.buttonFullscreen.Enabled = false;

            DisplayResolution res = (DisplayResolution)this.comboBoxResolutions.SelectedItem;

            if (!this.Engine.GoFakeFullScreen(res))
            {
                this.interfaceManager.AddMessage(MessageType.ImportantSystemMessage, "Failed to change screen resolution.");
            }
            else
            {
                // TODO: We don't want to ALWAYS cancel it!
                Thread.Sleep(5000);
                this.ButtonWindowed_Click(this, e);
            }
        }

        /// <summary>
        /// Event handler for changing the selected screen resolution.
        /// Changes the current fullscreen resolution if the application is in fullscreen.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ComboBoxResolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Engine.InFakeFullScreen)
            {
                this.ButtonFullscreen_Click(this, e);
            }
        }
    }
}
