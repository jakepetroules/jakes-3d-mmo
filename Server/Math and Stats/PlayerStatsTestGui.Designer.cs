namespace MMO3D.PlayerStatsTest
{
    partial class PlayerStatsTestGui
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
            this.propertyGridPlayerStats = new System.Windows.Forms.PropertyGrid();
            this.labelEditPlayerStats = new System.Windows.Forms.Label();
            this.propertyGridCharacterClass = new System.Windows.Forms.PropertyGrid();
            this.labelEditCharacterClass = new System.Windows.Forms.Label();
            this.buttonIncreaseLevel = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // propertyGridPlayerStats
            // 
            this.propertyGridPlayerStats.Location = new System.Drawing.Point(12, 25);
            this.propertyGridPlayerStats.Name = "propertyGridPlayerStats";
            this.propertyGridPlayerStats.Size = new System.Drawing.Size(382, 732);
            this.propertyGridPlayerStats.TabIndex = 0;
            this.propertyGridPlayerStats.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridCharacterClass_PropertyValueChanged);
            // 
            // labelEditPlayerStats
            // 
            this.labelEditPlayerStats.Location = new System.Drawing.Point(12, 9);
            this.labelEditPlayerStats.Name = "labelEditPlayerStats";
            this.labelEditPlayerStats.Size = new System.Drawing.Size(382, 13);
            this.labelEditPlayerStats.TabIndex = 1;
            this.labelEditPlayerStats.Text = "Edit the player stats here";
            this.labelEditPlayerStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // propertyGridCharacterClass
            // 
            this.propertyGridCharacterClass.Location = new System.Drawing.Point(400, 25);
            this.propertyGridCharacterClass.Name = "propertyGridCharacterClass";
            this.propertyGridCharacterClass.Size = new System.Drawing.Size(382, 761);
            this.propertyGridCharacterClass.TabIndex = 2;
            this.propertyGridCharacterClass.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridCharacterClass_PropertyValueChanged);
            // 
            // labelEditCharacterClass
            // 
            this.labelEditCharacterClass.Location = new System.Drawing.Point(400, 9);
            this.labelEditCharacterClass.Name = "labelEditCharacterClass";
            this.labelEditCharacterClass.Size = new System.Drawing.Size(382, 13);
            this.labelEditCharacterClass.TabIndex = 3;
            this.labelEditCharacterClass.Text = "Edit the character class here";
            this.labelEditCharacterClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonIncreaseLevel
            // 
            this.buttonIncreaseLevel.Location = new System.Drawing.Point(244, 763);
            this.buttonIncreaseLevel.Name = "buttonIncreaseLevel";
            this.buttonIncreaseLevel.Size = new System.Drawing.Size(150, 23);
            this.buttonIncreaseLevel.TabIndex = 4;
            this.buttonIncreaseLevel.Text = "Increase level";
            this.buttonIncreaseLevel.UseVisualStyleBackColor = true;
            this.buttonIncreaseLevel.Click += new System.EventHandler(this.ButtonIncreaseLevel_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(12, 763);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(150, 23);
            this.buttonReset.TabIndex = 5;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // PlayerStatsTestGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 798);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonIncreaseLevel);
            this.Controls.Add(this.labelEditCharacterClass);
            this.Controls.Add(this.propertyGridCharacterClass);
            this.Controls.Add(this.labelEditPlayerStats);
            this.Controls.Add(this.propertyGridPlayerStats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PlayerStatsTestGui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Player Stats Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGridPlayerStats;
        private System.Windows.Forms.Label labelEditPlayerStats;
        private System.Windows.Forms.PropertyGrid propertyGridCharacterClass;
        private System.Windows.Forms.Label labelEditCharacterClass;
        private System.Windows.Forms.Button buttonIncreaseLevel;
        private System.Windows.Forms.Button buttonReset;
    }
}

