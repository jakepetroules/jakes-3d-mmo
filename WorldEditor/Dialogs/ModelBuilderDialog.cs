namespace MMO3D.WorldEditor
{
    using System;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using MMO3D.Engine;

    /// <summary>
    /// Defines a dialog allowing the user to build X and FBX model
    /// files into the native XNB format used by the XNA framework.
    /// </summary>
    public partial class ModelBuilderDialog : Form
    {
        /// <summary>
        /// Content manager used to load models.
        /// </summary>
        private ContentManager contentManager;

        /// <summary>
        /// Initializes a new instance of the ModelBuilderDialog class.
        /// </summary>
        public ModelBuilderDialog()
        {
            this.InitializeComponent();

            this.contentManager = new ContentManager(this.modelViewerControl.Services, ModelBuilder.OutputDirectory);

            this.UpdateGUI();
        }

        /// <summary>
        /// Converts a <see cref="System.Windows.Forms.ListBox.ObjectCollection" /> to a string array.
        /// </summary>
        /// <param name="objects">The <see cref="System.Windows.Forms.ListBox.ObjectCollection" /> to convert.</param>
        /// <returns>See summary.</returns>
        private static string[] ToStringArray(ListBox.ObjectCollection objects)
        {
            string[] items = new string[objects.Count];

            for (int i = 0; i < objects.Count; i++)
            {
                items[i] = objects[i].ToString();
            }

            return items;
        }

        /// <summary>
        /// Launches a dialog allowing the user to browse for model files.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonBrowseFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Model Files (*.fbx;*.x)|*.fbx;*.x|All Files (*.*)|*.*";
            open.Multiselect = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in open.FileNames)
                {
                    if (!this.listBoxFiles.Items.Contains(file))
                    {
                        this.listBoxFiles.Items.Add(file);
                    }
                }

                this.UpdateGUI();
            }
        }

        /// <summary>
        /// Launches a dialog allowing the user to browse for a model directory.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonBrowseDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                if (!this.listBoxFiles.Items.Contains(folder.SelectedPath))
                {
                    this.listBoxFiles.Items.Add(folder.SelectedPath);
                }

                this.UpdateGUI();
            }
        }

        /// <summary>
        /// Removes the selected file and/or directory entries from the list box.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            while (this.listBoxFiles.SelectedItems.Count > 0)
            {
                this.listBoxFiles.Items.Remove(this.listBoxFiles.SelectedItems[0]);
            }

            this.UpdateGUI();
        }

        /// <summary>
        /// Launches dialog allowing the user to select a directory to copy the built XNB model files to.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonBrowseDestination_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                this.textBoxDestination.Text = folder.SelectedPath;
            }
        }

        /// <summary>
        /// Builds the models in the list view to XNB files.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonBuildModels_Click(object sender, EventArgs e)
        {
            this.richTextBoxOutput.Clear();

            try
            {
                string[] results = ModelBuilder.BuildModels(ModelBuilderDialog.ToStringArray(this.listBoxFiles.Items));

                for (int i = 0; i < results.Length; i++)
                {
                    this.richTextBoxOutput.AppendText(results[i] + Environment.NewLine);
                }
            }
            catch (InvalidOperationException ex)
            {
                this.richTextBoxOutput.AppendText(ex.ToString() + Environment.NewLine);
            }
        }

        /// <summary>
        /// Displays the model selected in the list box, in a 3D view on the dialog.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ListBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadModel(this.richTextBoxOutput.Text.Split('\r', '\n')[this.listBoxFiles.SelectedIndex]);
        }

        /// <summary>
        /// Updates the GUI, enabling and disabling appropriate controls for the dialog's state.
        /// </summary>
        private void UpdateGUI()
        {
            this.buttonRemove.Enabled = this.listBoxFiles.Items.Count > 0;

            this.buttonBuildModels.Enabled = this.listBoxFiles.Items.Count > 0;
        }

        /// <summary>
        /// Loads a new 3D model file into the viewer.
        /// </summary>
        /// <param name="fileName">The filename of the X or FBX file to load.</param>
        private void LoadModel(string fileName)
        {
            this.Cursor = Cursors.WaitCursor;

            // Unload any existing model
            this.modelViewerControl.Model = null;
            this.contentManager.Unload();

            // If the build succeeded, use the ContentManager to
            // load the temporary .xnb file that we just created
            this.modelViewerControl.Model = this.contentManager.Load<Model>(ModelBuilder.GetAssetFriendlyName(fileName));

            this.Cursor = Cursors.Arrow;
        }
    }
}
