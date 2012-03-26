namespace MMO3D.WorldEditor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using MMO3D.Engine;

    /// <summary>
    /// Menu handlers section of the MMO3D World Editor main GUI form.
    /// </summary>
    public partial class WorldEditorForm
    {
        /// <summary>
        /// Occurs when the exit menu item is clicked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.ExitProgram();
        }

        /// <summary>
        /// Occurs when the build packed map button is clicked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void BuildPackedMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.EnsureDirectories();

            OpenFileDialog terrainPatches = new OpenFileDialog();
            terrainPatches.Filter = "Terrain Patch file (*.pat)|*.pat";
            terrainPatches.InitialDirectory = Application.StartupPath + @"\GameData\Terrain";
            terrainPatches.Multiselect = true;
            if (terrainPatches.ShowDialog() == DialogResult.OK)
            {
                StringCollection mpt = new StringCollection();
                mpt.AddRange(terrainPatches.FileNames);

                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Terrain Patch Collection file (*.tpc)|*.tpc";
                save.AddExtension = true;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    using (BinaryWriter write = new BinaryWriter(new FileStream(save.FileName, FileMode.Create)))
                    {
                        TerrainPackFileWriter.CreateFile(write, mpt);
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when the build model button is clicked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void BuildModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ModelBuilderDialog().ShowDialog();
        }

        /// <summary>
        /// Occurs when the build packed model archive button is clicked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void BuildPackedModelArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.EnsureDirectories();

            string initialDir = null;
            string[] files = null;

            if (MessageBox.Show("Would you like to select files individually?", Resources.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, this.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0) == DialogResult.No)
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                folder.SelectedPath = Application.StartupPath + @"\GameData\Models";
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    files = Directory.GetFiles(folder.SelectedPath, "*", SearchOption.AllDirectories);
                    initialDir = folder.SelectedPath;
                }
            }
            else
            {
                OpenFileDialog models = new OpenFileDialog();
                models.Filter = "Compiled model files (*.xnb)|*.xnb";
                models.InitialDirectory = Application.StartupPath + @"\GameData\Models";
                models.Multiselect = true;
                if (models.ShowDialog() == DialogResult.OK)
                {
                    files = models.FileNames;
                    initialDir = models.InitialDirectory;
                }
            }

            if (initialDir != null && files != null)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                for (int i = 0; i < files.Length; i++)
                {
                    string file = files[i].Substring(initialDir.Length + 1);

                    file = file.Substring(0, file.Length - 4);

                    dict.Add(file, files[i]);
                }

                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Model Collection file (*.mc)|*.mc";
                save.AddExtension = true;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    using (BinaryWriter write = new BinaryWriter(new FileStream(save.FileName, FileMode.Create)))
                    {
                        ModelPackFileWriter.CreateFile(write, dict);
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when the create world map button is clicked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void CreateWorldMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new WorldMapGeneratorDialog()).ShowDialog();
        }

        /// <summary>
        /// Occurs when the always on top menu item is clicked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void AlwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = this.alwaysOnTopToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Occurs when the message log menu item is clicked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void MessageLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.splitContainerMain.Panel2Collapsed = !this.messageLogToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Occurs when the options menu item is clicked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsDialog options = new OptionsDialog();

            options.CameraMovementSpeed = this.editorRenderWindow.CameraMovementSpeed;
            options.CameraRotationSpeed = this.editorRenderWindow.CameraRotationSpeed;

            if (options.ShowDialog() == DialogResult.OK)
            {
                this.editorRenderWindow.CameraMovementSpeed = options.CameraMovementSpeed;
                this.editorRenderWindow.CameraRotationSpeed = options.CameraRotationSpeed;
            }
        }

        /// <summary>
        /// Shows a dialog to add a new terrain patch to the world map.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void AddNewPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.EnsureDirectories();

            AddNewPatchDialog addPatchForm = new AddNewPatchDialog(this.editorRenderWindow.Engine.CurrentCamera.Target, this.editorRenderWindow.Engine.Terrain.CurrentHeightLevel);
            if (addPatchForm.ShowDialog() == DialogResult.OK)
            {
                TerrainPatch tp = new TerrainPatch(addPatchForm.PatchCoordinates, addPatchForm.DefaultHeight, addPatchForm.DefaultTerrainType);

                string destinationFilename = Helper.GetPatchFileName(tp.PatchId);

                if (File.Exists(destinationFilename))
                {
                    if (MessageBox.Show("Are you sure you want to overwrite?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, this.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0) == DialogResult.Yes)
                    {
                        File.WriteAllBytes(destinationFilename, tp.ToByteArray());
                        this.editorRenderWindow.Engine.Terrain.ForcePatchReload(addPatchForm.PatchCoordinates);
                    }
                }
                else
                {
                    File.WriteAllBytes(destinationFilename, tp.ToByteArray());
                }
            }
        }

        /// <summary>
        /// Shows a dialog to add an existing terrain patch from a file, to the world map.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void AddExistingPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.EnsureDirectories();

            AddExistingPatchDialog addPatchForm = new AddExistingPatchDialog(this.editorRenderWindow.Engine.CurrentCamera.Target, this.editorRenderWindow.Engine.Terrain.CurrentHeightLevel);
            if (addPatchForm.ShowDialog() == DialogResult.OK)
            {
                TerrainPatch tp = addPatchForm.TerrainPatch;

                string destinationFilename = Helper.GetPatchFileName(tp.PatchId);

                if (File.Exists(destinationFilename))
                {
                    if (MessageBox.Show("Are you sure you want to overwrite?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, this.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0) == DialogResult.Yes)
                    {
                        File.WriteAllBytes(destinationFilename, tp.ToByteArray());
                        this.editorRenderWindow.Engine.Terrain.ForcePatchReload(tp.PatchId);
                    }
                }
                else
                {
                    File.WriteAllBytes(destinationFilename, tp.ToByteArray());
                }
            }
        }

        /// <summary>
        /// Shows a dialog to remove a terrain patch from the world map.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void RemovePatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.EnsureDirectories();

            RemovePatchDialog removePatchForm = new RemovePatchDialog(this.editorRenderWindow.Engine.CurrentCamera.Target, this.editorRenderWindow.Engine.Terrain.CurrentHeightLevel);
            if (removePatchForm.ShowDialog() == DialogResult.OK)
            {
                string destinationFilename = Helper.GetPatchFileName(removePatchForm.PatchCoordinates);

                if (File.Exists(destinationFilename))
                {
                    if (MessageBox.Show(string.Format(CultureInfo.InvariantCulture, "Are you sure you want to delete patch: {0}?", removePatchForm.PatchCoordinates), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, this.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0) == DialogResult.Yes)
                    {
                        File.Delete(Helper.GetPatchFileName(removePatchForm.PatchCoordinates));
                        this.editorRenderWindow.Engine.Terrain.ForcePatchReload(removePatchForm.PatchCoordinates);
                    }
                }
                else
                {
                    MessageBox.Show(string.Format(CultureInfo.InvariantCulture, "Patch {0} does not exist!", removePatchForm.PatchCoordinates), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, this.RightToLeft == RightToLeft.Yes ? MessageBoxOptions.RtlReading : 0);
                }
            }
        }

        /// <summary>
        /// Changes the visibility of a toolbar depending on a menu item's check-state.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                if (menuItem == this.renderingToolStripMenuItem)
                {
                    this.toolStripRendering.Visible = menuItem.Checked;
                }
                else if (menuItem == this.terrainToolStripMenuItem)
                {
                    this.toolStripTerrain.Visible = menuItem.Checked;
                }
            }
        }
    }
}