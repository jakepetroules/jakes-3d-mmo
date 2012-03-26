namespace MMO3D.WorldEditor
{
    /// <summary>
    /// The Windows Forms initialization of the terrain height sidebar control.
    /// </summary>
    public partial class TerrainControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Label showing the purpose of the height cursor size.
        /// </summary>
        private System.Windows.Forms.Label labelCursorSize;

        /// <summary>
        /// Cursor size of the height adjustment tool.
        /// </summary>
        private System.Windows.Forms.NumericUpDown numericUpDownCursorSize;
        
        /// <summary>
        /// The flow layout panel for the sidebar.
        /// </summary>
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMain;

        /// <summary>
        /// The height tool group box of controls.
        /// </summary>
        private System.Windows.Forms.GroupBox groupBoxElevationTerrainTypeTool;

        /// <summary>
        /// The flatten terrain option.
        /// </summary>
        private System.Windows.Forms.RadioButton radioButtonFlatten;

        /// <summary>
        /// The lower terrain option.
        /// </summary>
        private System.Windows.Forms.RadioButton radioButtonLower;

        /// <summary>
        /// The raise terrain option.
        /// </summary>
        private System.Windows.Forms.RadioButton radioButtonRaise;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numericUpDownCursorSize = new System.Windows.Forms.NumericUpDown();
            this.labelCursorSize = new System.Windows.Forms.Label();
            this.flowLayoutPanelMain = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxGlobal = new System.Windows.Forms.GroupBox();
            this.checkBoxHideBaseSeasonTerrain = new System.Windows.Forms.CheckBox();
            this.labelCurrentSeason = new System.Windows.Forms.Label();
            this.comboBoxCurrentSeason = new System.Windows.Forms.ComboBox();
            this.labelCurrentHeightLevel = new System.Windows.Forms.Label();
            this.numericUpDownCurrentHeightLevel = new System.Windows.Forms.NumericUpDown();
            this.groupBoxElevationTerrainTypeTool = new System.Windows.Forms.GroupBox();
            this.checkBoxTerrainType = new System.Windows.Forms.CheckBox();
            this.comboBoxApplyTerrainType = new System.Windows.Forms.ComboBox();
            this.labelIntensity = new System.Windows.Forms.Label();
            this.numericUpDownIntensity = new System.Windows.Forms.NumericUpDown();
            this.radioButtonSmooth = new System.Windows.Forms.RadioButton();
            this.radioButtonSelect = new System.Windows.Forms.RadioButton();
            this.radioButtonFlatten = new System.Windows.Forms.RadioButton();
            this.radioButtonLower = new System.Windows.Forms.RadioButton();
            this.radioButtonRaise = new System.Windows.Forms.RadioButton();
            this.groupBoxManualData = new System.Windows.Forms.GroupBox();
            this.labelPatchIDZ = new System.Windows.Forms.Label();
            this.numericUpDownPatchIDZ = new System.Windows.Forms.NumericUpDown();
            this.labelPatchCoordsY = new System.Windows.Forms.Label();
            this.labelPatchVertexY = new System.Windows.Forms.Label();
            this.labelPatchIDY = new System.Windows.Forms.Label();
            this.labelPatchCoordsX = new System.Windows.Forms.Label();
            this.labelPatchVertexX = new System.Windows.Forms.Label();
            this.labelPatchIDX = new System.Windows.Forms.Label();
            this.buttonGlueEdges = new System.Windows.Forms.Button();
            this.comboBoxTerrainType = new System.Windows.Forms.ComboBox();
            this.labelTerrainType = new System.Windows.Forms.Label();
            this.labelElevation = new System.Windows.Forms.Label();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.labelGlobalCoords = new System.Windows.Forms.Label();
            this.numericUpDownCoordinatesY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCoordinatesX = new System.Windows.Forms.NumericUpDown();
            this.labelPatchVertex = new System.Windows.Forms.Label();
            this.numericUpDownPatchVertexY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPatchVertexX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPatchIDY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPatchIDX = new System.Windows.Forms.NumericUpDown();
            this.labelPatchID = new System.Windows.Forms.Label();
            this.groupBoxPatchFluids = new System.Windows.Forms.GroupBox();
            this.linkLabelEditFluid = new System.Windows.Forms.LinkLabel();
            this.linkLabelRemoveFluid = new System.Windows.Forms.LinkLabel();
            this.linkLabelAddFluid = new System.Windows.Forms.LinkLabel();
            this.listBoxPatchFluids = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCursorSize)).BeginInit();
            this.flowLayoutPanelMain.SuspendLayout();
            this.groupBoxGlobal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCurrentHeightLevel)).BeginInit();
            this.groupBoxElevationTerrainTypeTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntensity)).BeginInit();
            this.groupBoxManualData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatchIDZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCoordinatesY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCoordinatesX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatchVertexY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatchVertexX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatchIDY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatchIDX)).BeginInit();
            this.groupBoxPatchFluids.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownCursorSize
            // 
            this.numericUpDownCursorSize.Location = new System.Drawing.Point(155, 26);
            this.numericUpDownCursorSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCursorSize.Name = "numericUpDownCursorSize";
            this.numericUpDownCursorSize.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownCursorSize.TabIndex = 7;
            this.numericUpDownCursorSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCursorSize.ValueChanged += new System.EventHandler(this.NumericUpDownCursorSize_ValueChanged);
            // 
            // labelCursorSize
            // 
            this.labelCursorSize.AutoSize = true;
            this.labelCursorSize.Location = new System.Drawing.Point(152, 10);
            this.labelCursorSize.Name = "labelCursorSize";
            this.labelCursorSize.Size = new System.Drawing.Size(61, 13);
            this.labelCursorSize.TabIndex = 6;
            this.labelCursorSize.Text = "Cursor size:";
            // 
            // flowLayoutPanelMain
            // 
            this.flowLayoutPanelMain.AutoScroll = true;
            this.flowLayoutPanelMain.Controls.Add(this.groupBoxGlobal);
            this.flowLayoutPanelMain.Controls.Add(this.groupBoxElevationTerrainTypeTool);
            this.flowLayoutPanelMain.Controls.Add(this.groupBoxManualData);
            this.flowLayoutPanelMain.Controls.Add(this.groupBoxPatchFluids);
            this.flowLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelMain.Name = "flowLayoutPanelMain";
            this.flowLayoutPanelMain.Size = new System.Drawing.Size(670, 320);
            this.flowLayoutPanelMain.TabIndex = 0;
            // 
            // groupBoxGlobal
            // 
            this.groupBoxGlobal.Controls.Add(this.checkBoxHideBaseSeasonTerrain);
            this.groupBoxGlobal.Controls.Add(this.labelCurrentSeason);
            this.groupBoxGlobal.Controls.Add(this.comboBoxCurrentSeason);
            this.groupBoxGlobal.Controls.Add(this.labelCurrentHeightLevel);
            this.groupBoxGlobal.Controls.Add(this.numericUpDownCurrentHeightLevel);
            this.groupBoxGlobal.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGlobal.Name = "groupBoxGlobal";
            this.groupBoxGlobal.Size = new System.Drawing.Size(216, 95);
            this.groupBoxGlobal.TabIndex = 0;
            this.groupBoxGlobal.TabStop = false;
            this.groupBoxGlobal.Text = "Global";
            // 
            // checkBoxHideBaseSeasonTerrain
            // 
            this.checkBoxHideBaseSeasonTerrain.AutoSize = true;
            this.checkBoxHideBaseSeasonTerrain.Location = new System.Drawing.Point(9, 72);
            this.checkBoxHideBaseSeasonTerrain.Name = "checkBoxHideBaseSeasonTerrain";
            this.checkBoxHideBaseSeasonTerrain.Size = new System.Drawing.Size(143, 17);
            this.checkBoxHideBaseSeasonTerrain.TabIndex = 4;
            this.checkBoxHideBaseSeasonTerrain.Text = "Hide base season terrain";
            this.checkBoxHideBaseSeasonTerrain.UseVisualStyleBackColor = true;
            this.checkBoxHideBaseSeasonTerrain.CheckedChanged += new System.EventHandler(this.CheckBoxHideBaseSeasonTerrain_CheckedChanged);
            // 
            // labelCurrentSeason
            // 
            this.labelCurrentSeason.AutoSize = true;
            this.labelCurrentSeason.Location = new System.Drawing.Point(6, 48);
            this.labelCurrentSeason.Name = "labelCurrentSeason";
            this.labelCurrentSeason.Size = new System.Drawing.Size(81, 13);
            this.labelCurrentSeason.TabIndex = 2;
            this.labelCurrentSeason.Text = "Current season:";
            // 
            // comboBoxCurrentSeason
            // 
            this.comboBoxCurrentSeason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrentSeason.FormattingEnabled = true;
            this.comboBoxCurrentSeason.Location = new System.Drawing.Point(110, 45);
            this.comboBoxCurrentSeason.Name = "comboBoxCurrentSeason";
            this.comboBoxCurrentSeason.Size = new System.Drawing.Size(100, 21);
            this.comboBoxCurrentSeason.TabIndex = 3;
            this.comboBoxCurrentSeason.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCurrentSeason_SelectedIndexChanged);
            // 
            // labelCurrentHeightLevel
            // 
            this.labelCurrentHeightLevel.AutoSize = true;
            this.labelCurrentHeightLevel.Location = new System.Drawing.Point(6, 21);
            this.labelCurrentHeightLevel.Name = "labelCurrentHeightLevel";
            this.labelCurrentHeightLevel.Size = new System.Drawing.Size(101, 13);
            this.labelCurrentHeightLevel.TabIndex = 0;
            this.labelCurrentHeightLevel.Text = "Current height level:";
            // 
            // numericUpDownCurrentHeightLevel
            // 
            this.numericUpDownCurrentHeightLevel.Location = new System.Drawing.Point(155, 19);
            this.numericUpDownCurrentHeightLevel.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownCurrentHeightLevel.Name = "numericUpDownCurrentHeightLevel";
            this.numericUpDownCurrentHeightLevel.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownCurrentHeightLevel.TabIndex = 1;
            this.numericUpDownCurrentHeightLevel.ValueChanged += new System.EventHandler(this.NumericUpDownCurrentHeightLevel_ValueChanged);
            // 
            // groupBoxElevationTerrainTypeTool
            // 
            this.groupBoxElevationTerrainTypeTool.Controls.Add(this.checkBoxTerrainType);
            this.groupBoxElevationTerrainTypeTool.Controls.Add(this.comboBoxApplyTerrainType);
            this.groupBoxElevationTerrainTypeTool.Controls.Add(this.labelIntensity);
            this.groupBoxElevationTerrainTypeTool.Controls.Add(this.numericUpDownIntensity);
            this.groupBoxElevationTerrainTypeTool.Controls.Add(this.radioButtonSmooth);
            this.groupBoxElevationTerrainTypeTool.Controls.Add(this.radioButtonSelect);
            this.groupBoxElevationTerrainTypeTool.Controls.Add(this.radioButtonFlatten);
            this.groupBoxElevationTerrainTypeTool.Controls.Add(this.labelCursorSize);
            this.groupBoxElevationTerrainTypeTool.Controls.Add(this.radioButtonLower);
            this.groupBoxElevationTerrainTypeTool.Controls.Add(this.numericUpDownCursorSize);
            this.groupBoxElevationTerrainTypeTool.Controls.Add(this.radioButtonRaise);
            this.groupBoxElevationTerrainTypeTool.Location = new System.Drawing.Point(3, 104);
            this.groupBoxElevationTerrainTypeTool.Name = "groupBoxElevationTerrainTypeTool";
            this.groupBoxElevationTerrainTypeTool.Size = new System.Drawing.Size(216, 118);
            this.groupBoxElevationTerrainTypeTool.TabIndex = 1;
            this.groupBoxElevationTerrainTypeTool.TabStop = false;
            this.groupBoxElevationTerrainTypeTool.Text = "Elevation and terrain type tool";
            // 
            // checkBoxTerrainType
            // 
            this.checkBoxTerrainType.AutoSize = true;
            this.checkBoxTerrainType.Location = new System.Drawing.Point(6, 93);
            this.checkBoxTerrainType.Name = "checkBoxTerrainType";
            this.checkBoxTerrainType.Size = new System.Drawing.Size(85, 17);
            this.checkBoxTerrainType.TabIndex = 5;
            this.checkBoxTerrainType.Text = "Terrain type:";
            this.checkBoxTerrainType.UseVisualStyleBackColor = true;
            this.checkBoxTerrainType.CheckedChanged += new System.EventHandler(this.RadioButtonHeightTextureTool_CheckedChanged);
            // 
            // comboBoxApplyTerrainType
            // 
            this.comboBoxApplyTerrainType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxApplyTerrainType.FormattingEnabled = true;
            this.comboBoxApplyTerrainType.Location = new System.Drawing.Point(110, 91);
            this.comboBoxApplyTerrainType.Name = "comboBoxApplyTerrainType";
            this.comboBoxApplyTerrainType.Size = new System.Drawing.Size(100, 21);
            this.comboBoxApplyTerrainType.TabIndex = 10;
            this.comboBoxApplyTerrainType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxApplyTerrainType_SelectedIndexChanged);
            // 
            // labelIntensity
            // 
            this.labelIntensity.AutoSize = true;
            this.labelIntensity.Location = new System.Drawing.Point(152, 49);
            this.labelIntensity.Name = "labelIntensity";
            this.labelIntensity.Size = new System.Drawing.Size(49, 13);
            this.labelIntensity.TabIndex = 8;
            this.labelIntensity.Text = "Intensity:";
            // 
            // numericUpDownIntensity
            // 
            this.numericUpDownIntensity.DecimalPlaces = 3;
            this.numericUpDownIntensity.Location = new System.Drawing.Point(155, 65);
            this.numericUpDownIntensity.Name = "numericUpDownIntensity";
            this.numericUpDownIntensity.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownIntensity.TabIndex = 9;
            this.numericUpDownIntensity.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownIntensity.ValueChanged += new System.EventHandler(this.NumericUpDownIntensity_ValueChanged);
            // 
            // radioButtonSmooth
            // 
            this.radioButtonSmooth.AutoSize = true;
            this.radioButtonSmooth.Location = new System.Drawing.Point(69, 65);
            this.radioButtonSmooth.Name = "radioButtonSmooth";
            this.radioButtonSmooth.Size = new System.Drawing.Size(61, 17);
            this.radioButtonSmooth.TabIndex = 4;
            this.radioButtonSmooth.Text = "Smooth";
            this.radioButtonSmooth.UseVisualStyleBackColor = true;
            this.radioButtonSmooth.CheckedChanged += new System.EventHandler(this.RadioButtonHeightTextureTool_CheckedChanged);
            // 
            // radioButtonSelect
            // 
            this.radioButtonSelect.AutoSize = true;
            this.radioButtonSelect.Location = new System.Drawing.Point(6, 19);
            this.radioButtonSelect.Name = "radioButtonSelect";
            this.radioButtonSelect.Size = new System.Drawing.Size(55, 17);
            this.radioButtonSelect.TabIndex = 0;
            this.radioButtonSelect.Text = "Select";
            this.radioButtonSelect.UseVisualStyleBackColor = true;
            this.radioButtonSelect.CheckedChanged += new System.EventHandler(this.RadioButtonHeightTextureTool_CheckedChanged);
            // 
            // radioButtonFlatten
            // 
            this.radioButtonFlatten.AutoSize = true;
            this.radioButtonFlatten.Location = new System.Drawing.Point(6, 65);
            this.radioButtonFlatten.Name = "radioButtonFlatten";
            this.radioButtonFlatten.Size = new System.Drawing.Size(57, 17);
            this.radioButtonFlatten.TabIndex = 3;
            this.radioButtonFlatten.Text = "Flatten";
            this.radioButtonFlatten.UseVisualStyleBackColor = true;
            this.radioButtonFlatten.CheckedChanged += new System.EventHandler(this.RadioButtonHeightTextureTool_CheckedChanged);
            // 
            // radioButtonLower
            // 
            this.radioButtonLower.AutoSize = true;
            this.radioButtonLower.Location = new System.Drawing.Point(69, 42);
            this.radioButtonLower.Name = "radioButtonLower";
            this.radioButtonLower.Size = new System.Drawing.Size(54, 17);
            this.radioButtonLower.TabIndex = 2;
            this.radioButtonLower.Text = "Lower";
            this.radioButtonLower.UseVisualStyleBackColor = true;
            this.radioButtonLower.CheckedChanged += new System.EventHandler(this.RadioButtonHeightTextureTool_CheckedChanged);
            // 
            // radioButtonRaise
            // 
            this.radioButtonRaise.AutoSize = true;
            this.radioButtonRaise.Location = new System.Drawing.Point(6, 42);
            this.radioButtonRaise.Name = "radioButtonRaise";
            this.radioButtonRaise.Size = new System.Drawing.Size(52, 17);
            this.radioButtonRaise.TabIndex = 1;
            this.radioButtonRaise.Text = "Raise";
            this.radioButtonRaise.UseVisualStyleBackColor = true;
            this.radioButtonRaise.CheckedChanged += new System.EventHandler(this.RadioButtonHeightTextureTool_CheckedChanged);
            // 
            // groupBoxManualData
            // 
            this.groupBoxManualData.Controls.Add(this.labelPatchIDZ);
            this.groupBoxManualData.Controls.Add(this.numericUpDownPatchIDZ);
            this.groupBoxManualData.Controls.Add(this.labelPatchCoordsY);
            this.groupBoxManualData.Controls.Add(this.labelPatchVertexY);
            this.groupBoxManualData.Controls.Add(this.labelPatchIDY);
            this.groupBoxManualData.Controls.Add(this.labelPatchCoordsX);
            this.groupBoxManualData.Controls.Add(this.labelPatchVertexX);
            this.groupBoxManualData.Controls.Add(this.labelPatchIDX);
            this.groupBoxManualData.Controls.Add(this.buttonGlueEdges);
            this.groupBoxManualData.Controls.Add(this.comboBoxTerrainType);
            this.groupBoxManualData.Controls.Add(this.labelTerrainType);
            this.groupBoxManualData.Controls.Add(this.labelElevation);
            this.groupBoxManualData.Controls.Add(this.numericUpDownHeight);
            this.groupBoxManualData.Controls.Add(this.labelGlobalCoords);
            this.groupBoxManualData.Controls.Add(this.numericUpDownCoordinatesY);
            this.groupBoxManualData.Controls.Add(this.numericUpDownCoordinatesX);
            this.groupBoxManualData.Controls.Add(this.labelPatchVertex);
            this.groupBoxManualData.Controls.Add(this.numericUpDownPatchVertexY);
            this.groupBoxManualData.Controls.Add(this.numericUpDownPatchVertexX);
            this.groupBoxManualData.Controls.Add(this.numericUpDownPatchIDY);
            this.groupBoxManualData.Controls.Add(this.numericUpDownPatchIDX);
            this.groupBoxManualData.Controls.Add(this.labelPatchID);
            this.groupBoxManualData.Location = new System.Drawing.Point(225, 3);
            this.groupBoxManualData.Name = "groupBoxManualData";
            this.groupBoxManualData.Size = new System.Drawing.Size(216, 309);
            this.groupBoxManualData.TabIndex = 2;
            this.groupBoxManualData.TabStop = false;
            this.groupBoxManualData.Text = "Manual terrain editing";
            // 
            // labelPatchIDZ
            // 
            this.labelPatchIDZ.AutoSize = true;
            this.labelPatchIDZ.Location = new System.Drawing.Point(132, 73);
            this.labelPatchIDZ.Name = "labelPatchIDZ";
            this.labelPatchIDZ.Size = new System.Drawing.Size(17, 13);
            this.labelPatchIDZ.TabIndex = 5;
            this.labelPatchIDZ.Text = "Z:";
            // 
            // numericUpDownPatchIDZ
            // 
            this.numericUpDownPatchIDZ.Location = new System.Drawing.Point(155, 71);
            this.numericUpDownPatchIDZ.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownPatchIDZ.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numericUpDownPatchIDZ.Name = "numericUpDownPatchIDZ";
            this.numericUpDownPatchIDZ.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownPatchIDZ.TabIndex = 6;
            // 
            // labelPatchCoordsY
            // 
            this.labelPatchCoordsY.AutoSize = true;
            this.labelPatchCoordsY.Location = new System.Drawing.Point(132, 177);
            this.labelPatchCoordsY.Name = "labelPatchCoordsY";
            this.labelPatchCoordsY.Size = new System.Drawing.Size(17, 13);
            this.labelPatchCoordsY.TabIndex = 15;
            this.labelPatchCoordsY.Text = "Y:";
            // 
            // labelPatchVertexY
            // 
            this.labelPatchVertexY.AutoSize = true;
            this.labelPatchVertexY.Location = new System.Drawing.Point(132, 125);
            this.labelPatchVertexY.Name = "labelPatchVertexY";
            this.labelPatchVertexY.Size = new System.Drawing.Size(17, 13);
            this.labelPatchVertexY.TabIndex = 10;
            this.labelPatchVertexY.Text = "Y:";
            // 
            // labelPatchIDY
            // 
            this.labelPatchIDY.AutoSize = true;
            this.labelPatchIDY.Location = new System.Drawing.Point(132, 47);
            this.labelPatchIDY.Name = "labelPatchIDY";
            this.labelPatchIDY.Size = new System.Drawing.Size(17, 13);
            this.labelPatchIDY.TabIndex = 3;
            this.labelPatchIDY.Text = "Y:";
            // 
            // labelPatchCoordsX
            // 
            this.labelPatchCoordsX.AutoSize = true;
            this.labelPatchCoordsX.Location = new System.Drawing.Point(132, 151);
            this.labelPatchCoordsX.Name = "labelPatchCoordsX";
            this.labelPatchCoordsX.Size = new System.Drawing.Size(17, 13);
            this.labelPatchCoordsX.TabIndex = 13;
            this.labelPatchCoordsX.Text = "X:";
            // 
            // labelPatchVertexX
            // 
            this.labelPatchVertexX.AutoSize = true;
            this.labelPatchVertexX.Location = new System.Drawing.Point(132, 99);
            this.labelPatchVertexX.Name = "labelPatchVertexX";
            this.labelPatchVertexX.Size = new System.Drawing.Size(17, 13);
            this.labelPatchVertexX.TabIndex = 8;
            this.labelPatchVertexX.Text = "X:";
            // 
            // labelPatchIDX
            // 
            this.labelPatchIDX.AutoSize = true;
            this.labelPatchIDX.Location = new System.Drawing.Point(132, 21);
            this.labelPatchIDX.Name = "labelPatchIDX";
            this.labelPatchIDX.Size = new System.Drawing.Size(17, 13);
            this.labelPatchIDX.TabIndex = 1;
            this.labelPatchIDX.Text = "X:";
            // 
            // buttonGlueEdges
            // 
            this.buttonGlueEdges.Location = new System.Drawing.Point(41, 280);
            this.buttonGlueEdges.Name = "buttonGlueEdges";
            this.buttonGlueEdges.Size = new System.Drawing.Size(135, 23);
            this.buttonGlueEdges.TabIndex = 21;
            this.buttonGlueEdges.Text = "Glue edges";
            this.buttonGlueEdges.UseVisualStyleBackColor = true;
            this.buttonGlueEdges.Click += new System.EventHandler(this.ButtonGlueEdges_Click);
            // 
            // comboBoxTerrainType
            // 
            this.comboBoxTerrainType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTerrainType.FormattingEnabled = true;
            this.comboBoxTerrainType.Location = new System.Drawing.Point(110, 253);
            this.comboBoxTerrainType.Name = "comboBoxTerrainType";
            this.comboBoxTerrainType.Size = new System.Drawing.Size(100, 21);
            this.comboBoxTerrainType.TabIndex = 20;
            // 
            // labelTerrainType
            // 
            this.labelTerrainType.AutoSize = true;
            this.labelTerrainType.Location = new System.Drawing.Point(6, 256);
            this.labelTerrainType.Name = "labelTerrainType";
            this.labelTerrainType.Size = new System.Drawing.Size(66, 13);
            this.labelTerrainType.TabIndex = 19;
            this.labelTerrainType.Text = "Terrain type:";
            // 
            // labelElevation
            // 
            this.labelElevation.AutoSize = true;
            this.labelElevation.Location = new System.Drawing.Point(6, 229);
            this.labelElevation.Name = "labelElevation";
            this.labelElevation.Size = new System.Drawing.Size(54, 13);
            this.labelElevation.TabIndex = 17;
            this.labelElevation.Text = "Elevation:";
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.DecimalPlaces = 3;
            this.numericUpDownHeight.Location = new System.Drawing.Point(150, 227);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownHeight.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownHeight.TabIndex = 18;
            // 
            // labelGlobalCoords
            // 
            this.labelGlobalCoords.AutoSize = true;
            this.labelGlobalCoords.Location = new System.Drawing.Point(6, 165);
            this.labelGlobalCoords.Name = "labelGlobalCoords";
            this.labelGlobalCoords.Size = new System.Drawing.Size(66, 13);
            this.labelGlobalCoords.TabIndex = 12;
            this.labelGlobalCoords.Text = "Coordinates:";
            // 
            // numericUpDownCoordinatesY
            // 
            this.numericUpDownCoordinatesY.Location = new System.Drawing.Point(155, 175);
            this.numericUpDownCoordinatesY.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownCoordinatesY.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numericUpDownCoordinatesY.Name = "numericUpDownCoordinatesY";
            this.numericUpDownCoordinatesY.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownCoordinatesY.TabIndex = 16;
            // 
            // numericUpDownCoordinatesX
            // 
            this.numericUpDownCoordinatesX.Location = new System.Drawing.Point(155, 149);
            this.numericUpDownCoordinatesX.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownCoordinatesX.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numericUpDownCoordinatesX.Name = "numericUpDownCoordinatesX";
            this.numericUpDownCoordinatesX.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownCoordinatesX.TabIndex = 14;
            // 
            // labelPatchVertex
            // 
            this.labelPatchVertex.AutoSize = true;
            this.labelPatchVertex.Location = new System.Drawing.Point(6, 113);
            this.labelPatchVertex.Name = "labelPatchVertex";
            this.labelPatchVertex.Size = new System.Drawing.Size(70, 13);
            this.labelPatchVertex.TabIndex = 7;
            this.labelPatchVertex.Text = "Patch vertex:";
            // 
            // numericUpDownPatchVertexY
            // 
            this.numericUpDownPatchVertexY.Location = new System.Drawing.Point(155, 123);
            this.numericUpDownPatchVertexY.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.numericUpDownPatchVertexY.Name = "numericUpDownPatchVertexY";
            this.numericUpDownPatchVertexY.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownPatchVertexY.TabIndex = 11;
            // 
            // numericUpDownPatchVertexX
            // 
            this.numericUpDownPatchVertexX.Location = new System.Drawing.Point(155, 97);
            this.numericUpDownPatchVertexX.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.numericUpDownPatchVertexX.Name = "numericUpDownPatchVertexX";
            this.numericUpDownPatchVertexX.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownPatchVertexX.TabIndex = 9;
            // 
            // numericUpDownPatchIDY
            // 
            this.numericUpDownPatchIDY.Location = new System.Drawing.Point(155, 45);
            this.numericUpDownPatchIDY.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownPatchIDY.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numericUpDownPatchIDY.Name = "numericUpDownPatchIDY";
            this.numericUpDownPatchIDY.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownPatchIDY.TabIndex = 4;
            // 
            // numericUpDownPatchIDX
            // 
            this.numericUpDownPatchIDX.Location = new System.Drawing.Point(155, 19);
            this.numericUpDownPatchIDX.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownPatchIDX.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numericUpDownPatchIDX.Name = "numericUpDownPatchIDX";
            this.numericUpDownPatchIDX.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownPatchIDX.TabIndex = 2;
            // 
            // labelPatchID
            // 
            this.labelPatchID.AutoSize = true;
            this.labelPatchID.Location = new System.Drawing.Point(6, 47);
            this.labelPatchID.Name = "labelPatchID";
            this.labelPatchID.Size = new System.Drawing.Size(52, 13);
            this.labelPatchID.TabIndex = 0;
            this.labelPatchID.Text = "Patch ID:";
            // 
            // groupBoxPatchFluids
            // 
            this.groupBoxPatchFluids.Controls.Add(this.linkLabelEditFluid);
            this.groupBoxPatchFluids.Controls.Add(this.linkLabelRemoveFluid);
            this.groupBoxPatchFluids.Controls.Add(this.linkLabelAddFluid);
            this.groupBoxPatchFluids.Controls.Add(this.listBoxPatchFluids);
            this.groupBoxPatchFluids.Location = new System.Drawing.Point(447, 3);
            this.groupBoxPatchFluids.Name = "groupBoxPatchFluids";
            this.groupBoxPatchFluids.Size = new System.Drawing.Size(216, 151);
            this.groupBoxPatchFluids.TabIndex = 3;
            this.groupBoxPatchFluids.TabStop = false;
            this.groupBoxPatchFluids.Text = "Patch fluids";
            // 
            // linkLabelEditFluid
            // 
            this.linkLabelEditFluid.AutoSize = true;
            this.linkLabelEditFluid.Location = new System.Drawing.Point(96, 16);
            this.linkLabelEditFluid.Name = "linkLabelEditFluid";
            this.linkLabelEditFluid.Size = new System.Drawing.Size(25, 13);
            this.linkLabelEditFluid.TabIndex = 1;
            this.linkLabelEditFluid.TabStop = true;
            this.linkLabelEditFluid.Text = "Edit";
            this.linkLabelEditFluid.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelEditFluid_LinkClicked);
            // 
            // linkLabelRemoveFluid
            // 
            this.linkLabelRemoveFluid.AutoSize = true;
            this.linkLabelRemoveFluid.Location = new System.Drawing.Point(172, 16);
            this.linkLabelRemoveFluid.Name = "linkLabelRemoveFluid";
            this.linkLabelRemoveFluid.Size = new System.Drawing.Size(38, 13);
            this.linkLabelRemoveFluid.TabIndex = 2;
            this.linkLabelRemoveFluid.TabStop = true;
            this.linkLabelRemoveFluid.Text = "Delete";
            this.linkLabelRemoveFluid.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelRemoveFluid_LinkClicked);
            // 
            // linkLabelAddFluid
            // 
            this.linkLabelAddFluid.AutoSize = true;
            this.linkLabelAddFluid.Location = new System.Drawing.Point(6, 16);
            this.linkLabelAddFluid.Name = "linkLabelAddFluid";
            this.linkLabelAddFluid.Size = new System.Drawing.Size(26, 13);
            this.linkLabelAddFluid.TabIndex = 0;
            this.linkLabelAddFluid.TabStop = true;
            this.linkLabelAddFluid.Text = "Add";
            this.linkLabelAddFluid.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelAddFluid_LinkClicked);
            // 
            // listBoxPatchFluids
            // 
            this.listBoxPatchFluids.FormattingEnabled = true;
            this.listBoxPatchFluids.HorizontalScrollbar = true;
            this.listBoxPatchFluids.Location = new System.Drawing.Point(6, 32);
            this.listBoxPatchFluids.Name = "listBoxPatchFluids";
            this.listBoxPatchFluids.Size = new System.Drawing.Size(204, 108);
            this.listBoxPatchFluids.TabIndex = 3;
            // 
            // TerrainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelMain);
            this.Name = "TerrainControl";
            this.Size = new System.Drawing.Size(670, 320);
            this.Load += new System.EventHandler(this.TerrainControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCursorSize)).EndInit();
            this.flowLayoutPanelMain.ResumeLayout(false);
            this.groupBoxGlobal.ResumeLayout(false);
            this.groupBoxGlobal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCurrentHeightLevel)).EndInit();
            this.groupBoxElevationTerrainTypeTool.ResumeLayout(false);
            this.groupBoxElevationTerrainTypeTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntensity)).EndInit();
            this.groupBoxManualData.ResumeLayout(false);
            this.groupBoxManualData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatchIDZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCoordinatesY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCoordinatesX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatchVertexY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatchVertexX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatchIDY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatchIDX)).EndInit();
            this.groupBoxPatchFluids.ResumeLayout(false);
            this.groupBoxPatchFluids.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonSmooth;
        private System.Windows.Forms.RadioButton radioButtonSelect;
        private System.Windows.Forms.Label labelIntensity;
        private System.Windows.Forms.NumericUpDown numericUpDownIntensity;
        private System.Windows.Forms.ComboBox comboBoxApplyTerrainType;
        private System.Windows.Forms.GroupBox groupBoxPatchFluids;
        private System.Windows.Forms.LinkLabel linkLabelEditFluid;
        private System.Windows.Forms.LinkLabel linkLabelRemoveFluid;
        private System.Windows.Forms.LinkLabel linkLabelAddFluid;
        private System.Windows.Forms.ListBox listBoxPatchFluids;
        private System.Windows.Forms.GroupBox groupBoxManualData;
        private System.Windows.Forms.Label labelPatchIDZ;
        private System.Windows.Forms.NumericUpDown numericUpDownPatchIDZ;
        private System.Windows.Forms.Label labelPatchCoordsY;
        private System.Windows.Forms.Label labelPatchVertexY;
        private System.Windows.Forms.Label labelPatchIDY;
        private System.Windows.Forms.Label labelPatchCoordsX;
        private System.Windows.Forms.Label labelPatchVertexX;
        private System.Windows.Forms.Label labelPatchIDX;
        private System.Windows.Forms.Button buttonGlueEdges;
        private System.Windows.Forms.ComboBox comboBoxTerrainType;
        private System.Windows.Forms.Label labelTerrainType;
        private System.Windows.Forms.Label labelElevation;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label labelGlobalCoords;
        private System.Windows.Forms.NumericUpDown numericUpDownCoordinatesY;
        private System.Windows.Forms.NumericUpDown numericUpDownCoordinatesX;
        private System.Windows.Forms.Label labelPatchVertex;
        private System.Windows.Forms.NumericUpDown numericUpDownPatchVertexY;
        private System.Windows.Forms.NumericUpDown numericUpDownPatchVertexX;
        private System.Windows.Forms.NumericUpDown numericUpDownPatchIDY;
        private System.Windows.Forms.NumericUpDown numericUpDownPatchIDX;
        private System.Windows.Forms.Label labelPatchID;
        private System.Windows.Forms.CheckBox checkBoxTerrainType;
        private System.Windows.Forms.GroupBox groupBoxGlobal;
        private System.Windows.Forms.Label labelCurrentHeightLevel;
        private System.Windows.Forms.NumericUpDown numericUpDownCurrentHeightLevel;
        private System.Windows.Forms.Label labelCurrentSeason;
        private System.Windows.Forms.ComboBox comboBoxCurrentSeason;
        private System.Windows.Forms.CheckBox checkBoxHideBaseSeasonTerrain;
    }
}
