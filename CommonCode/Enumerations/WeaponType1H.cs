namespace MMO3D.CommonCode
{
    using System;
    using Petroules.Synteza;

    /// <summary>
    /// Defines types of one-handed weapons.
    /// </summary>
    [Serializable]
    public enum WeaponType1H
    {
        /// <summary>
        /// Specifies a one handed weapon whose type is undefined.
        /// This is for use in error handling.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Specifies an axe.
        /// </summary>
        Axe = 1,

        /// <summary>
        /// Specifies a dagger.
        /// </summary>
        Dagger = 2,

        /// <summary>
        /// Specifies a hammer.
        /// </summary>
        Hammer = 3,

        /// <summary>
        /// Specifies a spear.
        /// </summary>
        Spear = 4,

        /// <summary>
        /// Specifies a sword.
        /// </summary>
        Sword = 5
    }

    /// <summary>
    /// Extensions for the WeaponType1H enumeration.
    /// </summary>
    public static class WeaponType1HExtensions
    {
        /// <summary>
        /// Parses an enumeration constant from a string. If no equivalent is found,
        /// the constant representing none, null, empty or default will be returned.
        /// </summary>
        /// <param name="enumConstant">The string to parse.</param>
        /// <returns>See summary.</returns>
        public static WeaponType1H ParseFromString(string enumConstant)
        {
            return EnumHelper.ParseFromString<WeaponType1H>(enumConstant, WeaponType1H.Undefined);
        }

        /// <summary>
        /// Gets a sorted list of the enumeration constants as a
        /// collection of strings with the same names. The constant
        /// representing none, null, empty or default will be placed
        /// at the top of the list, the rest following alphabetically.
        /// </summary>
        /// <returns>See summary.</returns>
        public static string[] GetSortedList()
        {
            return EnumHelper.GetSortedList<WeaponType1H>(WeaponType1H.Undefined);
        }
    }
}
