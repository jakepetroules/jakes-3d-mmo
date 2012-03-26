namespace MMO3D.WorldEditor
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Defines a dialog to allow the user to configure various options in the program.
    /// </summary>
    public partial class OptionsDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the OptionsDialog class.
        /// </summary>
        public OptionsDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the speed at which the camera moves.
        /// </summary>
        /// <value>See summary.</value>
        public float CameraMovementSpeed
        {
            get { return Convert.ToSingle(this.numericUpDownMoveSpeed.Value); }
            set { this.numericUpDownMoveSpeed.Value = Convert.ToDecimal(value); }
        }

        /// <summary>
        /// Gets or sets the speed at which the camera is rotated.
        /// </summary>
        /// <value>See summary.</value>
        public float CameraRotationSpeed
        {
            get { return Convert.ToSingle(this.numericUpDownRotationSpeed.Value); }
            set { this.numericUpDownRotationSpeed.Value = Convert.ToDecimal(value); }
        }

        /// <summary>
        /// Sets a particular setting to its default, depending on which link label was clicked.
        /// </summary>
        /// <param name="sender">The link label that was clicked.</param>
        /// <param name="e">The event arguments.</param>
        private void LinkLabelDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel label = sender as LinkLabel;
            if (label != null)
            {
                if (label == this.linkLabelCameraMoveSpeedDefault)
                {
                    this.numericUpDownMoveSpeed.Value = Convert.ToDecimal(DefaultProperties.CameraMovementSpeed);
                }
                else if (label == this.linkLabelCameraRotateSpeedDefault)
                {
                    this.numericUpDownRotationSpeed.Value = Convert.ToDecimal(DefaultProperties.CameraRotationSpeed);
                }
            }
        }
    }
}
