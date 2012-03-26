namespace MMO3D.CommonCode
{
    using System;
    using Petroules.Synteza;

    /// <summary>
    /// A list of the possible classes of items.
    /// </summary>
    [Serializable]
    public enum ItemClass
    {
        /// <summary>
        /// Defines an item whose class is undefined.
        /// This is for use in error handling.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Defines a one-handed weapon.
        /// </summary>
        OneHandedWeapon = 1,

        /// <summary>
        /// Defines a two-handed weapon.
        /// </summary>
        TwoHandedWeapon = 2,

        /// <summary>
        /// Defines a shield.
        /// </summary>
        Shield = 3,

        /// <summary>
        /// Defines armor.
        /// </summary>
        Armor = 4,

        /// <summary>
        /// Defines a belt.
        /// </summary>
        Belt = 5,

        /// <summary>
        /// Defines an accessory.
        /// </summary>
        Accessory = 6,

        /// <summary>
        /// Defines a potion.
        /// </summary>
        Potion = 7,

        /// <summary>
        /// Defines a spell shard.
        /// </summary>
        SpellShard = 8,

        /// <summary>
        /// Defines a tool.
        /// </summary>
        Tool = 9,

        /// <summary>
        /// Defines a material.
        /// </summary>
        Material = 10,

        /// <summary>
        /// Defines food.
        /// </summary>
        Food = 11,

        /// <summary>
        /// Defines a miscellaneous item.
        /// </summary>
        MiscellaneousItem = 12
    }

    /// <summary>
    /// Extensions for the ItemClass enumeration.
    /// </summary>
    public static class ItemClassExtensions
    {
        /// <summary>
        /// Parses an enumeration constant from a string. If no equivalent is found,
        /// the constant representing none, null, empty or default will be returned.
        /// </summary>
        /// <param name="enumConstant">The string to parse.</param>
        /// <returns>See summary.</returns>
        public static ItemClass ParseFromString(string enumConstant)
        {
            return EnumHelper.ParseFromString<ItemClass>(enumConstant, ItemClass.Undefined);
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
            return EnumHelper.GetSortedList<ItemClass>(ItemClass.Undefined);
        }

        /// <summary>
        /// Checks to see if <paramref name="checkClass"/> is an item with a quality state.
        /// </summary>
        /// <param name="checkClass">The class to check.</param>
        /// <returns>See summary.</returns>
        public static bool IsIQuality(this ItemClass checkClass)
        {
            if (checkClass.IsMunition())
            {
                return true;
            }

            switch (checkClass)
            {
                case ItemClass.Accessory:
                case ItemClass.Food:
                case ItemClass.Material:
                case ItemClass.Potion:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Checks to see if <paramref name="checkClass"/> is a munition.
        /// </summary>
        /// <param name="checkClass">The class to check.</param>
        /// <returns>See summary.</returns>
        public static bool IsMunition(this ItemClass checkClass)
        {
            return checkClass.IsOffensiveMunition() || checkClass.IsDefensiveMunition();
        }

        /// <summary>
        /// Checks to see if <paramref name="checkClass"/> is an offensive munition.
        /// </summary>
        /// <param name="checkClass">The class to check.</param>
        /// <returns>See summary.</returns>
        public static bool IsOffensiveMunition(this ItemClass checkClass)
        {
            switch (checkClass)
            {
                case ItemClass.OneHandedWeapon:
                case ItemClass.TwoHandedWeapon:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Checks to see if <paramref name="checkClass"/> is a defensive munition.
        /// </summary>
        /// <param name="checkClass">The class to check.</param>
        /// <returns>See summary.</returns>
        public static bool IsDefensiveMunition(this ItemClass checkClass)
        {
            switch (checkClass)
            {
                case ItemClass.Armor:
                case ItemClass.Shield:
                    return true;
                default:
                    return false;
            }
        }
    }
}
