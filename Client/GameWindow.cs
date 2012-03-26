namespace MMO3D.Client
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Threading;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Petroules.Synteza.Networking;
    using MMO3D.CommonCode;
    using MMO3D.Engine;
    using MMO3D.NetworkInterface;
    using WinFormsGraphicsDevice;

    /// <summary>
    /// The MMO3D game rendering window.
    /// </summary>
    public partial class GameWindow : GraphicsDeviceControl
    {
        /// <summary>
        /// The interfece manager used to manage the game's interface.
        /// </summary>
        private InterfaceManager interfaceManager;

        /// <summary>
        /// This is the main chase camera used in the game.
        /// </summary>
        private ChaseCamera chaseCamera;

        /// <summary>
        /// The time at which the last movement packet was sent.
        /// </summary>
        private DateTime lastMovementPacket = DateTime.UtcNow;

        /// <summary>
        /// Initializes a new instance of the GameWindow class.
        /// </summary>
        public GameWindow()
        {
        }

        /// <summary>
        /// Gets the game engine used to run the game.
        /// </summary>
        /// <value>See summary.</value>
        public GameEngine Engine
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the network connection to the server.
        /// </summary>
        /// <value>See summary.</value>
        public NetworkClient Network
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control will be drawn.
        /// </summary>
        /// <value>See summary.</value>
        public bool ShouldDraw
        {
            get;
            set;
        }

        /// <summary>
        /// Unhooks the rendering event.
        /// </summary>
        /// <value>See summary.</value>
        public void StopRendering()
        {
            this.Enabled = false;
        }

        /// <summary>
        /// Initializes the game.
        /// </summary>
        protected override void Initialize()
        {
            Thread.CurrentThread.Name = "MMO3D Rendering Thread";

            this.Engine = new GameEngine(this.GraphicsDevice, new PackFileContentManager(new ContentManager(this.Services)));
            this.Engine.FakeFullScreenForm = this.Parent as System.Windows.Forms.Form;
            this.Engine.Initialize();

            this.Engine.Content = new PackFileContentManager(this.Engine.Content.ServiceProvider, "GameData");

            this.Engine.Fonts.Add(this.Engine.FontCreator.CreateFont(new FontDefinition()));

            this.Resize += new EventHandler(this.MMO3DRenderer_Resize);

            this.Engine.MainPlayer = new Player(ExtendedModel.Load(this.Engine.Content, "models.mc->orienter"));
            this.Engine.MainPlayer.DisplayName = "Main Player";

            this.Engine.CurrentCamera = this.chaseCamera = new ChaseCamera(this.Engine.MainPlayer) { CameraDistance = 6, HeightDifference = 1.5f };

            this.Network = new NetworkClient();
            this.Network.PacketReceived += this.Network_Received;

            this.interfaceManager = new InterfaceManager(this.Engine, this.Network, this);
        }

        /// <summary>
        /// Processes user input.
        /// </summary>
        protected void ProcessInput()
        {
            KeyboardState state = Keyboard.GetState();
            Direction movementDirection = Direction.None;
            bool moved = false;

            if (state.IsKeyDown(Keys.Up))
            {
                movementDirection = Direction.Forward;
                moved = true;
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                movementDirection = Direction.Backward;
                moved = true;
            }

            if (state.IsKeyDown(Keys.Left))
            {
                this.Engine.MainPlayer.RotationDegrees = new Vector3(this.Engine.MainPlayer.RotationDegrees.X, this.Engine.MainPlayer.RotationDegrees.Y, this.Engine.MainPlayer.RotationDegrees.Z + 1);
                moved = true;
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                this.Engine.MainPlayer.RotationDegrees = new Vector3(this.Engine.MainPlayer.RotationDegrees.X, this.Engine.MainPlayer.RotationDegrees.Y, this.Engine.MainPlayer.RotationDegrees.Z - 1);
                moved = true;
            }

            if (this.Network.IsLoggedIn && moved && (DateTime.UtcNow - this.lastMovementPacket).TotalSeconds >= 0.5)
            {
                this.Network.SendPacket(new MovementPacket(movementDirection, this.Engine.MainPlayer.RotationDegrees));
                this.lastMovementPacket = DateTime.UtcNow;
            }

            if (state.IsKeyDown(Keys.OemOpenBrackets))
            {
                this.chaseCamera.HeightDifference--;
            }
            else if (state.IsKeyDown(Keys.OemCloseBrackets))
            {
                this.chaseCamera.HeightDifference++;
            }

            if (state.IsKeyDown(Keys.Q))
            {
                if (this.Engine.GraphicsDevice.RenderState.FillMode == FillMode.Solid)
                {
                    this.Engine.GraphicsDevice.RenderState.FillMode = FillMode.WireFrame;
                }
                else if (this.Engine.GraphicsDevice.RenderState.FillMode == FillMode.WireFrame)
                {
                    this.Engine.GraphicsDevice.RenderState.FillMode = FillMode.Point;
                }
                else
                {
                    this.Engine.GraphicsDevice.RenderState.FillMode = FillMode.Solid;
                }
            }
        }

        /// <summary>
        /// Renders the game to the screen.
        /// </summary>
        protected override void Draw()
        {
            this.ProcessInput();

            this.Engine.MainPlayer.SnapToTerrainHeight();

            this.Engine.Update(new Point(MousePosition.X, MousePosition.Y));
            this.Engine.Draw();

            this.Parent.Text = "MMO3D Client - " + this.GraphicsDevice.Viewport.Width + " × " + this.GraphicsDevice.Viewport.Height;

            // Get display names of picked objects
            StringBuilder pickedObjectNames = new StringBuilder();
            for (int i = 0; i < this.Engine.PickedObjects.Count; i++)
            {
                pickedObjectNames.AppendLine(this.Engine.PickedObjects[i].DisplayName);
            }

            this.Engine.Compass.Position = new Vector2(this.Engine.GraphicsDevice.Viewport.Width - this.Engine.Compass.Width - 4, 4);
            this.Engine.Compass.RotationDegrees = this.Engine.MainPlayer.RotationDegrees.Z;

            this.Engine.BeginDraw2D();
            this.Engine.Draw2D();
            this.Engine.SpriteBatch.DrawString(this.Engine.Fonts[0], string.Format(CultureInfo.CurrentCulture, "FPS: {0}\n\nPosition: {1}\nRotation: {2}\nScaling: {3}\n\nMouse is over:\n{4}", this.Engine.Fps, this.Engine.MainPlayer.Position, this.Engine.MainPlayer.RotationDegrees, this.Engine.MainPlayer.Scaling, pickedObjectNames), new Vector2(4, 4), Color.Yellow);
            this.Engine.EndDraw2D();
        }

        /// <summary>
        /// Event handler for render window resizing.
        /// Adjusts the user interface controls to their appropriate positions.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void MMO3DRenderer_Resize(object sender, System.EventArgs e)
        {
            this.interfaceManager.UpdateGuiPositions();
        }

        /// <summary>
        /// Processes packets received from the server.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="data">The packet that was received.</param>
        private void Network_Received(object sender, PacketEventArgs data)
        {
            ChatPacket chatPacket = data.Packet as ChatPacket;
            if (chatPacket != null)
            {
                this.interfaceManager.AddMessage(MessageType.PlayerChat, string.Format(CultureInfo.CurrentCulture, "{0}: {1}", chatPacket.Sender, chatPacket.Chat));
                return;
            }

            ErrorMessagePacket messagePacket = data.Packet as ErrorMessagePacket;
            if (messagePacket != null)
            {
                this.interfaceManager.AddMessage(MessageType.SystemMessage, messagePacket.ErrorMessage);
                return;
            }

            IntroductionPacket introductionPacket = data.Packet as IntroductionPacket;
            if (introductionPacket != null)
            {
                this.Engine.Players.Clear();

                this.Engine.MainPlayer.UserName = introductionPacket.UserName;
                this.Engine.MainPlayer.SetOrientation(
                    introductionPacket.OrientationPacket.Position,
                    introductionPacket.OrientationPacket.Rotation,
                    introductionPacket.OrientationPacket.Scaling);
                this.interfaceManager.InventoryWindow.Inventory = introductionPacket.InventoryPacket.Inventory;

                for (int i = 0; i < introductionPacket.OtherPlayersOrientation.Count; i++)
                {
                    Player player = new Player(ExtendedModel.Load(this.Engine.Content, "models.mc->Orienter"), introductionPacket.OtherPlayersOrientation[i].UserName);
                    player.SetOrientation(
                        introductionPacket.OtherPlayersOrientation[i].Position,
                        introductionPacket.OtherPlayersOrientation[i].Rotation,
                        introductionPacket.OtherPlayersOrientation[i].Scaling);
                    this.Engine.Players.Add(player);
                }

                return;
            }

            InventoryPacket inventoryPacket = data.Packet as InventoryPacket;
            if (inventoryPacket != null)
            {
                this.interfaceManager.InventoryWindow.Inventory = inventoryPacket.Inventory;
                return;
            }

            OrientationPacket orientationPacket = data.Packet as OrientationPacket;
            if (orientationPacket != null)
            {
                if (orientationPacket.UserName == this.Engine.MainPlayer.UserName)
                {
                    this.Engine.MainPlayer.SetOrientation(
                        orientationPacket.Position,
                        orientationPacket.Rotation,
                        orientationPacket.Scaling);
                }
                else
                {
                    if (this.Engine.Players.Contains(orientationPacket.UserName))
                    {
                        this.Engine.Players[orientationPacket.UserName].SetOrientation(
                            orientationPacket.Position,
                            orientationPacket.Rotation,
                            orientationPacket.Scaling);
                    }
                }

                return;
            }

            PlayerLoggedPacket playerLoggedPacket = data.Packet as PlayerLoggedPacket;
            if (playerLoggedPacket != null)
            {
                this.interfaceManager.AddMessage(MessageType.SystemMessage, string.Format(CultureInfo.CurrentCulture, playerLoggedPacket.OnOff ? Resources.PlayerLoggedOn : Resources.PlayerLoggedOff, playerLoggedPacket.UserName));

                if (playerLoggedPacket.UserName != this.Engine.MainPlayer.UserName)
                {
                    if (playerLoggedPacket.OnOff)
                    {
                        Player player = new Player(ExtendedModel.Load(this.Engine.Content, "models.mc->Orienter"), playerLoggedPacket.UserName);
                        player.SetOrientation(
                            playerLoggedPacket.Orientation.Position,
                            playerLoggedPacket.Orientation.Rotation,
                            playerLoggedPacket.Orientation.Scaling);
                        this.Engine.Players.Add(player);
                    }
                    else
                    {
                        this.Engine.Players.Remove(playerLoggedPacket.UserName);
                    }
                }

                return;
            }
        }
    }
}
