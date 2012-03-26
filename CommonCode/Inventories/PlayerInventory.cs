namespace MMO3D.CommonCode
{
    using System;

    /// <summary>
    /// Class for managing player inventory. This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class PlayerInventory : Inventory
    {
        /// <summary>
        /// The size of a player's inventory.
        /// </summary>
        public const int InventorySize = 8;

        /// <summary>
        /// The weapon the player is wielding.
        /// </summary>
        private OffensiveMunition weapon;

        /// <summary>
        /// The shield the player is wielding.
        /// </summary>
        private Shield shield;

        /// <summary>
        /// The armor the player is wearing.
        /// </summary>
        private Armor armor;

        /// <summary>
        /// The belt the player is wearing.
        /// </summary>
        private Belt belt;

        /// <summary>
        /// The first accessory the player has equipped.
        /// </summary>
        private Accessory accessory1;

        /// <summary>
        /// The second accessory the player has equipped.
        /// </summary>
        private Accessory accessory2;

        /// <summary>
        /// Initializes a new instance of the PlayerInventory class.
        /// </summary>
        public PlayerInventory()
            : base(PlayerInventory.InventorySize)
        {
        }

        /// <summary>
        /// Raised when the value of an equipped item property changes.
        /// </summary>
        public event EventHandler EquipmentChanged = delegate { };

        /// <summary>
        /// Gets or sets the weapon the player is wielding.
        /// </summary>
        /// <value>See summary.</value>
        public OffensiveMunition Weapon
        {
            get
            {
                return this.weapon;
            }

            set
            {
                this.weapon = value;
                this.EquipmentChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the shield the player is wielding.
        /// </summary>
        /// <value>See summary.</value>
        public Shield Shield
        {
            get
            {
                return this.shield;
            }

            set
            {
                this.shield = value;
                this.EquipmentChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the armor the player is wearing.
        /// </summary>
        /// <value>See summary.</value>
        public Armor Armor
        {
            get
            {
                return this.armor;
            }

            set
            {
                this.armor = value;
                this.EquipmentChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the belt the player is wearing.
        /// </summary>
        /// <value>See summary.</value>
        public Belt Belt
        {
            get
            {
                return this.belt;
            }

            set
            {
                this.belt = value;
                this.EquipmentChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the first accessory the player has equipped.
        /// </summary>
        /// <value>See summary.</value>
        public Accessory Accessory1
        {
            get
            {
                return this.accessory1;
            }

            set
            {
                this.accessory1 = value;
                this.EquipmentChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the second accessory the player has equipped.
        /// </summary>
        /// <value>See summary.</value>
        public Accessory Accessory2
        {
            get
            {
                return this.accessory2;
            }

            set
            {
                this.accessory2 = value;
                this.EquipmentChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets the item in a particular inventory slot.
        /// </summary>
        /// <param name="slot">The slot to get the item in.</param>
        /// <returns>See summary.</returns>
        public Item GetItem(EquipmentSlot slot)
        {
            switch (slot)
            {
                case EquipmentSlot.WeaponArm:
                case EquipmentSlot.TwoHandedWeapon:
                    return this.Weapon;
                case EquipmentSlot.ShieldArm:
                    return this.Shield;
                case EquipmentSlot.Armor:
                    return this.Armor;
                case EquipmentSlot.Belt:
                    return this.Belt;
                case EquipmentSlot.Accessory1:
                    return this.Accessory1;
                case EquipmentSlot.Accessory2:
                    return this.Accessory2;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Equips an item.
        /// </summary>
        /// <param name="index">The index of the item to equip.</param>
        /// <param name="slot">The slot (only used if the item is an accessory) to equip to.</param>
        /// <returns>Whether there was space to equip.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Item index is less than zero or greater than or equal to the inventory's size.</exception>
        public bool Equip(int index, EquipmentSlot slot)
        {
            try
            {
                // Make sure client's not trying to do anything fishy...
                if (this.GetItems()[index].EquipmentSlot != EquipmentSlot.Accessory)
                {
                    slot = this.GetItems()[index].EquipmentSlot;
                }

                if (slot != EquipmentSlot.NotEquipped)
                {
                    bool finish = true;
                    bool success = true;
                    Item equippedItem = this.GetItem(slot);

                    switch (slot)
                    {
                        case EquipmentSlot.WeaponArm:
                            this.Weapon = (OffensiveMunition)this.GetItems()[index];
                            break;
                        case EquipmentSlot.TwoHandedWeapon:
                            // We'll do everything here
                            finish = false;

                            if (this.CountEmptySlots >= ((this.Weapon != null && this.Shield != null) ? 1 : 0))
                            {
                                this.Weapon = (OffensiveMunition)this.GetItems()[index];
                                this.Remove(index);
                                this.Add(equippedItem);
                                this.Remove(EquipmentSlot.ShieldArm);
                            }
                            else
                            {
                                success = false;
                            }

                            break;
                        case EquipmentSlot.ShieldArm:
                            // If there's a 2H weapon equipped, we need to take it off...
                            if (this.Weapon is TwoHandedWeapon)
                            {
                                // We'll do everything here
                                finish = false;

                                if (this.CountEmptySlots >= ((this.Weapon != null && this.Shield != null) ? 1 : 0))
                                {
                                    this.Shield = (Shield)this.GetItems()[index];
                                    this.Remove(index);
                                    this.Add(equippedItem);
                                    this.Remove(EquipmentSlot.WeaponArm);
                                }
                                else
                                {
                                    success = false;
                                }
                            }
                            else
                            {
                                // Otherwise just equip shield normally
                                this.Shield = (Shield)this.GetItems()[index];
                            }

                            break;
                        case EquipmentSlot.Armor:
                            this.Armor = (Armor)this.GetItems()[index];
                            break;
                        case EquipmentSlot.Belt:
                            this.Belt = (Belt)this.GetItems()[index];
                            break;
                        case EquipmentSlot.Accessory1:
                            this.Accessory1 = (Accessory)this.GetItems()[index];
                            break;
                        case EquipmentSlot.Accessory2:
                            this.Accessory2 = (Accessory)this.GetItems()[index];
                            break;
                    }

                    if (finish)
                    {
                        this.Remove(index);
                        this.Add(equippedItem);
                    }

                    return success;
                }

                return false;
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("index", "Item index must be greater than or equal to zero, and less than the size of the inventory.");
            }
        }

        /// <summary>
        /// Equips an item.
        /// </summary>
        /// <param name="index">The index of the item to equip.</param>
        /// <returns>Whether there was space to equip.</returns>
        public bool Equip(int index)
        {
            return this.Equip(index, this.GetItems()[index].EquipmentSlot);
        }

        /// <summary>
        /// Removes (un-equips) an item.
        /// </summary>
        /// <param name="slot">The slot to unequip the item from.</param>
        /// <returns>Whether there was space to unequip.</returns>
        public bool Remove(EquipmentSlot slot)
        {
            if (this.CountEmptySlots > 0)
            {
                switch (slot)
                {
                    case EquipmentSlot.WeaponArm:
                    case EquipmentSlot.TwoHandedWeapon:
                        this.Add(this.Weapon);
                        this.Weapon = null;
                        break;
                    case EquipmentSlot.ShieldArm:
                        this.Add(this.Shield);
                        this.Shield = null;
                        break;
                    case EquipmentSlot.Armor:
                        this.Add(this.Armor);
                        this.Armor = null;
                        break;
                    case EquipmentSlot.Belt:
                        this.Add(this.Belt);
                        this.Belt = null;
                        break;
                    case EquipmentSlot.Accessory1:
                        this.Add(this.Accessory1);
                        this.Accessory1 = null;
                        break;
                    case EquipmentSlot.Accessory2:
                        this.Add(this.Accessory2);
                        this.Accessory2 = null;
                        break;
                }

                return true;
            }

            return false;
        }
    }
}
