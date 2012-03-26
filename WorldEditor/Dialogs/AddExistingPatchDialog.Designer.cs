namespace MMO3D.WorldEditor
{
    partial class AddExistingPatchDialog
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
            this.buttonBottomCenter = new System.Windows.Forms.Button();
            this.buttonBottomLeft = new System.Windows.Forms.Button();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonBottomRight = new System.Windows.Forms.Button();
            this.numericUpDownZ = new System.Windows.Forms.NumericUpDown();
            this.groupBoxQuickLinks = new System.Windows.Forms.GroupBox();
            this.buttonMiddleRight = new System.Windows.Forms.Button();
            this.buttonCenter = new System.Windows.Forms.Button();
            this.buttonMiddleLeft = new System.Windows.Forms.Button();
            this.buttonTopRight = new System.Windows.Forms.Button();
            this.buttonTopCenter = new System.Windows.Forms.Button();
            this.buttonTopLeft = new System.Windows.Forms.Button();
            this.labelTileY = new System.Windows.Forms.Label();
            this.labelPatchX = new System.Windows.Forms.Label();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelFile = new System.Windows.Forms.Label();
            this.flowLayoutPanelFileBrowser = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelTileZ = new System.Windows.Forms.Label();
            this.radioButtonManual = new System.Windows.Forms.RadioButton();
            this.radioButtonAuto = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).BeginInit();
            this.groupBoxQuickLinks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.flowLayoutPanelFileBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonBottomCenter
            // 
            this.buttonBottomCenter.Location = new System.Drawing.Point(114, 77);
            this.buttonBottomCenter.Name = "buttonBottomCenter";
            this.buttonBottomCenter.Size = new System.Drawing.Size(23, 23);
            this.buttonBottomCenter.TabIndex = 7;
            this.buttonBottomCenter.Tag = "1";
            this.buttonBottomCenter.UseVisualStyleBackColor = true;
            this.buttonBottomCenter.Click += new System.EventHandler(this.PatchIDButton_Click);
            // 
            // buttonBottomLeft
            // 
            this.buttonBottomLeft.Location = new System.Drawing.Point(85, 77);
            this.buttonBottomLeft.Name = "buttonBottomLeft";
            this.buttonBottomLeft.Size = new System.Drawing.Size(23, 23);
            this.buttonBottomLeft.TabIndex = 6;
            this.buttonBottomLeft.Tag = "0";
            this.buttonBottomLeft.UseVisualStyleBackColor = true;
            this.buttonBottomLeft.Click += new System.EventHandler(this.PatchIDButton_Click);
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Location = new System.Drawing.Point(250, 26);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            65536,
            0,
            0,
            -2147483648});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownX.TabIndex = 3;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(250, 264);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(169, 264);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 11;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonBottomRight
            // 
            this.buttonBottomRight.Location = new System.Drawing.Point(143, 77);
            this.buttonBottomRight.Name = "buttonBottomRight";
            this.buttonBottomRight.Size = new System.Drawing.Size(23, 23);
            this.buttonBottomRight.TabIndex = 8;
            this.buttonBottomRight.Tag = "2";
            this.buttonBottomRight.UseVisualStyleBackColor = true;
            this.buttonBottomRight.Click += new System.EventHandler(this.PatchIDButton_Click);
            // 
            // numericUpDownZ
            // 
            this.numericUpDownZ.Location = new System.Drawing.Point(250, 78);
            this.numericUpDownZ.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownZ.Minimum = new decimal(new int[] {
            65536,
            0,
            0,
            -2147483648});
            this.numericUpDownZ.Name = "numericUpDownZ";
            this.numericUpDownZ.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownZ.TabIndex = 7;
            // 
            // groupBoxQuickLinks
            // 
            this.groupBoxQuickLinks.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel.SetColumnSpan(this.groupBoxQuickLinks, 2);
            this.groupBoxQuickLinks.Controls.Add(this.buttonBottomRight);
            this.groupBoxQuickLinks.Controls.Add(this.buttonBottomCenter);
            this.groupBoxQuickLinks.Controls.Add(this.buttonBottomLeft);
            this.groupBoxQuickLinks.Controls.Add(this.buttonMiddleRight);
            this.groupBoxQuickLinks.Controls.Add(this.buttonCenter);
            this.groupBoxQuickLinks.Controls.Add(this.buttonMiddleLeft);
            this.groupBoxQuickLinks.Controls.Add(this.buttonTopRight);
            this.groupBoxQuickLinks.Controls.Add(this.buttonTopCenter);
            this.groupBoxQuickLinks.Controls.Add(this.buttonTopLeft);
            this.groupBoxQuickLinks.Location = new System.Drawing.Point(122, 139);
            this.groupBoxQuickLinks.Name = "groupBoxQuickLinks";
            this.groupBoxQuickLinks.Size = new System.Drawing.Size(250, 106);
            this.groupBoxQuickLinks.TabIndex = 10;
            this.groupBoxQuickLinks.TabStop = false;
            this.groupBoxQuickLinks.Text = "Quick links for patch ID";
            // 
            // buttonMiddleRight
            // 
            this.buttonMiddleRight.Location = new System.Drawing.Point(143, 48);
            this.buttonMiddleRight.Name = "buttonMiddleRight";
            this.buttonMiddleRight.Size = new System.Drawing.Size(23, 23);
            this.buttonMiddleRight.TabIndex = 5;
            this.buttonMiddleRight.Tag = "5";
            this.buttonMiddleRight.UseVisualStyleBackColor = true;
            this.buttonMiddleRight.Click += new System.EventHandler(this.PatchIDButton_Click);
            // 
            // buttonCenter
            // 
            this.buttonCenter.Location = new System.Drawing.Point(114, 48);
            this.buttonCenter.Name = "buttonCenter";
            this.buttonCenter.Size = new System.Drawing.Size(23, 23);
            this.buttonCenter.TabIndex = 4;
            this.buttonCenter.Tag = "4";
            this.buttonCenter.UseVisualStyleBackColor = true;
            this.buttonCenter.Click += new System.EventHandler(this.PatchIDButton_Click);
            // 
            // buttonMiddleLeft
            // 
            this.buttonMiddleLeft.Location = new System.Drawing.Point(85, 48);
            this.buttonMiddleLeft.Name = "buttonMiddleLeft";
            this.buttonMiddleLeft.Size = new System.Drawing.Size(23, 23);
            this.buttonMiddleLeft.TabIndex = 3;
            this.buttonMiddleLeft.Tag = "3";
            this.buttonMiddleLeft.UseVisualStyleBackColor = true;
            this.buttonMiddleLeft.Click += new System.EventHandler(this.PatchIDButton_Click);
            // 
            // buttonTopRight
            // 
            this.buttonTopRight.Location = new System.Drawing.Point(143, 19);
            this.buttonTopRight.Name = "buttonTopRight";
            this.buttonTopRight.Size = new System.Drawing.Size(23, 23);
            this.buttonTopRight.TabIndex = 2;
            this.buttonTopRight.Tag = "8";
            this.buttonTopRight.UseVisualStyleBackColor = true;
            this.buttonTopRight.Click += new System.EventHandler(this.PatchIDButton_Click);
            // 
            // buttonTopCenter
            // 
            this.buttonTopCenter.Location = new System.Drawing.Point(114, 19);
            this.buttonTopCenter.Name = "buttonTopCenter";
            this.buttonTopCenter.Size = new System.Drawing.Size(23, 23);
            this.buttonTopCenter.TabIndex = 1;
            this.buttonTopCenter.Tag = "7";
            this.buttonTopCenter.UseVisualStyleBackColor = true;
            this.buttonTopCenter.Click += new System.EventHandler(this.PatchIDButton_Click);
            // 
            // buttonTopLeft
            // 
            this.buttonTopLeft.Location = new System.Drawing.Point(85, 19);
            this.buttonTopLeft.Name = "buttonTopLeft";
            this.buttonTopLeft.Size = new System.Drawing.Size(23, 23);
            this.buttonTopLeft.TabIndex = 0;
            this.buttonTopLeft.Tag = "6";
            this.buttonTopLeft.UseVisualStyleBackColor = true;
            this.buttonTopLeft.Click += new System.EventHandler(this.PatchIDButton_Click);
            // 
            // labelTileY
            // 
            this.labelTileY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTileY.Location = new System.Drawing.Point(3, 49);
            this.labelTileY.Name = "labelTileY";
            this.labelTileY.Size = new System.Drawing.Size(241, 26);
            this.labelTileY.TabIndex = 4;
            this.labelTileY.Text = "Patch ID - Y coordinate";
            this.labelTileY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPatchX
            // 
            this.labelPatchX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPatchX.Location = new System.Drawing.Point(3, 23);
            this.labelPatchX.Name = "labelPatchX";
            this.labelPatchX.Size = new System.Drawing.Size(241, 26);
            this.labelPatchX.TabIndex = 2;
            this.labelPatchX.Text = "Patch ID - X coordinate";
            this.labelPatchX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Location = new System.Drawing.Point(250, 52);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownY.Minimum = new decimal(new int[] {
            65536,
            0,
            0,
            -2147483648});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownY.TabIndex = 5;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.labelFile, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.groupBoxQuickLinks, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.buttonCancel, 1, 6);
            this.tableLayoutPanel.Controls.Add(this.buttonOK, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanelFileBrowser, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.numericUpDownZ, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.numericUpDownY, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.numericUpDownX, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelTileZ, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.labelTileY, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.radioButtonManual, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelPatchX, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.radioButtonAuto, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 7;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(494, 303);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelFile
            // 
            this.labelFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFile.Location = new System.Drawing.Point(3, 101);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(241, 35);
            this.labelFile.TabIndex = 8;
            this.labelFile.Text = "Terrain patch file:";
            this.labelFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanelFileBrowser
            // 
            this.flowLayoutPanelFileBrowser.Controls.Add(this.textBoxFile);
            this.flowLayoutPanelFileBrowser.Controls.Add(this.buttonBrowse);
            this.flowLayoutPanelFileBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelFileBrowser.Location = new System.Drawing.Point(250, 104);
            this.flowLayoutPanelFileBrowser.Name = "flowLayoutPanelFileBrowser";
            this.flowLayoutPanelFileBrowser.Size = new System.Drawing.Size(241, 29);
            this.flowLayoutPanelFileBrowser.TabIndex = 9;
            // 
            // textBoxFile
            // 
            this.textBoxFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxFile.Location = new System.Drawing.Point(3, 4);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(120, 20);
            this.textBoxFile.TabIndex = 0;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonBrowse.Location = new System.Drawing.Point(129, 3);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
            // 
            // labelTileZ
            // 
            this.labelTileZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTileZ.Location = new System.Drawing.Point(3, 75);
            this.labelTileZ.Name = "labelTileZ";
            this.labelTileZ.Size = new System.Drawing.Size(241, 26);
            this.labelTileZ.TabIndex = 6;
            this.labelTileZ.Text = "Patch ID - Z coordinate";
            this.labelTileZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButtonManual
            // 
            this.radioButtonManual.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radioButtonManual.AutoSize = true;
            this.radioButtonManual.Checked = true;
            this.radioButtonManual.Location = new System.Drawing.Point(250, 3);
            this.radioButtonManual.Name = "radioButtonManual";
            this.radioButtonManual.Size = new System.Drawing.Size(105, 17);
            this.radioButtonManual.TabIndex = 1;
            this.radioButtonManual.TabStop = true;
            this.radioButtonManual.Text = "Manual Patch ID";
            this.radioButtonManual.UseVisualStyleBackColor = true;
            this.radioButtonManual.CheckedChanged += new System.EventHandler(this.RadioButtonAuto_CheckedChanged);
            // 
            // radioButtonAuto
            // 
            this.radioButtonAuto.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.radioButtonAuto.AutoSize = true;
            this.radioButtonAuto.Location = new System.Drawing.Point(127, 3);
            this.radioButtonAuto.Name = "radioButtonAuto";
            this.radioButtonAuto.Size = new System.Drawing.Size(117, 17);
            this.radioButtonAuto.TabIndex = 0;
            this.radioButtonAuto.Text = "Automatic Patch ID";
            this.radioButtonAuto.UseVisualStyleBackColor = true;
            this.radioButtonAuto.CheckedChanged += new System.EventHandler(this.RadioButtonAuto_CheckedChanged);
            // 
            // AddExistingPatchDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(494, 303);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddExistingPatchDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add existing terrain patch";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).EndInit();
            this.groupBoxQuickLinks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.flowLayoutPanelFileBrowser.ResumeLayout(false);
            this.flowLayoutPanelFileBrowser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBottomCenter;
        private System.Windows.Forms.Button buttonBottomLeft;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonBottomRight;
        private System.Windows.Forms.NumericUpDown numericUpDownZ;
        private System.Windows.Forms.GroupBox groupBoxQuickLinks;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelPatchX;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.Label labelTileY;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Label labelTileZ;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFileBrowser;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonMiddleRight;
        private System.Windows.Forms.Button buttonCenter;
        private System.Windows.Forms.Button buttonMiddleLeft;
        private System.Windows.Forms.Button buttonTopRight;
        private System.Windows.Forms.Button buttonTopCenter;
        private System.Windows.Forms.Button buttonTopLeft;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.RadioButton radioButtonManual;
        private System.Windows.Forms.RadioButton radioButtonAuto;
    }
}