namespace MMO3D.CommonCode
{
    using System;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a belt.
    /// </summary>
    [Serializable]
    public sealed class Belt : Item
    {
        /// <summary>
        /// Initializes a new instance of the Belt class.
        /// </summary>
        /// <param name="itemTypeId">The ID of the item to create.</param>
        /// <param name="name">The item's name.</param>
        /// <param name="description">The item's description.</param>
        /// <param name="potionStraps">The number of potions on the belt.</param>
        /// <param name="weaponSheaths">The number of weapon slots on the belt.</param>
        /// <param name="castersPouches">The number of spell shard slots on the belt.</param>
        /// <param name="toolPouches">The number of tool pouches on the belt.</param>
        /// <exception cref="System.ArgumentNullException">Any parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">ItemTypeId is less than zero.</exception>
        public Belt(long itemTypeId, string name, string description, byte potionStraps, byte weaponSheaths, byte castersPouches, byte toolPouches)
            : base(itemTypeId, ItemClass.Belt, name, description)
        {
            this.PotionStraps = potionStraps;
            this.WeaponSheaths = weaponSheaths;
            this.CastersPouches = castersPouches;
            this.ToolPouches = toolPouches;
        }

        /// <summary>
        /// Gets the number of potion slots on the belt.
        /// </summary>
        /// <value>See summary.</value>
        public byte PotionStraps
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the amount of weapon slots on the belt.
        /// </summary>
        /// <value>See summary.</value>
        public byte WeaponSheaths
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the amount of spell shard slots on the belt.
        /// </summary>
        /// <value>See summary.</value>
        public byte CastersPouches
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the amount of tool pouches on the belt.
        /// </summary>
        /// <value>See summary.</value>
        public byte ToolPouches
        {
            get;
            private set;
        }
    }
}
