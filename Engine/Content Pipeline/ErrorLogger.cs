//-----------------------------------------------------------------------------
// ErrorLogger.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
namespace MMO3D.Engine
{
    using System.Collections.Specialized;
    using Microsoft.Build.Framework;

    /// <summary>
    /// Custom implementation of the MSBuild ILogger interface records
    /// content build errors so we can later display them to the user.
    /// </summary>
    public class ErrorLogger : ILogger
    {
        /// <summary>
        /// Initializes a new instance of the ErrorLogger class.
        /// </summary>
        public ErrorLogger()
        {
            this.Errors = new StringCollection();
            this.Verbosity = LoggerVerbosity.Normal;
        }

        /// <summary>
        /// Gets a list of all the errors that have been logged.
        /// </summary>
        /// <value>See summary.</value>
        public StringCollection Errors
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the user-defined parameters of the logger.
        /// </summary>
        /// <value>See summary.</value>
        public string Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the level of detail to show in the event log.
        /// </summary>
        /// <value>See summary.</value>
        public LoggerVerbosity Verbosity
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes the custom logger, hooking the ErrorRaised notification event.
        /// </summary>
        /// <param name="eventSource">The events available to loggers.</param>
        public void Initialize(IEventSource eventSource)
        {
            if (eventSource != null)
            {
                eventSource.ErrorRaised += this.ErrorRaised;
            }
        }

        /// <summary>
        /// Shuts down the custom logger.
        /// </summary>
        public void Shutdown()
        {
        }

        /// <summary>
        /// Handles error notification events by storing the error message string.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ErrorRaised(object sender, BuildErrorEventArgs e)
        {
            this.Errors.Add(e.Message);
        }
    }
}
