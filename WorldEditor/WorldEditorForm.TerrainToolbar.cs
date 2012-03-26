namespace MMO3D.WorldEditor
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using MMO3D.Engine;

    /// <summary>
    /// Terrain toolbar event handlers.
    /// </summary>
    public partial class WorldEditorForm
    {
        /// <summary>
        /// Changes the current height level.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripTextBoxHeightLevel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.terrainControl.CurrentHeightLevel = Convert.ToInt32(this.toolStripTextBoxHeightLevel.Text, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
            }
            catch (OverflowException)
            {
            }

            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Changes the current season.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripComboBoxSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.terrainControl.CurrentSeason = SeasonExtensions.ParseFromString(this.toolStripComboBoxSeason.SelectedItem.ToString());
            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Changes whether the base season terrain is hidden.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripButtonHideBaseSeasonTerrain_Click(object sender, EventArgs e)
        {
            this.terrainControl.HideBaseSeasonTerrain = !this.terrainControl.HideBaseSeasonTerrain;
            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Changes the terrain tool to select.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripButtonSelect_Click(object sender, EventArgs e)
        {
            this.terrainControl.TerrainTool = this.GetToolDefault().Set(TerrainControlTools.Select);
            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Changes the terrain tool to raise.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripButtonRaise_Click(object sender, EventArgs e)
        {
            this.terrainControl.TerrainTool = this.GetToolDefault().Set(TerrainControlTools.Raise);
            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Changes the terrain tool to lower.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripButtonLower_Click(object sender, EventArgs e)
        {
            this.terrainControl.TerrainTool = this.GetToolDefault().Set(TerrainControlTools.Lower);
            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Changes the terrain tool to flatten.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripButtonFlatten_Click(object sender, EventArgs e)
        {
            this.terrainControl.TerrainTool = this.GetToolDefault().Set(TerrainControlTools.Flatten);
            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Changes the terrain tool to smooth.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripButtonSmooth_Click(object sender, EventArgs e)
        {
            this.terrainControl.TerrainTool = this.GetToolDefault().Set(TerrainControlTools.Smooth);
            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Changes the terrain tool cursor size.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripTextBoxCursorSize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.terrainControl.CursorSize = Convert.ToInt32(this.toolStripTextBoxCursorSize.Text, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
            }
            catch (OverflowException)
            {
            }

            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Changes the terrain tool intensity.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripTextBoxIntensity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.terrainControl.Intensity = Convert.ToSingle(this.toolStripTextBoxIntensity.Text, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
            }
            catch (OverflowException)
            {
            }

            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Changes the apply terrain type tool state.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripButtonApplyTerrainType_Click(object sender, EventArgs e)
        {
            if (this.terrainControl.TerrainTool.IsSet(TerrainControlTools.TerrainType))
            {
                this.terrainControl.TerrainTool = this.terrainControl.TerrainTool.Clear(TerrainControlTools.TerrainType);
            }
            else
            {
                this.terrainControl.TerrainTool = this.terrainControl.TerrainTool.Set(TerrainControlTools.TerrainType);
            }

            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Changes the application terrain type.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripComboBoxApplyTerrainType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.terrainControl.ApplyTerrainType = TerrainTypeExtensions.ParseFromString(this.toolStripComboBoxApplyTerrainType.SelectedItem.ToString());
            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Synchronizes the toolbar whenever a terrain control property changes.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TerrainControl_PropertyChanged(object sender, EventArgs e)
        {
            this.SyncTerrainToolbar();
        }

        /// <summary>
        /// Updates the terrain toolbar with properties from the edit control.
        /// </summary>
        private void SyncTerrainToolbar()
        {
            this.toolStripTextBoxHeightLevel.TextChanged -= this.ToolStripTextBoxHeightLevel_TextChanged;
            this.toolStripComboBoxSeason.SelectedIndexChanged -= this.ToolStripComboBoxSeason_SelectedIndexChanged;
            this.toolStripButtonHideBaseSeasonTerrain.Click -= this.ToolStripButtonHideBaseSeasonTerrain_Click;
            this.toolStripButtonSelect.Click -= this.ToolStripButtonSelect_Click;
            this.toolStripButtonRaise.Click -= this.ToolStripButtonRaise_Click;
            this.toolStripButtonLower.Click -= this.ToolStripButtonLower_Click;
            this.toolStripButtonFlatten.Click -= this.ToolStripButtonFlatten_Click;
            this.toolStripButtonSmooth.Click -= this.ToolStripButtonSmooth_Click;
            this.toolStripTextBoxCursorSize.TextChanged -= this.ToolStripTextBoxCursorSize_TextChanged;
            this.toolStripTextBoxIntensity.TextChanged -= this.ToolStripTextBoxIntensity_TextChanged;
            this.toolStripButtonApplyTerrainType.Click -= this.ToolStripButtonApplyTerrainType_Click;
            this.toolStripComboBoxApplyTerrainType.SelectedIndexChanged -= this.ToolStripComboBoxApplyTerrainType_SelectedIndexChanged;

            if (this.toolStripComboBoxSeason.Items.Count == 0)
            {
                this.toolStripComboBoxSeason.Items.AddRange(SeasonExtensions.GetSortedList());
            }

            if (this.toolStripComboBoxApplyTerrainType.Items.Count == 0)
            {
                this.toolStripComboBoxApplyTerrainType.Items.AddRange(TerrainTypeExtensions.GetSortedList());
            }

            this.toolStripButtonHideBaseSeasonTerrain.Checked = this.terrainControl.HideBaseSeasonTerrain;

            this.toolStripTextBoxHeightLevel.Text = this.terrainControl.CurrentHeightLevel.ToString(CultureInfo.InvariantCulture);
            this.toolStripTextBoxCursorSize.Text = this.terrainControl.CursorSize.ToString(CultureInfo.InvariantCulture);
            this.toolStripTextBoxIntensity.Text = this.terrainControl.Intensity.ToString(CultureInfo.InvariantCulture);

            this.toolStripButtonSelect.Checked = this.terrainControl.TerrainTool.IsSet(TerrainControlTools.Select);
            this.toolStripButtonRaise.Checked = this.terrainControl.TerrainTool.IsSet(TerrainControlTools.Raise);
            this.toolStripButtonLower.Checked = this.terrainControl.TerrainTool.IsSet(TerrainControlTools.Lower);
            this.toolStripButtonFlatten.Checked = this.terrainControl.TerrainTool.IsSet(TerrainControlTools.Flatten);
            this.toolStripButtonSmooth.Checked = this.terrainControl.TerrainTool.IsSet(TerrainControlTools.Smooth);
            this.toolStripButtonApplyTerrainType.Checked = this.terrainControl.TerrainTool.IsSet(TerrainControlTools.TerrainType);

            this.toolStripTextBoxCursorSize.Enabled = !this.terrainControl.TerrainTool.IsSet(TerrainControlTools.Select) || this.terrainControl.TerrainTool.IsSet(TerrainControlTools.TerrainType);
            this.toolStripTextBoxIntensity.Enabled = !this.terrainControl.TerrainTool.IsSet(TerrainControlTools.Select);
            this.toolStripComboBoxApplyTerrainType.Enabled = this.terrainControl.TerrainTool.IsSet(TerrainControlTools.TerrainType);

            StringCollection seasons = new StringCollection();
            seasons.AddRange(SeasonExtensions.GetSortedList());
            this.toolStripComboBoxSeason.SelectedIndex = seasons.IndexOf(this.terrainControl.CurrentSeason.ToString());

            StringCollection terrainTypes = new StringCollection();
            terrainTypes.AddRange(TerrainTypeExtensions.GetSortedList());
            this.toolStripComboBoxApplyTerrainType.SelectedIndex = terrainTypes.IndexOf(this.terrainControl.ApplyTerrainType.ToString());

            this.toolStripTextBoxHeightLevel.TextChanged += this.ToolStripTextBoxHeightLevel_TextChanged;
            this.toolStripComboBoxSeason.SelectedIndexChanged += this.ToolStripComboBoxSeason_SelectedIndexChanged;
            this.toolStripButtonHideBaseSeasonTerrain.Click += this.ToolStripButtonHideBaseSeasonTerrain_Click;
            this.toolStripButtonSelect.Click += this.ToolStripButtonSelect_Click;
            this.toolStripButtonRaise.Click += this.ToolStripButtonRaise_Click;
            this.toolStripButtonLower.Click += this.ToolStripButtonLower_Click;
            this.toolStripButtonFlatten.Click += this.ToolStripButtonFlatten_Click;
            this.toolStripButtonSmooth.Click += this.ToolStripButtonSmooth_Click;
            this.toolStripTextBoxCursorSize.TextChanged += this.ToolStripTextBoxCursorSize_TextChanged;
            this.toolStripTextBoxIntensity.TextChanged += this.ToolStripTextBoxIntensity_TextChanged;
            this.toolStripButtonApplyTerrainType.Click += this.ToolStripButtonApplyTerrainType_Click;
            this.toolStripComboBoxApplyTerrainType.SelectedIndexChanged += this.ToolStripComboBoxApplyTerrainType_SelectedIndexChanged;
        }

        /// <summary>
        /// Gets the TerrainType tool if it is part of the current selection, or a blank tool if it is not.
        /// </summary>
        /// <returns>See summary.</returns>
        private TerrainControlTools GetToolDefault()
        {
            return this.terrainControl.TerrainTool.IsSet(TerrainControlTools.TerrainType) ? TerrainControlTools.TerrainType : 0;
        }
    }
}
