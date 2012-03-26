namespace MMO3D.CommonCode
{
    using System;

    /// <summary>
    /// Defines slots in which items can be equipped.
    /// </summary>
    [Serializable]
    public enum EquipmentSlot
    {
        /// <summary>
        /// This item is not equipped.
        /// </summary>
        NotEquipped = 0,

        /// <summary>
        /// This is a weapon or item that goes on the weapon arm.
        /// </summary>
        WeaponArm,

        /// <summary>
        /// This is a shield or item that goes on the shield arm.
        /// </summary>
        ShieldArm,

        /// <summary>
        /// This is a two-handed weapon.
        /// </summary>
        TwoHandedWeapon = WeaponArm | ShieldArm,

        /// <summary>
        /// This is armor.
        /// </summary>
        Armor,

        /// <summary>
        /// This is a belt.
        /// </summary>
        Belt,

        /// <summary>
        /// The first accessory slot.
        /// </summary>
        Accessory1,

        /// <summary>
        /// The second accessory slot.
        /// </summary>
        Accessory2,

        /// <summary>
        /// This is an accessory.
        /// </summary>
        Accessory = Accessory1 | Accessory2
    }
}
