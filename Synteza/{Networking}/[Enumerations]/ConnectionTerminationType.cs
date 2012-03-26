namespace Petroules.Synteza.Networking
{
    /// <summary>
    /// The reason the network connection was terminated.
    /// </summary>
    public enum ConnectionTerminationType
    {
        /// <summary>
        /// Failed to create the connection in the first place.
        /// </summary>
        FailedToConnect,

        /// <summary>
        /// The user failed to authenticate with the server.
        /// </summary>
        FailedAuthentication,

        /// <summary>
        /// The user manually terminated the connection.
        /// </summary>
        UserTermination,

        /// <summary>
        /// The connection to the server was lost on the server side.
        /// </summary>
        ServerOffline
    }
}
