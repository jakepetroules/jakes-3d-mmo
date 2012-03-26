namespace Petroules.Synteza.Networking
{
    /// <summary>
    /// The status of the connection.
    /// </summary>
    public enum ConnectionStatus
    {
        /// <summary>
        /// Already connected.
        /// </summary>
        AlreadyConnected,

        /// <summary>
        /// Connection succeeded.
        /// </summary>
        Connected,

        /// <summary>
        /// Connection failed.
        /// </summary>
        Failed,
    }
}
