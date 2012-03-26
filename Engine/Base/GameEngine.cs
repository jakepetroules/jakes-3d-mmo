namespace MMO3D.Engine
{
    using System;
    using System.Collections.ObjectModel;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using OpenTK;
    using OpenTK.Graphics;
    using Petroules.Synteza.Windows.Forms;

    /// <summary>
    /// The MMO3D game engine.
    /// </summary>
    public sealed class GameEngine : IDisposable
    {
        /// <summary>
        /// The GraphicsDeviceManager associated with this instance.
        /// </summary>
        private GraphicsDeviceManager graphicsDeviceManager;

        /// <summary>
        /// The GraphicsDevice associated with this instance.
        /// </summary>
        private GraphicsDevice graphicsDevice;

        /// <summary>
        /// The frame counter.
        /// </summary>
        private FrameCounter frameCounter;

        /// <summary>
        /// Picking class for checking intersection of objects with the mouse.
        /// </summary>
        private ObjectPicking objectPicking;

        /// <summary>
        /// The previous form border style of the fake fullscreen window.
        /// </summary>
        private System.Windows.Forms.FormBorderStyle oldBorderStyle;

        /// <summary>
        /// The previous window state of the fake fullscreen window.
        /// </summary>
        private System.Windows.Forms.FormWindowState oldWindowState;

        /// <summary>
        /// Initializes a new instance of the GameEngine class.
        /// </summary>
        /// <param name="device">The GraphicsDevice to manage rendering.</param>
        /// <param name="contentManager">The ContentManager for the content pipeline to load content from.</param>
        /// <exception cref="System.ArgumentNullException">GraphicsDevice is null -or- ContentManager is null.</exception>
        public GameEngine(GraphicsDevice device, PackFileContentManager contentManager)
            : this(contentManager)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }

            this.GraphicsDevice = device;
        }

        /// <summary>
        /// Initializes a new instance of the GameEngine class.
        /// </summary>
        /// <param name="deviceManager">The GraphicsDeviceManager to manage rendering.</param>
        /// <param name="contentManager">The ContentManager for the content pipeline to load content from.</param>
        /// <exception cref="System.ArgumentNullException">GraphicsDeviceManager is null -or- ContentManager is null.</exception>
        public GameEngine(GraphicsDeviceManager deviceManager, PackFileContentManager contentManager)
            : this(contentManager)
        {
            if (deviceManager == null)
            {
                throw new ArgumentNullException("deviceManager");
            }

            this.GraphicsDeviceManager = deviceManager;
        }

        /// <summary>
        /// Initializes a new instance of the GameEngine class.
        /// </summary>
        public GameEngine()
        {
            EngineManager.Engine = this;
        }

        /// <summary>
        /// Initializes a new instance of the GameEngine class.
        /// </summary>
        /// <param name="content">The ContentManager for the content pipeline to load content from.</param>
        /// <exception cref="System.ArgumentNullException">ContentManager is null.</exception>
        private GameEngine(PackFileContentManager content)
            : this()
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            this.Content = content;
            this.FontCreator = new SpriteFontCreator(content.ServiceProvider);
        }

        /// <summary>
        /// Gets the current rendering speed of the game in frames per second.
        /// </summary>
        /// <value>See summary.</value>
        public int Fps
        {
            get { return this.frameCounter.Fps; }
        }

        /// <summary>
        /// Gets or sets the GraphicsDeviceManager associated with this instance.
        /// Setting this property sets the GraphicsDevice to null and the engine
        /// will automatically use the GraphicsDeviceManager's GraphicsDevice.
        /// </summary>
        /// <value>See summary.</value>
        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get
            {
                return this.graphicsDeviceManager;
            }

            set
            {
                this.graphicsDeviceManager = value;
                this.graphicsDevice = null;

                if (this.SpriteBatch == null)
                {
                    this.SpriteBatch = new SpriteBatch(value.GraphicsDevice);
                }
            }
        }

        /// <summary>
        /// Gets or sets the GraphicsDevice associated with this instance.
        /// </summary>
        /// <value>
        /// This returns the GraphicsDevice of the GraphicsDeviceManager IF
        /// it is not null, otherwise it returns the GraphicsDevice set by
        /// the constructor.
        /// Setting this property sets the GraphicsDeviceManager to null.
        /// </value>
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return this.graphicsDeviceManager != null ? this.graphicsDeviceManager.GraphicsDevice : this.graphicsDevice;
            }

            set
            {
                this.graphicsDeviceManager = null;
                this.graphicsDevice = value;

                if (this.SpriteBatch == null)
                {
                    this.SpriteBatch = new SpriteBatch(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the ContentManager associated with this instance.
        /// </summary>
        /// <value>See summary.</value>
        public PackFileContentManager Content
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the sprite batch used for drawing 2D sprites.
        /// </summary>
        /// <value>See summary.</value>
        public SpriteBatch SpriteBatch
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to render the game in full screen.
        /// </summary>
        /// <value>See summary.</value>
        /// <exception cref="System.InvalidOperationException">Thrown if there is no GraphicsDeviceManager present.</exception>
        public bool FullScreen
        {
            get
            {
                if (this.graphicsDeviceManager == null)
                {
                    throw new InvalidOperationException(Resources.NoDeviceForFullscreen);
                }
                else
                {
                    return this.graphicsDeviceManager.IsFullScreen;
                }
            }

            set
            {
                if (this.graphicsDeviceManager == null)
                {
                    throw new InvalidOperationException(Resources.NoDeviceForFullscreen);
                }
                else
                {
                    this.graphicsDeviceManager.IsFullScreen = value;
                    this.graphicsDeviceManager.ApplyChanges();
                }
            }
        }

        /// <summary>
        /// Gets the SpriteFontCreator, used to create fonts.
        /// </summary>
        /// <value>See summary.</value>
        public SpriteFontCreator FontCreator
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a list of fonts used by the game engine.
        /// </summary>
        /// <value>See summary.</value>
        public Collection<SpriteFont> Fonts
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a list of effects used by the game engine.
        /// </summary>
        /// <value>See summary.</value>
        public Collection<Effect> Effects
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a list of cameras used by the game engine.
        /// </summary>
        /// <value>See summary.</value>
        public Collection<Camera> Cameras
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the current camera being used by the game engine.
        /// </summary>
        /// <value>See summary.</value>
        public Camera CurrentCamera
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the terrain associated with this instance.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainManager Terrain
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the local player playing the game.
        /// </summary>
        /// <value>See summary.</value>
        public Player MainPlayer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of other players playing the game.
        /// </summary>
        /// <value>See summary.</value>
        public PlayerCollection Players
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the background color to clear to between frame draws.
        /// </summary>
        /// <value>See summary.</value>
        public Color ClearColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the engine has been initialized.
        /// </summary>
        /// <value>See summary.</value>
        public bool EngineInitialized
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw picked triangles.
        /// </summary>
        /// <value>See summary.</value>
        public bool DrawPickedTriangles
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the color to use to draw picked triangles.
        /// </summary>
        /// <value>See summary.</value>
        public Color DrawPickedTrianglesColor
        {
            get { return this.objectPicking.DrawColor; }
            set { this.objectPicking.DrawColor = value; }
        }

        /// <summary>
        /// Gets the objects currently being picked by the mouse.
        /// </summary>
        /// <value>See summary.</value>
        public ReadOnlyCollection<GameObjectBase> PickedObjects
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a collection of ALL game objects currently managed by the engine.
        /// This includes players, objects, etc... every object that is of a class
        /// inheriting from GameObjectBase.
        /// </summary>
        /// <value>See summary.</value>
        public ReadOnlyCollection<GameObjectBase> AllGameObjects
        {
            get
            {
                // Create a collection object
                Collection<GameObjectBase> objectList = new Collection<GameObjectBase>();

                // Add the main player
                objectList.Add(this.MainPlayer);

                // Add all the other players
                for (int i = 0; i < this.Players.Count; i++)
                {
                    objectList.Add(this.Players[i]);
                }

                return new ReadOnlyCollection<GameObjectBase>(objectList);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the client is rendering in fake full-screen.
        /// </summary>
        /// <value>See summary.</value>
        public bool InFakeFullScreen
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the fake full-screen form.
        /// </summary>
        /// <value>See summary.</value>
        public System.Windows.Forms.Form FakeFullScreenForm
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the engine is in editor mode.
        /// </summary>
        /// <value>See summary.</value>
        public bool InEditorMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a compass showing which way the user is facing.
        /// </summary>
        /// <value>See summary.</value>
        public Compass Compass
        {
            get;
            set;
        }

        /// <summary>
        /// Exits fake full-screen mode. This method is thread-safe.
        /// </summary>
        /// <returns>Whether the operation succeeded.</returns>
        public bool GoFakeWindowed()
        {
            if (this.InFakeFullScreen)
            {
                bool succeeded = true;

                try
                {
                    DisplayDevice.Default.RestoreResolution();
                }
                catch (GraphicsModeException)
                {
                    succeeded = false;
                }

                this.FakeFullScreenForm.SetWindowState(this.oldWindowState);
                this.FakeFullScreenForm.SetFormBorderStyle(this.oldBorderStyle);

                this.InFakeFullScreen = false;

                return succeeded;
            }

            return true;
        }

        /// <summary>
        /// Enters fake full-screen mode. This method is thread-safe.
        /// </summary>
        /// <param name="resolution">The screen resolution to enter fake fullscreen mode in.</param>
        /// <returns>Whether the operation succeeded.</returns>
        public bool GoFakeFullScreen(DisplayResolution resolution)
        {
            bool succeeded = true;

            try
            {
                DisplayDevice.Default.ChangeResolution(resolution);
            }
            catch (GraphicsModeException)
            {
                succeeded = false;
            }

            if (!this.InFakeFullScreen && succeeded)
            {
                this.oldBorderStyle = this.FakeFullScreenForm.GetFormBorderStyle();
                this.FakeFullScreenForm.SetFormBorderStyle(System.Windows.Forms.FormBorderStyle.None);

                this.oldWindowState = this.FakeFullScreenForm.GetWindowState();
                this.FakeFullScreenForm.SetWindowState(System.Windows.Forms.FormWindowState.Maximized);

                this.FakeFullScreenForm.BringToFrontSafe();

                this.InFakeFullScreen = true;
            }

            return succeeded;
        }

        /// <summary>
        /// Initializes the engine. Should be called in <c>Game.LoadContent()</c>.
        /// </summary>
        public void Initialize()
        {
            if (!this.EngineInitialized)
            {
                this.Cameras = new Collection<Camera>();
                this.ClearColor = Color.SkyBlue;
                this.Effects = new Collection<Effect>();
                this.Fonts = new Collection<SpriteFont>();
                this.Players = new PlayerCollection();
                this.CurrentCamera = new ChaseCamera(null);
                this.Terrain = new TerrainManager();

                if (this.GraphicsDevice != null)
                {
                    this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);
                }

                this.PickedObjects = new ReadOnlyCollection<GameObjectBase>(new Collection<GameObjectBase>());
                this.objectPicking = new ObjectPicking();
                this.frameCounter = new FrameCounter();
                this.Compass = new Compass();

                this.EngineInitialized = true;
            }
        }

        /// <summary>
        /// Sets properties part of our default render-state.
        /// </summary>
        public void SetDefaultRenderState()
        {
            RenderState rs = this.GraphicsDevice.RenderState;

            rs.AlphaBlendEnable = true;
            rs.SourceBlend = Blend.SourceAlpha;
            rs.DestinationBlend = Blend.InverseSourceAlpha;
            rs.DepthBufferEnable = true;

            rs.CullMode = CullMode.None;
        }

        /// <summary>
        /// Allows the engine to run logic such as updating
        /// the world, checking for collisions, gathering input, and playing audio.
        /// </summary>
        public void Update(Point mousePosition)
        {
            this.CurrentCamera.Update();

            for (int i = 0; i < this.Cameras.Count; i++)
            {
                this.Cameras[i].Update();
            }

            this.PickedObjects = this.objectPicking.GetPickedObjects(this.CurrentCamera, mousePosition);
        }

        /// <summary>
        /// This is called when the engine should draw itself.
        /// </summary>
        public void Draw()
        {
            if (this.EngineInitialized)
            {
                this.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, this.ClearColor, 1, 0);

                // Draw players first so they won't be obscured by transparent terrain
                this.MainPlayer.Draw(this.CurrentCamera);
                for (int i = 0; i < this.Players.Count; i++)
                {
                    this.Players[i].Draw(this.CurrentCamera);
                }

                this.Terrain.Draw();

                // Draw triangles last because they should be on top of EVERYTHING
                if (this.DrawPickedTriangles)
                {
                    this.objectPicking.DrawPickedTriangle(this.CurrentCamera);
                }

                this.frameCounter.Update();
            }
        }

        /// <summary>
        /// Prepares to draw 2D objects.
        /// </summary>
        public void BeginDraw2D()
        {
            this.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.SaveState);
        }

        /// <summary>
        /// Draws the 2D objects.
        /// </summary>
        public void Draw2D()
        {
            this.Compass.Draw(this.SpriteBatch);
        }

        /// <summary>
        /// Finishes drawing the 2D objects.
        /// </summary>
        public void EndDraw2D()
        {
            this.SpriteBatch.End();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        private void Dispose(bool disposing)
        {
            this.EngineInitialized = false;

            if (disposing)
            {
                this.SpriteBatch.Dispose();
                this.Terrain.Dispose();
                this.FontCreator.Dispose();
            }
        }
    }
}
