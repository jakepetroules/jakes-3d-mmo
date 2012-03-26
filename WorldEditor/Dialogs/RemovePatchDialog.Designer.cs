namespace MMO3D.WorldEditor
{
    partial class RemovePatchDialog
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelPatchX = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxQuickLinks = new System.Windows.Forms.GroupBox();
            this.buttonBottomRight = new System.Windows.Forms.Button();
            this.buttonBottomCenter = new System.Windows.Forms.Button();
            this.buttonBottomLeft = new System.Windows.Forms.Button();
            this.buttonMiddleRight = new System.Windows.Forms.Button();
            this.buttonCenter = new System.Windows.Forms.Button();
            this.buttonMiddleLeft = new System.Windows.Forms.Button();
            this.buttonTopRight = new System.Windows.Forms.Button();
            this.buttonTopCenter = new System.Windows.Forms.Button();
            this.buttonTopLeft = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.labelTileY = new System.Windows.Forms.Label();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownZ = new System.Windows.Forms.NumericUpDown();
            this.labelTileZ = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.groupBoxQuickLinks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.labelPatchX, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.buttonCancel, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.groupBoxQuickLinks, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.buttonOK, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.numericUpDownY, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelTileY, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.numericUpDownX, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.numericUpDownZ, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelTileZ, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(394, 238);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelPatchX
            // 
            this.labelPatchX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPatchX.Location = new System.Drawing.Point(3, 0);
            this.labelPatchX.Name = "labelPatchX";
            this.labelPatchX.Size = new System.Drawing.Size(191, 26);
            this.labelPatchX.TabIndex = 0;
            this.labelPatchX.Text = "Patch X coordinate";
            this.labelPatchX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(200, 202);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
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
            this.groupBoxQuickLinks.Location = new System.Drawing.Point(72, 81);
            this.groupBoxQuickLinks.Name = "groupBoxQuickLinks";
            this.groupBoxQuickLinks.Size = new System.Drawing.Size(250, 106);
            this.groupBoxQuickLinks.TabIndex = 6;
            this.groupBoxQuickLinks.TabStop = false;
            this.groupBoxQuickLinks.Text = "Quick links for patch ID";
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
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(119, 202);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Location = new System.Drawing.Point(200, 29);
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
            this.numericUpDownY.TabIndex = 3;
            // 
            // labelTileY
            // 
            this.labelTileY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTileY.Location = new System.Drawing.Point(3, 26);
            this.labelTileY.Name = "labelTileY";
            this.labelTileY.Size = new System.Drawing.Size(191, 26);
            this.labelTileY.TabIndex = 2;
            this.labelTileY.Text = "Patch Y coordinate";
            this.labelTileY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Location = new System.Drawing.Point(200, 3);
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
            this.numericUpDownX.TabIndex = 1;
            // 
            // numericUpDownZ
            // 
            this.numericUpDownZ.Location = new System.Drawing.Point(200, 55);
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
            this.numericUpDownZ.TabIndex = 5;
            // 
            // labelTileZ
            // 
            this.labelTileZ.AutoSize = true;
            this.labelTileZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTileZ.Location = new System.Drawing.Point(3, 52);
            this.labelTileZ.Name = "labelTileZ";
            this.labelTileZ.Size = new System.Drawing.Size(191, 26);
            this.labelTileZ.TabIndex = 4;
            this.labelTileZ.Text = "Patch Z coordinate";
            this.labelTileZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RemovePatchDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(394, 238);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RemovePatchDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remove terrain patch";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.groupBoxQuickLinks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelPatchX;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.Label labelTileY;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBoxQuickLinks;
        private System.Windows.Forms.Button buttonBottomRight;
        private System.Windows.Forms.Button buttonBottomCenter;
        private System.Windows.Forms.Button buttonBottomLeft;
        private System.Windows.Forms.Button buttonMiddleRight;
        private System.Windows.Forms.Button buttonCenter;
        private System.Windows.Forms.Button buttonMiddleLeft;
        private System.Windows.Forms.Button buttonTopRight;
        private System.Windows.Forms.Button buttonTopCenter;
        private System.Windows.Forms.Button buttonTopLeft;
        private System.Windows.Forms.NumericUpDown numericUpDownZ;
        private System.Windows.Forms.Label labelTileZ;
    }
}