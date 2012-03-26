namespace MMO3D.Client
{
    using System;
    using Petroules.Synteza.Windows.Forms;
    using MMO3D.Engine;
    using MMO3D.NetworkInterface;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// The window allowing a user to send chat and other messages to the server.
    /// </summary>
    public partial class MessageAreaPanel : Window
    {
        /// <summary>
        /// Initializes a new instance of the MessageAreaPanel class.
        /// </summary>
        /// <param name="gameEngine">The game engine associated with this instance.</param>
        /// <param name="network">The network connection manager used to connect to the network.</param>
        public MessageAreaPanel(GameEngine gameEngine, NetworkClient network)
            : base(gameEngine, network)
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Appends a line of text to the chat box. This method is thread-safe.
        /// </summary>
        /// <param name="text">The text to append.</param>
        /// <param name="prependNewLine">Whether to prepend a newline character to the added text.</param>
        public void AppendText(string text, bool prependNewLine)
        {
            string textToAdd = (prependNewLine ? System.Environment.NewLine : string.Empty) + text;

            this.richTextBoxMessageHistory.AppendTextSafe(textToAdd);
        }

        /// <summary>
        /// Appends a line of text to the chat box. This method is thread-safe.
        /// </summary>
        /// <param name="text">The text to append. A newline is prepended to the passed text.</param>
        public void AppendText(string text)
        {
            this.AppendText(text, true);
        }

        /// <summary>
        /// Sends the chat message currently in the text box and clears the text box.
        /// </summary>
        private void SendChat()
        {
            if (!string.IsNullOrEmpty(this.richTextBoxMessageBox.Text))
            {
                if (this.Network != null && this.Network.IsConnected)
                {
                    this.Network.SendPacket(new ChatPacket(this.richTextBoxMessageBox.Text, ChatType.Global));
                    this.richTextBoxMessageBox.Clear();
                }
            }
        }

        /// <summary>
        /// Event handler for the text in the message history box, changing.
        /// Scrolls the text box to the bottom.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            this.richTextBoxMessageHistory.Select(this.richTextBoxMessageHistory.Text.Length, 0);
            this.richTextBoxMessageHistory.ScrollToCaret();
        }

        /// <summary>
        /// Event handler for the send chat button. Simply calls the <see cref="SendChat"/> method.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonSendMessage_Click(object sender, EventArgs e)
        {
            this.SendChat();
        }
    }
}
