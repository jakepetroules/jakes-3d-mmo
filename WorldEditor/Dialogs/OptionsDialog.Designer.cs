namespace MMO3D.WorldEditor
{
    partial class OptionsDialog
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.tabControlOptions = new System.Windows.Forms.TabControl();
            this.tabPageRenderer = new System.Windows.Forms.TabPage();
            this.linkLabelCameraRotateSpeedDefault = new System.Windows.Forms.LinkLabel();
            this.linkLabelCameraMoveSpeedDefault = new System.Windows.Forms.LinkLabel();
            this.numericUpDownRotationSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelCameraRotateSpeed = new System.Windows.Forms.Label();
            this.numericUpDownMoveSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelCameraMoveSpeed = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.tabControlOptions.SuspendLayout();
            this.tabPageRenderer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRotationSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMoveSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.buttonCancel, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.buttonOK, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.tabControlOptions, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(570, 344);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(492, 318);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(411, 318);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // tabControlOptions
            // 
            this.tableLayoutPanel.SetColumnSpan(this.tabControlOptions, 2);
            this.tabControlOptions.Controls.Add(this.tabPageRenderer);
            this.tabControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOptions.Location = new System.Drawing.Point(3, 3);
            this.tabControlOptions.Name = "tabControlOptions";
            this.tabControlOptions.SelectedIndex = 0;
            this.tabControlOptions.Size = new System.Drawing.Size(564, 309);
            this.tabControlOptions.TabIndex = 0;
            // 
            // tabPageRenderer
            // 
            this.tabPageRenderer.Controls.Add(this.linkLabelCameraRotateSpeedDefault);
            this.tabPageRenderer.Controls.Add(this.linkLabelCameraMoveSpeedDefault);
            this.tabPageRenderer.Controls.Add(this.numericUpDownRotationSpeed);
            this.tabPageRenderer.Controls.Add(this.labelCameraRotateSpeed);
            this.tabPageRenderer.Controls.Add(this.numericUpDownMoveSpeed);
            this.tabPageRenderer.Controls.Add(this.labelCameraMoveSpeed);
            this.tabPageRenderer.Location = new System.Drawing.Point(4, 22);
            this.tabPageRenderer.Name = "tabPageRenderer";
            this.tabPageRenderer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRenderer.Size = new System.Drawing.Size(556, 283);
            this.tabPageRenderer.TabIndex = 0;
            this.tabPageRenderer.Text = "Renderer";
            this.tabPageRenderer.UseVisualStyleBackColor = true;
            // 
            // linkLabelCameraRotateSpeedDefault
            // 
            this.linkLabelCameraRotateSpeedDefault.AutoSize = true;
            this.linkLabelCameraRotateSpeedDefault.Location = new System.Drawing.Point(509, 34);
            this.linkLabelCameraRotateSpeedDefault.Name = "linkLabelCameraRotateSpeedDefault";
            this.linkLabelCameraRotateSpeedDefault.Size = new System.Drawing.Size(41, 13);
            this.linkLabelCameraRotateSpeedDefault.TabIndex = 5;
            this.linkLabelCameraRotateSpeedDefault.TabStop = true;
            this.linkLabelCameraRotateSpeedDefault.Text = "Default";
            this.linkLabelCameraRotateSpeedDefault.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelDefault_LinkClicked);
            // 
            // linkLabelCameraMoveSpeedDefault
            // 
            this.linkLabelCameraMoveSpeedDefault.AutoSize = true;
            this.linkLabelCameraMoveSpeedDefault.Location = new System.Drawing.Point(509, 8);
            this.linkLabelCameraMoveSpeedDefault.Name = "linkLabelCameraMoveSpeedDefault";
            this.linkLabelCameraMoveSpeedDefault.Size = new System.Drawing.Size(41, 13);
            this.linkLabelCameraMoveSpeedDefault.TabIndex = 2;
            this.linkLabelCameraMoveSpeedDefault.TabStop = true;
            this.linkLabelCameraMoveSpeedDefault.Text = "Default";
            this.linkLabelCameraMoveSpeedDefault.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelDefault_LinkClicked);
            // 
            // numericUpDownRotationSpeed
            // 
            this.numericUpDownRotationSpeed.DecimalPlaces = 3;
            this.numericUpDownRotationSpeed.Location = new System.Drawing.Point(195, 32);
            this.numericUpDownRotationSpeed.Name = "numericUpDownRotationSpeed";
            this.numericUpDownRotationSpeed.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownRotationSpeed.TabIndex = 4;
            // 
            // labelCameraRotateSpeed
            // 
            this.labelCameraRotateSpeed.AutoSize = true;
            this.labelCameraRotateSpeed.Location = new System.Drawing.Point(6, 34);
            this.labelCameraRotateSpeed.Name = "labelCameraRotateSpeed";
            this.labelCameraRotateSpeed.Size = new System.Drawing.Size(169, 13);
            this.labelCameraRotateSpeed.TabIndex = 3;
            this.labelCameraRotateSpeed.Text = "Camera rotation speed (per frame):";
            // 
            // numericUpDownMoveSpeed
            // 
            this.numericUpDownMoveSpeed.DecimalPlaces = 3;
            this.numericUpDownMoveSpeed.Location = new System.Drawing.Point(195, 6);
            this.numericUpDownMoveSpeed.Name = "numericUpDownMoveSpeed";
            this.numericUpDownMoveSpeed.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownMoveSpeed.TabIndex = 1;
            // 
            // labelCameraMoveSpeed
            // 
            this.labelCameraMoveSpeed.AutoSize = true;
            this.labelCameraMoveSpeed.Location = new System.Drawing.Point(6, 8);
            this.labelCameraMoveSpeed.Name = "labelCameraMoveSpeed";
            this.labelCameraMoveSpeed.Size = new System.Drawing.Size(183, 13);
            this.labelCameraMoveSpeed.TabIndex = 0;
            this.labelCameraMoveSpeed.Text = "Camera movement speed (per frame):";
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(594, 368);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tabControlOptions.ResumeLayout(false);
            this.tabPageRenderer.ResumeLayout(false);
            this.tabPageRenderer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRotationSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMoveSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TabControl tabControlOptions;
        private System.Windows.Forms.TabPage tabPageRenderer;
        private System.Windows.Forms.NumericUpDown numericUpDownMoveSpeed;
        private System.Windows.Forms.Label labelCameraMoveSpeed;
        private System.Windows.Forms.NumericUpDown numericUpDownRotationSpeed;
        private System.Windows.Forms.Label labelCameraRotateSpeed;
        private System.Windows.Forms.LinkLabel linkLabelCameraRotateSpeedDefault;
        private System.Windows.Forms.LinkLabel linkLabelCameraMoveSpeedDefault;
    }
}