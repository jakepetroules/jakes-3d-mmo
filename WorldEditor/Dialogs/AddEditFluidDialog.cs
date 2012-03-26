namespace MMO3D.WorldEditor
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;
    using MMO3D.Engine;

    /// <summary>
    /// Defines a dialog that allows the user to define the properties of a fluid.
    /// </summary>
    public partial class AddEditFluidDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the AddEditFluidDialog class.
        /// </summary>
        public AddEditFluidDialog()
        {
            this.InitializeComponent();

            // Add types to comboboxes
            this.comboBoxFluidTypeMidseason.Items.AddRange(FluidTypeExtensions.GetSortedList());
            this.comboBoxFluidTypeMidseason.SelectedIndex = 0;

            this.comboBoxFluidTypeSummer.Items.AddRange(FluidTypeExtensions.GetSortedList());
            this.comboBoxFluidTypeSummer.SelectedIndex = 0;

            this.comboBoxFluidTypeWinter.Items.AddRange(FluidTypeExtensions.GetSortedList());
            this.comboBoxFluidTypeWinter.SelectedIndex = 0;

            // Add flow directions to comboboxes
            this.comboBoxFlowDirectionMidseason.Items.AddRange(FluidFlowDirectionExtensions.GetSortedList());
            this.comboBoxFlowDirectionMidseason.SelectedIndex = 0;

            this.comboBoxFlowDirectionSummer.Items.AddRange(FluidFlowDirectionExtensions.GetSortedList());
            this.comboBoxFlowDirectionSummer.SelectedIndex = 0;

            this.comboBoxFlowDirectionWinter.Items.AddRange(FluidFlowDirectionExtensions.GetSortedList());
            this.comboBoxFlowDirectionWinter.SelectedIndex = 0;

            // Set combobox limits for midseason
            this.numericUpDownSWXMidseason.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownSWYMidseason.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownSEXMidseason.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownSEYMidseason.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownNWXMidseason.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownNWYMidseason.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownNEXMidseason.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownNEYMidseason.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownWidthMidseason.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownDepthMidseason.Maximum = TerrainPatch.PatchSize;

            // Set combobox limits for summer
            this.numericUpDownSWXSummer.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownSWYSummer.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownSEXSummer.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownSEYSummer.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownNWXSummer.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownNWYSummer.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownNEXSummer.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownNEYSummer.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownWidthSummer.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownDepthSummer.Maximum = TerrainPatch.PatchSize;

            // Set combobox limits for winter
            this.numericUpDownSWXWinter.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownSWYWinter.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownSEXWinter.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownSEYWinter.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownNWXWinter.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownNWYWinter.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownNEXWinter.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownNEYWinter.Maximum = TerrainPatch.PatchSize;

            this.numericUpDownWidthWinter.Maximum = TerrainPatch.PatchSize;
            this.numericUpDownDepthWinter.Maximum = TerrainPatch.PatchSize;
        }

        /// <summary>
        /// Enumerates the axes of the three dimensions.
        /// </summary>
        private enum Axis
        {
            /// <summary>
            /// Specifies the X axis.
            /// </summary>
            X,

            /// <summary>
            /// Specifies the Y axis.
            /// </summary>
            Y,

            /// <summary>
            /// Specifies the Z axis.
            /// </summary>
            Z
        }

        /// <summary>
        /// Gets the fluid type of this fluid in the particular season.
        /// </summary>
        /// <param name="season">The season to get the fluid type in.</param>
        /// <returns>See summary.</returns>
        public FluidType GetFluidType(Season season)
        {
            return FluidTypeExtensions.ParseFromString(this.GetFluidTypeControl(season).SelectedItem.ToString());
        }

        /// <summary>
        /// Sets the fluid type of this fluid in the particular season.
        /// </summary>
        /// <param name="season">The season to set the fluid type in.</param>
        /// <param name="fluidType">The fluid type to set.</param>
        public void SetFluidType(Season season, FluidType fluidType)
        {
            StringCollection fluidTypes = new StringCollection();
            fluidTypes.AddRange(FluidTypeExtensions.GetSortedList());

            this.GetFluidTypeControl(season).SelectedIndex = fluidTypes.IndexOf(fluidType.ToString());
        }

        /// <summary>
        /// Gets the vertex position of the specified corner of the fluid in the particular season.
        /// </summary>
        /// <param name="season">The season to get the vertex position in.</param>
        /// <param name="fluidVertex">The fluid vertex to get the position of.</param>
        /// <returns>See summary.</returns>
        public Vector3 GetVertex(Season season, FluidVertex fluidVertex)
        {
            float x = Convert.ToSingle(this.GetVertexControl(season, fluidVertex, Axis.X).Value);
            float y = Convert.ToSingle(this.GetVertexControl(season, fluidVertex, Axis.Y).Value);
            float z = Convert.ToSingle(this.GetVertexControl(season, fluidVertex, Axis.Z).Value);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Sets the vertex position of the specified corner of the fluid in the particular season.
        /// </summary>
        /// <param name="season">The season to set the vertex position in.</param>
        /// <param name="fluidVertex">The fluid vertex to set the position of.</param>
        /// <param name="vertex">The vertex position to set.</param>
        public void SetVertex(Season season, FluidVertex fluidVertex, Vector3 vertex)
        {
            this.GetVertexControl(season, fluidVertex, Axis.X).Value = Convert.ToDecimal(vertex.X);
            this.GetVertexControl(season, fluidVertex, Axis.Y).Value = Convert.ToDecimal(vertex.Y);
            this.GetVertexControl(season, fluidVertex, Axis.Z).Value = Convert.ToDecimal(vertex.Z);
        }

        /// <summary>
        /// Gets the direction in which the fluid flows in the particular season.
        /// </summary>
        /// <param name="season">The season to get the flow direction in.</param>
        /// <returns>See summary.</returns>
        public FluidFlowDirection GetFlowDirection(Season season)
        {
            return FluidFlowDirectionExtensions.ParseFromString(this.GetFlowDirectionControl(season).SelectedItem.ToString());
        }

        /// <summary>
        /// Sets the direction in which the fluid flows in the particular season.
        /// </summary>
        /// <param name="season">The season to set the flow direction in.</param>
        /// <param name="flowDirection">The flow direction to set.</param>
        public void SetFlowDirection(Season season, FluidFlowDirection flowDirection)
        {
            StringCollection fluidFlowDirections = new StringCollection();
            fluidFlowDirections.AddRange(FluidFlowDirectionExtensions.GetSortedList());

            this.GetFlowDirectionControl(season).SelectedIndex = fluidFlowDirections.IndexOf(flowDirection.ToString());
        }

        /// <summary>
        /// Gets the speed at which the fluid flows in the particular season.
        /// </summary>
        /// <param name="season">The season to get the flow speed in.</param>
        /// <returns>See summary.</returns>
        public float GetFlowSpeed(Season season)
        {
            return Convert.ToSingle(this.GetFlowSpeedControl(season).Value);
        }

        /// <summary>
        /// Sets the speed at which the fluid flows in the particular season.
        /// </summary>
        /// <param name="season">The season to set the flow speed in.</param>
        /// <param name="flowSpeed">The flow speed to set.</param>
        public void SetFlowSpeed(Season season, float flowSpeed)
        {
            this.GetFlowSpeedControl(season).Value = Convert.ToDecimal(flowSpeed);
        }

        /// <summary>
        /// Sets the coordinates of the fluid so that it will fill the patch in which it resides.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void LinkLabelFill_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Season season;

                if (linkLabel == this.linkLabelFillMidseason)
                {
                    season = Season.Midseason;
                }
                else if (linkLabel == this.linkLabelFillPatchSummer)
                {
                    season = Season.Summer;
                }
                else if (linkLabel == this.linkLabelFillPatchWinter)
                {
                    season = Season.Winter;
                }
                else
                {
                    return;
                }

                this.SetVertex(season, FluidVertex.Southwest, Vector3.Zero);
                this.SetVertex(season, FluidVertex.Southeast, new Vector3(TerrainPatch.PatchSize, 0, 0));
                this.SetVertex(season, FluidVertex.Northwest, new Vector3(0, TerrainPatch.PatchSize, 0));
                this.SetVertex(season, FluidVertex.Northeast, new Vector3(TerrainPatch.PatchSize, TerrainPatch.PatchSize, 0));
            }
        }

        /// <summary>
        /// Matches the X coordinates of the south-east and north-east points.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NumericUpDownSEX_NEX_ValueChanged(object sender, EventArgs e)
        {
            ReadOnlyCollection<Season> seasons = SeasonExtensions.GetSeasons();
            for (int i = 0; i < seasons.Count; i++)
            {
                if (sender == this.GetVertexControl(seasons[i], FluidVertex.Northeast, Axis.X))
                {
                    this.GetVertexControl(seasons[i], FluidVertex.Southeast, Axis.X).Value = this.GetVertexControl(seasons[i], FluidVertex.Northeast, Axis.X).Value;
                }
                else if (sender == this.GetVertexControl(seasons[i], FluidVertex.Southeast, Axis.X))
                {
                    this.GetVertexControl(seasons[i], FluidVertex.Northeast, Axis.X).Value = this.GetVertexControl(seasons[i], FluidVertex.Southeast, Axis.X).Value;
                }

                this.CalculateWidthAndDepth();
            }
        }

        /// <summary>
        /// Matches the X coordinates of the south-west and north-west points.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NumericUpDownSWX_NWX_ValueChanged(object sender, EventArgs e)
        {
            ReadOnlyCollection<Season> seasons = SeasonExtensions.GetSeasons();
            for (int i = 0; i < seasons.Count; i++)
            {
                if (sender == this.GetVertexControl(seasons[i], FluidVertex.Northwest, Axis.X))
                {
                    this.GetVertexControl(seasons[i], FluidVertex.Southwest, Axis.X).Value = this.GetVertexControl(seasons[i], FluidVertex.Northwest, Axis.X).Value;
                }
                else if (sender == this.GetVertexControl(seasons[i], FluidVertex.Southwest, Axis.X))
                {
                    this.GetVertexControl(seasons[i], FluidVertex.Northwest, Axis.X).Value = this.GetVertexControl(seasons[i], FluidVertex.Southwest, Axis.X).Value;
                }

                this.CalculateWidthAndDepth();
            }
        }

        /// <summary>
        /// Matches Y coordinates of the south-west and south-east points.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NumericUpDownSWY_SEY_ValueChanged(object sender, EventArgs e)
        {
            ReadOnlyCollection<Season> seasons = SeasonExtensions.GetSeasons();
            for (int i = 0; i < seasons.Count; i++)
            {
                if (sender == this.GetVertexControl(seasons[i], FluidVertex.Southeast, Axis.Y))
                {
                    this.GetVertexControl(seasons[i], FluidVertex.Southwest, Axis.Y).Value = this.GetVertexControl(seasons[i], FluidVertex.Southeast, Axis.Y).Value;
                }
                else if (sender == this.GetVertexControl(seasons[i], FluidVertex.Southwest, Axis.Y))
                {
                    this.GetVertexControl(seasons[i], FluidVertex.Southeast, Axis.Y).Value = this.GetVertexControl(seasons[i], FluidVertex.Southwest, Axis.Y).Value;
                }

                this.CalculateWidthAndDepth();
            }
        }

        /// <summary>
        /// Matches Y coordinates of the north-west and north-east points.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NumericUpDownNWY_NEY_ValueChanged(object sender, EventArgs e)
        {
            ReadOnlyCollection<Season> seasons = SeasonExtensions.GetSeasons();
            for (int i = 0; i < seasons.Count; i++)
            {
                if (sender == this.GetVertexControl(seasons[i], FluidVertex.Northeast, Axis.Y))
                {
                    this.GetVertexControl(seasons[i], FluidVertex.Northwest, Axis.Y).Value = this.GetVertexControl(seasons[i], FluidVertex.Northeast, Axis.Y).Value;
                }
                else if (sender == this.GetVertexControl(seasons[i], FluidVertex.Northwest, Axis.Y))
                {
                    this.GetVertexControl(seasons[i], FluidVertex.Northeast, Axis.Y).Value = this.GetVertexControl(seasons[i], FluidVertex.Northwest, Axis.Y).Value;
                }

                this.CalculateWidthAndDepth();
            }
        }

        /// <summary>
        /// Calculates the width and depth of the fluid for all seasons.
        /// </summary>
        private void CalculateWidthAndDepth()
        {
            ReadOnlyCollection<Season> seasons = SeasonExtensions.GetSeasons();
            for (int i = 0; i < seasons.Count; i++)
            {
                this.GetWidthControl(seasons[i]).Value = this.GetVertexControl(seasons[i], FluidVertex.Northeast, Axis.X).Value - this.GetVertexControl(seasons[i], FluidVertex.Southwest, Axis.X).Value;
                this.GetDepthControl(seasons[i]).Value = this.GetVertexControl(seasons[i], FluidVertex.Northeast, Axis.Y).Value - this.GetVertexControl(seasons[i], FluidVertex.Southwest, Axis.Y).Value;
            }
        }

        /// <summary>
        /// Gets the GUI control for the fluid type in the specified season.
        /// </summary>
        /// <param name="season">The season to get the control for.</param>
        /// <returns>See summary.</returns>
        private ComboBox GetFluidTypeControl(Season season)
        {
            switch (season)
            {
                case Season.Midseason:
                    return this.comboBoxFluidTypeMidseason;
                case Season.Summer:
                    return this.comboBoxFluidTypeSummer;
                case Season.Winter:
                    return this.comboBoxFluidTypeWinter;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the GUI control for the vertex in the specified season and with the specified axis.
        /// </summary>
        /// <param name="season">The season to get the control for.</param>
        /// <param name="fluidVertex">The vertex to get the control for.</param>
        /// <param name="axis">The axis to get the control for.</param>
        /// <returns>See summary.</returns>
        private NumericUpDown GetVertexControl(Season season, FluidVertex fluidVertex, Axis axis)
        {
            switch (season)
            {
                case Season.Midseason:
                    switch (fluidVertex)
                    {
                        case FluidVertex.Southwest:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownSWXMidseason;
                                case Axis.Y:
                                    return this.numericUpDownSWYMidseason;
                                case Axis.Z:
                                    return this.numericUpDownSWZMidseason;
                                default:
                                    return null;
                            }

                        case FluidVertex.Southeast:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownSEXMidseason;
                                case Axis.Y:
                                    return this.numericUpDownSEYMidseason;
                                case Axis.Z:
                                    return this.numericUpDownSEZMidseason;
                                default:
                                    return null;
                            }

                        case FluidVertex.Northwest:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownNWXMidseason;
                                case Axis.Y:
                                    return this.numericUpDownNWYMidseason;
                                case Axis.Z:
                                    return this.numericUpDownNWZMidseason;
                                default:
                                    return null;
                            }

                        case FluidVertex.Northeast:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownNEXMidseason;
                                case Axis.Y:
                                    return this.numericUpDownNEYMidseason;
                                case Axis.Z:
                                    return this.numericUpDownNEZMidseason;
                                default:
                                    return null;
                            }

                        default:
                            return null;
                    }

                case Season.Summer:
                    switch (fluidVertex)
                    {
                        case FluidVertex.Southwest:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownSWXSummer;
                                case Axis.Y:
                                    return this.numericUpDownSWYSummer;
                                case Axis.Z:
                                    return this.numericUpDownSWZSummer;
                                default:
                                    return null;
                            }

                        case FluidVertex.Southeast:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownSEXSummer;
                                case Axis.Y:
                                    return this.numericUpDownSEYSummer;
                                case Axis.Z:
                                    return this.numericUpDownSEZSummer;
                                default:
                                    return null;
                            }

                        case FluidVertex.Northwest:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownNWXSummer;
                                case Axis.Y:
                                    return this.numericUpDownNWYSummer;
                                case Axis.Z:
                                    return this.numericUpDownNWZSummer;
                                default:
                                    return null;
                            }

                        case FluidVertex.Northeast:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownNEXSummer;
                                case Axis.Y:
                                    return this.numericUpDownNEYSummer;
                                case Axis.Z:
                                    return this.numericUpDownNEZSummer;
                                default:
                                    return null;
                            }

                        default:
                            return null;
                    }

                case Season.Winter:
                    switch (fluidVertex)
                    {
                        case FluidVertex.Southwest:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownSWXWinter;
                                case Axis.Y:
                                    return this.numericUpDownSWYWinter;
                                case Axis.Z:
                                    return this.numericUpDownSWZWinter;
                                default:
                                    return null;
                            }

                        case FluidVertex.Southeast:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownSEXWinter;
                                case Axis.Y:
                                    return this.numericUpDownSEYWinter;
                                case Axis.Z:
                                    return this.numericUpDownSEZWinter;
                                default:
                                    return null;
                            }

                        case FluidVertex.Northwest:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownNWXWinter;
                                case Axis.Y:
                                    return this.numericUpDownNWYWinter;
                                case Axis.Z:
                                    return this.numericUpDownNWZWinter;
                                default:
                                    return null;
                            }

                        case FluidVertex.Northeast:
                            switch (axis)
                            {
                                case Axis.X:
                                    return this.numericUpDownNEXWinter;
                                case Axis.Y:
                                    return this.numericUpDownNEYWinter;
                                case Axis.Z:
                                    return this.numericUpDownNEZWinter;
                                default:
                                    return null;
                            }

                        default:
                            return null;
                    }

                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the GUI control for the flow direction in the specified season.
        /// </summary>
        /// <param name="season">The season to get the control for.</param>
        /// <returns>See summary.</returns>
        private ComboBox GetFlowDirectionControl(Season season)
        {
            switch (season)
            {
                case Season.Midseason:
                    return this.comboBoxFlowDirectionMidseason;
                case Season.Summer:
                    return this.comboBoxFlowDirectionSummer;
                case Season.Winter:
                    return this.comboBoxFlowDirectionWinter;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the GUI control for the flow speed in the specified season.
        /// </summary>
        /// <param name="season">The season to get the control for.</param>
        /// <returns>See summary.</returns>
        private NumericUpDown GetFlowSpeedControl(Season season)
        {
            switch (season)
            {
                case Season.Midseason:
                    return this.numericUpDownFlowSpeedMidseason;
                case Season.Summer:
                    return this.numericUpDownFlowSpeedSummer;
                case Season.Winter:
                    return this.numericUpDownFlowSpeedWinter;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the GUI control for the fluid width in the specified season.
        /// </summary>
        /// <param name="season">The season to get the control for.</param>
        /// <returns>See summary.</returns>
        private NumericUpDown GetWidthControl(Season season)
        {
            switch (season)
            {
                case Season.Midseason:
                    return this.numericUpDownWidthMidseason;
                case Season.Summer:
                    return this.numericUpDownWidthSummer;
                case Season.Winter:
                    return this.numericUpDownWidthWinter;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the GUI control for the fluid depth in the specified season.
        /// </summary>
        /// <param name="season">The season to get the control for.</param>
        /// <returns>See summary.</returns>
        private NumericUpDown GetDepthControl(Season season)
        {
            switch (season)
            {
                case Season.Midseason:
                    return this.numericUpDownDepthMidseason;
                case Season.Summer:
                    return this.numericUpDownDepthSummer;
                case Season.Winter:
                    return this.numericUpDownDepthWinter;
                default:
                    return null;
            }
        }
    }
}
