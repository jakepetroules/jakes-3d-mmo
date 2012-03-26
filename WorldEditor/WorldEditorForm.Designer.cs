namespace MMO3D.WorldEditor
{
    /// <summary>
    /// The Windows Forms initialization section of the MMO3D World Editor main GUI form.
    /// </summary>
    public partial class WorldEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// The application main menu.
        /// </summary>
        private System.Windows.Forms.MenuStrip menuStripMain;

        /// <summary>
        /// Application file menu.
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;

        /// <summary>
        /// Exit menu item.
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

        /// <summary>
        /// Application edit menu.
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;

        /// <summary>
        /// Application view menu.
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;

        /// <summary>
        /// Always on top menu item.
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;

        /// <summary>
        /// The program status bar.
        /// </summary>
        private System.Windows.Forms.StatusStrip statusStripMain;

        /// <summary>
        /// Editor log window.
        /// </summary>
        private System.Windows.Forms.RichTextBox richTextBoxLog;

        /// <summary>
        /// A menu item separator.
        /// </summary>
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

        /// <summary>
        /// Toolbars menu item.
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem toolbarsToolStripMenuItem;

        /// <summary>
        /// Menu item to access sub-menu items to select screen resolution.
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem fullscreenToolStripMenuItem;

        /// <summary>
        /// Temporary menu item. Will be deleted and replaced with a menu item for each of the monitor's supported resolutions.
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem willBePopulatedWithSupportedResolutionsToolStripMenuItem;

        /// <summary>
        /// Camera position tool strip label.
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCamera;

        /// <summary>
        /// Sidebar tab control.
        /// </summary>
        private System.Windows.Forms.TabControl tabControlControls;

        /// <summary>
        /// Current sidebar tool tab page.
        /// </summary>
        private System.Windows.Forms.TabPage tabPageTerrain;

        /// <summary>
        /// Object properties tab page.
        /// </summary>
        private System.Windows.Forms.TabPage tabPageProperties;

        /// <summary>
        /// Object properties tree view.
        /// </summary>
        private System.Windows.Forms.TreeView treeViewGameObjects;

        /// <summary>
        /// Flow layout panel in the object properties sidebar.
        /// </summary>
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProperties;

        /// <summary>
        /// Object properties property grid.
        /// </summary>
        private System.Windows.Forms.PropertyGrid propertyGridGameObjectProperties;

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
            WorldEditorForm.Instance = null;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldEditorForm));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullscreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.willBePopulatedWithSupportedResolutionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.messageLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terrainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewPatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addExistingPatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removePatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.buildPackedMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createWorldMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildPackedModelArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.tabControlControls = new System.Windows.Forms.TabControl();
            this.tabPage3dView = new System.Windows.Forms.TabPage();
            this.editorRenderWindow = new MMO3D.WorldEditor.EditorRenderWindow();
            this.tabPageTerrain = new System.Windows.Forms.TabPage();
            this.terrainControl = new MMO3D.WorldEditor.TerrainControl();
            this.tabPageObjectSpawns = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            //this.objectSpawnsEditor = new MMO3D.DatabaseManagement.ObjectSpawnsEditor();
            this.tabPageProperties = new System.Windows.Forms.TabPage();
            this.flowLayoutPanelProperties = new System.Windows.Forms.FlowLayoutPanel();
            this.treeViewGameObjects = new System.Windows.Forms.TreeView();
            this.propertyGridGameObjectProperties = new System.Windows.Forms.PropertyGrid();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCamera = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.toolStripRendering = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSolid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPoint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonWireframe = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelCameraDistance = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxCameraDistance = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabelCameraHeightDifference = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxCameraHeightDifference = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabelFPS = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTerrain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelHeightLevel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxHeightLevel = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabelSeason = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxSeason = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonHideBaseSeasonTerrain = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRaise = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLower = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFlatten = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSmooth = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelCursorSize = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxCursorSize = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabelIntensity = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxIntensity = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonApplyTerrainType = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxApplyTerrainType = new System.Windows.Forms.ToolStripComboBox();
            this.menuStripMain.SuspendLayout();
            this.tabControlControls.SuspendLayout();
            this.tabPage3dView.SuspendLayout();
            this.tabPageTerrain.SuspendLayout();
            this.tabPageObjectSpawns.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.tabPageProperties.SuspendLayout();
            this.flowLayoutPanelProperties.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.toolStripRendering.SuspendLayout();
            this.toolStripTerrain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.objectsToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(892, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "Main menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysOnTopToolStripMenuItem,
            this.fullscreenToolStripMenuItem,
            this.toolStripSeparator2,
            this.messageLogToolStripMenuItem,
            this.toolbarsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.Image = global::MMO3D.WorldEditor.Resources.AlwaysOnTop;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always on Top";
            this.alwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.AlwaysOnTopToolStripMenuItem_Click);
            // 
            // fullscreenToolStripMenuItem
            // 
            this.fullscreenToolStripMenuItem.CheckOnClick = true;
            this.fullscreenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.willBePopulatedWithSupportedResolutionsToolStripMenuItem});
            this.fullscreenToolStripMenuItem.Name = "fullscreenToolStripMenuItem";
            this.fullscreenToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.fullscreenToolStripMenuItem.Text = "Fullscreen";
            this.fullscreenToolStripMenuItem.DropDownOpening += new System.EventHandler(this.FullscreenToolStripMenuItem_DropDownOpening);
            // 
            // willBePopulatedWithSupportedResolutionsToolStripMenuItem
            // 
            this.willBePopulatedWithSupportedResolutionsToolStripMenuItem.Name = "willBePopulatedWithSupportedResolutionsToolStripMenuItem";
            this.willBePopulatedWithSupportedResolutionsToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.willBePopulatedWithSupportedResolutionsToolStripMenuItem.Text = "< Will be populated with supported resolutions >";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(179, 6);
            // 
            // messageLogToolStripMenuItem
            // 
            this.messageLogToolStripMenuItem.Checked = true;
            this.messageLogToolStripMenuItem.CheckOnClick = true;
            this.messageLogToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.messageLogToolStripMenuItem.Name = "messageLogToolStripMenuItem";
            this.messageLogToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.messageLogToolStripMenuItem.Text = "Message Log";
            this.messageLogToolStripMenuItem.Click += new System.EventHandler(this.MessageLogToolStripMenuItem_Click);
            // 
            // toolbarsToolStripMenuItem
            // 
            this.toolbarsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renderingToolStripMenuItem,
            this.terrainToolStripMenuItem});
            this.toolbarsToolStripMenuItem.Name = "toolbarsToolStripMenuItem";
            this.toolbarsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.toolbarsToolStripMenuItem.Text = "Toolbars";
            // 
            // renderingToolStripMenuItem
            // 
            this.renderingToolStripMenuItem.Checked = true;
            this.renderingToolStripMenuItem.CheckOnClick = true;
            this.renderingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.renderingToolStripMenuItem.Name = "renderingToolStripMenuItem";
            this.renderingToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.renderingToolStripMenuItem.Text = "Rendering";
            this.renderingToolStripMenuItem.Click += new System.EventHandler(this.ToolbarToolStripMenuItem_Click);
            // 
            // terrainToolStripMenuItem
            // 
            this.terrainToolStripMenuItem.Checked = true;
            this.terrainToolStripMenuItem.CheckOnClick = true;
            this.terrainToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.terrainToolStripMenuItem.Name = "terrainToolStripMenuItem";
            this.terrainToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.terrainToolStripMenuItem.Text = "Terrain";
            this.terrainToolStripMenuItem.Click += new System.EventHandler(this.ToolbarToolStripMenuItem_Click);
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewPatchToolStripMenuItem,
            this.addExistingPatchToolStripMenuItem,
            this.removePatchToolStripMenuItem,
            this.toolStripSeparator4,
            this.buildPackedMapToolStripMenuItem,
            this.createWorldMapToolStripMenuItem});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.mapToolStripMenuItem.Text = "Map";
            // 
            // addNewPatchToolStripMenuItem
            // 
            this.addNewPatchToolStripMenuItem.Image = global::MMO3D.WorldEditor.Resources.Add;
            this.addNewPatchToolStripMenuItem.Name = "addNewPatchToolStripMenuItem";
            this.addNewPatchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addNewPatchToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.addNewPatchToolStripMenuItem.Text = "Add New Patch...";
            this.addNewPatchToolStripMenuItem.Click += new System.EventHandler(this.AddNewPatchToolStripMenuItem_Click);
            // 
            // addExistingPatchToolStripMenuItem
            // 
            this.addExistingPatchToolStripMenuItem.Image = global::MMO3D.WorldEditor.Resources.AddExisting;
            this.addExistingPatchToolStripMenuItem.Name = "addExistingPatchToolStripMenuItem";
            this.addExistingPatchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.addExistingPatchToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.addExistingPatchToolStripMenuItem.Text = "Add Existing Patch...";
            this.addExistingPatchToolStripMenuItem.Click += new System.EventHandler(this.AddExistingPatchToolStripMenuItem_Click);
            // 
            // removePatchToolStripMenuItem
            // 
            this.removePatchToolStripMenuItem.Image = global::MMO3D.WorldEditor.Resources.Delete;
            this.removePatchToolStripMenuItem.Name = "removePatchToolStripMenuItem";
            this.removePatchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.removePatchToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.removePatchToolStripMenuItem.Text = "Remove Patch...";
            this.removePatchToolStripMenuItem.Click += new System.EventHandler(this.RemovePatchToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(240, 6);
            // 
            // buildPackedMapToolStripMenuItem
            // 
            this.buildPackedMapToolStripMenuItem.Name = "buildPackedMapToolStripMenuItem";
            this.buildPackedMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.M)));
            this.buildPackedMapToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.buildPackedMapToolStripMenuItem.Text = "Build packed map...";
            this.buildPackedMapToolStripMenuItem.Click += new System.EventHandler(this.BuildPackedMapToolStripMenuItem_Click);
            // 
            // createWorldMapToolStripMenuItem
            // 
            this.createWorldMapToolStripMenuItem.Name = "createWorldMapToolStripMenuItem";
            this.createWorldMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.W)));
            this.createWorldMapToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.createWorldMapToolStripMenuItem.Text = "Create world map...";
            this.createWorldMapToolStripMenuItem.Click += new System.EventHandler(this.CreateWorldMapToolStripMenuItem_Click);
            // 
            // objectsToolStripMenuItem
            // 
            this.objectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildModelToolStripMenuItem,
            this.buildPackedModelArchiveToolStripMenuItem});
            this.objectsToolStripMenuItem.Name = "objectsToolStripMenuItem";
            this.objectsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.objectsToolStripMenuItem.Text = "Objects";
            // 
            // buildModelToolStripMenuItem
            // 
            this.buildModelToolStripMenuItem.Name = "buildModelToolStripMenuItem";
            this.buildModelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.B)));
            this.buildModelToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.buildModelToolStripMenuItem.Text = "Build model...";
            this.buildModelToolStripMenuItem.Click += new System.EventHandler(this.BuildModelToolStripMenuItem_Click);
            // 
            // buildPackedModelArchiveToolStripMenuItem
            // 
            this.buildPackedModelArchiveToolStripMenuItem.Name = "buildPackedModelArchiveToolStripMenuItem";
            this.buildPackedModelArchiveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.O)));
            this.buildPackedModelArchiveToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.buildPackedModelArchiveToolStripMenuItem.Text = "Build packed model archive...";
            this.buildPackedModelArchiveToolStripMenuItem.Click += new System.EventHandler(this.BuildPackedModelArchiveToolStripMenuItem_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLog.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(892, 114);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            // 
            // tabControlControls
            // 
            this.tabControlControls.Controls.Add(this.tabPage3dView);
            this.tabControlControls.Controls.Add(this.tabPageTerrain);
            this.tabControlControls.Controls.Add(this.tabPageObjectSpawns);
            this.tabControlControls.Controls.Add(this.tabPageProperties);
            this.tabControlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlControls.HotTrack = true;
            this.tabControlControls.Location = new System.Drawing.Point(0, 0);
            this.tabControlControls.Multiline = true;
            this.tabControlControls.Name = "tabControlControls";
            this.tabControlControls.SelectedIndex = 0;
            this.tabControlControls.ShowToolTips = true;
            this.tabControlControls.Size = new System.Drawing.Size(892, 352);
            this.tabControlControls.TabIndex = 0;
            // 
            // tabPage3dView
            // 
            this.tabPage3dView.Controls.Add(this.editorRenderWindow);
            this.tabPage3dView.Location = new System.Drawing.Point(4, 22);
            this.tabPage3dView.Name = "tabPage3dView";
            this.tabPage3dView.Size = new System.Drawing.Size(884, 326);
            this.tabPage3dView.TabIndex = 2;
            this.tabPage3dView.Text = "3D View";
            this.tabPage3dView.ToolTipText = "Displays a 3D view of the world";
            this.tabPage3dView.UseVisualStyleBackColor = true;
            // 
            // editorRenderWindow
            // 
            this.editorRenderWindow.BackColor = System.Drawing.Color.CornflowerBlue;
            this.editorRenderWindow.CameraMovementSpeed = 0F;
            this.editorRenderWindow.CameraRotationSpeed = 0F;
            this.editorRenderWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorRenderWindow.Location = new System.Drawing.Point(0, 0);
            this.editorRenderWindow.Name = "editorRenderWindow";
            this.editorRenderWindow.Size = new System.Drawing.Size(884, 326);
            this.editorRenderWindow.TabIndex = 0;
            this.editorRenderWindow.Text = "MMO3D World Editor Render Window";
            this.editorRenderWindow.MouseLeave += new System.EventHandler(this.EditorRenderWindow_MouseLeave);
            this.editorRenderWindow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EditorRenderWindow_MouseMove);
            this.editorRenderWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EditorRenderWindow_MouseDown);
            this.editorRenderWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EditorRenderWindow_MouseUp);
            this.editorRenderWindow.MouseEnter += new System.EventHandler(this.EditorRenderWindow_MouseEnter);
            // 
            // tabPageTerrain
            // 
            this.tabPageTerrain.Controls.Add(this.terrainControl);
            this.tabPageTerrain.Location = new System.Drawing.Point(4, 22);
            this.tabPageTerrain.Name = "tabPageTerrain";
            this.tabPageTerrain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTerrain.Size = new System.Drawing.Size(884, 326);
            this.tabPageTerrain.TabIndex = 0;
            this.tabPageTerrain.Text = "Terrain";
            this.tabPageTerrain.ToolTipText = "Allows editing of the terrain";
            this.tabPageTerrain.UseVisualStyleBackColor = true;
            // 
            // terrainControl
            // 
            this.terrainControl.ApplyTerrainType = MMO3D.Engine.TerrainType.UndefinedTerrain;
            this.terrainControl.CurrentHeightLevel = 0;
            this.terrainControl.CurrentSeason = MMO3D.Engine.Season.Midseason;
            this.terrainControl.CursorSize = 1;
            this.terrainControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.terrainControl.GlobalCoordinate = new Microsoft.Xna.Framework.Point(0, 0);
            this.terrainControl.HideBaseSeasonTerrain = false;
            this.terrainControl.Intensity = 0.1F;
            this.terrainControl.Location = new System.Drawing.Point(3, 3);
            this.terrainControl.Name = "terrainControl";
            this.terrainControl.Size = new System.Drawing.Size(878, 320);
            this.terrainControl.TabIndex = 0;
            this.terrainControl.TerrainHeight = 0F;
            this.terrainControl.TerrainType = MMO3D.Engine.TerrainType.UndefinedTerrain;
            this.terrainControl.PropertyChanged += new System.EventHandler(this.TerrainControl_PropertyChanged);
            // 
            // tabPageObjectSpawns
            // 
            this.tabPageObjectSpawns.Controls.Add(this.flowLayoutPanel);
            this.tabPageObjectSpawns.Location = new System.Drawing.Point(4, 22);
            this.tabPageObjectSpawns.Name = "tabPageObjectSpawns";
            this.tabPageObjectSpawns.Size = new System.Drawing.Size(884, 326);
            this.tabPageObjectSpawns.TabIndex = 3;
            this.tabPageObjectSpawns.Text = "Object Spawns";
            this.tabPageObjectSpawns.ToolTipText = "Allows editing of object spawns";
            this.tabPageObjectSpawns.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            //this.flowLayoutPanel.Controls.Add(this.objectSpawnsEditor);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(884, 326);
            this.flowLayoutPanel.TabIndex = 1;
            // 
            // objectSpawnsEditor
            // 
            /*this.objectSpawnsEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objectSpawnsEditor.DatabaseManager = null;
            this.objectSpawnsEditor.Location = new System.Drawing.Point(3, 3);
            this.objectSpawnsEditor.Name = "objectSpawnsEditor";
            this.objectSpawnsEditor.Size = new System.Drawing.Size(900, 450);
            this.objectSpawnsEditor.TabIndex = 0;*/
            // 
            // tabPageProperties
            // 
            this.tabPageProperties.Controls.Add(this.flowLayoutPanelProperties);
            this.tabPageProperties.Location = new System.Drawing.Point(4, 22);
            this.tabPageProperties.Name = "tabPageProperties";
            this.tabPageProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProperties.Size = new System.Drawing.Size(884, 326);
            this.tabPageProperties.TabIndex = 1;
            this.tabPageProperties.Text = "Properties";
            this.tabPageProperties.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelProperties
            // 
            this.flowLayoutPanelProperties.Controls.Add(this.treeViewGameObjects);
            this.flowLayoutPanelProperties.Controls.Add(this.propertyGridGameObjectProperties);
            this.flowLayoutPanelProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelProperties.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelProperties.Name = "flowLayoutPanelProperties";
            this.flowLayoutPanelProperties.Size = new System.Drawing.Size(878, 320);
            this.flowLayoutPanelProperties.TabIndex = 4;
            // 
            // treeViewGameObjects
            // 
            this.treeViewGameObjects.Location = new System.Drawing.Point(3, 3);
            this.treeViewGameObjects.Name = "treeViewGameObjects";
            this.treeViewGameObjects.Size = new System.Drawing.Size(224, 341);
            this.treeViewGameObjects.TabIndex = 3;
            this.treeViewGameObjects.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewGameObjects_NodeMouseDoubleClick);
            // 
            // propertyGridGameObjectProperties
            // 
            this.propertyGridGameObjectProperties.Location = new System.Drawing.Point(233, 3);
            this.propertyGridGameObjectProperties.Name = "propertyGridGameObjectProperties";
            this.propertyGridGameObjectProperties.Size = new System.Drawing.Size(224, 300);
            this.propertyGridGameObjectProperties.TabIndex = 2;
            // 
            // statusStripMain
            // 
            this.statusStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCamera});
            this.statusStripMain.Location = new System.Drawing.Point(0, 0);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(892, 22);
            this.statusStripMain.TabIndex = 0;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCamera
            // 
            this.toolStripStatusLabelCamera.Name = "toolStripStatusLabelCamera";
            this.toolStripStatusLabelCamera.Size = new System.Drawing.Size(155, 17);
            this.toolStripStatusLabelCamera.Text = "Camera: Position = , Target = ";
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStripMain);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainerMain);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(892, 470);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(892, 566);
            this.toolStripContainer.TabIndex = 2;
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStripMain);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStripRendering);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStripTerrain);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.tabControlControls);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.richTextBoxLog);
            this.splitContainerMain.Size = new System.Drawing.Size(892, 470);
            this.splitContainerMain.SplitterDistance = 352;
            this.splitContainerMain.TabIndex = 2;
            // 
            // toolStripRendering
            // 
            this.toolStripRendering.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripRendering.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSolid,
            this.toolStripButtonPoint,
            this.toolStripButtonWireframe,
            this.toolStripSeparator5,
            this.toolStripLabelCameraDistance,
            this.toolStripTextBoxCameraDistance,
            this.toolStripLabelCameraHeightDifference,
            this.toolStripTextBoxCameraHeightDifference,
            this.toolStripLabelFPS});
            this.toolStripRendering.Location = new System.Drawing.Point(0, 24);
            this.toolStripRendering.Name = "toolStripRendering";
            this.toolStripRendering.Size = new System.Drawing.Size(892, 25);
            this.toolStripRendering.Stretch = true;
            this.toolStripRendering.TabIndex = 1;
            this.toolStripRendering.Text = "Rendering";
            // 
            // toolStripButtonSolid
            // 
            this.toolStripButtonSolid.Image = global::MMO3D.WorldEditor.Resources.Solid;
            this.toolStripButtonSolid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSolid.Name = "toolStripButtonSolid";
            this.toolStripButtonSolid.Size = new System.Drawing.Size(98, 22);
            this.toolStripButtonSolid.Text = "Solid rendering";
            this.toolStripButtonSolid.Click += new System.EventHandler(this.ToolStripButtonRenderingType_Click);
            // 
            // toolStripButtonPoint
            // 
            this.toolStripButtonPoint.Image = global::MMO3D.WorldEditor.Resources.Point;
            this.toolStripButtonPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPoint.Name = "toolStripButtonPoint";
            this.toolStripButtonPoint.Size = new System.Drawing.Size(100, 22);
            this.toolStripButtonPoint.Text = "Point rendering";
            this.toolStripButtonPoint.Click += new System.EventHandler(this.ToolStripButtonRenderingType_Click);
            // 
            // toolStripButtonWireframe
            // 
            this.toolStripButtonWireframe.Image = global::MMO3D.WorldEditor.Resources.Wireframe;
            this.toolStripButtonWireframe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonWireframe.Name = "toolStripButtonWireframe";
            this.toolStripButtonWireframe.Size = new System.Drawing.Size(126, 22);
            this.toolStripButtonWireframe.Text = "Wireframe rendering";
            this.toolStripButtonWireframe.Click += new System.EventHandler(this.ToolStripButtonRenderingType_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabelCameraDistance
            // 
            this.toolStripLabelCameraDistance.Name = "toolStripLabelCameraDistance";
            this.toolStripLabelCameraDistance.Size = new System.Drawing.Size(91, 22);
            this.toolStripLabelCameraDistance.Text = "Camera distance:";
            // 
            // toolStripTextBoxCameraDistance
            // 
            this.toolStripTextBoxCameraDistance.Name = "toolStripTextBoxCameraDistance";
            this.toolStripTextBoxCameraDistance.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBoxCameraDistance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ToolStripTextBoxCameraDistance_KeyDown);
            this.toolStripTextBoxCameraDistance.Validating += new System.ComponentModel.CancelEventHandler(this.ToolStripTextBoxCameraDistance_Validating);
            this.toolStripTextBoxCameraDistance.Validated += new System.EventHandler(this.ToolStripTextBoxCameraDistance_Validated);
            // 
            // toolStripLabelCameraHeightDifference
            // 
            this.toolStripLabelCameraHeightDifference.Name = "toolStripLabelCameraHeightDifference";
            this.toolStripLabelCameraHeightDifference.Size = new System.Drawing.Size(133, 22);
            this.toolStripLabelCameraHeightDifference.Text = "Camera height difference:";
            // 
            // toolStripTextBoxCameraHeightDifference
            // 
            this.toolStripTextBoxCameraHeightDifference.Name = "toolStripTextBoxCameraHeightDifference";
            this.toolStripTextBoxCameraHeightDifference.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBoxCameraHeightDifference.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ToolStripTextBoxCameraDistance_KeyDown);
            this.toolStripTextBoxCameraHeightDifference.Validating += new System.ComponentModel.CancelEventHandler(this.ToolStripTextBoxCameraDistance_Validating);
            this.toolStripTextBoxCameraHeightDifference.Validated += new System.EventHandler(this.ToolStripTextBoxCameraDistance_Validated);
            // 
            // toolStripLabelFPS
            // 
            this.toolStripLabelFPS.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelFPS.Name = "toolStripLabelFPS";
            this.toolStripLabelFPS.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabelFPS.Text = "{0} FPS";
            // 
            // toolStripTerrain
            // 
            this.toolStripTerrain.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripTerrain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelHeightLevel,
            this.toolStripTextBoxHeightLevel,
            this.toolStripLabelSeason,
            this.toolStripComboBoxSeason,
            this.toolStripButtonHideBaseSeasonTerrain,
            this.toolStripSeparator1,
            this.toolStripButtonSelect,
            this.toolStripButtonRaise,
            this.toolStripButtonLower,
            this.toolStripButtonFlatten,
            this.toolStripButtonSmooth,
            this.toolStripLabelCursorSize,
            this.toolStripTextBoxCursorSize,
            this.toolStripLabelIntensity,
            this.toolStripTextBoxIntensity,
            this.toolStripButtonApplyTerrainType,
            this.toolStripComboBoxApplyTerrainType});
            this.toolStripTerrain.Location = new System.Drawing.Point(0, 49);
            this.toolStripTerrain.Name = "toolStripTerrain";
            this.toolStripTerrain.Size = new System.Drawing.Size(892, 25);
            this.toolStripTerrain.Stretch = true;
            this.toolStripTerrain.TabIndex = 2;
            this.toolStripTerrain.Text = "Terrain";
            // 
            // toolStripLabelHeightLevel
            // 
            this.toolStripLabelHeightLevel.Name = "toolStripLabelHeightLevel";
            this.toolStripLabelHeightLevel.Size = new System.Drawing.Size(67, 22);
            this.toolStripLabelHeightLevel.Text = "Height level:";
            // 
            // toolStripTextBoxHeightLevel
            // 
            this.toolStripTextBoxHeightLevel.Name = "toolStripTextBoxHeightLevel";
            this.toolStripTextBoxHeightLevel.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBoxHeightLevel.TextChanged += new System.EventHandler(this.ToolStripTextBoxHeightLevel_TextChanged);
            // 
            // toolStripLabelSeason
            // 
            this.toolStripLabelSeason.Name = "toolStripLabelSeason";
            this.toolStripLabelSeason.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabelSeason.Text = "Season:";
            // 
            // toolStripComboBoxSeason
            // 
            this.toolStripComboBoxSeason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSeason.Name = "toolStripComboBoxSeason";
            this.toolStripComboBoxSeason.Size = new System.Drawing.Size(81, 25);
            this.toolStripComboBoxSeason.SelectedIndexChanged += new System.EventHandler(this.ToolStripComboBoxSeason_SelectedIndexChanged);
            // 
            // toolStripButtonHideBaseSeasonTerrain
            // 
            this.toolStripButtonHideBaseSeasonTerrain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHideBaseSeasonTerrain.Image = global::MMO3D.WorldEditor.Resources.HideBase;
            this.toolStripButtonHideBaseSeasonTerrain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHideBaseSeasonTerrain.Name = "toolStripButtonHideBaseSeasonTerrain";
            this.toolStripButtonHideBaseSeasonTerrain.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonHideBaseSeasonTerrain.Text = "Hide base season terrain";
            this.toolStripButtonHideBaseSeasonTerrain.Click += new System.EventHandler(this.ToolStripButtonHideBaseSeasonTerrain_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonSelect
            // 
            this.toolStripButtonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelect.Image = global::MMO3D.WorldEditor.Resources.Select;
            this.toolStripButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelect.Name = "toolStripButtonSelect";
            this.toolStripButtonSelect.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSelect.Text = "Select terrain";
            this.toolStripButtonSelect.Click += new System.EventHandler(this.ToolStripButtonSelect_Click);
            // 
            // toolStripButtonRaise
            // 
            this.toolStripButtonRaise.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRaise.Image = global::MMO3D.WorldEditor.Resources.Raise;
            this.toolStripButtonRaise.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRaise.Name = "toolStripButtonRaise";
            this.toolStripButtonRaise.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRaise.Text = "Raise terrain";
            this.toolStripButtonRaise.Click += new System.EventHandler(this.ToolStripButtonRaise_Click);
            // 
            // toolStripButtonLower
            // 
            this.toolStripButtonLower.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLower.Image = global::MMO3D.WorldEditor.Resources.Lower;
            this.toolStripButtonLower.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLower.Name = "toolStripButtonLower";
            this.toolStripButtonLower.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLower.Text = "Lower terrain";
            this.toolStripButtonLower.Click += new System.EventHandler(this.ToolStripButtonLower_Click);
            // 
            // toolStripButtonFlatten
            // 
            this.toolStripButtonFlatten.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFlatten.Image = global::MMO3D.WorldEditor.Resources.Flatten;
            this.toolStripButtonFlatten.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFlatten.Name = "toolStripButtonFlatten";
            this.toolStripButtonFlatten.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonFlatten.Text = "Flatten terrain";
            this.toolStripButtonFlatten.Click += new System.EventHandler(this.ToolStripButtonFlatten_Click);
            // 
            // toolStripButtonSmooth
            // 
            this.toolStripButtonSmooth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSmooth.Image = global::MMO3D.WorldEditor.Resources.Smooth;
            this.toolStripButtonSmooth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSmooth.Name = "toolStripButtonSmooth";
            this.toolStripButtonSmooth.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSmooth.Text = "Smooth terrain";
            this.toolStripButtonSmooth.Click += new System.EventHandler(this.ToolStripButtonSmooth_Click);
            // 
            // toolStripLabelCursorSize
            // 
            this.toolStripLabelCursorSize.Name = "toolStripLabelCursorSize";
            this.toolStripLabelCursorSize.Size = new System.Drawing.Size(64, 22);
            this.toolStripLabelCursorSize.Text = "Cursor size:";
            // 
            // toolStripTextBoxCursorSize
            // 
            this.toolStripTextBoxCursorSize.Name = "toolStripTextBoxCursorSize";
            this.toolStripTextBoxCursorSize.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBoxCursorSize.TextChanged += new System.EventHandler(this.ToolStripTextBoxCursorSize_TextChanged);
            // 
            // toolStripLabelIntensity
            // 
            this.toolStripLabelIntensity.Name = "toolStripLabelIntensity";
            this.toolStripLabelIntensity.Size = new System.Drawing.Size(54, 22);
            this.toolStripLabelIntensity.Text = "Intensity:";
            // 
            // toolStripTextBoxIntensity
            // 
            this.toolStripTextBoxIntensity.Name = "toolStripTextBoxIntensity";
            this.toolStripTextBoxIntensity.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBoxIntensity.TextChanged += new System.EventHandler(this.ToolStripTextBoxIntensity_TextChanged);
            // 
            // toolStripButtonApplyTerrainType
            // 
            this.toolStripButtonApplyTerrainType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonApplyTerrainType.Image = global::MMO3D.WorldEditor.Resources.TerrainType;
            this.toolStripButtonApplyTerrainType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonApplyTerrainType.Name = "toolStripButtonApplyTerrainType";
            this.toolStripButtonApplyTerrainType.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonApplyTerrainType.Text = "Change terrain type";
            this.toolStripButtonApplyTerrainType.Click += new System.EventHandler(this.ToolStripButtonApplyTerrainType_Click);
            // 
            // toolStripComboBoxApplyTerrainType
            // 
            this.toolStripComboBoxApplyTerrainType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxApplyTerrainType.Name = "toolStripComboBoxApplyTerrainType";
            this.toolStripComboBoxApplyTerrainType.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBoxApplyTerrainType.SelectedIndexChanged += new System.EventHandler(this.ToolStripComboBoxApplyTerrainType_SelectedIndexChanged);
            // 
            // WorldEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 566);
            this.Controls.Add(this.toolStripContainer);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "WorldEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MMO3D World Editor";
            this.Load += new System.EventHandler(this.WorldEditorForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorldEditorForm_FormClosing);
            this.Resize += new System.EventHandler(this.WorldEditorForm_Resize);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.tabControlControls.ResumeLayout(false);
            this.tabPage3dView.ResumeLayout(false);
            this.tabPageTerrain.ResumeLayout(false);
            this.tabPageObjectSpawns.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            this.tabPageProperties.ResumeLayout(false);
            this.flowLayoutPanelProperties.ResumeLayout(false);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.toolStripRendering.ResumeLayout(false);
            this.toolStripRendering.PerformLayout();
            this.toolStripTerrain.ResumeLayout(false);
            this.toolStripTerrain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removePatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem buildPackedMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createWorldMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStrip toolStripRendering;
        private System.Windows.Forms.ToolStripButton toolStripButtonSolid;
        private System.Windows.Forms.ToolStripButton toolStripButtonPoint;
        private System.Windows.Forms.ToolStripButton toolStripButtonWireframe;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabelCameraDistance;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxCameraDistance;
        private System.Windows.Forms.ToolStripLabel toolStripLabelCameraHeightDifference;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxCameraHeightDifference;
        private System.Windows.Forms.ToolStripMenuItem objectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildPackedModelArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabelFPS;
        private System.Windows.Forms.ToolStripMenuItem buildModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addExistingPatchToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3dView;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private EditorRenderWindow editorRenderWindow;
        private System.Windows.Forms.ToolStripMenuItem messageLogToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripTerrain;
        private System.Windows.Forms.TabPage tabPageObjectSpawns;
        private TerrainControl terrainControl;
        private System.Windows.Forms.ToolStripMenuItem renderingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terrainToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSeason;
        private System.Windows.Forms.ToolStripLabel toolStripLabelHeightLevel;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxHeightLevel;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSeason;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelect;
        private System.Windows.Forms.ToolStripButton toolStripButtonRaise;
        private System.Windows.Forms.ToolStripButton toolStripButtonLower;
        private System.Windows.Forms.ToolStripButton toolStripButtonFlatten;
        private System.Windows.Forms.ToolStripButton toolStripButtonSmooth;
        private System.Windows.Forms.ToolStripLabel toolStripLabelCursorSize;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxCursorSize;
        private System.Windows.Forms.ToolStripButton toolStripButtonApplyTerrainType;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxApplyTerrainType;
        private System.Windows.Forms.ToolStripLabel toolStripLabelIntensity;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxIntensity;
        private System.Windows.Forms.ToolStripButton toolStripButtonHideBaseSeasonTerrain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        //private MMO3D.DatabaseManagement.ObjectSpawnsEditor objectSpawnsEditor;
    }
}