namespace Petroules.Synteza.Networking
{
    /// <summary>
    /// Enumerates reasons for a <see cref="NetworkServerListener"/> having stopped listening for clients.
    /// </summary>
    public enum ListenerStopType
    {
        /// <summary>
        /// The listener was manually stopped.
        /// </summary>
        Manual,

        /// <summary>
        /// The listener stopped listening for clients due to an exception.
        /// </summary>
        Exception
    }
}
