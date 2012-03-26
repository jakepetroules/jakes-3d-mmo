namespace MMO3D.WorldEditor
{
    using System;
    using System.Windows.Forms;
    using MMO3D.CommonCode;
    using MMO3D.Engine;

    /// <summary>
    /// Event handlers for the world editor's rendering window.
    /// </summary>
    public partial class WorldEditorForm
    {
        /// <summary>
        /// Whether the mouse button is down.
        /// </summary>
        private bool mouseDown;

        /// <summary>
        /// Shows the terrain cursor when the mouse enters the rendering panel.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void EditorRenderWindow_MouseEnter(object sender, EventArgs e)
        {
            this.editorRenderWindow.Engine.Terrain.Cursor.Show = true;
        }

        /// <summary>
        /// Hides the terrain cursor when the mouse leaves the rendering panel.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void EditorRenderWindow_MouseLeave(object sender, EventArgs e)
        {
            this.editorRenderWindow.Engine.Terrain.Cursor.Show = false;
        }

        /// <summary>
        /// Sets the terrain cursor properties as the mouse moves.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void EditorRenderWindow_MouseMove(object sender, MouseEventArgs e)
        {
            TerrainCursor cursor = this.editorRenderWindow.Engine.Terrain.Cursor;
            cursor.Show = true;
            cursor.Position = MathExtensions.PointToVector3(this.editorRenderWindow.PickedPosition);
            cursor.Size = this.terrainControl.CursorSize;
        }

        /// <summary>
        /// Performs the operation set on the terrain tool control (raise, texture, etc).
        /// This method first always selects the global coordinate that the mouse is picking and updates the terrain tool control.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void EditorRenderWindow_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseDown = true;

            // Keep going until we release the mouse
            while (this.mouseDown)
            {
                // Execute the terrain tool operation
                this.terrainControl.ExecuteTerrainToolOperation(this.editorRenderWindow.PickedPosition);

                // The message queue is NOT empty, so we
                // must manually invalidate the render window
                this.editorRenderWindow.Invalidate();

                // Now we process all events in the queue,
                // namely the MouseUp event, or we get
                // stuck in an infinite loop
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Tells the class that the mouse has been released.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void EditorRenderWindow_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseDown = false;
            this.terrainControl.CommitTerrainChanges();
        }
    }
}