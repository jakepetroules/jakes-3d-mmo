namespace MMO3D.NetworkInterface
{
    using System;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// Contains an error message.
    /// </summary>
    [Serializable]
    public sealed class ErrorMessagePacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the ErrorMessagePacket class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public ErrorMessagePacket(string errorMessage)
            : base()
        {
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>See summary.</value>
        public string ErrorMessage
        {
            get;
            private set;
        }
    }
}
