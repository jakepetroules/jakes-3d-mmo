namespace Petroules.Synteza.Native
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// The exception that is thrown when an attempt is made to invoke a method that is not available on the current platform.
    /// </summary>
    [Serializable]
    public class UnsupportedPlatformException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedPlatformException"/> class.
        /// </summary>
        /// <param name="supportedPlatforms">The platforms on which the invoked method is supported.</param>
        public UnsupportedPlatformException(params PlatformID[] supportedPlatforms)
            : base()
        {
            this.SupportedPlatforms = new ReadOnlyCollection<PlatformID>(supportedPlatforms);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedPlatformException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="supportedPlatforms">The platforms on which the invoked method is supported.</param>
        public UnsupportedPlatformException(string message, params PlatformID[] supportedPlatforms)
            : base(message)
        {
            this.SupportedPlatforms = new ReadOnlyCollection<PlatformID>(supportedPlatforms);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedPlatformException"/> class
        /// with a specified error message and a reference to the inner exception that is the
        /// cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not a null reference, the current exception is raised in a catch
        /// block that handles the inner exception.
        /// </param>
        /// <param name="supportedPlatforms">The platforms on which the invoked method is supported.</param>
        public UnsupportedPlatformException(string message, Exception innerException, params PlatformID[] supportedPlatforms)
            : base(message, innerException)
        {
            this.SupportedPlatforms = new ReadOnlyCollection<PlatformID>(supportedPlatforms);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedPlatformException"/> class
        /// with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// <param name="supportedPlatforms">The platforms on which the invoked method is supported.</param>
        protected UnsupportedPlatformException(SerializationInfo info, StreamingContext context, params PlatformID[] supportedPlatforms)
            : base(info, context)
        {
            this.SupportedPlatforms = new ReadOnlyCollection<PlatformID>(supportedPlatforms);
        }

        /// <summary>
        /// Gets the platform on which the method invocation failed.
        /// </summary>
        public PlatformID CurrentPlatform
        {
            get { return Environment.OSVersion.Platform; }
        }

        /// <summary>
        /// Gets a collection of the platforms on which the invoked method is supported.
        /// </summary>
        public ReadOnlyCollection<PlatformID> SupportedPlatforms
        {
            get;
            private set;
        }
    }
}
