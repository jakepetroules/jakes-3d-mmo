namespace MMO3D.CommonCode
{
    using System;
    using Petroules.Synteza;

    /// <summary>
    /// Defines types of two-handed weapons.
    /// </summary>
    [Serializable]
    public enum WeaponType2H
    {
        /// <summary>
        /// Specifies a two handed weapon whose type is undefined.
        /// This is for use in error handling.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Specifies a greataxe.
        /// </summary>
        GreatAxe = 1,

        /// <summary>
        /// Specifies a greatsword.
        /// </summary>
        GreatSword = 2,

        /// <summary>
        /// Specifies a halberd.
        /// </summary>
        Halberd = 3,

        /// <summary>
        /// Specifies a scythe.
        /// </summary>
        Scythe = 4
    }

    /// <summary>
    /// Extensions for the WeaponType2H enumeration.
    /// </summary>
    public static class WeaponType2HExtensions
    {
        /// <summary>
        /// Parses an enumeration constant from a string. If no equivalent is found,
        /// the constant representing none, null, empty or default will be returned.
        /// </summary>
        /// <param name="enumConstant">The string to parse.</param>
        /// <returns>See summary.</returns>
        public static WeaponType2H ParseFromString(string enumConstant)
        {
            return EnumHelper.ParseFromString<WeaponType2H>(enumConstant, WeaponType2H.Undefined);
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
            return EnumHelper.GetSortedList<WeaponType2H>(WeaponType2H.Undefined);
        }
    }
}
