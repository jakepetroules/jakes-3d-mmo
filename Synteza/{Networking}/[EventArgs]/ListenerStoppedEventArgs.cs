namespace Petroules.Synteza.Networking
{
    using System;

    /// <summary>
    /// Provides data for the <see cref="NetworkServerListener.ListenerStopped"/> event.
    /// </summary>
    public sealed class ListenerStoppedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerStoppedEventArgs"/> class.
        /// </summary>
        /// <param name="type">The reason the listener was stopped.</param>
        /// <param name="exception">The exception that caused the listener to stop, if any.</param>
        public ListenerStoppedEventArgs(ListenerStopType type, Exception exception)
        {
            this.ListenerStopType = type;
            this.Exception = exception;
        }

        /// <summary>
        /// Gets the reason the listener was stopped.
        /// </summary>
        public ListenerStopType ListenerStopType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception that caused the listener to stop, if any.
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }
    }
}
