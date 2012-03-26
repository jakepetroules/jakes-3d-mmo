namespace MMO3D.Engine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines a player character.
    /// </summary>
    public class Player : GameObjectBase
    {
        /// <summary>
        /// Initializes a new instance of the Player class.
        /// </summary>
        /// <param name="model">The model to use for this game object.</param>
        public Player(ExtendedModel model)
            : base(model)
        {
            this.UserName = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the Player class.
        /// </summary>
        /// <param name="model">The model to use for this game object.</param>
        /// <param name="userName">The player's username.</param>
        public Player(ExtendedModel model, string userName)
            : this(model)
        {
            this.UserName = userName;
        }

        /// <summary>
        /// Initializes a new instance of the Player class.
        /// This is for special use by the server.
        /// Do not invoke or extend otherwise.
        /// </summary>
        protected Player()
            : this(null)
        {
        }

        /// <summary>
        /// Gets or sets the player's username.
        /// </summary>
        /// <value>See summary.</value>
        public string UserName
        {
            get;
            set;
        }
    }
}
