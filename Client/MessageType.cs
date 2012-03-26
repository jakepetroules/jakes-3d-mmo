namespace MMO3D.Client
{
    /// <summary>
    /// The type of message to insert in the text box.
    /// Different types of messages are colored differently.
    /// TODO: Implement the actual color difference...
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Regular player chat.
        /// </summary>
        PlayerChat,

        /// <summary>
        /// A message from the game.
        /// </summary>
        SystemMessage,

        /// <summary>
        /// An important message from the game.
        /// </summary>
        ImportantSystemMessage
    }
}
