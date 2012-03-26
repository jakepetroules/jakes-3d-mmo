namespace MMO3D.Client
{
    partial class ClientWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientWindow));
            this.renderWindow = new MMO3D.Client.GameWindow();
            this.SuspendLayout();
            // 
            // renderWindow
            // 
            this.renderWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderWindow.Location = new System.Drawing.Point(0, 0);
            this.renderWindow.Name = "renderWindow";
            this.renderWindow.ShouldDraw = false;
            this.renderWindow.Size = new System.Drawing.Size(792, 566);
            this.renderWindow.TabIndex = 0;
            // 
            // ClientWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.renderWindow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ClientWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MMO3D Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientWindow_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private GameWindow renderWindow;
    }
}