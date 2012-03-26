namespace MMO3D.Client
{
    public partial class SettingsWindow
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.comboBoxResolutions = new System.Windows.Forms.ComboBox();
            this.buttonWindowed = new System.Windows.Forms.Button();
            this.buttonFullscreen = new System.Windows.Forms.Button();
            this.labelFullscreenResolution = new System.Windows.Forms.Label();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageDisplay = new System.Windows.Forms.TabPage();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabPageDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControlSettings);
            // 
            // comboBoxResolutions
            // 
            this.comboBoxResolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResolutions.FormattingEnabled = true;
            this.comboBoxResolutions.Location = new System.Drawing.Point(142, 35);
            this.comboBoxResolutions.Name = "comboBoxResolutions";
            this.comboBoxResolutions.Size = new System.Drawing.Size(121, 21);
            this.comboBoxResolutions.TabIndex = 0;
            this.comboBoxResolutions.SelectedIndexChanged += new System.EventHandler(this.ComboBoxResolutions_SelectedIndexChanged);
            // 
            // buttonWindowed
            // 
            this.buttonWindowed.Enabled = false;
            this.buttonWindowed.Location = new System.Drawing.Point(68, 6);
            this.buttonWindowed.Name = "buttonWindowed";
            this.buttonWindowed.Size = new System.Drawing.Size(75, 23);
            this.buttonWindowed.TabIndex = 1;
            this.buttonWindowed.Text = "Windowed";
            this.buttonWindowed.UseVisualStyleBackColor = true;
            this.buttonWindowed.Click += new System.EventHandler(this.ButtonWindowed_Click);
            // 
            // buttonFullscreen
            // 
            this.buttonFullscreen.Location = new System.Drawing.Point(149, 6);
            this.buttonFullscreen.Name = "buttonFullscreen";
            this.buttonFullscreen.Size = new System.Drawing.Size(75, 23);
            this.buttonFullscreen.TabIndex = 2;
            this.buttonFullscreen.Text = "Fullscreen";
            this.buttonFullscreen.UseVisualStyleBackColor = true;
            this.buttonFullscreen.Click += new System.EventHandler(this.ButtonFullscreen_Click);
            // 
            // labelFullscreenResolution
            // 
            this.labelFullscreenResolution.AutoSize = true;
            this.labelFullscreenResolution.Location = new System.Drawing.Point(30, 38);
            this.labelFullscreenResolution.Name = "labelFullscreenResolution";
            this.labelFullscreenResolution.Size = new System.Drawing.Size(106, 13);
            this.labelFullscreenResolution.TabIndex = 3;
            this.labelFullscreenResolution.Text = "Fullscreen resolution:";
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabPageDisplay);
            this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(300, 272);
            this.tabControlSettings.TabIndex = 4;
            // 
            // tabPageDisplay
            // 
            this.tabPageDisplay.Controls.Add(this.buttonWindowed);
            this.tabPageDisplay.Controls.Add(this.labelFullscreenResolution);
            this.tabPageDisplay.Controls.Add(this.buttonFullscreen);
            this.tabPageDisplay.Controls.Add(this.comboBoxResolutions);
            this.tabPageDisplay.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisplay.Name = "tabPageDisplay";
            this.tabPageDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisplay.Size = new System.Drawing.Size(292, 246);
            this.tabPageDisplay.TabIndex = 0;
            this.tabPageDisplay.Text = "Display";
            this.tabPageDisplay.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            this.Name = "SettingsWindow";
            this.Size = new System.Drawing.Size(300, 300);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageDisplay.ResumeLayout(false);
            this.tabPageDisplay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPageDisplay;
        private System.Windows.Forms.Button buttonWindowed;
        private System.Windows.Forms.Label labelFullscreenResolution;
        private System.Windows.Forms.Button buttonFullscreen;
        private System.Windows.Forms.ComboBox comboBoxResolutions;
    }
}
