namespace Petroules.Synteza.Windows.Forms
{
    public partial class ExtendedColorDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TrackBar trackBarRed;
        private System.Windows.Forms.TrackBar trackBarGreen;
        private System.Windows.Forms.TrackBar trackBarBlue;
        private System.Windows.Forms.TrackBar trackBarAlpha;
        private System.Windows.Forms.NumericUpDown numericUpDownRed;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelColor;
        private System.Windows.Forms.NumericUpDown numericUpDownAlpha;
        private System.Windows.Forms.NumericUpDown numericUpDownBlue;
        private System.Windows.Forms.NumericUpDown numericUpDownGreen;
        private System.Windows.Forms.Label labelRed;
        private System.Windows.Forms.Label labelGreen;
        private System.Windows.Forms.Label labelBlue;
        private System.Windows.Forms.Panel panelTransparency;
        private System.Windows.Forms.PictureBox pictureBoxColor;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxKnownColors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelAlpha;
        private System.Windows.Forms.Label labelAlpha;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.trackBarRed = new System.Windows.Forms.TrackBar();
            this.trackBarGreen = new System.Windows.Forms.TrackBar();
            this.trackBarBlue = new System.Windows.Forms.TrackBar();
            this.trackBarAlpha = new System.Windows.Forms.TrackBar();
            this.numericUpDownRed = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanelColor = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxKnownColors = new System.Windows.Forms.ComboBox();
            this.numericUpDownAlpha = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBlue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownGreen = new System.Windows.Forms.NumericUpDown();
            this.labelRed = new System.Windows.Forms.Label();
            this.labelGreen = new System.Windows.Forms.Label();
            this.labelBlue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTransparency = new System.Windows.Forms.Panel();
            this.pictureBoxColor = new System.Windows.Forms.PictureBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelAlpha = new System.Windows.Forms.Panel();
            this.labelAlpha = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRed)).BeginInit();
            this.tableLayoutPanelColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGreen)).BeginInit();
            this.panelTransparency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColor)).BeginInit();
            this.panelAlpha.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarRed
            // 
            this.trackBarRed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.trackBarRed.Location = new System.Drawing.Point(177, 8);
            this.trackBarRed.Maximum = 255;
            this.trackBarRed.Name = "trackBarRed";
            this.trackBarRed.Size = new System.Drawing.Size(282, 45);
            this.trackBarRed.TabIndex = 0;
            this.trackBarRed.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            // 
            // trackBarGreen
            // 
            this.trackBarGreen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.trackBarGreen.Location = new System.Drawing.Point(177, 70);
            this.trackBarGreen.Maximum = 255;
            this.trackBarGreen.Name = "trackBarGreen";
            this.trackBarGreen.Size = new System.Drawing.Size(282, 45);
            this.trackBarGreen.TabIndex = 1;
            this.trackBarGreen.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            // 
            // trackBarBlue
            // 
            this.trackBarBlue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.trackBarBlue.Location = new System.Drawing.Point(177, 132);
            this.trackBarBlue.Maximum = 255;
            this.trackBarBlue.Name = "trackBarBlue";
            this.trackBarBlue.Size = new System.Drawing.Size(282, 45);
            this.trackBarBlue.TabIndex = 2;
            this.trackBarBlue.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            // 
            // trackBarAlpha
            // 
            this.trackBarAlpha.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.trackBarAlpha.Location = new System.Drawing.Point(177, 194);
            this.trackBarAlpha.Maximum = 255;
            this.trackBarAlpha.Name = "trackBarAlpha";
            this.trackBarAlpha.Size = new System.Drawing.Size(282, 45);
            this.trackBarAlpha.TabIndex = 3;
            this.trackBarAlpha.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            // 
            // numericUpDownRed
            // 
            this.numericUpDownRed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownRed.Location = new System.Drawing.Point(131, 21);
            this.numericUpDownRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownRed.Name = "numericUpDownRed";
            this.numericUpDownRed.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownRed.TabIndex = 4;
            this.numericUpDownRed.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            // 
            // tableLayoutPanelColor
            // 
            this.tableLayoutPanelColor.ColumnCount = 3;
            this.tableLayoutPanelColor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelColor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelColor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelColor.Controls.Add(this.comboBoxKnownColors, 2, 4);
            this.tableLayoutPanelColor.Controls.Add(this.numericUpDownAlpha, 1, 3);
            this.tableLayoutPanelColor.Controls.Add(this.trackBarRed, 2, 0);
            this.tableLayoutPanelColor.Controls.Add(this.numericUpDownBlue, 1, 2);
            this.tableLayoutPanelColor.Controls.Add(this.trackBarGreen, 2, 1);
            this.tableLayoutPanelColor.Controls.Add(this.numericUpDownGreen, 1, 1);
            this.tableLayoutPanelColor.Controls.Add(this.trackBarAlpha, 2, 3);
            this.tableLayoutPanelColor.Controls.Add(this.trackBarBlue, 2, 2);
            this.tableLayoutPanelColor.Controls.Add(this.numericUpDownRed, 1, 0);
            this.tableLayoutPanelColor.Controls.Add(this.labelRed, 0, 0);
            this.tableLayoutPanelColor.Controls.Add(this.labelGreen, 0, 1);
            this.tableLayoutPanelColor.Controls.Add(this.labelBlue, 0, 2);
            this.tableLayoutPanelColor.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanelColor.Controls.Add(this.panelAlpha, 0, 3);
            this.tableLayoutPanelColor.Location = new System.Drawing.Point(220, 12);
            this.tableLayoutPanelColor.Name = "tableLayoutPanelColor";
            this.tableLayoutPanelColor.RowCount = 5;
            this.tableLayoutPanelColor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelColor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelColor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelColor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelColor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelColor.Size = new System.Drawing.Size(462, 275);
            this.tableLayoutPanelColor.TabIndex = 5;
            // 
            // comboBoxKnownColors
            // 
            this.comboBoxKnownColors.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxKnownColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKnownColors.FormattingEnabled = true;
            this.comboBoxKnownColors.Location = new System.Drawing.Point(177, 251);
            this.comboBoxKnownColors.Name = "comboBoxKnownColors";
            this.comboBoxKnownColors.Size = new System.Drawing.Size(150, 21);
            this.comboBoxKnownColors.TabIndex = 9;
            this.comboBoxKnownColors.SelectedIndexChanged += new System.EventHandler(this.ComboBoxKnownColors_SelectedIndexChanged);
            // 
            // numericUpDownAlpha
            // 
            this.numericUpDownAlpha.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownAlpha.Location = new System.Drawing.Point(131, 207);
            this.numericUpDownAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownAlpha.Name = "numericUpDownAlpha";
            this.numericUpDownAlpha.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownAlpha.TabIndex = 8;
            this.numericUpDownAlpha.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            // 
            // numericUpDownBlue
            // 
            this.numericUpDownBlue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownBlue.Location = new System.Drawing.Point(131, 145);
            this.numericUpDownBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownBlue.Name = "numericUpDownBlue";
            this.numericUpDownBlue.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownBlue.TabIndex = 7;
            this.numericUpDownBlue.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            // 
            // numericUpDownGreen
            // 
            this.numericUpDownGreen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownGreen.Location = new System.Drawing.Point(131, 83);
            this.numericUpDownGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownGreen.Name = "numericUpDownGreen";
            this.numericUpDownGreen.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownGreen.TabIndex = 6;
            this.numericUpDownGreen.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            // 
            // labelRed
            // 
            this.labelRed.BackColor = System.Drawing.Color.Red;
            this.labelRed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRed.Location = new System.Drawing.Point(3, 0);
            this.labelRed.Name = "labelRed";
            this.labelRed.Size = new System.Drawing.Size(122, 62);
            this.labelRed.TabIndex = 9;
            this.labelRed.Text = "Red";
            this.labelRed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelGreen
            // 
            this.labelGreen.BackColor = System.Drawing.Color.Lime;
            this.labelGreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGreen.Location = new System.Drawing.Point(3, 62);
            this.labelGreen.Name = "labelGreen";
            this.labelGreen.Size = new System.Drawing.Size(122, 62);
            this.labelGreen.TabIndex = 10;
            this.labelGreen.Text = "Green";
            this.labelGreen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelBlue
            // 
            this.labelBlue.BackColor = System.Drawing.Color.Blue;
            this.labelBlue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBlue.ForeColor = System.Drawing.Color.White;
            this.labelBlue.Location = new System.Drawing.Point(3, 124);
            this.labelBlue.Name = "labelBlue";
            this.labelBlue.Size = new System.Drawing.Size(122, 62);
            this.labelBlue.TabIndex = 11;
            this.labelBlue.Text = "Blue";
            this.labelBlue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Predefined";
            // 
            // panelTransparency
            // 
            this.panelTransparency.BackgroundImage = global::Petroules.Synteza.Properties.Resources.Transparent;
            this.panelTransparency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTransparency.Controls.Add(this.pictureBoxColor);
            this.panelTransparency.Location = new System.Drawing.Point(12, 12);
            this.panelTransparency.Name = "panelTransparency";
            this.panelTransparency.Size = new System.Drawing.Size(202, 202);
            this.panelTransparency.TabIndex = 6;
            // 
            // pictureBoxColor
            // 
            this.pictureBoxColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxColor.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxColor.Name = "pictureBoxColor";
            this.pictureBoxColor.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxColor.TabIndex = 7;
            this.pictureBoxColor.TabStop = false;
            this.pictureBoxColor.BackColorChanged += new System.EventHandler(this.PictureBoxColor_BackColorChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(35, 246);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(116, 246);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panelAlpha
            // 
            this.panelAlpha.BackgroundImage = global::Petroules.Synteza.Properties.Resources.Transparent;
            this.panelAlpha.Controls.Add(this.labelAlpha);
            this.panelAlpha.Location = new System.Drawing.Point(3, 189);
            this.panelAlpha.Name = "panelAlpha";
            this.panelAlpha.Size = new System.Drawing.Size(122, 56);
            this.panelAlpha.TabIndex = 14;
            // 
            // labelAlpha
            // 
            this.labelAlpha.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAlpha.AutoSize = true;
            this.labelAlpha.Location = new System.Drawing.Point(3, 22);
            this.labelAlpha.Name = "labelAlpha";
            this.labelAlpha.Size = new System.Drawing.Size(34, 13);
            this.labelAlpha.TabIndex = 0;
            this.labelAlpha.Text = "Alpha";
            this.labelAlpha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExtendedColorDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(694, 298);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.panelTransparency);
            this.Controls.Add(this.tableLayoutPanelColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtendedColorDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Extended Color Dialog";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRed)).EndInit();
            this.tableLayoutPanelColor.ResumeLayout(false);
            this.tableLayoutPanelColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGreen)).EndInit();
            this.panelTransparency.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColor)).EndInit();
            this.panelAlpha.ResumeLayout(false);
            this.panelAlpha.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}