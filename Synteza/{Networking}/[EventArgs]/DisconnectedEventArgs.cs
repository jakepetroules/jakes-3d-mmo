namespace Petroules.Synteza.Networking
{
    using System;

    /// <summary>
    /// Provides data for the <see cref="NetworkClient.Disconnected"/> event.
    /// </summary>
    public sealed class DisconnectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisconnectedEventArgs"/> class.
        /// </summary>
        /// <param name="type">The type of connection termination that occurred.</param>
        /// <param name="exception">The exception detailing the reason for the connection termination, if any.</param>
        public DisconnectedEventArgs(ConnectionTerminationType type, Exception exception)
        {
            this.ConnectionTerminationType = type;
            this.Exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisconnectedEventArgs"/> class.
        /// </summary>
        /// <param name="type">The type of connection termination that occurred.</param>
        /// <param name="authenticationResult">The authentication result, if any.</param>
        public DisconnectedEventArgs(ConnectionTerminationType type, AuthenticationResult authenticationResult)
        {
            this.ConnectionTerminationType = type;
            this.AuthenticationResult = authenticationResult;
        }

        /// <summary>
        /// Gets the type of connection termination that occurred.
        /// </summary>
        public ConnectionTerminationType ConnectionTerminationType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the authentication result, if any.
        /// </summary>
        public AuthenticationResult AuthenticationResult
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception detailing the reason for the connection termination, if any.
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }
    }
}
