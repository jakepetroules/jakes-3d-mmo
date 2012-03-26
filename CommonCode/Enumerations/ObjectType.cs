namespace MMO3D.CommonCode
{
    using System;
    using Petroules.Synteza;

    /// <summary>
    /// Enumerates different types of 3D objects.
    /// </summary>
    [Serializable]
    public enum ObjectType
    {
        /// <summary>
        /// Defines an object whose type is undefined.
        /// This is for use in error handling.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Defines a basic object.
        /// </summary>
        BasicObject = 1,

        /// <summary>
        /// Defines a building.
        /// </summary>
        Building = 2,

        /// <summary>
        /// Defines a combat building.
        /// </summary>
        CombatBuilding = 3,

        /// <summary>
        /// Defines a door.
        /// </summary>
        Door = 4,

        /// <summary>
        /// Defines a gateway.
        /// </summary>
        Gateway = 5,

        /// <summary>
        /// Defines an NPC.
        /// </summary>
        Npc = 6,

        /// <summary>
        /// Defines a combat NPC.
        /// </summary>
        CombatNpc = 7,

        /// <summary>
        /// Defines a player.
        /// </summary>
        Player = 8,

        /// <summary>
        /// Defines a resource.
        /// </summary>
        Resource = 9,

        /// <summary>
        /// Defines a treasure generator.
        /// </summary>
        TreasureGenerator = 10
    }

    /// <summary>
    /// Extensions for the ObjectType enumeration.
    /// </summary>
    public static class ObjectTypeExtensions
    {
        /// <summary>
        /// Parses an enumeration constant from a string. If no equivalent is found,
        /// the constant representing none, null, empty or default will be returned.
        /// </summary>
        /// <param name="enumConstant">The string to parse.</param>
        /// <returns>See summary.</returns>
        public static ObjectType ParseFromString(string enumConstant)
        {
            return EnumHelper.ParseFromString<ObjectType>(enumConstant, ObjectType.Undefined);
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
            return EnumHelper.GetSortedList<ObjectType>(ObjectType.Undefined);
        }
    }
}
