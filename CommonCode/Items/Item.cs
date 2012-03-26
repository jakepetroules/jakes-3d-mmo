namespace MMO3D.CommonCode
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Security;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a game item.
    /// </summary>
    [Serializable]
    public abstract class Item
    {
        /// <summary>
        /// The integer representing the ID of a null item.
        /// </summary>
        public const short NullItemId = -1;

        /// <summary>
        /// The item's image (32x32x32, PNG).
        /// </summary>
        [NonSerialized]
        private Image image;

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="itemTypeId">The ID of the item to create.</param>
        /// <param name="itemClass">The type of item that this is.</param>
        /// <param name="name">The item's name.</param>
        /// <param name="description">The item's description.</param>
        /// <exception cref="System.ArgumentNullException">Any parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Item type ID is less than zero.</exception>
        /// <exception cref="System.ArgumentException">Item class is ItemClass.Undefined.</exception>
        protected Item(long itemTypeId, ItemClass itemClass, string name, string description)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            if (itemTypeId < 0)
            {
                throw new ArgumentOutOfRangeException("itemTypeId", "must be greater than or equal to zero.");
            }

            if (itemClass == ItemClass.Undefined)
            {
                throw new ArgumentException("cannot be ItemClass.Undefined", "itemClass");
            }

            this.ItemTypeId = itemTypeId;
            this.ItemClass = itemClass;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Gets the type of item that this is.
        /// </summary>
        /// <value>See summary.</value>
        public long ItemTypeId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the item's class.
        /// </summary>
        /// <value>See summary.</value>
        public ItemClass ItemClass
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the item's name.
        /// </summary>
        /// <value>See summary.</value>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the item's description.
        /// </summary>
        /// <value>See summary.</value>
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the item's image (32x32x32, PNG).
        /// </summary>
        /// <value>See summary.</value>
        public Image Image
        {
            get { return this.image ?? (this.image = ItemUtilities.GetItemImage(this.ItemTypeId)); }
        }

        /// <summary>
        /// Gets the slot into which this item should be equipped.
        /// </summary>
        /// <value>See summary.</value>
        public EquipmentSlot EquipmentSlot
        {
            get
            {
                switch (this.ItemClass)
                {
                    case ItemClass.OneHandedWeapon:
                        return EquipmentSlot.WeaponArm;
                    case ItemClass.Shield:
                        return EquipmentSlot.ShieldArm;
                    case ItemClass.TwoHandedWeapon:
                        return EquipmentSlot.TwoHandedWeapon;
                    case ItemClass.Armor:
                        return EquipmentSlot.Armor;
                    case ItemClass.Belt:
                        return EquipmentSlot.Belt;
                    case ItemClass.Accessory:
                        return EquipmentSlot.Accessory;
                    default:
                        return EquipmentSlot.NotEquipped;
                }
            }
        }
    }
}
