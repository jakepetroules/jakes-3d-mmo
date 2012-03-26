namespace MMO3D.Server
{
    public partial class ServerGui
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            if (base.InvokeRequired)
            {
                base.Invoke((System.Action<bool>)((bool b) => this.DisposeHelper(b)), disposing);
            }
            else
            {
                base.Dispose(disposing);
            }

            System.Windows.Forms.Application.ExitThread();
        }

        /// <summary>
        /// Calls base.Dispose to prevent an unverifiable code warning with the lambda expression in Dispose.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        private void DisposeHelper(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerGui));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.richTextBoxMessageLog = new System.Windows.Forms.RichTextBox();
            this.buttonShutDown = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.labelPlayers, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.richTextBoxMessageLog, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.buttonShutDown, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(792, 566);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(3, 0);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(107, 13);
            this.labelPlayers.TabIndex = 0;
            this.labelPlayers.Text = "Players connected: 0";
            // 
            // richTextBoxMessageLog
            // 
            this.richTextBoxMessageLog.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel.SetColumnSpan(this.richTextBoxMessageLog, 3);
            this.richTextBoxMessageLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMessageLog.Location = new System.Drawing.Point(3, 354);
            this.richTextBoxMessageLog.Name = "richTextBoxMessageLog";
            this.richTextBoxMessageLog.ReadOnly = true;
            this.richTextBoxMessageLog.Size = new System.Drawing.Size(786, 209);
            this.richTextBoxMessageLog.TabIndex = 2;
            this.richTextBoxMessageLog.Text = "";
            // 
            // buttonShutDown
            // 
            this.buttonShutDown.Location = new System.Drawing.Point(321, 325);
            this.buttonShutDown.Name = "buttonShutDown";
            this.buttonShutDown.Size = new System.Drawing.Size(150, 23);
            this.buttonShutDown.TabIndex = 1;
            this.buttonShutDown.Text = "Shut down server";
            this.buttonShutDown.UseVisualStyleBackColor = true;
            this.buttonShutDown.Click += new System.EventHandler(this.ButtonShutDown_Click);
            // 
            // ServerGui
            // 
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerGui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MMO3D Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerGui_FormClosing);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.RichTextBox richTextBoxMessageLog;
        private System.Windows.Forms.Button buttonShutDown;
    }
}
