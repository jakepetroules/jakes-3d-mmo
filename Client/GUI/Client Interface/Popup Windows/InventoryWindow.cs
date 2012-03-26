namespace MMO3D.Client
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using Petroules.Synteza.Imaging;
    using Petroules.Synteza.Windows.Forms;
    using MMO3D.CommonCode;
    using MMO3D.Engine;
    using MMO3D.NetworkInterface;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// The inventory window of the MMO3D client GUI.
    /// </summary>
    public partial class InventoryWindow : Window
    {
        /// <summary>
        /// Control name prefix for the 8 inventory buttons.
        /// </summary>
        private static string inventoryButtonPrefix = "buttonInventory";

        /// <summary>
        /// Control name prefix for the 2 accessory buttons.
        /// </summary>
        private static string accessoryButtonPrefix = "buttonAccessory";

        /// <summary>
        /// The player inventory object that encapsulates the actual inventory data.
        /// </summary>
        private PlayerInventory inventory;

        /// <summary>
        /// Array of the equipment labels on the GUI.
        /// </summary>
        private Label[] equipmentLabels;

        /// <summary>
        /// Array of the equipment slots on the GUI.
        /// </summary>
        private PictureBox[] equipmentButtons;

        /// <summary>
        /// Array of the inventory slots on the GUI.
        /// </summary>
        private PictureBox[] inventoryButtons;

        /// <summary>
        /// Whether we are currently dragging an item.
        /// </summary>
        private bool movingItem = false;

        /// <summary>
        /// The picture box we started dragging from.
        /// </summary>
        private PictureBox picSource;

        /// <summary>
        /// The image in the picture box we started dragging from.
        /// </summary>
        private Image tempImage;

        /// <summary>
        /// Initializes a new instance of the InventoryWindow class.
        /// </summary>
        /// <param name="engine">The game engine associated with this instance.</param>
        /// <param name="network">The network connection manager used to connect to the network.</param>
        public InventoryWindow(GameEngine engine, NetworkClient network)
            : base(engine, network)
        {
            this.InitializeComponent();
            this.Text = "Inventory";

            this.equipmentLabels = new Label[]
            {
                this.labelWeaponArm,
                this.labelShieldArm,
                this.labelArmor,
                this.labelBelt,
                this.labelAccessory1,
                this.labelAccessory2
            };

            this.equipmentButtons = new PictureBox[]
            {
                this.buttonWeaponArm,
                this.buttonShieldArm,
                this.buttonArmor,
                this.buttonBelt,
                this.buttonAccessory1,
                this.buttonAccessory2
            };

            this.inventoryButtons = new PictureBox[]
            {
                this.buttonInventory1,
                this.buttonInventory2,
                this.buttonInventory3,
                this.buttonInventory4,
                this.buttonInventory5,
                this.buttonInventory6,
                this.buttonInventory7,
                this.buttonInventory8
            };
        }

        /// <summary>
        /// Gets or sets the player inventory object that encapsulates the actual inventory data.
        /// </summary>
        /// <value>See summary.</value>
        public PlayerInventory Inventory
        {
            get
            {
                return this.inventory;
            }

            set
            {
                this.inventory = value ?? new PlayerInventory();

                this.inventory.EquipmentChanged += new EventHandler(this.Inventory_Event);
                this.inventory.InventoryOrderChanged += new EventHandler(this.Inventory_Event);
                this.inventory.ItemsAdded += new EventHandler(this.Inventory_Event);
                this.inventory.ItemsRemoved += new EventHandler(this.Inventory_Event);

                this.RefreshGui();
            }
        }

        /// <summary>
        /// Event handler for the mouse entering the visible area of any button.
        /// Puts the description of the item the mouse is hovering over, in the text box.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Buttons_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb != null)
            {
                Item item = pb.Tag as Item;
                if (item != null)
                {
                    this.richTextBoxItemDescription.Text = string.Format(CultureInfo.CurrentCulture, "{0}\r\n{1}\r\n\r\n{2}", item.Name, item.ItemClass, item.Description);

                    this.richTextBoxItemDescription.Select(0, item.Name.Length);
                    this.richTextBoxItemDescription.SelectionFont = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
                }

                if (this.movingItem && pb.Name.StartsWith(InventoryWindow.inventoryButtonPrefix, StringComparison.Ordinal))
                {
                    pb.BorderStyle = BorderStyle.FixedSingle;
                }
            }
        }

        /// <summary>
        /// Event handler for the mouse leaving the visible area of any button.
        /// Clears the item description text box.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Buttons_MouseLeave(object sender, EventArgs e)
        {
            this.richTextBoxItemDescription.Clear();

            PictureBox pb = sender as PictureBox;
            if (pb != null)
            {
                pb.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        /// <summary>
        /// Event handler for the mouse being clicked down on an inventory slot.
        /// Stores a reference to the button that launched the right-click menu.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void InventoryButtons_MouseDown(object sender, MouseEventArgs e)
        {
            this.contextMenuStripItem.Tag = sender;
            this.contextMenuStripAccessory.Tag = sender;

            PictureBox pb = sender as PictureBox;
            if (pb != null)
            {
                Item i = pb.Tag as Item;
                if (i != null)
                {
                    if (i.EquipmentSlot == EquipmentSlot.Accessory)
                    {
                        pb.ContextMenuStrip = this.contextMenuStripAccessory;
                    }
                    else
                    {
                        pb.ContextMenuStrip = this.contextMenuStripItem;
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for the mouse being clicked down on an equipment slot.
        /// Stores a reference to the button that launched the right-click menu.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void EquipmentButtons_MouseDown(object sender, MouseEventArgs e)
        {
            this.contextMenuStripEquipment.Tag = sender;
        }

        /// <summary>
        /// Event handler for when an item slot is clicked.
        /// Moves items around in the inventory.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Buttons_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb != null)
            {
                if (!this.movingItem)
                {
                    this.movingItem = true;
                    this.picSource = pb;
                    this.tempImage = pb.Image;
                    this.picSource.Image = null;

                    if (this.tempImage != null)
                    {
                        this.Cursor = CursorCreator.CreateCursor(new Bitmap(this.tempImage));
                    }
                }
                else
                {
                    this.movingItem = false;
                    this.picSource.Image = this.tempImage;
                    this.Cursor = Cursors.Default;

                    int slot1 = Convert.ToInt32(this.picSource.Name.Replace(InventoryWindow.inventoryButtonPrefix, string.Empty), CultureInfo.InvariantCulture);
                    int slot2 = Convert.ToInt32(pb.Name.Replace(InventoryWindow.inventoryButtonPrefix, string.Empty), CultureInfo.InvariantCulture);

                    this.Network.SendPacket(new SwapSlotsPacket(slot1 - 1, slot2 - 1));
                }
            }
        }

        /// <summary>
        /// Event handler for clicking the discard option.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void DiscardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi != null && tsmi.Owner != null)
            {
                ContextMenuStrip cms = tsmi.Owner as ContextMenuStrip;
                if (cms != null && cms.Tag != null)
                {
                    PictureBox pb = cms.Tag as PictureBox;
                    if (pb != null)
                    {
                        string nameString = pb.Name.Replace(InventoryWindow.inventoryButtonPrefix, string.Empty);

                        this.Network.SendPacket(new DiscardPacket(Convert.ToInt32(nameString, CultureInfo.InvariantCulture) - 1));
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for clicking the equip option.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void EquipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi != null && tsmi.Owner != null)
            {
                int acc = -1;
                if (tsmi.Text.Length > 0)
                {
                    if (char.IsDigit(tsmi.Text[tsmi.Text.Length - 1]))
                    {
                        acc = Convert.ToInt32(tsmi.Text[tsmi.Text.Length - 1].ToString(), CultureInfo.InvariantCulture);
                    }
                }

                ContextMenuStrip cms = tsmi.Owner as ContextMenuStrip;
                if (cms != null && cms.Tag != null)
                {
                    PictureBox pb = cms.Tag as PictureBox;
                    if (pb != null)
                    {
                        string nameString = pb.Name.Replace(InventoryWindow.inventoryButtonPrefix, string.Empty);

                        EquipmentSlot eq = EquipmentSlot.NotEquipped;
                        if (acc == 1)
                        {
                            eq = EquipmentSlot.Accessory1;
                        }
                        else if (acc == 2)
                        {
                            eq = EquipmentSlot.Accessory2;
                        }

                        this.Network.SendPacket(new EquipPacket(true, Convert.ToInt32(nameString, CultureInfo.InvariantCulture) - 1, eq));
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for clicking the unequip option.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void UnequipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi != null && tsmi.Owner != null)
            {
                ContextMenuStrip cms = tsmi.Owner as ContextMenuStrip;
                if (cms != null && cms.Tag != null)
                {
                    PictureBox pb = cms.Tag as PictureBox;
                    if (pb != null)
                    {
                        Item item;
                        if (pb.Tag != null && (item = pb.Tag as Item) != null)
                        {
                            EquipmentSlot eq = item.EquipmentSlot;

                            if (item is Accessory)
                            {
                                int acc = Convert.ToInt32(pb.Name.Replace(InventoryWindow.accessoryButtonPrefix, string.Empty), CultureInfo.InvariantCulture);

                                if (acc == 1)
                                {
                                    eq = EquipmentSlot.Accessory1;
                                }
                                else if (acc == 2)
                                {
                                    eq = EquipmentSlot.Accessory2;
                                }
                            }

                            this.Network.SendPacket(new EquipPacket(false, eq));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for all events raised by the PlayerInventory class.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Inventory_Event(object sender, EventArgs e)
        {
            this.RefreshGui();
        }

        /// <summary>
        /// Reloads the user interface from the PlayerInventory object.
        /// </summary>
        private void RefreshGui()
        {
            // Set all the buttons to have the correct images and also set objects themselves as tag
            Item[] equippedItems = new Item[]
            {
                this.Inventory.Weapon,
                this.Inventory.Shield,
                this.Inventory.Armor,
                this.Inventory.Belt,
                this.Inventory.Accessory1,
                this.Inventory.Accessory2
            };

            for (int i = 0; i < equippedItems.Length; i++)
            {
                this.equipmentButtons[i].Tag = equippedItems[i];

                if (equippedItems[i] != null)
                {
                    this.equipmentLabels[i].SetText(equippedItems[i].Name);
                    this.equipmentButtons[i].SetImage(equippedItems[i].Image);
                }
                else
                {
                    this.equipmentLabels[i].SetText(this.equipmentLabels[i].Tag.ToString());
                    this.equipmentButtons[i].SetImage(null);
                }
            }
            
            for (int i = 0; i < this.Inventory.CountTotalSlots; i++)
            {
                this.inventoryButtons[i].Tag = this.Inventory.GetItems()[i];

                if (this.Inventory.GetItems()[i] != null)
                {
                    this.inventoryButtons[i].SetImage(this.Inventory.GetItems()[i].Image);
                }
                else
                {
                    this.inventoryButtons[i].SetImage(null);
                }
            }
        }
    }
}
