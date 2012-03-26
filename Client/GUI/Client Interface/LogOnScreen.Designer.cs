namespace MMO3D.Client
{
    partial class LogOnWindow
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogOnWindow));
            this.groupBoxLogin = new System.Windows.Forms.GroupBox();
            this.linkLabelRefreshServerList = new System.Windows.Forms.LinkLabel();
            this.labelConnectionStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.maskedTextBoxPassword = new System.Windows.Forms.MaskedTextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.dataGridViewServers = new System.Windows.Forms.DataGridView();
            this.ColumnFlag = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPlayers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnServer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnServerAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noFocusButtonExit = new MMO3D.Client.NoFocusButton();
            this.noFocusButtonLogin = new MMO3D.Client.NoFocusButton();
            this.panel = new System.Windows.Forms.Panel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.imageListFlags = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxLogin.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewServers)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxLogin
            // 
            this.groupBoxLogin.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxLogin.Controls.Add(this.linkLabelRefreshServerList);
            this.groupBoxLogin.Controls.Add(this.labelConnectionStatus);
            this.groupBoxLogin.Controls.Add(this.tableLayoutPanel);
            this.groupBoxLogin.Controls.Add(this.dataGridViewServers);
            this.groupBoxLogin.Controls.Add(this.noFocusButtonExit);
            this.groupBoxLogin.Controls.Add(this.noFocusButtonLogin);
            this.groupBoxLogin.ForeColor = System.Drawing.Color.White;
            this.groupBoxLogin.Location = new System.Drawing.Point(17, 259);
            this.groupBoxLogin.Name = "groupBoxLogin";
            this.groupBoxLogin.Size = new System.Drawing.Size(478, 247);
            this.groupBoxLogin.TabIndex = 0;
            this.groupBoxLogin.TabStop = false;
            this.groupBoxLogin.Text = "Login";
            // 
            // linkLabelRefreshServerList
            // 
            this.linkLabelRefreshServerList.AutoSize = true;
            this.linkLabelRefreshServerList.LinkColor = System.Drawing.Color.White;
            this.linkLabelRefreshServerList.Location = new System.Drawing.Point(6, 194);
            this.linkLabelRefreshServerList.Name = "linkLabelRefreshServerList";
            this.linkLabelRefreshServerList.Size = new System.Drawing.Size(91, 13);
            this.linkLabelRefreshServerList.TabIndex = 2;
            this.linkLabelRefreshServerList.TabStop = true;
            this.linkLabelRefreshServerList.Text = "Refresh server list";
            this.linkLabelRefreshServerList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelRefreshServerList_LinkClicked);
            // 
            // labelConnectionStatus
            // 
            this.labelConnectionStatus.Location = new System.Drawing.Point(6, 227);
            this.labelConnectionStatus.Name = "labelConnectionStatus";
            this.labelConnectionStatus.Size = new System.Drawing.Size(466, 13);
            this.labelConnectionStatus.TabIndex = 5;
            this.labelConnectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.textBoxUsername, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.maskedTextBoxPassword, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelUsername, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelPassword, 0, 1);
            this.tableLayoutPanel.Location = new System.Drawing.Point(114, 19);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(250, 46);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUsername.Location = new System.Drawing.Point(67, 3);
            this.textBoxUsername.MaxLength = 24;
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(180, 20);
            this.textBoxUsername.TabIndex = 1;
            // 
            // maskedTextBoxPassword
            // 
            this.maskedTextBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBoxPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maskedTextBoxPassword.Location = new System.Drawing.Point(67, 26);
            this.maskedTextBoxPassword.Name = "maskedTextBoxPassword";
            this.maskedTextBoxPassword.PasswordChar = '*';
            this.maskedTextBoxPassword.Size = new System.Drawing.Size(180, 20);
            this.maskedTextBoxPassword.TabIndex = 3;
            // 
            // labelUsername
            // 
            this.labelUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelUsername.ForeColor = System.Drawing.Color.White;
            this.labelUsername.Location = new System.Drawing.Point(3, 0);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(58, 23);
            this.labelUsername.TabIndex = 0;
            this.labelUsername.Text = "Username:";
            this.labelUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPassword
            // 
            this.labelPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPassword.ForeColor = System.Drawing.Color.White;
            this.labelPassword.Location = new System.Drawing.Point(3, 23);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(58, 23);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Password:";
            this.labelPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridViewServers
            // 
            this.dataGridViewServers.AllowUserToAddRows = false;
            this.dataGridViewServers.AllowUserToDeleteRows = false;
            this.dataGridViewServers.AllowUserToResizeColumns = false;
            this.dataGridViewServers.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewServers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewServers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewServers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFlag,
            this.ColumnLocation,
            this.ColumnPlayers,
            this.ColumnServer,
            this.ColumnStatus,
            this.ColumnServerAddress});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewServers.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewServers.Location = new System.Drawing.Point(6, 71);
            this.dataGridViewServers.MultiSelect = false;
            this.dataGridViewServers.Name = "dataGridViewServers";
            this.dataGridViewServers.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewServers.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewServers.RowHeadersVisible = false;
            this.dataGridViewServers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewServers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewServers.ShowCellErrors = false;
            this.dataGridViewServers.ShowEditingIcon = false;
            this.dataGridViewServers.ShowRowErrors = false;
            this.dataGridViewServers.Size = new System.Drawing.Size(466, 120);
            this.dataGridViewServers.TabIndex = 1;
            // 
            // ColumnFlag
            // 
            this.ColumnFlag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnFlag.HeaderText = "";
            this.ColumnFlag.Name = "ColumnFlag";
            this.ColumnFlag.ReadOnly = true;
            this.ColumnFlag.Width = 5;
            // 
            // ColumnLocation
            // 
            this.ColumnLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnLocation.HeaderText = "Location";
            this.ColumnLocation.Name = "ColumnLocation";
            this.ColumnLocation.ReadOnly = true;
            this.ColumnLocation.Width = 73;
            // 
            // ColumnPlayers
            // 
            this.ColumnPlayers.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnPlayers.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnPlayers.HeaderText = "Players";
            this.ColumnPlayers.Name = "ColumnPlayers";
            this.ColumnPlayers.ReadOnly = true;
            this.ColumnPlayers.Width = 66;
            // 
            // ColumnServer
            // 
            this.ColumnServer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnServer.HeaderText = "Server";
            this.ColumnServer.Name = "ColumnServer";
            this.ColumnServer.ReadOnly = true;
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnStatus.HeaderText = "Status";
            this.ColumnStatus.Name = "ColumnStatus";
            this.ColumnStatus.ReadOnly = true;
            this.ColumnStatus.Width = 62;
            // 
            // ColumnServerAddress
            // 
            this.ColumnServerAddress.HeaderText = "Server Address";
            this.ColumnServerAddress.Name = "ColumnServerAddress";
            this.ColumnServerAddress.ReadOnly = true;
            this.ColumnServerAddress.Visible = false;
            // 
            // noFocusButtonExit
            // 
            this.noFocusButtonExit.ForeColor = System.Drawing.Color.Black;
            this.noFocusButtonExit.Location = new System.Drawing.Point(242, 197);
            this.noFocusButtonExit.Name = "noFocusButtonExit";
            this.noFocusButtonExit.Size = new System.Drawing.Size(75, 23);
            this.noFocusButtonExit.TabIndex = 4;
            this.noFocusButtonExit.Text = "Exit";
            this.noFocusButtonExit.UseVisualStyleBackColor = true;
            this.noFocusButtonExit.Click += new System.EventHandler(this.NoFocusButtonExit_Click);
            // 
            // noFocusButtonLogin
            // 
            this.noFocusButtonLogin.ForeColor = System.Drawing.Color.Black;
            this.noFocusButtonLogin.Location = new System.Drawing.Point(161, 197);
            this.noFocusButtonLogin.Name = "noFocusButtonLogin";
            this.noFocusButtonLogin.Size = new System.Drawing.Size(75, 23);
            this.noFocusButtonLogin.TabIndex = 3;
            this.noFocusButtonLogin.Text = "Login";
            this.noFocusButtonLogin.UseVisualStyleBackColor = true;
            this.noFocusButtonLogin.Click += new System.EventHandler(this.NoFocusButtonLogin_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.Controls.Add(this.pictureBoxLogo);
            this.panel.Controls.Add(this.groupBoxLogin);
            this.panel.Location = new System.Drawing.Point(144, 46);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(513, 509);
            this.panel.TabIndex = 0;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::MMO3D.Client.Resources.MainLogo;
            this.pictureBoxLogo.Location = new System.Drawing.Point(131, 3);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(250, 250);
            this.pictureBoxLogo.TabIndex = 1;
            this.pictureBoxLogo.TabStop = false;
            // 
            // imageListFlags
            // 
            this.imageListFlags.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFlags.ImageStream")));
            this.imageListFlags.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListFlags.Images.SetKeyName(0, "us.png");
            // 
            // LogOnWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MMO3D.Client.Resources.LoginBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.panel);
            this.Name = "LogOnWindow";
            this.Size = new System.Drawing.Size(800, 600);
            this.Resize += new System.EventHandler(this.LoginScreen_Resize);
            this.groupBoxLogin.ResumeLayout(false);
            this.groupBoxLogin.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewServers)).EndInit();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLogin;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUsername;
        private NoFocusButton noFocusButtonLogin;
        private NoFocusButton noFocusButtonExit;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.DataGridView dataGridViewServers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelConnectionStatus;
        private System.Windows.Forms.ImageList imageListFlags;
        private System.Windows.Forms.DataGridViewImageColumn ColumnFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPlayers;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnServer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnServerAddress;
        private System.Windows.Forms.LinkLabel linkLabelRefreshServerList;
    }
}
