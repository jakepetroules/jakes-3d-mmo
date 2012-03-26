namespace MMO3D.NetworkInterface
{
    using System;
    using System.Collections.Specialized;

    /// <summary>
    /// Defines text commands that are sent through the chat box to perform various actions.
    /// </summary>
    public sealed class ServerCommand
    {
        /// <summary>
        /// The string preceding a command to differentiate between commands and chat.
        /// </summary>
        public const string CommandPrefix = "::";

        /// <summary>
        /// Prevents a default instance of the ServerCommand class from being created.
        /// </summary>
        private ServerCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServerCommand class.
        /// </summary>
        /// <param name="commandName">The name of the command.</param>
        private ServerCommand(string commandName)
            : this()
        {
            this.CommandName = commandName;
            this.Parameters = new StringCollection();
        }

        /// <summary>
        /// Initializes a new instance of the ServerCommand class.
        /// </summary>
        /// <param name="commandName">The name of the command.</param>
        /// <param name="parameters">The parameters of the command.</param>
        private ServerCommand(string commandName, params string[] parameters)
            : this(commandName)
        {
            this.Parameters.AddRange(parameters);
        }

        /// <summary>
        /// Gets or sets the name of the command.
        /// </summary>
        /// <value>See summary.</value>
        public string CommandName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the parameters of the command.
        /// </summary>
        /// <value>See summary.</value>
        public StringCollection Parameters
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the given text is a command.
        /// </summary>
        /// <param name="text">The string to check.</param>
        /// <returns>Whether the string is a command.</returns>
        public static bool IsCommand(string text)
        {
            return text.StartsWith(ServerCommand.CommandPrefix, StringComparison.Ordinal) && text.Length > ServerCommand.CommandPrefix.Length;
        }

        /// <summary>
        /// Parses a command object from a text string.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <returns>A command object, or null if the string was not a command.</returns>
        public static ServerCommand Parse(string text)
        {
            if (ServerCommand.IsCommand(text))
            {
                string[] pieces = text.Split(new string[] { ServerCommand.CommandPrefix, " " }, StringSplitOptions.RemoveEmptyEntries);

                if (pieces.Length > 1)
                {
                    ServerCommand cmd = new ServerCommand(pieces[0], pieces);
                    cmd.Parameters.RemoveAt(0);

                    return cmd;
                }
                else if (pieces.Length > 0)
                {
                    return new ServerCommand(pieces[0]);
                }
            }

            return null;
        }
    }
}
