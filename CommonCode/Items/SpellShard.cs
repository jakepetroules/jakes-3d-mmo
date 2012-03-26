namespace MMO3D.CommonCode
{
    using System;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a spell shard.
    /// </summary>
    [Serializable]
    public sealed class SpellShard : Item
    {
        /// <summary>
        /// The current number of charges that the spell shard has. Range: 0 to MaxCharges.
        /// </summary>
        private byte charges;

        /// <summary>
        /// The maximum number of charges that the spell shard can have. Range: 1 to 255.
        /// </summary>
        private byte maxCharges;

        /// <summary>
        /// Initializes a new instance of the SpellShard class.
        /// </summary>
        /// <param name="itemTypeId">The ID of the item to create.</param>
        /// <param name="name">The item's name.</param>
        /// <param name="description">The item's description.</param>
        /// <param name="maxCharges">The maximum number of charges that the spell shard can have.</param>
        /// <exception cref="System.ArgumentNullException">Any parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">ItemTypeId is less than zero.</exception>
        public SpellShard(long itemTypeId, string name, string description, byte maxCharges)
            : base(itemTypeId, ItemClass.SpellShard, name, description)
        {
            this.MaxCharges = maxCharges;
        }

        /// <summary>
        /// Gets or sets the current number of charges that the spell shard has. Range: 0 to MaxCharges.
        /// </summary>
        /// <value>See summary.</value>
        public byte Charges
        {
            get { return this.charges; }
            set { this.charges = (byte)MathHelper.Clamp(value, 0, this.MaxCharges); }
        }

        /// <summary>
        /// Gets the maximum number of charges that the spell shard can have. Range: 1 to 255.
        /// </summary>
        /// <value>See summary.</value>
        public byte MaxCharges
        {
            get { return this.maxCharges; }
            private set { this.maxCharges = (byte)MathHelper.Clamp(value, 1, 255); }
        }
    }
}
