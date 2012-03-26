namespace MMO3D.WorldEditor
{
    partial class ModelBuilderDialog
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
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }

                this.contentManager.Dispose();
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
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.buttonBrowseFiles = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonBrowseDirectory = new System.Windows.Forms.Button();
            this.buttonBuildModels = new System.Windows.Forms.Button();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.comboBoxErrorAction = new System.Windows.Forms.ComboBox();
            this.comboBoxOverwriteAction = new System.Windows.Forms.ComboBox();
            this.labelErrorAction = new System.Windows.Forms.Label();
            this.labelOverwriteAction = new System.Windows.Forms.Label();
            this.buttonBrowseDestination = new System.Windows.Forms.Button();
            this.textBoxDestination = new System.Windows.Forms.TextBox();
            this.labelDestination = new System.Windows.Forms.Label();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.modelViewerControl = new MMO3D.WorldEditor.ModelViewerControl();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.HorizontalScrollbar = true;
            this.listBoxFiles.Location = new System.Drawing.Point(12, 12);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxFiles.Size = new System.Drawing.Size(452, 329);
            this.listBoxFiles.TabIndex = 0;
            this.listBoxFiles.SelectedIndexChanged += new System.EventHandler(this.ListBoxFiles_SelectedIndexChanged);
            // 
            // buttonBrowseFiles
            // 
            this.buttonBrowseFiles.Location = new System.Drawing.Point(470, 12);
            this.buttonBrowseFiles.Name = "buttonBrowseFiles";
            this.buttonBrowseFiles.Size = new System.Drawing.Size(100, 23);
            this.buttonBrowseFiles.TabIndex = 1;
            this.buttonBrowseFiles.Text = "Browse files...";
            this.buttonBrowseFiles.UseVisualStyleBackColor = true;
            this.buttonBrowseFiles.Click += new System.EventHandler(this.ButtonBrowseFiles_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(682, 12);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(100, 23);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.ButtonRemove_Click);
            // 
            // buttonBrowseDirectory
            // 
            this.buttonBrowseDirectory.Location = new System.Drawing.Point(576, 12);
            this.buttonBrowseDirectory.Name = "buttonBrowseDirectory";
            this.buttonBrowseDirectory.Size = new System.Drawing.Size(100, 23);
            this.buttonBrowseDirectory.TabIndex = 2;
            this.buttonBrowseDirectory.Text = "Browse folders...";
            this.buttonBrowseDirectory.UseVisualStyleBackColor = true;
            this.buttonBrowseDirectory.Click += new System.EventHandler(this.ButtonBrowseDirectory_Click);
            // 
            // buttonBuildModels
            // 
            this.buttonBuildModels.Location = new System.Drawing.Point(337, 452);
            this.buttonBuildModels.Name = "buttonBuildModels";
            this.buttonBuildModels.Size = new System.Drawing.Size(120, 23);
            this.buttonBuildModels.TabIndex = 6;
            this.buttonBuildModels.Text = "Build Models";
            this.buttonBuildModels.UseVisualStyleBackColor = true;
            this.buttonBuildModels.Click += new System.EventHandler(this.ButtonBuildModels_Click);
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.comboBoxErrorAction);
            this.groupBoxOptions.Controls.Add(this.comboBoxOverwriteAction);
            this.groupBoxOptions.Controls.Add(this.labelErrorAction);
            this.groupBoxOptions.Controls.Add(this.labelOverwriteAction);
            this.groupBoxOptions.Controls.Add(this.buttonBrowseDestination);
            this.groupBoxOptions.Controls.Add(this.textBoxDestination);
            this.groupBoxOptions.Controls.Add(this.labelDestination);
            this.groupBoxOptions.Location = new System.Drawing.Point(12, 347);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(770, 99);
            this.groupBoxOptions.TabIndex = 5;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // comboBoxErrorAction
            // 
            this.comboBoxErrorAction.FormattingEnabled = true;
            this.comboBoxErrorAction.Location = new System.Drawing.Point(118, 72);
            this.comboBoxErrorAction.Name = "comboBoxErrorAction";
            this.comboBoxErrorAction.Size = new System.Drawing.Size(121, 21);
            this.comboBoxErrorAction.TabIndex = 6;
            // 
            // comboBoxOverwriteAction
            // 
            this.comboBoxOverwriteAction.FormattingEnabled = true;
            this.comboBoxOverwriteAction.Location = new System.Drawing.Point(118, 45);
            this.comboBoxOverwriteAction.Name = "comboBoxOverwriteAction";
            this.comboBoxOverwriteAction.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOverwriteAction.TabIndex = 4;
            // 
            // labelErrorAction
            // 
            this.labelErrorAction.AutoSize = true;
            this.labelErrorAction.Location = new System.Drawing.Point(6, 75);
            this.labelErrorAction.Name = "labelErrorAction";
            this.labelErrorAction.Size = new System.Drawing.Size(64, 13);
            this.labelErrorAction.TabIndex = 5;
            this.labelErrorAction.Text = "Error action:";
            // 
            // labelOverwriteAction
            // 
            this.labelOverwriteAction.AutoSize = true;
            this.labelOverwriteAction.Location = new System.Drawing.Point(6, 48);
            this.labelOverwriteAction.Name = "labelOverwriteAction";
            this.labelOverwriteAction.Size = new System.Drawing.Size(87, 13);
            this.labelOverwriteAction.TabIndex = 3;
            this.labelOverwriteAction.Text = "Overwrite action:";
            // 
            // buttonBrowseDestination
            // 
            this.buttonBrowseDestination.Location = new System.Drawing.Point(689, 17);
            this.buttonBrowseDestination.Name = "buttonBrowseDestination";
            this.buttonBrowseDestination.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseDestination.TabIndex = 2;
            this.buttonBrowseDestination.Text = "Browse...";
            this.buttonBrowseDestination.UseVisualStyleBackColor = true;
            this.buttonBrowseDestination.Click += new System.EventHandler(this.ButtonBrowseDestination_Click);
            // 
            // textBoxDestination
            // 
            this.textBoxDestination.Location = new System.Drawing.Point(118, 19);
            this.textBoxDestination.Name = "textBoxDestination";
            this.textBoxDestination.Size = new System.Drawing.Size(565, 20);
            this.textBoxDestination.TabIndex = 1;
            // 
            // labelDestination
            // 
            this.labelDestination.AutoSize = true;
            this.labelDestination.Location = new System.Drawing.Point(6, 22);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(106, 13);
            this.labelDestination.TabIndex = 0;
            this.labelDestination.Text = "Destination directory:";
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Location = new System.Drawing.Point(12, 481);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(770, 75);
            this.richTextBoxOutput.TabIndex = 7;
            this.richTextBoxOutput.Text = "";
            // 
            // modelViewerControl
            // 
            this.modelViewerControl.Location = new System.Drawing.Point(470, 41);
            this.modelViewerControl.Model = null;
            this.modelViewerControl.Name = "modelViewerControl";
            this.modelViewerControl.Size = new System.Drawing.Size(312, 300);
            this.modelViewerControl.TabIndex = 4;
            this.modelViewerControl.Text = "Model Viewer Control";
            // 
            // ModelBuilderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 568);
            this.Controls.Add(this.modelViewerControl);
            this.Controls.Add(this.richTextBoxOutput);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.buttonBuildModels);
            this.Controls.Add(this.buttonBrowseDirectory);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonBrowseFiles);
            this.Controls.Add(this.listBoxFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelBuilderDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Model Builder";
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Button buttonBrowseFiles;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonBrowseDirectory;
        private System.Windows.Forms.Button buttonBuildModels;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.Label labelOverwriteAction;
        private System.Windows.Forms.Button buttonBrowseDestination;
        private System.Windows.Forms.TextBox textBoxDestination;
        private System.Windows.Forms.Label labelDestination;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.Label labelErrorAction;
        private System.Windows.Forms.ComboBox comboBoxErrorAction;
        private System.Windows.Forms.ComboBox comboBoxOverwriteAction;
        private ModelViewerControl modelViewerControl;

    }
}