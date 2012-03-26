namespace Petroules.Synteza.Windows.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Represents a dialog box that displays available colors along
    /// with controls that enable the user to define custom colors,
    /// with extended functionality from the Windows default dialog.
    /// </summary>
    [DefaultProperty("Color")]
    public partial class ExtendedColorDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the ExtendedColorDialog class.
        /// </summary>
        public ExtendedColorDialog()
        {
            this.InitializeComponent();
            this.AllowTransparentColors = true;
            this.Color = Color.Black;
            this.comboBoxKnownColors.Items.AddRange(System.Enum.GetNames(typeof(KnownColor)));
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow the user to select transparent colors.
        /// </summary>
        /// <value>See summary.</value>
        [DefaultValue(true)]
        public bool AllowTransparentColors
        {
            get { return this.trackBarAlpha.Enabled && this.numericUpDownAlpha.Enabled; }
            set { this.trackBarAlpha.Enabled = this.numericUpDownAlpha.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets the color selected by the user.
        /// </summary>
        /// <value>The color selected by the user. If a color is not selected, the default value is black.</value>
        public Color Color
        {
            get
            {
                return this.pictureBoxColor.BackColor;
            }

            set
            {
                this.pictureBoxColor.BackColor = value;
                this.comboBoxKnownColors.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sets the color from the KnownColor.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ComboBoxKnownColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxKnownColors.SelectedIndex >= 0)
            {
                this.Color = Color.FromName(this.comboBoxKnownColors.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// Sets the values of the numeric up-downs and picture box when the track bars are changed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            this.SetEvents(false);

            this.numericUpDownAlpha.Value = this.trackBarAlpha.Value;
            this.numericUpDownRed.Value = this.trackBarRed.Value;
            this.numericUpDownGreen.Value = this.trackBarGreen.Value;
            this.numericUpDownBlue.Value = this.trackBarBlue.Value;

            this.Color = Color.FromArgb(this.trackBarAlpha.Value, this.trackBarRed.Value, this.trackBarGreen.Value, this.trackBarBlue.Value);

            this.SetEvents(true);
        }

        /// <summary>
        /// Sets the values of the track bars when the numeric up-downs are changed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.SetEvents(false);

            this.trackBarAlpha.Value = (int)this.numericUpDownAlpha.Value;
            this.trackBarRed.Value = (int)this.numericUpDownRed.Value;
            this.trackBarGreen.Value = (int)this.numericUpDownGreen.Value;
            this.trackBarBlue.Value = (int)this.numericUpDownBlue.Value;

            this.Color = Color.FromArgb(this.trackBarAlpha.Value, this.trackBarRed.Value, this.trackBarGreen.Value, this.trackBarBlue.Value);

            this.SetEvents(true);
        }

        /// <summary>
        /// Sets the values of the numeric up-downs then the color of the picturebox is changed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void PictureBoxColor_BackColorChanged(object sender, EventArgs e)
        {
            this.SetEvents(false);

            this.numericUpDownAlpha.Value = this.pictureBoxColor.BackColor.A;
            this.numericUpDownRed.Value = this.pictureBoxColor.BackColor.R;
            this.numericUpDownGreen.Value = this.pictureBoxColor.BackColor.G;
            this.numericUpDownBlue.Value = this.pictureBoxColor.BackColor.B;

            this.trackBarAlpha.Value = (int)this.numericUpDownAlpha.Value;
            this.trackBarRed.Value = (int)this.numericUpDownRed.Value;
            this.trackBarGreen.Value = (int)this.numericUpDownGreen.Value;
            this.trackBarBlue.Value = (int)this.numericUpDownBlue.Value;

            this.SetEvents(true);
        }

        /// <summary>
        /// Turns the change events for the track bars and numeric up-downs on or off.
        /// </summary>
        /// <param name="onOff">True to enable the events, false to disable them.</param>
        private void SetEvents(bool onOff)
        {
            if (onOff)
            {
                this.numericUpDownAlpha.ValueChanged += this.NumericUpDown_ValueChanged;
                this.numericUpDownRed.ValueChanged += this.NumericUpDown_ValueChanged;
                this.numericUpDownGreen.ValueChanged += this.NumericUpDown_ValueChanged;
                this.numericUpDownBlue.ValueChanged += this.NumericUpDown_ValueChanged;

                this.trackBarAlpha.Scroll += this.TrackBar_Scroll;
                this.trackBarRed.Scroll += this.TrackBar_Scroll;
                this.trackBarGreen.Scroll += this.TrackBar_Scroll;
                this.trackBarBlue.Scroll += this.TrackBar_Scroll;
            }
            else
            {
                this.numericUpDownAlpha.ValueChanged -= this.NumericUpDown_ValueChanged;
                this.numericUpDownRed.ValueChanged -= this.NumericUpDown_ValueChanged;
                this.numericUpDownGreen.ValueChanged -= this.NumericUpDown_ValueChanged;
                this.numericUpDownBlue.ValueChanged -= this.NumericUpDown_ValueChanged;

                this.trackBarAlpha.Scroll -= this.TrackBar_Scroll;
                this.trackBarRed.Scroll -= this.TrackBar_Scroll;
                this.trackBarGreen.Scroll -= this.TrackBar_Scroll;
                this.trackBarBlue.Scroll -= this.TrackBar_Scroll;
            }
        }
    }
}
