namespace MMO3D.NetworkInterface
{
    using System;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// The type of chat.
    /// </summary>
    [Serializable]
    public enum ChatType
    {
        /// <summary>
        /// Chat with everyone.
        /// </summary>
        Global
    }

    /// <summary>
    /// The chat packet.
    /// </summary>
    [Serializable]
    public sealed class ChatPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the ChatPacket class.
        /// This overload should be used by the client because
        /// the server knows the sender automatically.
        /// </summary>
        /// <param name="chat">The text of the chat message.</param>
        /// <param name="chatType">The type of chat.</param>
        public ChatPacket(string chat, ChatType chatType)
            : this(chat, null, chatType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ChatPacket class.
        /// This overload should be used by the server because
        /// the client needs to know who sent the message.
        /// </summary>
        /// <param name="chat">The text of the chat message.</param>
        /// <param name="sender">The player who sent the chat message.</param>
        /// <param name="chatType">The type of chat.</param>
        public ChatPacket(string chat, string sender, ChatType chatType)
            : base()
        {
            this.Chat = chat;
            this.Sender = sender;
            this.ChatType = chatType;
        }

        /// <summary>
        /// Gets a value indicating whether this is a command.
        /// </summary>
        /// <value>See summary.</value>
        public bool IsCommand
        {
            get { return ServerCommand.IsCommand(this.Chat); }
        }

        /// <summary>
        /// Gets the text of the chat message.
        /// </summary>
        /// <value>See summary.</value>
        public string Chat
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the player who sent the chat message.
        /// </summary>
        /// <value>See summary.</value>
        public string Sender
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of chat.
        /// </summary>
        /// <value>See summary.</value>
        public ChatType ChatType
        {
            get;
            private set;
        }
    }
}
