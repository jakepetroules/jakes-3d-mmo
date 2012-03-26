namespace MMO3D.WorldEditor
{
    partial class WorldMapGeneratorDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxMapFile = new System.Windows.Forms.TextBox();
            this.buttonBrowseMapFile = new System.Windows.Forms.Button();
            this.labelMapFile = new System.Windows.Forms.Label();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.labelSeason = new System.Windows.Forms.Label();
            this.checkBoxHideBaseSeasonTerrain = new System.Windows.Forms.CheckBox();
            this.comboBoxSeason = new System.Windows.Forms.ComboBox();
            this.checkBoxTextured = new System.Windows.Forms.CheckBox();
            this.progressBarMapGeneration = new System.Windows.Forms.ProgressBar();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.groupBoxPreview = new System.Windows.Forms.GroupBox();
            this.labelImageDetails = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.backgroundWorkerMapGenerator = new System.ComponentModel.BackgroundWorker();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxMapFile
            // 
            this.textBoxMapFile.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMapFile.Location = new System.Drawing.Point(65, 15);
            this.textBoxMapFile.Name = "textBoxMapFile";
            this.textBoxMapFile.ReadOnly = true;
            this.textBoxMapFile.Size = new System.Drawing.Size(200, 20);
            this.textBoxMapFile.TabIndex = 1;
            // 
            // buttonBrowseMapFile
            // 
            this.buttonBrowseMapFile.Location = new System.Drawing.Point(271, 12);
            this.buttonBrowseMapFile.Name = "buttonBrowseMapFile";
            this.buttonBrowseMapFile.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseMapFile.TabIndex = 2;
            this.buttonBrowseMapFile.Text = "Browse...";
            this.buttonBrowseMapFile.UseVisualStyleBackColor = true;
            this.buttonBrowseMapFile.Click += new System.EventHandler(this.ButtonBrowseMapFile_Click);
            // 
            // labelMapFile
            // 
            this.labelMapFile.AutoSize = true;
            this.labelMapFile.Location = new System.Drawing.Point(12, 17);
            this.labelMapFile.Name = "labelMapFile";
            this.labelMapFile.Size = new System.Drawing.Size(47, 13);
            this.labelMapFile.TabIndex = 0;
            this.labelMapFile.Text = "Map file:";
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.labelSeason);
            this.groupBoxOptions.Controls.Add(this.checkBoxHideBaseSeasonTerrain);
            this.groupBoxOptions.Controls.Add(this.comboBoxSeason);
            this.groupBoxOptions.Controls.Add(this.checkBoxTextured);
            this.groupBoxOptions.Location = new System.Drawing.Point(12, 41);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(334, 92);
            this.groupBoxOptions.TabIndex = 3;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // labelSeason
            // 
            this.labelSeason.AutoSize = true;
            this.labelSeason.Location = new System.Drawing.Point(6, 45);
            this.labelSeason.Name = "labelSeason";
            this.labelSeason.Size = new System.Drawing.Size(46, 13);
            this.labelSeason.TabIndex = 1;
            this.labelSeason.Text = "Season:";
            // 
            // checkBoxHideBaseSeasonTerrain
            // 
            this.checkBoxHideBaseSeasonTerrain.AutoSize = true;
            this.checkBoxHideBaseSeasonTerrain.Location = new System.Drawing.Point(9, 69);
            this.checkBoxHideBaseSeasonTerrain.Name = "checkBoxHideBaseSeasonTerrain";
            this.checkBoxHideBaseSeasonTerrain.Size = new System.Drawing.Size(143, 17);
            this.checkBoxHideBaseSeasonTerrain.TabIndex = 3;
            this.checkBoxHideBaseSeasonTerrain.Text = "Hide base season terrain";
            this.checkBoxHideBaseSeasonTerrain.UseVisualStyleBackColor = true;
            // 
            // comboBoxSeason
            // 
            this.comboBoxSeason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSeason.FormattingEnabled = true;
            this.comboBoxSeason.Location = new System.Drawing.Point(58, 42);
            this.comboBoxSeason.Name = "comboBoxSeason";
            this.comboBoxSeason.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSeason.TabIndex = 2;
            // 
            // checkBoxTextured
            // 
            this.checkBoxTextured.AutoSize = true;
            this.checkBoxTextured.Checked = true;
            this.checkBoxTextured.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTextured.Location = new System.Drawing.Point(9, 19);
            this.checkBoxTextured.Name = "checkBoxTextured";
            this.checkBoxTextured.Size = new System.Drawing.Size(113, 17);
            this.checkBoxTextured.TabIndex = 0;
            this.checkBoxTextured.Text = "Draw with textures";
            this.checkBoxTextured.UseVisualStyleBackColor = true;
            // 
            // progressBarMapGeneration
            // 
            this.progressBarMapGeneration.Location = new System.Drawing.Point(79, 139);
            this.progressBarMapGeneration.Name = "progressBarMapGeneration";
            this.progressBarMapGeneration.Size = new System.Drawing.Size(200, 23);
            this.progressBarMapGeneration.TabIndex = 4;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(101, 168);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 5;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.ButtonGenerate_Click);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(322, 162);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 6;
            this.pictureBoxPreview.TabStop = false;
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Controls.Add(this.labelImageDetails);
            this.groupBoxPreview.Controls.Add(this.pictureBoxPreview);
            this.groupBoxPreview.Location = new System.Drawing.Point(12, 197);
            this.groupBoxPreview.Name = "groupBoxPreview";
            this.groupBoxPreview.Size = new System.Drawing.Size(334, 200);
            this.groupBoxPreview.TabIndex = 7;
            this.groupBoxPreview.TabStop = false;
            this.groupBoxPreview.Text = "Preview";
            // 
            // labelImageDetails
            // 
            this.labelImageDetails.AutoSize = true;
            this.labelImageDetails.Location = new System.Drawing.Point(6, 184);
            this.labelImageDetails.Name = "labelImageDetails";
            this.labelImageDetails.Size = new System.Drawing.Size(0, 13);
            this.labelImageDetails.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(142, 403);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // backgroundWorkerMapGenerator
            // 
            this.backgroundWorkerMapGenerator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerMapGenerator_DoWork);
            this.backgroundWorkerMapGenerator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerMapGenerator_RunWorkerCompleted);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(182, 168);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // WorldMapGeneratorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 438);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxPreview);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.progressBarMapGeneration);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.labelMapFile);
            this.Controls.Add(this.buttonBrowseMapFile);
            this.Controls.Add(this.textBoxMapFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorldMapGeneratorDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "World Map Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorldMapGeneratorDialog_FormClosing);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMapFile;
        private System.Windows.Forms.Button buttonBrowseMapFile;
        private System.Windows.Forms.Label labelMapFile;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.Label labelSeason;
        private System.Windows.Forms.CheckBox checkBoxHideBaseSeasonTerrain;
        private System.Windows.Forms.ComboBox comboBoxSeason;
        private System.Windows.Forms.CheckBox checkBoxTextured;
        private System.Windows.Forms.ProgressBar progressBarMapGeneration;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.GroupBox groupBoxPreview;
        private System.Windows.Forms.Label labelImageDetails;
        private System.Windows.Forms.Button buttonSave;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMapGenerator;
        private System.Windows.Forms.Button buttonCancel;
    }
}