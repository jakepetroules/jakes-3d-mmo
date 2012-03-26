namespace MMO3D.WorldEditor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;
    using MMO3D.Engine;

    /// <summary>
    /// Defines a dialog allowing the user to add an existing terrain patch to the world map.
    /// </summary>
    public partial class AddExistingPatchDialog : Form
    {
        /// <summary>
        /// The default patch coordinates upon launching of this dialog.
        /// </summary>
        private Point3D defaultPatchCoordinates;

        /// <summary>
        /// Initializes a new instance of the AddExistingPatchDialog class.
        /// </summary>
        /// <param name="position">The position of the camera.</param>
        /// <param name="heightLevel">The terrain height level we are working with.</param>
        public AddExistingPatchDialog(Vector3 position, int heightLevel)
        {
            this.InitializeComponent();

            this.defaultPatchCoordinates = TerrainManager.GetTerrainPatchId(position, heightLevel);
            this.PatchCoordinates = this.defaultPatchCoordinates;
        }

        /// <summary>
        /// Gets the terrain patch loaded by the dialog.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainPatch TerrainPatch
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the patch coordinates to add a terrain patch to.
        /// </summary>
        /// <value>See summary.</value>
        private Point3D PatchCoordinates
        {
            get
            {
                return new Point3D((int)this.numericUpDownX.Value, (int)this.numericUpDownY.Value, (int)this.numericUpDownZ.Value);
            }

            set
            {
                this.numericUpDownX.Value = (int)value.X;
                this.numericUpDownY.Value = (int)value.Y;
                this.numericUpDownZ.Value = (int)value.Z;
            }
        }

        /// <summary>
        /// Allows the user to browse for a terrain patch file.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Terrain Patch file (*.pat)|*.pat";
            if (open.ShowDialog() == DialogResult.OK)
            {
                TerrainPatch tp = TerrainPatch.FromByteArray(File.ReadAllBytes(open.FileName));
                if (tp != null)
                {
                    this.textBoxFile.Text = open.FileName;

                    if (this.radioButtonAuto.Checked)
                    {
                        this.PatchCoordinates = tp.PatchId;
                    }

                    this.TerrainPatch = tp;
                }
                else
                {
                    MessageBox.Show("Unable to read terrain patch file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
                }
            }
        }

        /// <summary>
        /// Enables or disables the patch ID selectors depending on the patch ID selection method (auto or manual).
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void RadioButtonAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (this.TerrainPatch != null)
            {
                this.PatchCoordinates = this.TerrainPatch.PatchId;
            }

            this.numericUpDownX.Enabled = this.radioButtonManual.Checked;
            this.numericUpDownY.Enabled = this.radioButtonManual.Checked;
            this.numericUpDownZ.Enabled = this.radioButtonManual.Checked;

            this.buttonBottomCenter.Enabled = this.radioButtonManual.Checked;
            this.buttonBottomLeft.Enabled = this.radioButtonManual.Checked;
            this.buttonBottomRight.Enabled = this.radioButtonManual.Checked;

            this.buttonCenter.Enabled = this.radioButtonManual.Checked;
            this.buttonMiddleLeft.Enabled = this.radioButtonManual.Checked;
            this.buttonMiddleRight.Enabled = this.radioButtonManual.Checked;

            this.buttonTopCenter.Enabled = this.radioButtonManual.Checked;
            this.buttonTopLeft.Enabled = this.radioButtonManual.Checked;
            this.buttonTopRight.Enabled = this.radioButtonManual.Checked;
        }

        /// <summary>
        /// Dismisses the dialog and confirms the user pressed OK.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.TerrainPatch = TerrainPatch.FromExisting(this.PatchCoordinates, this.TerrainPatch);
        }

        /// <summary>
        /// Sets the coordinates relative to the center tile.
        /// </summary>
        /// <param name="sender">The offset button that was clicked.</param>
        /// <param name="e">The event arguments.</param>
        private void PatchIDButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                try
                {
                    if (this.radioButtonManual.Checked)
                    {
                        Point offsets = TerrainManager.Offsets[Convert.ToInt32(button.Tag.ToString(), CultureInfo.InvariantCulture)];
                        this.PatchCoordinates = MathExtensions.AddPoints(this.defaultPatchCoordinates, new Point3D(offsets.X, offsets.Y, 0));
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Internal error: button does not contain a valid integer offset tag.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
                }
            }
        }
    }
}
