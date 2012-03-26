namespace MMO3D.WorldEditor
{
    using System;
    using System.Threading;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using MMO3D.CommonCode;
    using MMO3D.Engine;
    using WinFormsGraphicsDevice;
    using Microsoft.Xna.Framework.Content;

    /// <summary>
    /// The class used to render the game in the editor.
    /// </summary>
    public class EditorRenderWindow : GraphicsDeviceControl
    {
        /// <summary>
        /// This is the main chase camera used in the game.
        /// </summary>
        private ChaseCamera chaseCamera;

        /// <summary>
        /// Initializes a new instance of the EditorRenderWindow class.
        /// </summary>
        public EditorRenderWindow()
        {
        }

        /// <summary>
        /// Gets the game engine to run the game with.
        /// </summary>
        /// <value>See summary.</value>
        public GameEngine Engine
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the speed at which the camera moves.
        /// </summary>
        /// <value>See summary.</value>
        public float CameraMovementSpeed
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the speed at which the camera is rotated.
        /// </summary>
        /// <value>See summary.</value>
        public float CameraRotationSpeed
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the picked position of the terrain.
        /// </summary>
        /// <value>See summary.</value>
        public Point PickedPosition
        {
            get;
            private set;
        }

        public Point GameMousePosition
        {
            get
            {
                var temp = PointToClient(MousePosition);
                return new Point(temp.X - 17, temp.Y - 17); // Why -17?
            }
        }

        /// <summary>
        /// Initializes the renderer.
        /// </summary>
        protected override void Initialize()
        {
            this.CameraMovementSpeed = DefaultProperties.CameraMovementSpeed;
            this.CameraRotationSpeed = DefaultProperties.CameraRotationSpeed;

            Thread.CurrentThread.Name = "MMO3D Rendering Thread";

            this.Engine = new GameEngine(this.GraphicsDevice, new PackFileContentManager(new ContentManager(this.Services)));
            this.Engine.InEditorMode = true;
            this.Engine.Initialize();

            this.Engine.MainPlayer = new Player(null);
            this.Engine.MainPlayer.DisplayName = "Terrain Explorer Object";

            this.chaseCamera = new ChaseCamera(this.Engine.MainPlayer);
            this.chaseCamera.CameraDistance = 15;
            this.chaseCamera.HeightDifference = 6;

            this.Engine.CurrentCamera = this.chaseCamera;
        }

        /// <summary>
        /// Processes user input.
        /// </summary>
        protected void ProcessInput()
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Up))
            {
                this.Engine.MainPlayer.MoveForwardBack(this.Engine.MainPlayer, this.CameraMovementSpeed, Direction.Forward, true);
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                this.Engine.MainPlayer.MoveForwardBack(this.Engine.MainPlayer, this.CameraMovementSpeed, Direction.Backward, true);
            }

            if (state.IsKeyDown(Keys.Left))
            {
                this.Engine.MainPlayer.RotationDegrees = new Vector3(this.Engine.MainPlayer.RotationDegrees.X, this.Engine.MainPlayer.RotationDegrees.Y, this.Engine.MainPlayer.RotationDegrees.Z + this.CameraRotationSpeed);
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                this.Engine.MainPlayer.RotationDegrees = new Vector3(this.Engine.MainPlayer.RotationDegrees.X, this.Engine.MainPlayer.RotationDegrees.Y, this.Engine.MainPlayer.RotationDegrees.Z - this.CameraRotationSpeed);
            }

            if (state.IsKeyDown(Keys.OemOpenBrackets))
            {
                this.chaseCamera.HeightDifference--;
            }
            else if (state.IsKeyDown(Keys.OemCloseBrackets))
            {
                this.chaseCamera.HeightDifference++;
            }
        }

        /// <summary>
        /// Updates and draws the game.
        /// </summary>
        protected override void Draw()
        {
            Engine.Terrain.Cursor.Position = MathExtensions.PointToVector3(PickedPosition);

            this.ProcessInput();

            this.Engine.MainPlayer.SnapToTerrainHeight();

            this.PickedPosition = MathExtensions.VectorToPoint(MathExtensions.TruncateVector(Picking.GetPickedPosition(this.Engine.CurrentCamera, GameMousePosition)));
            this.Engine.Update(GameMousePosition);
            this.Engine.Draw();

            this.Engine.Compass.Position = new Vector2(this.Engine.GraphicsDevice.Viewport.Width - this.Engine.Compass.Width - 4, 4);
            this.Engine.Compass.RotationDegrees = this.Engine.MainPlayer.RotationDegrees.Z;

            this.Engine.BeginDraw2D();
            this.Engine.Draw2D();
            this.Engine.EndDraw2D();
        }
    }
}