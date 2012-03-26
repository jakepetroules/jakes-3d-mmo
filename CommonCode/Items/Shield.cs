namespace MMO3D.CommonCode
{
    using System;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines shields.
    /// </summary>
    [Serializable]
    public sealed class Shield : DefensiveMunition
    {
        /// <summary>
        /// Initializes a new instance of the Shield class.
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
        public Shield(long itemTypeId, string name, string description, float weight, short integrity, short maximumIntegrity, byte integrityLevel, byte evasionBonus, ArmorType armorType, byte defenseBonus)
            : base(itemTypeId, ItemClass.Shield, name, description, weight, integrity, maximumIntegrity, integrityLevel, evasionBonus, armorType, defenseBonus)
        {
        }
    }
}
