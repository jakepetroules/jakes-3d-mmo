namespace MMO3D.Client
{
    partial class MessageAreaPanel
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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxMessageHistory = new System.Windows.Forms.RichTextBox();
            this.richTextBoxMessageBox = new System.Windows.Forms.RichTextBox();
            this.buttonSendMessage = new MMO3D.Client.NoFocusButton();
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
            this.splitContainer.Panel2.Controls.Add(this.buttonSendMessage);
            this.splitContainer.Panel2.Controls.Add(this.richTextBoxMessageBox);
            this.splitContainer.Panel2.Controls.Add(this.richTextBoxMessageHistory);
            this.splitContainer.Size = new System.Drawing.Size(422, 125);
            // 
            // richTextBoxMessageHistory
            // 
            this.richTextBoxMessageHistory.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxMessageHistory.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxMessageHistory.Name = "richTextBoxMessageHistory";
            this.richTextBoxMessageHistory.ReadOnly = true;
            this.richTextBoxMessageHistory.Size = new System.Drawing.Size(420, 100);
            this.richTextBoxMessageHistory.TabIndex = 0;
            this.richTextBoxMessageHistory.Text = "";
            this.richTextBoxMessageHistory.TextChanged += new System.EventHandler(this.RichTextBox_TextChanged);
            // 
            // richTextBoxMessageBox
            // 
            this.richTextBoxMessageBox.Location = new System.Drawing.Point(0, 100);
            this.richTextBoxMessageBox.Name = "richTextBoxMessageBox";
            this.richTextBoxMessageBox.Size = new System.Drawing.Size(345, 23);
            this.richTextBoxMessageBox.TabIndex = 1;
            this.richTextBoxMessageBox.Text = "";
            // 
            // buttonSendMessage
            // 
            this.buttonSendMessage.Location = new System.Drawing.Point(346, 101);
            this.buttonSendMessage.Name = "buttonSendMessage";
            this.buttonSendMessage.Size = new System.Drawing.Size(75, 23);
            this.buttonSendMessage.TabIndex = 2;
            this.buttonSendMessage.Text = "Send";
            this.buttonSendMessage.UseVisualStyleBackColor = true;
            this.buttonSendMessage.Click += new System.EventHandler(this.ButtonSendMessage_Click);
            // 
            // MessageAreaPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MessageAreaPanel";
            this.ShowTitleBar = false;
            this.Size = new System.Drawing.Size(422, 125);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxMessageHistory;
        private NoFocusButton buttonSendMessage;
        private System.Windows.Forms.RichTextBox richTextBoxMessageBox;
    }
}
