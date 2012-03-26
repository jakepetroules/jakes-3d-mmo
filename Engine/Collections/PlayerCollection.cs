namespace MMO3D.Engine
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;

    /// <summary>
    /// A collection of players.
    /// </summary>
    public sealed class PlayerCollection : Collection<Player>
    {
        /// <summary>
        /// Initializes a new instance of the PlayerCollection class.
        /// </summary>
        public PlayerCollection()
            : base()
        {
        }

        /// <summary>
        /// Gets the player with the specified username.
        /// </summary>
        /// <param name="userName">The username of the player to get.</param>
        /// <returns>The player at the specified index.</returns>
        /// <exception cref="System.ArgumentException">Player was not found in the collection.</exception>
        /// <exception cref="System.ArgumentNullException">The userName parameter was null.</exception>
        public Player this[string userName]
        {
            get
            {
                if (userName == null)
                {
                    throw new ArgumentNullException("userName", "Username cannot be null.");
                }

                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i] != null && this[i].UserName != null && this[i].UserName == userName)
                    {
                        return this[i];
                    }
                }

                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Player '{0}' was not found in the collection.", userName));
            }
        }

        /// <summary>
        /// Determines whether an player with the specified username is in the collection.
        /// </summary>
        /// <param name="userName">The username of the player locate in the collection.</param>
        /// <returns>True if the player is found in the collection; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The userName parameter is null.</exception>
        public bool Contains(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("userName", "Username cannot be null.");
            }

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] != null && this[i].UserName != null && this[i].UserName == userName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the player with the specified username from the collection.
        /// </summary>
        /// <param name="userName">The username of the player to remove from the collection.</param>
        /// <returns>
        /// True if item is successfully removed; otherwise, false. This method also
        /// returns false if item was not found in the original collection.
        /// </returns>
        public bool Remove(string userName)
        {
            try
            {
                return this.Remove(this[userName]);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
