namespace MMO3D.Server
{
    using MMO3D.Engine;

    /// <summary>
    /// Defines a door that can be opened or closed.
    /// </summary>
    public sealed class Door : GameObjectBase
    {
        /// <summary>
        /// Indicates whether the door is locked.
        /// </summary>
        private bool locked;

        /// <summary>
        /// Gets or sets a value indicating whether the door is opened.
        /// </summary>
        /// <value>See summary.</value>
        public bool Opened
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the door is locked.
        /// </summary>
        /// <value>See summary.</value>
        public bool Locked
        {
            get
            {
                return this.locked;
            }

            set
            {
                this.locked = value;
                this.Collides = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the object responds to collisions.
        /// </summary>
        /// <value>See summary.</value>
        public new bool Collides
        {
            get
            {
                return base.Collides;
            }

            private set
            {
                base.Collides = value;
            }
        }
    }
}
