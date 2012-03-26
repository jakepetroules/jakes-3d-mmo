namespace MMO3D.Engine
{
    using System;

    /// <summary>
    /// Enumerates the tools that can be used to manipulate terrain.
    /// </summary>
    [Flags]
    public enum TerrainControlTools
    {
        /// <summary>
        /// The select point tool.
        /// </summary>
        Select = 1,

        /// <summary>
        /// The raise height tool.
        /// </summary>
        Raise = 2,

        /// <summary>
        /// The lower height tool.
        /// </summary>
        Lower = 4,

        /// <summary>
        /// The flatten tool.
        /// </summary>
        Flatten = 8,

        /// <summary>
        /// The smooth tool.
        /// </summary>
        Smooth = 16,

        /// <summary>
        /// The terrain type tool.
        /// </summary>
        TerrainType = 32
    }

    /// <summary>
    /// Extensions for the TerrainControlTool enumeration.
    /// </summary>
    public static class TerrainControlToolExtensions
    {
        /// <summary>
        /// Determines whether a flag is set on the enumeration constant.
        /// </summary>
        /// <param name="tool">The enumeration constant to check.</param>
        /// <param name="value">The flag to check.</param>
        /// <returns>See summary.</returns>
        public static bool IsSet(this TerrainControlTools tool, TerrainControlTools value)
        {
            return (tool & value) == value;
        }

        /// <summary>
        /// Determines whether a flag is not set on the enumeration constant.
        /// </summary>
        /// <param name="tool">The enumeration constant to check.</param>
        /// <param name="value">The flag to check.</param>
        /// <returns>See summary.</returns>
        public static bool IsNotSet(this TerrainControlTools tool, TerrainControlTools value)
        {
            return (tool & (~value)) == 0;
        }

        /// <summary>
        /// Sets a flag on the enumeration constant and returns the new value.
        /// </summary>
        /// <param name="tool">The enumeration constant to set the flag on.</param>
        /// <param name="value">The flag to set.</param>
        /// <returns>See summary.</returns>
        public static TerrainControlTools Set(this TerrainControlTools tool, TerrainControlTools value)
        {
            return tool | value;
        }

        /// <summary>
        /// Clears a flag from the enumeration constant and returns the new value.
        /// </summary>
        /// <param name="tool">The enumeration constant to clear the flag from.</param>
        /// <param name="value">The flag to clear.</param>
        /// <returns>See summary.</returns>
        public static TerrainControlTools Clear(this TerrainControlTools tool, TerrainControlTools value)
        {
            return tool & (~value);
        }
    }
}
