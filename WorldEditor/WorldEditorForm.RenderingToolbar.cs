namespace MMO3D.WorldEditor
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework.Graphics;
    using MMO3D.Engine;

    /// <summary>
    /// Rendering toolbar event handlers.
    /// </summary>
    public partial class WorldEditorForm
    {
        /// <summary>
        /// Changes the render state to solid, point or wireframe when the corresponding buttons are clicked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripButtonRenderingType_Click(object sender, EventArgs e)
        {
            ToolStripButton tsb = sender as ToolStripButton;
            if (tsb != null)
            {
                RenderState render = this.editorRenderWindow.Engine.GraphicsDevice.RenderState;

                if (tsb == this.toolStripButtonSolid)
                {
                    render.FillMode = FillMode.Solid;
                }
                else if (tsb == this.toolStripButtonPoint)
                {
                    render.FillMode = FillMode.Point;
                }
                else if (tsb == this.toolStripButtonWireframe)
                {
                    render.FillMode = FillMode.WireFrame;
                }
            }
        }

        /// <summary>
        /// Validates the text in the camera properties tool strip text boxes.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripTextBoxCameraDistance_Validating(object sender, CancelEventArgs e)
        {
            ToolStripTextBox textBox = sender as ToolStripTextBox;
            if (textBox != null)
            {
                try
                {
                    Convert.ToSingle(textBox.Text, CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    e.Cancel = true;
                    textBox.SelectAll();
                }
            }
        }

        /// <summary>
        /// Commits the validation of the text in the camera properties tool strip text boxes.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripTextBoxCameraDistance_Validated(object sender, EventArgs e)
        {
            ToolStripTextBox textBox = sender as ToolStripTextBox;
            if (textBox != null)
            {
                // TODO: Assumes a chase camera
                ChaseCamera cam = this.editorRenderWindow.Engine.CurrentCamera as ChaseCamera;
                float val = Convert.ToSingle(textBox.Text, CultureInfo.InvariantCulture);

                if (textBox == this.toolStripTextBoxCameraDistance)
                {
                    cam.CameraDistance = val;
                }
                else if (textBox == this.toolStripTextBoxCameraHeightDifference)
                {
                    cam.HeightDifference = val;
                }
            }
        }

        /// <summary>
        /// Initiates the validation of text in the camera properties tool strip text boxes
        /// when the enter key is pressed while inside the text box, by focusing another control.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripTextBoxCameraDistance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.editorRenderWindow.Focus();
            }
        }
    }
}