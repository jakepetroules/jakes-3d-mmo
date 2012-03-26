namespace MMO3D.Client
{
    using MMO3D.Engine;
    using MMO3D.NetworkInterface;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// The arts window of the MMO3D client GUI.
    /// </summary>
    public partial class ArtsWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the ArtsWindow class.
        /// </summary>
        /// <param name="engine">The game engine associated with this instance.</param>
        /// <param name="network">The network connection manager used to connect to the network.</param>
        public ArtsWindow(GameEngine engine, NetworkClient network)
            : base(engine, network)
        {
            this.InitializeComponent();
            this.Text = "Arts";
        }
    }
}
