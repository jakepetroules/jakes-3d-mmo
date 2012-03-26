namespace MMO3D.CommonCode
{
    using System;
    using System.Globalization;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines armor.
    /// </summary>
    [Serializable]
    public sealed class Armor : DefensiveMunition
    {
        /// <summary>
        /// Initializes a new instance of the Armor class.
        /// </summary>
        /// <param name="itemTypeId">The ID of the item to create.</param>
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
        /// <exception cref="System.ArgumentException">ArmorType is ArmorType.Undefined.</exception>
        public Armor(long itemTypeId, string name, string description, float weight, short integrity, short maximumIntegrity, byte integrityLevel, byte evasionBonus, ArmorType armorType, byte defenseBonus)
            : base(itemTypeId, ItemClass.Armor, name, description, weight, integrity, maximumIntegrity, integrityLevel, evasionBonus, armorType, defenseBonus)
        {
        }

        /// <summary>
        /// Gets or sets the armor's evasion bonus. This property always gets or sets 0.
        /// </summary>
        /// <value>See summary.</value>
        public new byte EvasionBonus
        {
            get
            {
                // This will always be true but we get a StyleCop
                // warning if we don't reference "this" and error
                // suppression messages are ugly
                if (this.ItemClass == ItemClass.Armor)
                {
                    return 0;
                }
                else
                {
                    return base.EvasionBonus;
                }
            }

            set
            {
                // This will always be true but we get a StyleCop
                // warning if we don't reference "this" and error
                // suppression messages are ugly
                if (this.ItemClass == ItemClass.Armor)
                {
                    base.EvasionBonus = 0;
                }
                else
                {
                    base.EvasionBonus = value;
                }
            }
        }
    }
}
