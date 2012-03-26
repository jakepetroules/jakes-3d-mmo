namespace MMO3D.Engine
{
    using System;
    using System.Collections.ObjectModel;
    using Petroules.Synteza;

    /// <summary>
    /// Enumerates the seasons.
    /// </summary>
    [Serializable]
    public enum Season
    {
        /// <summary>
        /// The default season. This is a moderately temperate and humid season.
        /// </summary>
        Midseason = 0,

        /// <summary>
        /// This is the hot and dry season.
        /// </summary>
        Summer = 1,

        /// <summary>
        /// This is the cold and wet season.
        /// </summary>
        Winter = 2
    }

    /// <summary>
    /// Extensions for the Season enumeration.
    /// </summary>
    public static class SeasonExtensions
    {
        /// <summary>
        /// The number of seasons that exist.
        /// </summary>
        public const int Count = 3;

        /// <summary>
        /// Gets a collection of all the seasons.
        /// </summary>
        /// <returns>See summary.</returns>
        public static ReadOnlyCollection<Season> GetSeasons()
        {
            return new ReadOnlyCollection<Season>((Season[])Enum.GetValues(typeof(Season)));
        }

        /// <summary>
        /// Parses an enumeration constant from a string. If no equivalent is found,
        /// the constant representing none, null, empty or default will be returned.
        /// </summary>
        /// <param name="enumConstant">The string to parse.</param>
        /// <returns>See summary.</returns>
        public static Season ParseFromString(string enumConstant)
        {
            return EnumHelper.ParseFromString<Season>(enumConstant, Season.Midseason);
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
            return EnumHelper.GetSortedList<Season>(Season.Midseason);
        }
    }
}
