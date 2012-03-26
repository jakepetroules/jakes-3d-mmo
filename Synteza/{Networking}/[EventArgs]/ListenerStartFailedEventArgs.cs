namespace Petroules.Synteza.Networking
{
    using System;

    /// <summary>
    /// Provides data for the <see cref="NetworkServerListener.ListenerFailedStart"/> event.
    /// </summary>
    public sealed class ListenerStartFailedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerStartFailedEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The exception that caused the listener not to start.</param>
        /// <exception cref="ArgumentNullException"><paramref name="exception"/> is <c>null</c>.</exception>
        public ListenerStartFailedEventArgs(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            this.Exception = exception;
        }

        /// <summary>
        /// Gets the exception that caused the listener not to start.
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }
    }
}
