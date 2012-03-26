namespace MMO3D.WorldEditor
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Reflection;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework.Graphics;
    using MMO3D.Engine;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Event handlers section of the MMO3D World Editor main GUI form.
    /// </summary>
    public partial class WorldEditorForm
    {
        /// <summary>
        /// Occurs when the world editor main form loads.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void WorldEditorForm_Load(object sender, EventArgs e)
        {
            Application.Idle += delegate { this.Refresh3D(); };

            this.treeViewGameObjects.Nodes.Add("GameEngine", "GameEngine");
            Type gameEngine = this.editorRenderWindow.Engine.GetType();
            PropertyInfo[] properties = gameEngine.GetProperties();
            FieldInfo[] fields = gameEngine.GetFields(BindingFlags.NonPublic | BindingFlags.Public);

            for (int i = 0; i < properties.Length; i++)
            {
                this.treeViewGameObjects.Nodes["GameEngine"].Nodes.Add(properties[i].Name);
            }

            for (int i = 0; i < fields.Length; i++)
            {
                this.treeViewGameObjects.Nodes["GameEngine"].Nodes.Add(fields[i].Name);
            }

            this.terrainControl.LoadControl();
        }

        /// <summary>
        /// Draws the game and updates the user interface.
        /// </summary>
        private void Refresh3D()
        {
            this.editorRenderWindow.Invalidate();
            this.UpdateGUI();
        }

        /// <summary>
        /// Updates the user interface - called whenever the game window is redrawn.
        /// </summary>
        private void UpdateGUI()
        {
            this.UpdateRenderingToolbar();

            // Set the camera info on the status bar
            this.toolStripStatusLabelCamera.Text = string.Format(CultureInfo.InvariantCulture, "Camera: Position = {0}, Target = {1}; Cursor's 2D Position = {2}, Cursor's 3D Position = {3}", this.editorRenderWindow.Engine.CurrentCamera.Position, this.editorRenderWindow.Engine.CurrentCamera.Target, this.editorRenderWindow.GameMousePosition, this.editorRenderWindow.PickedPosition);
        }

        /// <summary>
        /// Updates the rendering toolbar.
        /// </summary>
        private void UpdateRenderingToolbar()
        {
            RenderState render = this.editorRenderWindow.Engine.GraphicsDevice.RenderState;

            // Set the fill mode buttons
            this.toolStripButtonSolid.Checked = render.FillMode == FillMode.Solid;
            this.toolStripButtonPoint.Checked = render.FillMode == FillMode.Point;
            this.toolStripButtonWireframe.Checked = render.FillMode == FillMode.WireFrame;

            // Set the current FPS
            this.toolStripLabelFPS.Text = string.Format(CultureInfo.InvariantCulture, Resources.FramesPerSecond, this.editorRenderWindow.Engine.Fps);

            // TODO: Assumes chase camera
            // Set the camera distance and height difference text boxes, if they aren't being edited currently
            ChaseCamera camera = this.editorRenderWindow.Engine.CurrentCamera as ChaseCamera;
            if (!this.toolStripTextBoxCameraDistance.Focused)
            {
                this.toolStripTextBoxCameraDistance.Text = camera.CameraDistance.ToString(CultureInfo.InvariantCulture);
            }

            if (!this.toolStripTextBoxCameraHeightDifference.Focused)
            {
                this.toolStripTextBoxCameraHeightDifference.Text = camera.HeightDifference.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Occurs when the world editor form resizes.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void WorldEditorForm_Resize(object sender, EventArgs e)
        {
            this.treeViewGameObjects.Height = this.flowLayoutPanelProperties.Height - 309;
        }

        /// <summary>
        /// Occurs when the world editor is closing.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void WorldEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Helper.ExitProgram();
        }

        /// <summary>
        /// Occurs when the fullscreen choices menu item opens.
        /// Populates the menu with submenus allowing the user to select which fullscreen resolution to use.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void FullscreenToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem fullscreenMenu = sender as ToolStripMenuItem;
            if (fullscreenMenu != null)
            {
                fullscreenMenu.DropDownItems.Clear();

                DisplayModeCollection displays = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes;
                IEnumerator<DisplayMode> modes = displays.GetEnumerator();

                List<Size> modesAlreadyAdded = new List<Size>();

                while (modes.MoveNext())
                {
                    DisplayMode mode = modes.Current;
                    Size displaySize = new Size(mode.Width, mode.Height);
                    string menuText = displaySize.Width + " × " + displaySize.Height;

                    ToolStripMenuItem menu = new ToolStripMenuItem(menuText);
                    menu.Click += new EventHandler(this.Menu_Click);

                    if (!modesAlreadyAdded.Contains(displaySize))
                    {
                        fullscreenMenu.DropDownItems.Add(menu);
                        modesAlreadyAdded.Add(displaySize);
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when any of the screen resolutions menus are clicked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            if (menu != null)
            {
                Size size = Helper.ResolutionFromString(menu.Text);
            }
        }

        /// <summary>
        /// Occurs when a node in the objects tree view is clicked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TreeViewGameObjects_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "GameEngine":
                    this.propertyGridGameObjectProperties.SelectedObject = this.editorRenderWindow.Engine;
                    break;
            }
        }
    }
}
