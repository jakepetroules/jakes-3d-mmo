namespace MMO3D.CommonCode
{
    using System;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a offensive munition.
    /// </summary>
    [Serializable]
    public abstract class OffensiveMunition : Munition
    {
        /// <summary>
        /// Initializes a new instance of the OffensiveMunition class.
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
        /// <param name="attackBonus">The offensive munition's attack bonus.</param>
        /// <param name="attackSpeed">The offensive munition's attack speed.</param>
        /// <exception cref="System.ArgumentNullException">Any parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">ItemTypeId is less than zero.</exception>
        /// <exception cref="System.ArgumentException">ItemClass is ItemClass.Undefined.</exception>
        protected OffensiveMunition(long itemTypeId, ItemClass itemClass, string name, string description, float weight, short integrity, short maximumIntegrity, byte integrityLevel, byte evasionBonus, byte attackBonus, int attackSpeed)
            : base(itemTypeId, itemClass, name, description, weight, integrity, maximumIntegrity, integrityLevel, evasionBonus)
        {
            this.AttackBonus = attackBonus;
            this.AttackSpeed = attackSpeed;
        }

        /// <summary>
        /// Gets or sets the offensive munition's attack bonus. Range: 0 to 255.
        /// </summary>
        /// <value>See summary.</value>
        public byte AttackBonus
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the offensive munition's attack speed.
        /// </summary>
        /// <value>See summary.</value>
        public int AttackSpeed
        {
            get;
            set;
        }
    }
}
