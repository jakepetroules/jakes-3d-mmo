namespace MMO3D.WorldEditor
{
    using System;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.Windows.Forms;
    using Petroules.Synteza.Windows.Forms;
    using MMO3D.Engine;

    /// <summary>
    /// Defines a dialog to allow the user to generate world maps.
    /// </summary>
    public partial class WorldMapGeneratorDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the WorldMapGeneratorDialog class.
        /// </summary>
        public WorldMapGeneratorDialog()
        {
            this.InitializeComponent();

            this.comboBoxSeason.Items.AddRange(SeasonExtensions.GetSortedList());
            this.comboBoxSeason.SelectedIndex = 0;

            this.UpdateGUI();
        }

        /// <summary>
        /// Gets the season to render the map in.
        /// </summary>
        /// <value>See summary.</value>
        private Season Season
        {
            get { return SeasonExtensions.ParseFromString(this.comboBoxSeason.GetSelectedItem().ToString()); }
        }

        /// <summary>
        /// Prevents the form from being closed if a map generation is in progress.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void WorldMapGeneratorDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.backgroundWorkerMapGenerator.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("Please wait until the operation has been completed or canceled before closing the window.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
            }
        }

        /// <summary>
        /// Launches a dialog allowing the user to select a TPC file to read from.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonBrowseMapFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openWorldMap = new OpenFileDialog();
            openWorldMap.Filter = "Terrain Patch Collection file (*.tpc)|*.tpc";
            if (openWorldMap.ShowDialog() == DialogResult.OK)
            {
                this.textBoxMapFile.Text = openWorldMap.FileName;

                this.UpdateGUI();
            }
        }

        /// <summary>
        /// Cancels generation of the map image file as soon as possible.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            WorldMap.Cancel = true;

            this.UpdateGUI();
        }

        /// <summary>
        /// Starts generating the map image file from the map data.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            // Erase previous image
            this.pictureBoxPreview.Image = null;
            this.labelImageDetails.Text = string.Empty;

            WorldMap.ProgressChanged += new EventHandler(this.WorldMap_ProgressChanged);
            this.backgroundWorkerMapGenerator.RunWorkerAsync();

            this.UpdateGUI();
        }

        /// <summary>
        /// Does the actual work of generating the map image file.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void BackgroundWorkerMapGenerator_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.pictureBoxPreview.SetImage(WorldMap.DrawMap(this.textBoxMapFile.GetText(), this.checkBoxTextured.GetChecked(), this.Season, this.checkBoxHideBaseSeasonTerrain.GetChecked()));
            this.labelImageDetails.SetText(string.Format(CultureInfo.CurrentCulture, "Image: {0} × {1} pixels", this.pictureBoxPreview.GetImage().Width, this.pictureBoxPreview.GetImage().Height));
        }

        /// <summary>
        /// Updates the GUI once the map generator background thread has completed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void BackgroundWorkerMapGenerator_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.UpdateGUI();
        }

        /// <summary>
        /// Updates the progress bar to reflect how far along the map image generation is.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void WorldMap_ProgressChanged(object sender, EventArgs e)
        {
            this.progressBarMapGeneration.SetValue(WorldMap.Progress);
        }

        /// <summary>
        /// Saves the generated map image file.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "GIF Image (*.gif)|*.gif|JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png";
            saveDialog.FilterIndex = 3;
            saveDialog.AddExtension = true;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                switch (saveDialog.FilterIndex)
                {
                    case 1:
                        this.pictureBoxPreview.Image.Save(saveDialog.FileName, ImageFormat.Gif);
                        break;
                    case 2:
                        this.pictureBoxPreview.Image.Save(saveDialog.FileName, ImageFormat.Jpeg);
                        break;
                    case 3:
                        this.pictureBoxPreview.Image.Save(saveDialog.FileName, ImageFormat.Png);
                        break;
                }
            }
        }

        /// <summary>
        /// Updates the GUI, enabling and disabling appropriate controls.
        /// </summary>
        private void UpdateGUI()
        {
            this.buttonBrowseMapFile.Enabled = !this.backgroundWorkerMapGenerator.IsBusy;

            this.groupBoxOptions.Enabled = false;
            this.progressBarMapGeneration.Enabled = false;
            this.buttonGenerate.Enabled = false;
            this.buttonCancel.Enabled = false;
            this.groupBoxPreview.Enabled = false;
            this.buttonSave.Enabled = false;

            if (!string.IsNullOrEmpty(this.textBoxMapFile.Text))
            {
                this.progressBarMapGeneration.Enabled = true;

                if (!this.backgroundWorkerMapGenerator.IsBusy)
                {
                    this.groupBoxOptions.Enabled = true;
                    this.buttonGenerate.Enabled = true;
                }

                if (this.backgroundWorkerMapGenerator.IsBusy)
                {
                    this.buttonCancel.Enabled = true;
                }

                if (this.pictureBoxPreview.Image != null)
                {
                    this.groupBoxPreview.Enabled = true;
                    this.buttonSave.Enabled = true;
                }
            }
        }
    }
}
