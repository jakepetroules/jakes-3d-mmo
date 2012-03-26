namespace MMO3D.CommonCode
{
    using System;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines munitions.
    /// </summary>
    [Serializable]
    public abstract class Munition : Item, IQuality
    {
        /// <summary>
        /// The munition's current integrity. Range: 0 to MaximumIntegrity.
        /// </summary>
        private short integrity;

        /// <summary>
        /// The munition's maximum integrity. Range: 1 to 10,000.
        /// </summary>
        private short maximumIntegrity;

        /// <summary>
        /// The munition's integrity level. Range: 1 to 255.
        /// </summary>
        private byte integrityLevel;

        /// <summary>
        /// Initializes a new instance of the Munition class.
        /// Any out-of-range arguments will be clamped into the proper range automatically.
        /// </summary>
        /// <param name="itemTypeId">The ID of the item to create.</param>
        /// <param name="itemClass">The type of item that this is.</param>
        /// <param name="name">The item's name.</param>
        /// <param name="description">The item's description.</param>
        /// <param name="weight">The weight of the munition.</param>
        /// <param name="integrity">The munition's current integrity.</param>
        /// <param name="maximumIntegrity">The munition's maximum integrity.</param>
        /// <param name="integrityLevel">The munition's integrity level.</param>
        /// <param name="evasionBonus">The munition's evasion bonus.</param>
        /// <exception cref="System.ArgumentNullException">Any parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">ItemTypeId is less than zero.</exception>
        /// <exception cref="System.ArgumentException">ItemClass is ItemClass.Undefined.</exception>
        protected Munition(long itemTypeId, ItemClass itemClass, string name, string description, float weight, short integrity, short maximumIntegrity, byte integrityLevel, byte evasionBonus)
            : base(itemTypeId, itemClass, name, description)
        {
            this.Weight = weight;
            this.Integrity = integrity;
            this.MaximumIntegrity = maximumIntegrity;
            this.IntegrityLevel = integrityLevel;
            this.EvasionBonus = evasionBonus;
        }

        /// <summary>
        /// Gets the weight of the munition.
        /// </summary>
        /// <value>See summary.</value>
        public float Weight
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the munition's current integrity. Range: 0 to MaximumIntegrity.
        /// </summary>
        /// <value>See summary.</value>
        public short Integrity
        {
            get { return this.integrity; }
            set { this.integrity = (short)MathHelper.Clamp(value, 0, this.MaximumIntegrity); }
        }

        /// <summary>
        /// Gets or sets the munition's maximum integrity. Range: 1 to 10,000.
        /// </summary>
        /// <value>See summary.</value>
        public short MaximumIntegrity
        {
            get { return this.maximumIntegrity; }
            set { this.maximumIntegrity = (short)MathHelper.Clamp(value, 1, 10000); }
        }

        /// <summary>
        /// Gets or sets the munition's integrity level. Range: 1 to 255.
        /// </summary>
        /// <value>See summary.</value>
        public byte IntegrityLevel
        {
            get { return this.integrityLevel; }
            set { this.integrityLevel = (byte)MathHelper.Clamp(value, 1, 255); }
        }

        /// <summary>
        /// Gets or sets the munition's evasion bonus. Range: 0 to 255.
        /// </summary>
        /// <value>See summary.</value>
        public byte EvasionBonus
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the quality of the item.
        /// </summary>
        /// <value>See summary.</value>
        public ItemQuality Quality
        {
            get;
            set;
        }
    }
}
