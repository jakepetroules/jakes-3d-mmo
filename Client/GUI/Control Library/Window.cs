namespace MMO3D.Client
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using MMO3D.Engine;
    using MMO3D.NetworkInterface;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// Defines a form-like window.
    /// </summary>
    public partial class Window : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the Window class.
        /// </summary>
        /// <param name="engine">The game engine associated with this instance.</param>
        /// <param name="network">The network connection manager used to connect to the network.</param>
        public Window(GameEngine engine, NetworkClient network)
        {
            this.InitializeComponent();
            this.Engine = engine;
            this.Network = network;
            this.componentInitialized = true;
            this.Visible = false;
        }

        /// <summary>
        /// Prevents a default instance of the Window class from being created.
        /// Required for designer support.
        /// </summary>
        private Window()
        {
        }

        /// <summary>
        /// Gets the game engine associated with this instance.
        /// </summary>
        /// <value>See summary.</value>
        public GameEngine Engine
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the network connection manager used to connect to the network.
        /// </summary>
        /// <value>See summary.</value>
        public NetworkClient Network
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a reference to the controls of the "client area" of the form.
        /// </summary>
        /// <remarks>
        /// Gets a reference to the actual UserControl's child controls if the
        /// InitializeComponent method has not finished executing. This prevents
        /// the Control designer from accessing the wrong control collection.
        /// </remarks>
        /// <value>See summary.</value>
        public new ControlCollection Controls
        {
            get { return this.componentInitialized ? this.splitContainer.Panel2.Controls : base.Controls; }
        }

        /// <summary>
        /// Gets or sets the window's title text.
        /// </summary>
        /// <value>See summary.</value>
        public override string Text
        {
            get { return this.labelWindowTitle.Text; }
            set { this.labelWindowTitle.Text = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window's title bar should be visible.
        /// </summary>
        /// <value>See summary.</value>
        public bool ShowTitleBar
        {
            get { return !this.splitContainer.Panel1Collapsed; }
            set { this.splitContainer.Panel1Collapsed = !value; }
        }

        /// <summary>
        /// Aligns the window to a particular edge of the screen.
        /// </summary>
        /// <param name="alignment">The edge of the screen to align the window to.</param>
        public void AlignWindow(WindowAlignment alignment)
        {
            switch (alignment)
            {
                case WindowAlignment.TopLeft:
                    this.Location = Point.Empty;
                    break;
                case WindowAlignment.TopCenter:
                    this.Location = new Point((this.Parent.ClientRectangle.Width - this.Width) / 2, 0);
                    break;
                case WindowAlignment.TopRight:
                    this.Location = new Point(this.Parent.ClientRectangle.Width - this.Width, 0);
                    break;
                case WindowAlignment.MiddleLeft:
                    this.Location = new Point(0, (this.Parent.ClientRectangle.Height - this.Height) / 2);
                    break;
                case WindowAlignment.MiddleCenter:
                    this.Location = new Point((this.Parent.ClientRectangle.Width - this.Width) / 2, (this.Parent.ClientRectangle.Height - this.Height) / 2);
                    break;
                case WindowAlignment.MiddleRight:
                    this.Location = new Point(this.Parent.ClientRectangle.Width - this.Width, (this.Parent.ClientRectangle.Height - this.Height) / 2);
                    break;
                case WindowAlignment.BottomLeft:
                    this.Location = new Point(0, this.Parent.ClientRectangle.Height - this.Height);
                    break;
                case WindowAlignment.BottomCenter:
                    this.Location = new Point((this.Parent.ClientRectangle.Width - this.Width) / 2, this.Parent.ClientRectangle.Height - this.Height);
                    break;
                case WindowAlignment.BottomRight:
                    this.Location = new Point(this.Parent.ClientRectangle.Width - this.Width, this.Parent.ClientRectangle.Height - this.Height);
                    break;
            }
        }

        /// <summary>
        /// Event handler for clicking the close button. Closes the window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
