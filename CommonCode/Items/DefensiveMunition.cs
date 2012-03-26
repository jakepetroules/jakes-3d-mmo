namespace MMO3D.CommonCode
{
    using System;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a defensive munition.
    /// </summary>
    [Serializable]
    public abstract class DefensiveMunition : Munition
    {
        /// <summary>
        /// Initializes a new instance of the DefensiveMunition class.
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
        /// <param name="armorType">The type of armor that this is.</param>
        /// <param name="defenseBonus">The item's defense bonus, if it is a shield or armor.</param>
        /// <exception cref="System.ArgumentNullException">Any parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">ItemTypeId is less than zero.</exception>
        /// <exception cref="System.ArgumentException">ItemClass is ItemClass.Undefined. -or- ArmorType is ArmorType.Undefined.</exception>
        protected DefensiveMunition(long itemTypeId, ItemClass itemClass, string name, string description, float weight, short integrity, short maximumIntegrity, byte integrityLevel, byte evasionBonus, ArmorType armorType, byte defenseBonus)
            : base(itemTypeId, itemClass, name, description, weight, integrity, maximumIntegrity, integrityLevel, evasionBonus)
        {
            if (armorType == ArmorType.Undefined)
            {
                throw new ArgumentException("cannot be ArmorType.Undefined", "armorType");
            }

            this.ArmorType = armorType;
            this.DefenseBonus = defenseBonus;
        }

        /// <summary>
        /// Gets the armor type.
        /// </summary>
        /// <value>See summary.</value>
        public ArmorType ArmorType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the item's defense bonus, if it is a shield or armor. Range: 0 to 255.
        /// </summary>
        /// <value>See summary.</value>
        public byte DefenseBonus
        {
            get;
            set;
        }
    }
}
