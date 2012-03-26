namespace MMO3D.WorldEditor
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;
    using Petroules.Synteza.Imaging;
    using MMO3D.CommonCode;
    using MMO3D.Engine;
    using Xna = Microsoft.Xna.Framework;

    /// <summary>
    /// Defines a control for editing the terrain.
    /// </summary>
    public partial class TerrainControl : UserControl
    {
        /// <summary>
        /// Whether the class has been fully initialized.
        /// </summary>
        private bool initialized;

        /// <summary>
        /// Whether events are on or off. See <see cref="SetEvents"/>.
        /// </summary>
        private bool events;

        /// <summary>
        /// Stores a collection of terrain patches that were changed by the World Editor.
        /// </summary>
        private Collection<TerrainPatch> unsavedPatches = new Collection<TerrainPatch>();

        /// <summary>
        /// Initializes a new instance of the TerrainControl class.
        /// </summary>
        public TerrainControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Raised when the value of a property changes.
        /// </summary>
        [Description("Raised when the value of a property changes.")]
        [Browsable(true)]
        public event EventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Gets or sets a value indicating whether render base season terrain as undefined.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        public bool HideBaseSeasonTerrain
        {
            get
            {
                return this.checkBoxHideBaseSeasonTerrain.Checked;
            }

            set
            {
                this.checkBoxHideBaseSeasonTerrain.Checked = value;
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the current height level we have loaded.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        public int CurrentHeightLevel
        {
            get
            {
                return Convert.ToInt32(this.numericUpDownCurrentHeightLevel.Value);
            }

            set
            {
                this.numericUpDownCurrentHeightLevel.Value = Convert.ToDecimal(value);
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the current season we have loaded.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        public Season CurrentSeason
        {
            get
            {
                if (this.DesignMode)
                {
                    return Season.Midseason;
                }

                return SeasonExtensions.ParseFromString(this.comboBoxCurrentSeason.SelectedItem.ToString());
            }

            set
            {
                if (this.DesignMode || this.comboBoxCurrentSeason.Items.Count == 0)
                {
                    return;
                }

                StringCollection seasons = new StringCollection();
                seasons.AddRange(SeasonExtensions.GetSortedList());

                this.comboBoxCurrentSeason.SelectedIndex = seasons.IndexOf(value.ToString());
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the currently selected terrain tool.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        public TerrainControlTools TerrainTool
        {
            get
            {
                TerrainControlTools tool = 0;

                if (this.radioButtonSelect.Checked)
                {
                    tool = tool.Set(TerrainControlTools.Select);
                }
                else if (this.radioButtonRaise.Checked)
                {
                    tool = tool.Set(TerrainControlTools.Raise);
                }
                else if (this.radioButtonLower.Checked)
                {
                    tool = tool.Set(TerrainControlTools.Lower);
                }
                else if (this.radioButtonFlatten.Checked)
                {
                    tool = tool.Set(TerrainControlTools.Flatten);
                }
                else if (this.radioButtonSmooth.Checked)
                {
                    tool = tool.Set(TerrainControlTools.Smooth);
                }

                if (this.checkBoxTerrainType.Checked)
                {
                    tool = tool.Set(TerrainControlTools.TerrainType);
                }

                return tool;
            }

            set
            {
                if (this.DesignMode)
                {
                    return;
                }

                // These are mutually exclusive
                if (value.IsSet(TerrainControlTools.Select))
                {
                    this.radioButtonSelect.Checked = true;
                }
                else if (value.IsSet(TerrainControlTools.Raise))
                {
                    this.radioButtonRaise.Checked = true;
                }
                else if (value.IsSet(TerrainControlTools.Lower))
                {
                    this.radioButtonLower.Checked = true;
                }
                else if (value.IsSet(TerrainControlTools.Flatten))
                {
                    this.radioButtonFlatten.Checked = true;
                }
                else if (value.IsSet(TerrainControlTools.Smooth))
                {
                    this.radioButtonSmooth.Checked = true;
                }

                this.checkBoxTerrainType.Checked = value.IsSet(TerrainControlTools.TerrainType);

                WorldEditorForm.Instance.RenderingWindowCursor = TerrainControl.GetTerrainToolCursor(value);

                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the currently selected global coordinate.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        public Xna.Point GlobalCoordinate
        {
            get
            {
                return new Xna.Point((int)this.numericUpDownCoordinatesX.Value, (int)this.numericUpDownCoordinatesY.Value);
            }

            set
            {
                this.numericUpDownCoordinatesX.Value = value.X;
                this.numericUpDownCoordinatesY.Value = value.Y;
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the currently selected terrain's height.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        public float TerrainHeight
        {
            get
            {
                return Convert.ToSingle(this.numericUpDownHeight.Value);
            }

            set
            {
                this.numericUpDownHeight.Value = Convert.ToDecimal(value);
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the currently selected terrain's type.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        public TerrainType TerrainType
        {
            get
            {
                if (this.DesignMode)
                {
                    return TerrainType.UndefinedTerrain;
                }

                return TerrainTypeExtensions.ParseFromString(this.comboBoxTerrainType.SelectedItem.ToString());
            }

            set
            {
                if (this.DesignMode || this.comboBoxTerrainType.Items.Count == 0)
                {
                    return;
                }

                StringCollection terrainTypes = new StringCollection();
                terrainTypes.AddRange(TerrainTypeExtensions.GetSortedList());

                this.comboBoxTerrainType.SelectedIndex = terrainTypes.IndexOf(value.ToString());
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the currently selected terrain type to apply.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        public TerrainType ApplyTerrainType
        {
            get
            {
                if (this.DesignMode)
                {
                    return TerrainType.UndefinedTerrain;
                }

                return TerrainTypeExtensions.ParseFromString(this.comboBoxApplyTerrainType.SelectedItem.ToString());
            }

            set
            {
                if (this.DesignMode || this.comboBoxApplyTerrainType.Items.Count == 0)
                {
                    return;
                }

                StringCollection terrainTypes = new StringCollection();
                terrainTypes.AddRange(TerrainTypeExtensions.GetSortedList());

                this.comboBoxApplyTerrainType.SelectedIndex = terrainTypes.IndexOf(value.ToString());
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the cursor size of the height and terrain type painter.
        /// The default value of 1 manipulates one vertex at a time.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        public int CursorSize
        {
            get
            {
                return Convert.ToInt32(this.numericUpDownCursorSize.Value);
            }

            set
            {
                this.numericUpDownCursorSize.Value = Convert.ToDecimal(value);
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the intensity of the height tool.
        /// The selected vertex will be raised by the value
        /// of this property, units per click.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        public float Intensity
        {
            get
            {
                return Convert.ToSingle(this.numericUpDownIntensity.Value);
            }

            set
            {
                this.numericUpDownIntensity.Value = Convert.ToDecimal(value);
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the currently selected terrain patch ID.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        private Point3D PatchID
        {
            get
            {
                return new Point3D((int)this.numericUpDownPatchIDX.Value, (int)this.numericUpDownPatchIDY.Value, (int)this.numericUpDownPatchIDZ.Value);
            }

            set
            {
                this.numericUpDownPatchIDX.Value = value.X;
                this.numericUpDownPatchIDY.Value = value.Y;
                this.numericUpDownPatchIDZ.Value = value.Z;
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the currently selected patch vertex.
        /// </summary>
        /// <value>See summary.</value>
        [Browsable(false)]
        private Xna.Point PatchVertex
        {
            get
            {
                return new Xna.Point((int)this.numericUpDownPatchVertexX.Value, (int)this.numericUpDownPatchVertexY.Value);
            }

            set
            {
                this.numericUpDownPatchVertexX.Value = value.X;
                this.numericUpDownPatchVertexY.Value = value.Y;
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets the cursor for the specified terrain tool.
        /// </summary>
        /// <param name="tool">The terrain tool to get the associated cursor of.</param>
        /// <returns>See summary.</returns>
        public static Cursor GetTerrainToolCursor(TerrainControlTools tool)
        {
            Cursor cursor = Cursors.Default;

            if (tool.IsSet(TerrainControlTools.Select))
            {
                cursor = Cursors.Arrow;
            }
            else if (tool.IsSet(TerrainControlTools.Raise))
            {
                cursor = Resources.Raise.CreateCursor();
            }
            else if (tool.IsSet(TerrainControlTools.Lower))
            {
                cursor = Resources.Lower.CreateCursor();
            }
            else if (tool.IsSet(TerrainControlTools.Flatten))
            {
                cursor = Resources.Flatten.CreateCursor();
            }
            else if (tool.IsSet(TerrainControlTools.Smooth))
            {
                cursor = Resources.Smooth.CreateCursor();
            }

            if (tool.IsSet(TerrainControlTools.TerrainType))
            {
                return cursor.CreateDualCursor(Resources.TerrainType.CreateCursor());
            }

            return cursor.CreateBitmap().CreateCursor();
        }

        /// <summary>
        /// Executes the operation currently selected on the terrain tool.
        /// </summary>
        /// <param name="position">The global position to set before executing the operation.</param>
        public void ExecuteTerrainToolOperation(Xna.Point position)
        {
            this.GlobalCoordinate = position;

            this.TerrainProperty_ValueChanged(null, EventArgs.Empty);
        }

        /// <summary>
        /// Commits changes made to the terrain.
        /// This should be called often to keep memory relatively free.
        /// </summary>
        public void CommitTerrainChanges()
        {
            for (int i = 0; i < this.unsavedPatches.Count; i++)
            {
                File.WriteAllBytes(Helper.GetPatchFileName(this.unsavedPatches[i].PatchId), this.unsavedPatches[i].ToByteArray());
                EngineManager.Engine.Terrain.ForcePatchReload(this.unsavedPatches[i].PatchId);
            }

            this.unsavedPatches.Clear();
        }

        /// <summary>
        /// Loads the form, initializing appropriate data.
        /// </summary>
        public void LoadControl()
        {
            // Only load if we're NOT in design mode and we haven't already initialized
            if (!this.DesignMode && !this.initialized)
            {
                this.comboBoxTerrainType.Items.AddRange(TerrainTypeExtensions.GetSortedList());
                this.comboBoxTerrainType.SelectedIndex = 0;

                this.comboBoxApplyTerrainType.Items.AddRange(TerrainTypeExtensions.GetSortedList());
                this.comboBoxApplyTerrainType.SelectedIndex = 0;

                this.numericUpDownPatchVertexX.Maximum = TerrainPatch.PatchSize - 1;
                this.numericUpDownPatchVertexY.Maximum = TerrainPatch.PatchSize - 1;

                this.numericUpDownCursorSize.Maximum = TerrainPatch.PatchSize - 1;

                this.numericUpDownCurrentHeightLevel.Value = 0;

                this.comboBoxCurrentSeason.Items.AddRange(SeasonExtensions.GetSortedList());
                this.comboBoxCurrentSeason.SelectedIndex = 0;

                this.HideBaseSeasonTerrain = false;

                this.TerrainTool = TerrainControlTools.Select;

                this.UpdateGUI();
                this.SetEvents(this.initialized = true);

                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Loads the form, initializing appropriate data.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TerrainControl_Load(object sender, EventArgs e)
        {
            this.LoadControl();
        }

        /// <summary>
        /// Turns the numeric up down value and terrain type combo box change events on or off.
        /// </summary>
        /// <param name="on">True to hook events; false to unhook.</param>
        private void SetEvents(bool on)
        {
            if (on)
            {
                this.numericUpDownPatchIDX.ValueChanged += this.NumericUpDownPatchID_ValueChanged;
                this.numericUpDownPatchIDY.ValueChanged += this.NumericUpDownPatchID_ValueChanged;

                this.numericUpDownPatchVertexX.ValueChanged += this.NumericUpDownPatchVertex_ValueChanged;
                this.numericUpDownPatchVertexY.ValueChanged += this.NumericUpDownPatchVertex_ValueChanged;

                this.numericUpDownCoordinatesX.ValueChanged += this.NumericUpDownCoordinates_ValueChanged;
                this.numericUpDownCoordinatesY.ValueChanged += this.NumericUpDownCoordinates_ValueChanged;

                this.numericUpDownHeight.ValueChanged += this.TerrainProperty_ValueChanged;
                this.comboBoxTerrainType.SelectedIndexChanged += this.TerrainProperty_ValueChanged;

                this.RefreshFluids(EngineManager.Engine.Terrain.GetTerrainPatch(this.PatchID));

                this.events = true;
            }
            else
            {
                this.numericUpDownPatchIDX.ValueChanged -= this.NumericUpDownPatchID_ValueChanged;
                this.numericUpDownPatchIDY.ValueChanged -= this.NumericUpDownPatchID_ValueChanged;

                this.numericUpDownPatchVertexX.ValueChanged -= this.NumericUpDownPatchVertex_ValueChanged;
                this.numericUpDownPatchVertexY.ValueChanged -= this.NumericUpDownPatchVertex_ValueChanged;

                this.numericUpDownCoordinatesX.ValueChanged -= this.NumericUpDownCoordinates_ValueChanged;
                this.numericUpDownCoordinatesY.ValueChanged -= this.NumericUpDownCoordinates_ValueChanged;

                this.numericUpDownHeight.ValueChanged -= this.TerrainProperty_ValueChanged;
                this.comboBoxTerrainType.SelectedIndexChanged -= this.TerrainProperty_ValueChanged;

                this.events = false;
            }
        }

        /// <summary>
        /// Sets the correct height and type on the GUI and refreshes fluids, from the selected coordinates.
        /// Also enables and disables controls depending on the state of the selected coordinates.
        /// </summary>
        private void UpdateGUI()
        {
            Vector3 position = new Vector3(this.GlobalCoordinate.X, this.GlobalCoordinate.Y, 0);

            this.TerrainHeight = EngineManager.Engine.Terrain.GetTerrainElevation(position);
            this.TerrainType = EngineManager.Engine.Terrain.GetTerrainType(position);

            TerrainPatch tpat = EngineManager.Engine.Terrain.GetLoadedTerrainPatch(position);
            this.numericUpDownHeight.Enabled = tpat != null;
            this.comboBoxTerrainType.Enabled = tpat != null;
            this.buttonGlueEdges.Enabled = tpat != null;

            this.RefreshFluids(EngineManager.Engine.Terrain.GetTerrainPatch(this.PatchID));
            this.groupBoxPatchFluids.Enabled = tpat != null;
        }

        /// <summary>
        /// Called when the terrain's height level is changed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NumericUpDownCurrentHeightLevel_ValueChanged(object sender, EventArgs e)
        {
            this.SetEvents(false);

            EngineManager.Engine.Terrain.CurrentHeightLevel = this.CurrentHeightLevel;

            this.UpdateGUI();

            this.SetEvents(true);

            this.PropertyChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when the terrain's current season is changed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ComboBoxCurrentSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetEvents(false);

            EngineManager.Engine.Terrain.CurrentSeason = this.CurrentSeason;

            this.UpdateGUI();

            this.SetEvents(true);

            this.PropertyChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when the terrain's hide base season terrain check box's check-state is changed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void CheckBoxHideBaseSeasonTerrain_CheckedChanged(object sender, EventArgs e)
        {
            this.SetEvents(false);

            EngineManager.Engine.Terrain.HideBaseSeasonTerrain = this.HideBaseSeasonTerrain;

            this.UpdateGUI();

            this.SetEvents(true);

            this.PropertyChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when the selected terrain patch ID is changed. This method:
        ///     Resets the selected patch vertex to zero.
        ///     Changes the selected global coordinate accordingly.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NumericUpDownPatchID_ValueChanged(object sender, System.EventArgs e)
        {
            this.SetEvents(false);

            Xna.Point global = CoordinateConverter.LocalToWorld(this.PatchID, Xna.Point.Zero);

            this.PatchVertex = Xna.Point.Zero;
            this.GlobalCoordinate = global;

            this.UpdateGUI();

            this.SetEvents(true);
        }

        /// <summary>
        /// Called when the selected terrain patch vertex is changed. This method:
        ///     Changes the selected global coordinate accordingly.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NumericUpDownPatchVertex_ValueChanged(object sender, EventArgs e)
        {
            this.SetEvents(false);

            this.GlobalCoordinate = CoordinateConverter.LocalToWorld(this.PatchID, this.PatchVertex);

            this.UpdateGUI();

            this.SetEvents(true);
        }

        /// <summary>
        /// Called when the selected global terrain coordinate is changed. This method:
        ///     Changes the selected terrain patch ID accordingly.
        ///     Changes the selected patch vertex accordingly.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NumericUpDownCoordinates_ValueChanged(object sender, EventArgs e)
        {
            this.SetEvents(false);

            Xna.Point vertexCoords = CoordinateConverter.WorldToLocal(this.GlobalCoordinate);

            this.PatchID = TerrainManager.GetTerrainPatchId(this.GlobalCoordinate, EngineManager.Engine.Terrain.CurrentHeightLevel);
            this.PatchVertex = vertexCoords;

            this.UpdateGUI();

            this.SetEvents(true);
        }

        /// <summary>
        /// Changes the height or texture of the terrain as requested by the user.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TerrainProperty_ValueChanged(object sender, System.EventArgs e)
        {
            // It is IMPORTANT we use this "events" variable
            // as this method can be inappropriately invoked
            // when events are off, which we do not want
            if (this.initialized && this.events)
            {
                // If this was GUI setting...
                if (sender != null)
                {
                    TerrainPatch tpat = EngineManager.Engine.Terrain.GetLoadedTerrainPatch(this.PatchID);
                    if (tpat != null)
                    {
                        Xna.Point localCoords = CoordinateConverter.WorldToLocal(this.GlobalCoordinate);

                        ReadOnlyCollection<TerrainPatch> pat = null;

                        if (sender == this.numericUpDownHeight)
                        {
                            pat = tpat.SetElevationData(localCoords, this.TerrainHeight);
                        }
                        else if (sender == this.comboBoxTerrainType)
                        {
                            pat = tpat.SetTerrainTypeData(localCoords, this.TerrainType);
                        }

                        if (pat != null)
                        {
                            for (int i = 0; i < pat.Count; i++)
                            {
                                if (!this.unsavedPatches.Contains(pat[i]))
                                {
                                    this.unsavedPatches.Add(pat[i]);
                                }
                            }
                        }

                        this.CommitTerrainChanges();
                    }
                }
                else
                {
                    // Collection of errors to display in the log
                    StringCollection errors = new StringCollection();

                    // Otherwise it was mouse setting
                    ReadOnlyCollection<VertexProperties> vertexProps = TerrainCursor.GetVertexProperties(EngineManager.Engine.Terrain, this.CursorSize, this.GlobalCoordinate, this.TerrainTool, this.Intensity, this.ApplyTerrainType);

                    for (int j = 0; j < vertexProps.Count; j++)
                    {
                        TerrainPatch tpat = EngineManager.Engine.Terrain.GetLoadedTerrainPatch(vertexProps[j].MainPatchId);
                        if (tpat != null)
                        {
                            // Set the base height or terrain type data
                            Xna.Point localCoords = CoordinateConverter.WorldToLocal(vertexProps[j].GlobalCoordinate);
                            float currentHeight = tpat.GetElevationData(localCoords.X, localCoords.Y);

                            ReadOnlyCollection<TerrainPatch> pat = null;

                            if (vertexProps[j].Height.HasValue)
                            {
                                pat = tpat.SetElevationData(localCoords, currentHeight + vertexProps[j].Height.Value);
                            }

                            if (vertexProps[j].TerrainType.HasValue)
                            {
                                pat = tpat.SetTerrainTypeData(localCoords, vertexProps[j].TerrainType.Value);
                            }

                            if (pat != null)
                            {
                                for (int i = 0; i < pat.Count; i++)
                                {
                                    if (!this.unsavedPatches.Contains(pat[i]))
                                    {
                                        this.unsavedPatches.Add(pat[i]);
                                    }
                                }
                            }
                            else
                            {
                                // We don't need this message for the select tool...
                                if (this.TerrainTool != TerrainControlTools.Select)
                                {
                                    string error = string.Format(CultureInfo.InvariantCulture, "WARNING: One or more of the terrain patches containing the global coordinates {0} on height level {1} are not currently loaded into memory and were not updated.", this.GlobalCoordinate, this.CurrentHeightLevel);

                                    if (!errors.Contains(error))
                                    {
                                        errors.Add(error);
                                    }
                                }
                            }
                        }
                    }

                    for (int i = 0; i < errors.Count; i++)
                    {
                        WorldEditorForm.Instance.AppendLogText(errors[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Glues the currently selected tile's geometry with that of the tiles bordering the west, southwest and south sides.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ButtonGlueEdges_Click(object sender, EventArgs e)
        {
            TerrainPatch tpat = EngineManager.Engine.Terrain.GetLoadedTerrainPatch(new Vector3(this.GlobalCoordinate.X, this.GlobalCoordinate.Y, 0));
            if (tpat != null)
            {
                ReadOnlyCollection<TerrainPatch> affectedPatches = tpat.GlueEdges();

                for (int i = 0; i < affectedPatches.Count; i++)
                {
                    File.WriteAllBytes(Helper.GetPatchFileName(affectedPatches[i].PatchId), affectedPatches[i].ToByteArray());
                    EngineManager.Engine.Terrain.ForcePatchReload(affectedPatches[i].PatchId);
                }
            }
        }

        /// <summary>
        /// Ensures the apply texture combo box is only enabled if the texture radio button is checked.
        /// Ensures the intensity numeric up down is only enabled if the texture radio button is not checked.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void RadioButtonHeightTextureTool_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDownCursorSize.Enabled = !this.TerrainTool.IsSet(TerrainControlTools.Select) || this.TerrainTool.IsSet(TerrainControlTools.TerrainType);
            this.numericUpDownIntensity.Enabled = !this.TerrainTool.IsSet(TerrainControlTools.Select);
            this.comboBoxApplyTerrainType.Enabled = this.TerrainTool.IsSet(TerrainControlTools.TerrainType);

            this.PropertyChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Reloads the list of fluids for the specified terrain patch.
        /// </summary>
        /// <param name="patch">The terrain patch to reload liquids for.</param>
        private void RefreshFluids(TerrainPatch patch)
        {
            this.listBoxPatchFluids.Items.Clear();

            if (patch != null)
            {
                for (int i = 0; i < patch.Fluids.Count; i++)
                {
                    this.listBoxPatchFluids.Items.Add(patch.Fluids[i]);
                }
            }
        }

        /// <summary>
        /// Prompts the user to add a liquid to the selected terrain patch.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void LinkLabelAddFluid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TerrainPatch tpat = EngineManager.Engine.Terrain.GetTerrainPatch(this.PatchID);
            if (tpat != null)
            {
                AddEditFluidDialog fluidDialog = new AddEditFluidDialog();
                if (fluidDialog.ShowDialog() == DialogResult.OK)
                {
                    Fluid fluid = new Fluid(this.PatchID);

                    ReadOnlyCollection<Season> seasons = SeasonExtensions.GetSeasons();
                    for (int i = 0; i < seasons.Count; i++)
                    {
                        fluid.SetFluidType(seasons[i], fluidDialog.GetFluidType(seasons[i]));
                        fluid.SetPoint(seasons[i], FluidVertex.Southwest, fluidDialog.GetVertex(seasons[i], FluidVertex.Southwest));
                        fluid.SetPoint(seasons[i], FluidVertex.Southeast, fluidDialog.GetVertex(seasons[i], FluidVertex.Southeast));
                        fluid.SetPoint(seasons[i], FluidVertex.Northwest, fluidDialog.GetVertex(seasons[i], FluidVertex.Northwest));
                        fluid.SetPoint(seasons[i], FluidVertex.Northeast, fluidDialog.GetVertex(seasons[i], FluidVertex.Northeast));
                        fluid.SetFlowDirection(seasons[i], fluidDialog.GetFlowDirection(seasons[i]));
                        fluid.SetFlowSpeed(seasons[i], fluidDialog.GetFlowSpeed(seasons[i]));
                    }

                    tpat.Fluids.Add(fluid);

                    this.RefreshFluids(tpat);

                    File.WriteAllBytes(Helper.GetPatchFileName(tpat.PatchId), tpat.ToByteArray());
                    EngineManager.Engine.Terrain.ForcePatchReload(tpat.PatchId);
                }
            }
        }

        /// <summary>
        /// Prompts the user to modify an existing fluid associated with the selected terrain patch.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void LinkLabelEditFluid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.listBoxPatchFluids.SelectedIndex >= 0)
            {
                TerrainPatch tpat = EngineManager.Engine.Terrain.GetTerrainPatch(this.PatchID);
                if (tpat != null)
                {
                    ReadOnlyCollection<Season> seasons = SeasonExtensions.GetSeasons();

                    AddEditFluidDialog fluidDialog = new AddEditFluidDialog();

                    for (int i = 0; i < seasons.Count; i++)
                    {
                        fluidDialog.SetFluidType(seasons[i], tpat.Fluids[this.listBoxPatchFluids.SelectedIndex].GetFluidType(seasons[i]));
                        fluidDialog.SetVertex(seasons[i], FluidVertex.Southwest, tpat.Fluids[this.listBoxPatchFluids.SelectedIndex].GetPoint(seasons[i], FluidVertex.Southwest));
                        fluidDialog.SetVertex(seasons[i], FluidVertex.Southeast, tpat.Fluids[this.listBoxPatchFluids.SelectedIndex].GetPoint(seasons[i], FluidVertex.Southeast));
                        fluidDialog.SetVertex(seasons[i], FluidVertex.Northwest, tpat.Fluids[this.listBoxPatchFluids.SelectedIndex].GetPoint(seasons[i], FluidVertex.Northwest));
                        fluidDialog.SetVertex(seasons[i], FluidVertex.Northeast, tpat.Fluids[this.listBoxPatchFluids.SelectedIndex].GetPoint(seasons[i], FluidVertex.Northeast));
                        fluidDialog.SetFlowDirection(seasons[i], tpat.Fluids[this.listBoxPatchFluids.SelectedIndex].GetFlowDirection(seasons[i]));
                        fluidDialog.SetFlowSpeed(seasons[i], tpat.Fluids[this.listBoxPatchFluids.SelectedIndex].GetFlowSpeed(seasons[i]));
                    }

                    if (fluidDialog.ShowDialog() == DialogResult.OK)
                    {
                        Fluid fluid = tpat.Fluids[this.listBoxPatchFluids.SelectedIndex];

                        for (int i = 0; i < seasons.Count; i++)
                        {
                            fluid.SetFluidType(seasons[i], fluidDialog.GetFluidType(seasons[i]));
                            fluid.SetPoint(seasons[i], FluidVertex.Southwest, fluidDialog.GetVertex(seasons[i], FluidVertex.Southwest));
                            fluid.SetPoint(seasons[i], FluidVertex.Southeast, fluidDialog.GetVertex(seasons[i], FluidVertex.Southeast));
                            fluid.SetPoint(seasons[i], FluidVertex.Northwest, fluidDialog.GetVertex(seasons[i], FluidVertex.Northwest));
                            fluid.SetPoint(seasons[i], FluidVertex.Northeast, fluidDialog.GetVertex(seasons[i], FluidVertex.Northeast));
                            fluid.SetFlowDirection(seasons[i], fluidDialog.GetFlowDirection(seasons[i]));
                            fluid.SetFlowSpeed(seasons[i], fluidDialog.GetFlowSpeed(seasons[i]));
                        }

                        this.RefreshFluids(tpat);

                        File.WriteAllBytes(Helper.GetPatchFileName(tpat.PatchId), tpat.ToByteArray());
                        EngineManager.Engine.Terrain.ForcePatchReload(tpat.PatchId);
                    }
                }
            }
        }

        /// <summary>
        /// Prompts the user to remove an existing fluid associated with the selected terrain patch.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void LinkLabelRemoveFluid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.listBoxPatchFluids.SelectedIndex >= 0)
            {
                TerrainPatch tpat = EngineManager.Engine.Terrain.GetTerrainPatch(this.PatchID);
                if (tpat != null)
                {
                    tpat.Fluids.RemoveAt(this.listBoxPatchFluids.SelectedIndex);

                    this.RefreshFluids(tpat);

                    File.WriteAllBytes(Helper.GetPatchFileName(tpat.PatchId), tpat.ToByteArray());
                    EngineManager.Engine.Terrain.ForcePatchReload(tpat.PatchId);
                }
            }
        }

        /// <summary>
        /// Support method for terrain toolbar.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NumericUpDownCursorSize_ValueChanged(object sender, EventArgs e)
        {
            if (this.initialized)
            {
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Support method for terrain toolbar.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NumericUpDownIntensity_ValueChanged(object sender, EventArgs e)
        {
            if (this.initialized)
            {
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Support method for terrain toolbar.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ComboBoxApplyTerrainType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.initialized)
            {
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }
    }
}
