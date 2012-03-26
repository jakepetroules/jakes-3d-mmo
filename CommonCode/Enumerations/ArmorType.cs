namespace MMO3D.CommonCode
{
    using System;
    using Petroules.Synteza;

    /// <summary>
    /// Defines different types of armor.
    /// </summary>
    [Serializable]
    public enum ArmorType
    {
        /// <summary>
        /// Defines armor whose type is undefined.
        /// This is for use in error handling.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Light armor.
        /// </summary>
        Light = 1,

        /// <summary>
        /// Heavy armor.
        /// </summary>
        Heavy = 2,

        /// <summary>
        /// Magical armor.
        /// </summary>
        Magical = 3
    }

    /// <summary>
    /// Extensions for the ArmorType enumeration.
    /// </summary>
    public static class ArmorTypeExtensions
    {
        /// <summary>
        /// Parses an enumeration constant from a string. If no equivalent is found,
        /// the constant representing none, null, empty or default will be returned.
        /// </summary>
        /// <param name="enumConstant">The string to parse.</param>
        /// <returns>See summary.</returns>
        public static ArmorType ParseFromString(string enumConstant)
        {
            return EnumHelper.ParseFromString<ArmorType>(enumConstant, ArmorType.Undefined);
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
            return EnumHelper.GetSortedList<ArmorType>(ArmorType.Undefined);
        }
    }
}
