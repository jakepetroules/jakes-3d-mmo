namespace MMO3D.Client
{
    public partial class ControlButtonsPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private NoFocusButton buttonInventory;
        private NoFocusButton buttonBelt;
        private NoFocusButton buttonArts;
        private NoFocusButton buttonCharacterDetails;
        private NoFocusButton buttonQuit;
        private NoFocusButton buttonSettings;
        private NoFocusButton buttonParty;
        private NoFocusButton buttonQuestJournal;

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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlButtonsPanel));
            this.buttonInventory = new MMO3D.Client.NoFocusButton();
            this.imageListButtonIcons = new System.Windows.Forms.ImageList(this.components);
            this.buttonBelt = new MMO3D.Client.NoFocusButton();
            this.buttonArts = new MMO3D.Client.NoFocusButton();
            this.buttonCharacterDetails = new MMO3D.Client.NoFocusButton();
            this.buttonQuit = new MMO3D.Client.NoFocusButton();
            this.buttonSettings = new MMO3D.Client.NoFocusButton();
            this.buttonParty = new MMO3D.Client.NoFocusButton();
            this.buttonQuestJournal = new MMO3D.Client.NoFocusButton();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Panel1Collapsed = true;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.buttonQuit);
            this.splitContainer.Panel2.Controls.Add(this.buttonSettings);
            this.splitContainer.Panel2.Controls.Add(this.buttonParty);
            this.splitContainer.Panel2.Controls.Add(this.buttonQuestJournal);
            this.splitContainer.Panel2.Controls.Add(this.buttonCharacterDetails);
            this.splitContainer.Panel2.Controls.Add(this.buttonArts);
            this.splitContainer.Panel2.Controls.Add(this.buttonBelt);
            this.splitContainer.Panel2.Controls.Add(this.buttonInventory);
            this.splitContainer.Size = new System.Drawing.Size(258, 34);
            // 
            // buttonInventory
            // 
            this.buttonInventory.ImageKey = "Inventory.png";
            this.buttonInventory.ImageList = this.imageListButtonIcons;
            this.buttonInventory.Location = new System.Drawing.Point(0, 0);
            this.buttonInventory.Name = "buttonInventory";
            this.buttonInventory.Size = new System.Drawing.Size(32, 32);
            this.buttonInventory.TabIndex = 0;
            this.buttonInventory.UseVisualStyleBackColor = true;
            // 
            // imageListButtonIcons
            // 
            this.imageListButtonIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListButtonIcons.ImageStream")));
            this.imageListButtonIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListButtonIcons.Images.SetKeyName(0, "Inventory.png");
            this.imageListButtonIcons.Images.SetKeyName(1, "Belt.png");
            this.imageListButtonIcons.Images.SetKeyName(2, "Arts.png");
            this.imageListButtonIcons.Images.SetKeyName(3, "CharacterDetails.png");
            this.imageListButtonIcons.Images.SetKeyName(4, "QuestJournal.png");
            this.imageListButtonIcons.Images.SetKeyName(5, "Party.png");
            this.imageListButtonIcons.Images.SetKeyName(6, "Settings.png");
            this.imageListButtonIcons.Images.SetKeyName(7, "Quit.png");
            // 
            // buttonBelt
            // 
            this.buttonBelt.ImageKey = "Belt.png";
            this.buttonBelt.ImageList = this.imageListButtonIcons;
            this.buttonBelt.Location = new System.Drawing.Point(32, 0);
            this.buttonBelt.Name = "buttonBelt";
            this.buttonBelt.Size = new System.Drawing.Size(32, 32);
            this.buttonBelt.TabIndex = 1;
            this.buttonBelt.UseVisualStyleBackColor = true;
            // 
            // buttonArts
            // 
            this.buttonArts.ImageKey = "Arts.png";
            this.buttonArts.ImageList = this.imageListButtonIcons;
            this.buttonArts.Location = new System.Drawing.Point(64, 0);
            this.buttonArts.Name = "buttonArts";
            this.buttonArts.Size = new System.Drawing.Size(32, 32);
            this.buttonArts.TabIndex = 2;
            this.buttonArts.UseVisualStyleBackColor = true;
            // 
            // buttonCharacterDetails
            // 
            this.buttonCharacterDetails.ImageKey = "CharacterDetails.png";
            this.buttonCharacterDetails.ImageList = this.imageListButtonIcons;
            this.buttonCharacterDetails.Location = new System.Drawing.Point(96, 0);
            this.buttonCharacterDetails.Name = "buttonCharacterDetails";
            this.buttonCharacterDetails.Size = new System.Drawing.Size(32, 32);
            this.buttonCharacterDetails.TabIndex = 3;
            this.buttonCharacterDetails.UseVisualStyleBackColor = true;
            // 
            // buttonQuit
            // 
            this.buttonQuit.ImageKey = "Quit.png";
            this.buttonQuit.ImageList = this.imageListButtonIcons;
            this.buttonQuit.Location = new System.Drawing.Point(224, 0);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(32, 32);
            this.buttonQuit.TabIndex = 7;
            this.buttonQuit.UseVisualStyleBackColor = true;
            // 
            // buttonSettings
            // 
            this.buttonSettings.ImageKey = "Settings.png";
            this.buttonSettings.ImageList = this.imageListButtonIcons;
            this.buttonSettings.Location = new System.Drawing.Point(192, 0);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(32, 32);
            this.buttonSettings.TabIndex = 6;
            this.buttonSettings.UseVisualStyleBackColor = true;
            // 
            // buttonParty
            // 
            this.buttonParty.ImageKey = "Party.png";
            this.buttonParty.ImageList = this.imageListButtonIcons;
            this.buttonParty.Location = new System.Drawing.Point(160, 0);
            this.buttonParty.Name = "buttonParty";
            this.buttonParty.Size = new System.Drawing.Size(32, 32);
            this.buttonParty.TabIndex = 5;
            this.buttonParty.UseVisualStyleBackColor = true;
            // 
            // buttonQuestJournal
            // 
            this.buttonQuestJournal.ImageKey = "QuestJournal.png";
            this.buttonQuestJournal.ImageList = this.imageListButtonIcons;
            this.buttonQuestJournal.Location = new System.Drawing.Point(128, 0);
            this.buttonQuestJournal.Name = "buttonQuestJournal";
            this.buttonQuestJournal.Size = new System.Drawing.Size(32, 32);
            this.buttonQuestJournal.TabIndex = 4;
            this.buttonQuestJournal.UseVisualStyleBackColor = true;
            // 
            // ControlButtonsPanel
            // 
            this.Name = "ControlButtonsPanel";
            this.ShowTitleBar = false;
            this.Size = new System.Drawing.Size(258, 34);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageListButtonIcons;
    }
}
