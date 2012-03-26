namespace MMO3D.WorldEditor
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;
    using MMO3D.Engine;

    /// <summary>
    /// Defines a dialog used to choose to coordinates for the location at which to add a new terrain patch.
    /// </summary>
    public partial class AddNewPatchDialog : Form
    {
        /// <summary>
        /// The default patch coordinates upon launching of this dialog.
        /// </summary>
        private Point3D defaultPatchCoordinates;

        /// <summary>
        /// Initializes a new instance of the AddNewPatchDialog class.
        /// </summary>
        /// <param name="position">The position of the camera.</param>
        /// <param name="heightLevel">The terrain height level we are working with.</param>
        public AddNewPatchDialog(Vector3 position, int heightLevel)
        {
            this.InitializeComponent();

            this.defaultPatchCoordinates = TerrainManager.GetTerrainPatchId(position, heightLevel);
            this.PatchCoordinates = this.defaultPatchCoordinates;

            this.comboBoxTerrainType.Items.AddRange(TerrainTypeExtensions.GetSortedList());
            this.comboBoxTerrainType.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets or sets the patch coordinates to add a terrain patch to.
        /// </summary>
        /// <value>See summary.</value>
        public Point3D PatchCoordinates
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
        /// Gets or sets the default height for all vertices of the terrain.
        /// </summary>
        /// <value>See summary.</value>
        public float DefaultHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default terrain type for all vertices of the terrain.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainType DefaultTerrainType
        {
            get;
            set;
        }

        /// <summary>
        /// Dismisses the dialog and confirms the user pressed OK.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.DefaultHeight = Convert.ToSingle(this.numericUpDownDefaultTerrainHeight.Value);
            this.DefaultTerrainType = TerrainTypeExtensions.ParseFromString(this.comboBoxTerrainType.SelectedItem.ToString());
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
                    Point offsets = TerrainManager.Offsets[Convert.ToInt32(button.Tag.ToString(), CultureInfo.InvariantCulture)];
                    this.PatchCoordinates = MathExtensions.AddPoints(this.defaultPatchCoordinates, new Point3D(offsets.X, offsets.Y, 0));
                }
                catch (FormatException)
                {
                    MessageBox.Show("Internal error: button does not contain a valid integer offset tag.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
                }
            }
        }
    }
}
