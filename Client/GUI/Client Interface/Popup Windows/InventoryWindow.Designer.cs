namespace MMO3D.Client
{
    partial class InventoryWindow
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
            this.buttonWeaponArm = new System.Windows.Forms.PictureBox();
            this.contextMenuStripEquipment = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.unequipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonShieldArm = new System.Windows.Forms.PictureBox();
            this.buttonArmor = new System.Windows.Forms.PictureBox();
            this.buttonBelt = new System.Windows.Forms.PictureBox();
            this.buttonAccessory1 = new System.Windows.Forms.PictureBox();
            this.buttonAccessory2 = new System.Windows.Forms.PictureBox();
            this.buttonInventory1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStripItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.equipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonInventory2 = new System.Windows.Forms.PictureBox();
            this.buttonInventory3 = new System.Windows.Forms.PictureBox();
            this.buttonInventory4 = new System.Windows.Forms.PictureBox();
            this.richTextBoxItemDescription = new System.Windows.Forms.RichTextBox();
            this.labelWeaponArm = new System.Windows.Forms.Label();
            this.labelShieldArm = new System.Windows.Forms.Label();
            this.labelArmor = new System.Windows.Forms.Label();
            this.labelBelt = new System.Windows.Forms.Label();
            this.labelAccessory1 = new System.Windows.Forms.Label();
            this.labelAccessory2 = new System.Windows.Forms.Label();
            this.tableLayoutPanelInventory = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelSlots = new System.Windows.Forms.TableLayoutPanel();
            this.buttonInventory8 = new System.Windows.Forms.PictureBox();
            this.buttonInventory5 = new System.Windows.Forms.PictureBox();
            this.buttonInventory7 = new System.Windows.Forms.PictureBox();
            this.buttonInventory6 = new System.Windows.Forms.PictureBox();
            this.pictureBoxDivider = new System.Windows.Forms.PictureBox();
            this.contextMenuStripAccessory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.equip1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.equip2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discardAccToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonWeaponArm)).BeginInit();
            this.contextMenuStripEquipment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonShieldArm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonArmor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonBelt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonAccessory1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonAccessory2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory1)).BeginInit();
            this.contextMenuStripItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory4)).BeginInit();
            this.tableLayoutPanelInventory.SuspendLayout();
            this.tableLayoutPanelSlots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDivider)).BeginInit();
            this.contextMenuStripAccessory.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanelInventory);
            this.splitContainer.Size = new System.Drawing.Size(400, 400);
            // 
            // buttonWeaponArm
            // 
            this.buttonWeaponArm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonWeaponArm.ContextMenuStrip = this.contextMenuStripEquipment;
            this.buttonWeaponArm.Location = new System.Drawing.Point(3, 3);
            this.buttonWeaponArm.Name = "buttonWeaponArm";
            this.buttonWeaponArm.Size = new System.Drawing.Size(40, 40);
            this.buttonWeaponArm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonWeaponArm.TabIndex = 0;
            this.buttonWeaponArm.TabStop = false;
            this.buttonWeaponArm.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonWeaponArm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EquipmentButtons_MouseDown);
            this.buttonWeaponArm.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // contextMenuStripEquipment
            // 
            this.contextMenuStripEquipment.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unequipToolStripMenuItem});
            this.contextMenuStripEquipment.Name = "contextMenuStripEquipment";
            this.contextMenuStripEquipment.ShowImageMargin = false;
            this.contextMenuStripEquipment.ShowItemToolTips = false;
            this.contextMenuStripEquipment.Size = new System.Drawing.Size(100, 26);
            // 
            // unequipToolStripMenuItem
            // 
            this.unequipToolStripMenuItem.Name = "unequipToolStripMenuItem";
            this.unequipToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.unequipToolStripMenuItem.Text = "Unequip";
            this.unequipToolStripMenuItem.Click += new System.EventHandler(this.UnequipToolStripMenuItem_Click);
            // 
            // buttonShieldArm
            // 
            this.buttonShieldArm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonShieldArm.ContextMenuStrip = this.contextMenuStripEquipment;
            this.buttonShieldArm.Location = new System.Drawing.Point(3, 49);
            this.buttonShieldArm.Name = "buttonShieldArm";
            this.buttonShieldArm.Size = new System.Drawing.Size(40, 40);
            this.buttonShieldArm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonShieldArm.TabIndex = 1;
            this.buttonShieldArm.TabStop = false;
            this.buttonShieldArm.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonShieldArm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EquipmentButtons_MouseDown);
            this.buttonShieldArm.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // buttonArmor
            // 
            this.buttonArmor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonArmor.ContextMenuStrip = this.contextMenuStripEquipment;
            this.buttonArmor.Location = new System.Drawing.Point(3, 95);
            this.buttonArmor.Name = "buttonArmor";
            this.buttonArmor.Size = new System.Drawing.Size(40, 40);
            this.buttonArmor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonArmor.TabIndex = 2;
            this.buttonArmor.TabStop = false;
            this.buttonArmor.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonArmor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EquipmentButtons_MouseDown);
            this.buttonArmor.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // buttonBelt
            // 
            this.buttonBelt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonBelt.ContextMenuStrip = this.contextMenuStripEquipment;
            this.buttonBelt.Location = new System.Drawing.Point(203, 3);
            this.buttonBelt.Name = "buttonBelt";
            this.buttonBelt.Size = new System.Drawing.Size(40, 40);
            this.buttonBelt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonBelt.TabIndex = 3;
            this.buttonBelt.TabStop = false;
            this.buttonBelt.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonBelt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EquipmentButtons_MouseDown);
            this.buttonBelt.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // buttonAccessory1
            // 
            this.buttonAccessory1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonAccessory1.ContextMenuStrip = this.contextMenuStripEquipment;
            this.buttonAccessory1.Location = new System.Drawing.Point(203, 49);
            this.buttonAccessory1.Name = "buttonAccessory1";
            this.buttonAccessory1.Size = new System.Drawing.Size(40, 40);
            this.buttonAccessory1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonAccessory1.TabIndex = 4;
            this.buttonAccessory1.TabStop = false;
            this.buttonAccessory1.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonAccessory1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EquipmentButtons_MouseDown);
            this.buttonAccessory1.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // buttonAccessory2
            // 
            this.buttonAccessory2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonAccessory2.ContextMenuStrip = this.contextMenuStripEquipment;
            this.buttonAccessory2.Location = new System.Drawing.Point(203, 95);
            this.buttonAccessory2.Name = "buttonAccessory2";
            this.buttonAccessory2.Size = new System.Drawing.Size(40, 40);
            this.buttonAccessory2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonAccessory2.TabIndex = 5;
            this.buttonAccessory2.TabStop = false;
            this.buttonAccessory2.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonAccessory2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EquipmentButtons_MouseDown);
            this.buttonAccessory2.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // buttonInventory1
            // 
            this.buttonInventory1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonInventory1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonInventory1.ContextMenuStrip = this.contextMenuStripItem;
            this.buttonInventory1.Location = new System.Drawing.Point(29, 6);
            this.buttonInventory1.Name = "buttonInventory1";
            this.buttonInventory1.Size = new System.Drawing.Size(40, 40);
            this.buttonInventory1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonInventory1.TabIndex = 6;
            this.buttonInventory1.TabStop = false;
            this.buttonInventory1.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonInventory1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Buttons_MouseClick);
            this.buttonInventory1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InventoryButtons_MouseDown);
            this.buttonInventory1.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // contextMenuStripItem
            // 
            this.contextMenuStripItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.equipToolStripMenuItem,
            this.discardToolStripMenuItem});
            this.contextMenuStripItem.Name = "contextMenuStripItem";
            this.contextMenuStripItem.ShowImageMargin = false;
            this.contextMenuStripItem.ShowItemToolTips = false;
            this.contextMenuStripItem.Size = new System.Drawing.Size(96, 48);
            // 
            // equipToolStripMenuItem
            // 
            this.equipToolStripMenuItem.Name = "equipToolStripMenuItem";
            this.equipToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.equipToolStripMenuItem.Text = "Equip";
            this.equipToolStripMenuItem.Click += new System.EventHandler(this.EquipToolStripMenuItem_Click);
            // 
            // discardToolStripMenuItem
            // 
            this.discardToolStripMenuItem.Name = "discardToolStripMenuItem";
            this.discardToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.discardToolStripMenuItem.Text = "Discard";
            this.discardToolStripMenuItem.Click += new System.EventHandler(this.DiscardToolStripMenuItem_Click);
            // 
            // buttonInventory2
            // 
            this.buttonInventory2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonInventory2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonInventory2.ContextMenuStrip = this.contextMenuStripItem;
            this.buttonInventory2.Location = new System.Drawing.Point(127, 6);
            this.buttonInventory2.Name = "buttonInventory2";
            this.buttonInventory2.Size = new System.Drawing.Size(40, 40);
            this.buttonInventory2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonInventory2.TabIndex = 7;
            this.buttonInventory2.TabStop = false;
            this.buttonInventory2.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonInventory2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Buttons_MouseClick);
            this.buttonInventory2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InventoryButtons_MouseDown);
            this.buttonInventory2.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // buttonInventory3
            // 
            this.buttonInventory3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonInventory3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonInventory3.ContextMenuStrip = this.contextMenuStripItem;
            this.buttonInventory3.Location = new System.Drawing.Point(225, 6);
            this.buttonInventory3.Name = "buttonInventory3";
            this.buttonInventory3.Size = new System.Drawing.Size(40, 40);
            this.buttonInventory3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonInventory3.TabIndex = 8;
            this.buttonInventory3.TabStop = false;
            this.buttonInventory3.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonInventory3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Buttons_MouseClick);
            this.buttonInventory3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InventoryButtons_MouseDown);
            this.buttonInventory3.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // buttonInventory4
            // 
            this.buttonInventory4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonInventory4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonInventory4.ContextMenuStrip = this.contextMenuStripItem;
            this.buttonInventory4.Location = new System.Drawing.Point(324, 6);
            this.buttonInventory4.Name = "buttonInventory4";
            this.buttonInventory4.Size = new System.Drawing.Size(40, 40);
            this.buttonInventory4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonInventory4.TabIndex = 9;
            this.buttonInventory4.TabStop = false;
            this.buttonInventory4.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonInventory4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Buttons_MouseClick);
            this.buttonInventory4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InventoryButtons_MouseDown);
            this.buttonInventory4.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // richTextBoxItemDescription
            // 
            this.richTextBoxItemDescription.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanelInventory.SetColumnSpan(this.richTextBoxItemDescription, 4);
            this.richTextBoxItemDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxItemDescription.Location = new System.Drawing.Point(3, 284);
            this.richTextBoxItemDescription.Name = "richTextBoxItemDescription";
            this.richTextBoxItemDescription.ReadOnly = true;
            this.richTextBoxItemDescription.Size = new System.Drawing.Size(394, 85);
            this.richTextBoxItemDescription.TabIndex = 12;
            this.richTextBoxItemDescription.Text = "";
            // 
            // labelWeaponArm
            // 
            this.labelWeaponArm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWeaponArm.Location = new System.Drawing.Point(49, 0);
            this.labelWeaponArm.Name = "labelWeaponArm";
            this.labelWeaponArm.Size = new System.Drawing.Size(148, 46);
            this.labelWeaponArm.TabIndex = 15;
            this.labelWeaponArm.Tag = "(Weapon)";
            this.labelWeaponArm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelShieldArm
            // 
            this.labelShieldArm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShieldArm.Location = new System.Drawing.Point(49, 46);
            this.labelShieldArm.Name = "labelShieldArm";
            this.labelShieldArm.Size = new System.Drawing.Size(148, 46);
            this.labelShieldArm.TabIndex = 16;
            this.labelShieldArm.Tag = "(Shield)";
            this.labelShieldArm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelArmor
            // 
            this.labelArmor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelArmor.Location = new System.Drawing.Point(49, 92);
            this.labelArmor.Name = "labelArmor";
            this.labelArmor.Size = new System.Drawing.Size(148, 46);
            this.labelArmor.TabIndex = 17;
            this.labelArmor.Tag = "(Armor)";
            this.labelArmor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelBelt
            // 
            this.labelBelt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBelt.Location = new System.Drawing.Point(249, 0);
            this.labelBelt.Name = "labelBelt";
            this.labelBelt.Size = new System.Drawing.Size(148, 46);
            this.labelBelt.TabIndex = 18;
            this.labelBelt.Tag = "(Belt)";
            this.labelBelt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAccessory1
            // 
            this.labelAccessory1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAccessory1.Location = new System.Drawing.Point(249, 46);
            this.labelAccessory1.Name = "labelAccessory1";
            this.labelAccessory1.Size = new System.Drawing.Size(148, 46);
            this.labelAccessory1.TabIndex = 19;
            this.labelAccessory1.Tag = "(Accessory 1)";
            this.labelAccessory1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAccessory2
            // 
            this.labelAccessory2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAccessory2.Location = new System.Drawing.Point(249, 92);
            this.labelAccessory2.Name = "labelAccessory2";
            this.labelAccessory2.Size = new System.Drawing.Size(148, 46);
            this.labelAccessory2.TabIndex = 20;
            this.labelAccessory2.Tag = "(Accessory 2)";
            this.labelAccessory2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelInventory
            // 
            this.tableLayoutPanelInventory.ColumnCount = 4;
            this.tableLayoutPanelInventory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInventory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInventory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInventory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInventory.Controls.Add(this.tableLayoutPanelSlots, 0, 4);
            this.tableLayoutPanelInventory.Controls.Add(this.pictureBoxDivider, 0, 3);
            this.tableLayoutPanelInventory.Controls.Add(this.buttonWeaponArm, 0, 0);
            this.tableLayoutPanelInventory.Controls.Add(this.labelAccessory2, 3, 2);
            this.tableLayoutPanelInventory.Controls.Add(this.buttonShieldArm, 0, 1);
            this.tableLayoutPanelInventory.Controls.Add(this.labelAccessory1, 3, 1);
            this.tableLayoutPanelInventory.Controls.Add(this.buttonArmor, 0, 2);
            this.tableLayoutPanelInventory.Controls.Add(this.labelBelt, 3, 0);
            this.tableLayoutPanelInventory.Controls.Add(this.buttonBelt, 2, 0);
            this.tableLayoutPanelInventory.Controls.Add(this.labelArmor, 1, 2);
            this.tableLayoutPanelInventory.Controls.Add(this.buttonAccessory1, 2, 1);
            this.tableLayoutPanelInventory.Controls.Add(this.labelShieldArm, 1, 1);
            this.tableLayoutPanelInventory.Controls.Add(this.buttonAccessory2, 2, 2);
            this.tableLayoutPanelInventory.Controls.Add(this.labelWeaponArm, 1, 0);
            this.tableLayoutPanelInventory.Controls.Add(this.richTextBoxItemDescription, 0, 5);
            this.tableLayoutPanelInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelInventory.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelInventory.Name = "tableLayoutPanelInventory";
            this.tableLayoutPanelInventory.RowCount = 6;
            this.tableLayoutPanelInventory.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInventory.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInventory.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInventory.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInventory.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInventory.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInventory.Size = new System.Drawing.Size(400, 372);
            this.tableLayoutPanelInventory.TabIndex = 21;
            // 
            // tableLayoutPanelSlots
            // 
            this.tableLayoutPanelSlots.ColumnCount = 4;
            this.tableLayoutPanelInventory.SetColumnSpan(this.tableLayoutPanelSlots, 4);
            this.tableLayoutPanelSlots.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSlots.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSlots.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSlots.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSlots.Controls.Add(this.buttonInventory1, 0, 0);
            this.tableLayoutPanelSlots.Controls.Add(this.buttonInventory2, 1, 0);
            this.tableLayoutPanelSlots.Controls.Add(this.buttonInventory3, 2, 0);
            this.tableLayoutPanelSlots.Controls.Add(this.buttonInventory8, 3, 1);
            this.tableLayoutPanelSlots.Controls.Add(this.buttonInventory4, 3, 0);
            this.tableLayoutPanelSlots.Controls.Add(this.buttonInventory5, 0, 1);
            this.tableLayoutPanelSlots.Controls.Add(this.buttonInventory7, 2, 1);
            this.tableLayoutPanelSlots.Controls.Add(this.buttonInventory6, 1, 1);
            this.tableLayoutPanelSlots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSlots.Location = new System.Drawing.Point(3, 172);
            this.tableLayoutPanelSlots.Name = "tableLayoutPanelSlots";
            this.tableLayoutPanelSlots.RowCount = 2;
            this.tableLayoutPanelSlots.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSlots.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSlots.Size = new System.Drawing.Size(394, 106);
            this.tableLayoutPanelSlots.TabIndex = 22;
            // 
            // buttonInventory8
            // 
            this.buttonInventory8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonInventory8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonInventory8.ContextMenuStrip = this.contextMenuStripItem;
            this.buttonInventory8.Location = new System.Drawing.Point(324, 59);
            this.buttonInventory8.Name = "buttonInventory8";
            this.buttonInventory8.Size = new System.Drawing.Size(40, 40);
            this.buttonInventory8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonInventory8.TabIndex = 14;
            this.buttonInventory8.TabStop = false;
            this.buttonInventory8.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonInventory8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Buttons_MouseClick);
            this.buttonInventory8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InventoryButtons_MouseDown);
            this.buttonInventory8.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // buttonInventory5
            // 
            this.buttonInventory5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonInventory5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonInventory5.ContextMenuStrip = this.contextMenuStripItem;
            this.buttonInventory5.Location = new System.Drawing.Point(29, 59);
            this.buttonInventory5.Name = "buttonInventory5";
            this.buttonInventory5.Size = new System.Drawing.Size(40, 40);
            this.buttonInventory5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonInventory5.TabIndex = 10;
            this.buttonInventory5.TabStop = false;
            this.buttonInventory5.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonInventory5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Buttons_MouseClick);
            this.buttonInventory5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InventoryButtons_MouseDown);
            this.buttonInventory5.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // buttonInventory7
            // 
            this.buttonInventory7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonInventory7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonInventory7.ContextMenuStrip = this.contextMenuStripItem;
            this.buttonInventory7.Location = new System.Drawing.Point(225, 59);
            this.buttonInventory7.Name = "buttonInventory7";
            this.buttonInventory7.Size = new System.Drawing.Size(40, 40);
            this.buttonInventory7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonInventory7.TabIndex = 13;
            this.buttonInventory7.TabStop = false;
            this.buttonInventory7.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonInventory7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Buttons_MouseClick);
            this.buttonInventory7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InventoryButtons_MouseDown);
            this.buttonInventory7.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // buttonInventory6
            // 
            this.buttonInventory6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonInventory6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buttonInventory6.ContextMenuStrip = this.contextMenuStripItem;
            this.buttonInventory6.Location = new System.Drawing.Point(127, 59);
            this.buttonInventory6.Name = "buttonInventory6";
            this.buttonInventory6.Size = new System.Drawing.Size(40, 40);
            this.buttonInventory6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.buttonInventory6.TabIndex = 11;
            this.buttonInventory6.TabStop = false;
            this.buttonInventory6.MouseLeave += new System.EventHandler(this.Buttons_MouseLeave);
            this.buttonInventory6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Buttons_MouseClick);
            this.buttonInventory6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InventoryButtons_MouseDown);
            this.buttonInventory6.MouseEnter += new System.EventHandler(this.Buttons_MouseEnter);
            // 
            // pictureBoxDivider
            // 
            this.tableLayoutPanelInventory.SetColumnSpan(this.pictureBoxDivider, 4);
            this.pictureBoxDivider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxDivider.Location = new System.Drawing.Point(3, 141);
            this.pictureBoxDivider.Name = "pictureBoxDivider";
            this.pictureBoxDivider.Size = new System.Drawing.Size(394, 25);
            this.pictureBoxDivider.TabIndex = 22;
            this.pictureBoxDivider.TabStop = false;
            // 
            // contextMenuStripAccessory
            // 
            this.contextMenuStripAccessory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.equip1ToolStripMenuItem,
            this.equip2ToolStripMenuItem,
            this.discardAccToolStripMenuItem});
            this.contextMenuStripAccessory.Name = "contextMenuStripAccessory";
            this.contextMenuStripAccessory.ShowImageMargin = false;
            this.contextMenuStripAccessory.ShowItemToolTips = false;
            this.contextMenuStripAccessory.Size = new System.Drawing.Size(97, 70);
            // 
            // equip1ToolStripMenuItem
            // 
            this.equip1ToolStripMenuItem.Name = "equip1ToolStripMenuItem";
            this.equip1ToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.equip1ToolStripMenuItem.Text = "Equip-1";
            this.equip1ToolStripMenuItem.Click += new System.EventHandler(this.EquipToolStripMenuItem_Click);
            // 
            // equip2ToolStripMenuItem
            // 
            this.equip2ToolStripMenuItem.Name = "equip2ToolStripMenuItem";
            this.equip2ToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.equip2ToolStripMenuItem.Text = "Equip-2";
            this.equip2ToolStripMenuItem.Click += new System.EventHandler(this.EquipToolStripMenuItem_Click);
            // 
            // discardAccToolStripMenuItem
            // 
            this.discardAccToolStripMenuItem.Name = "discardAccToolStripMenuItem";
            this.discardAccToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.discardAccToolStripMenuItem.Text = "Discard";
            this.discardAccToolStripMenuItem.Click += new System.EventHandler(this.DiscardToolStripMenuItem_Click);
            // 
            // InventoryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "InventoryWindow";
            this.Size = new System.Drawing.Size(400, 400);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonWeaponArm)).EndInit();
            this.contextMenuStripEquipment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonShieldArm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonArmor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonBelt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonAccessory1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonAccessory2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory1)).EndInit();
            this.contextMenuStripItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory4)).EndInit();
            this.tableLayoutPanelInventory.ResumeLayout(false);
            this.tableLayoutPanelSlots.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonInventory6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDivider)).EndInit();
            this.contextMenuStripAccessory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox buttonAccessory2;
        private System.Windows.Forms.PictureBox buttonAccessory1;
        private System.Windows.Forms.PictureBox buttonBelt;
        private System.Windows.Forms.PictureBox buttonArmor;
        private System.Windows.Forms.PictureBox buttonShieldArm;
        private System.Windows.Forms.PictureBox buttonWeaponArm;
        private System.Windows.Forms.RichTextBox richTextBoxItemDescription;
        private System.Windows.Forms.PictureBox buttonInventory4;
        private System.Windows.Forms.PictureBox buttonInventory3;
        private System.Windows.Forms.PictureBox buttonInventory2;
        private System.Windows.Forms.PictureBox buttonInventory1;
        private System.Windows.Forms.Label labelBelt;
        private System.Windows.Forms.Label labelArmor;
        private System.Windows.Forms.Label labelShieldArm;
        private System.Windows.Forms.Label labelWeaponArm;
        private System.Windows.Forms.Label labelAccessory2;
        private System.Windows.Forms.Label labelAccessory1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInventory;
        private System.Windows.Forms.PictureBox pictureBoxDivider;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSlots;
        private System.Windows.Forms.PictureBox buttonInventory8;
        private System.Windows.Forms.PictureBox buttonInventory5;
        private System.Windows.Forms.PictureBox buttonInventory7;
        private System.Windows.Forms.PictureBox buttonInventory6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem equipToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEquipment;
        private System.Windows.Forms.ToolStripMenuItem unequipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discardToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAccessory;
        private System.Windows.Forms.ToolStripMenuItem equip1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem equip2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discardAccToolStripMenuItem;
    }
}
