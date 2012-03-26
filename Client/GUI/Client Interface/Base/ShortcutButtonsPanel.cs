namespace MMO3D.Client
{
    using MMO3D.Engine;
    using MMO3D.NetworkInterface;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// Defines the shortcut buttons panel.
    /// </summary>
    public partial class ShortcutButtonsPanel : Window
    {
        /// <summary>
        /// Initializes a new instance of the ShortcutButtonsPanel class.
        /// </summary>
        /// <param name="engine">The game engine associated with this instance.</param>
        /// <param name="network">The network connection manager used to connect to the network.</param>
        public ShortcutButtonsPanel(GameEngine engine, NetworkClient network)
            : base(engine, network)
        {
            this.InitializeComponent();
        }
    }
}
