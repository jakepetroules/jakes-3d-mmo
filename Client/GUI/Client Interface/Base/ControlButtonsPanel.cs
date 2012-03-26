namespace MMO3D.Client
{
    using MMO3D.Engine;
    using MMO3D.NetworkInterface;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// Defines the top control buttons panel,
    /// which allows the user to click buttons opening various interfaces.
    /// </summary>
    public partial class ControlButtonsPanel : Window
    {
        /// <summary>
        /// Initializes a new instance of the ControlButtonsPanel class.
        /// </summary>
        /// <param name="engine">The game engine associated with this instance.</param>
        /// <param name="network">The network connection manager used to connect to the network.</param>
        public ControlButtonsPanel(GameEngine engine, NetworkClient network)
            : base(engine, network)
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the inventory/equipment button.
        /// </summary>
        /// <value>See summary.</value>
        public NoFocusButton ButtonInventory
        {
            get { return this.buttonInventory; }
        }

        /// <summary>
        /// Gets the belt slots button.
        /// </summary>
        /// <value>See summary.</value>
        public NoFocusButton ButtonBelt
        {
            get { return this.buttonBelt; }
        }

        /// <summary>
        /// Gets the arts button.
        /// </summary>
        /// <value>See summary.</value>
        public NoFocusButton ButtonArts
        {
            get { return this.buttonArts; }
        }

        /// <summary>
        /// Gets the character details button.
        /// </summary>
        /// <value>See summary.</value>
        public NoFocusButton ButtonCharacterDetails
        {
            get { return this.buttonCharacterDetails; }
        }

        /// <summary>
        /// Gets the quest journal button.
        /// </summary>
        /// <value>See summary.</value>
        public NoFocusButton ButtonQuestJournal
        {
            get { return this.buttonQuestJournal; }
        }

        /// <summary>
        /// Gets the party management button.
        /// </summary>
        /// <value>See summary.</value>
        public NoFocusButton ButtonParty
        {
            get { return this.buttonParty; }
        }

        /// <summary>
        /// Gets the settings button.
        /// </summary>
        /// <value>See summary.</value>
        public NoFocusButton ButtonSettings
        {
            get { return this.buttonSettings; }
        }

        /// <summary>
        /// Gets the quit button.
        /// </summary>
        /// <value>See summary.</value>
        public NoFocusButton ButtonQuit
        {
            get { return this.buttonQuit; }
        }
    }
}
