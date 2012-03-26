namespace MMO3D.CommonCode
{
    using System;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a two-handed weapon.
    /// </summary>
    [Serializable]
    public sealed class TwoHandedWeapon : OffensiveMunition
    {
        /// <summary>
        /// Initializes a new instance of the TwoHandedWeapon class.
        /// </summary>
        /// <param name="itemTypeId">The ID of the item to create.</param>
        /// <param name="name">The item's name.</param>
        /// <param name="description">The item's description.</param>
        /// <param name="weight">The weight of the munition.</param>
        /// <param name="integrity">The munition's current integrity.</param>
        /// <param name="maximumIntegrity">The munition's maximum integrity.</param>
        /// <param name="integrityLevel">The munition's integrity level.</param>
        /// <param name="evasionBonus">The munition's evasion bonus.</param>
        /// <param name="attackBonus">The weapon's attack bonus.</param>
        /// <param name="attackSpeed">The weapon's attack speed.</param>
        /// <param name="weaponType">The type of weapon.</param>
        /// <exception cref="System.ArgumentNullException">Any parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">ItemTypeId is less than zero.</exception>
        /// <exception cref="System.ArgumentException">WeaponType is WeaponType2H.Undefined.</exception>
        public TwoHandedWeapon(long itemTypeId, string name, string description, float weight, short integrity, short maximumIntegrity, byte integrityLevel, byte evasionBonus, byte attackBonus, int attackSpeed, WeaponType2H weaponType)
            : base(itemTypeId, ItemClass.TwoHandedWeapon, name, description, weight, integrity, maximumIntegrity, integrityLevel, evasionBonus, attackBonus, attackSpeed)
        {
            if (weaponType == WeaponType2H.Undefined)
            {
                throw new ArgumentException("cannot be WeaponType2H.Undefined", "weaponType");
            }

            this.WeaponType = weaponType;
        }

        /// <summary>
        /// Gets the weapon type.
        /// </summary>
        /// <value>See summary.</value>
        public WeaponType2H WeaponType
        {
            get;
            private set;
        }
    }
}
