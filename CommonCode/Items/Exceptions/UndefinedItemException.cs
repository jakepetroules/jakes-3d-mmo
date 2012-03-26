namespace MMO3D.CommonCode
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an error that means an item with a particular ID or class does not exist.
    /// </summary>
    [Serializable]
    public sealed class UndefinedItemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UndefinedItemException"/> class.
        /// </summary>
        public UndefinedItemException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UndefinedItemException"/> class with an item ID.
        /// </summary>
        /// <param name="itemId">The ID of the item the exception is thrown for.</param>
        public UndefinedItemException(long itemId)
            : this(string.Format(CultureInfo.InvariantCulture, "The item with the ID {0} does not exist.", itemId))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UndefinedItemException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UndefinedItemException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UndefinedItemException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public UndefinedItemException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UndefinedItemException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">The info parameter is null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The class name is null or System.Exception.HResult is zero (0).</exception>
        private UndefinedItemException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
