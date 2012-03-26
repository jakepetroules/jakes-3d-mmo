namespace MMO3D.NetworkInterface
{
    using System;
    using Petroules.Synteza.Networking;
    using MMO3D.CommonCode;

    /// <summary>
    /// Packet for equipping or unequipping an item.
    /// </summary>
    [Serializable]
    public sealed class EquipPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the EquipPacket class.
        /// This constructor is for equipping an item.
        /// </summary>
        /// <param name="equip">A value indicating whether we are equipping or un-equipping. True to equip, false to un-equip.</param>
        /// <param name="equipIndex">The index of the item to equip or un-equip.</param>
        public EquipPacket(bool equip, int equipIndex)
            : this(equip, equipIndex, EquipmentSlot.NotEquipped)
        {
        }

        /// <summary>
        /// Initializes a new instance of the EquipPacket class.
        /// This constructor is for un-equipping an item.
        /// </summary>
        /// <param name="equip">A value indicating whether we are equipping or un-equipping. True to equip, false to un-equip.</param>
        /// <param name="removeEquipItem">The equipment slot to un-equip.</param>
        public EquipPacket(bool equip, EquipmentSlot removeEquipItem)
            : this(equip, -1, removeEquipItem)
        {
        }

        /// <summary>
        /// Initializes a new instance of the EquipPacket class.
        /// This constructor is for equipping an accessory.
        /// </summary>
        /// <param name="equip">A value indicating whether we are equipping or un-equipping. True to equip, false to un-equip.</param>
        /// <param name="equipIndex">The index of the item to equip or un-equip.</param>
        /// <param name="slot">The equipment slot to equip to.</param>
        public EquipPacket(bool equip, int equipIndex, EquipmentSlot slot)
            : base()
        {
            this.Equip = equip;
            this.EquipIndex = equipIndex;
            this.Slot = slot;
        }

        /// <summary>
        /// Gets the index of the item to equip or un-equip.
        /// </summary>
        /// <value>See summary.</value>
        public int EquipIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the equipment slot to equip to / un-equip.
        /// </summary>
        /// <value>See summary.</value>
        public EquipmentSlot Slot
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether we are equipping or un-equipping. True to equip, false to un-equip.
        /// </summary>
        /// <value>See summary.</value>
        public bool Equip
        {
            get;
            private set;
        }
    }
}
