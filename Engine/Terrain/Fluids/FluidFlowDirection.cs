namespace MMO3D.Engine
{
    using System;
    using System.Collections.Specialized;
    using Petroules.Synteza;

    /// <summary>
    /// The direction in which a fluid should flow.
    /// </summary>
    public enum FluidFlowDirection
    {
        /// <summary>
        /// The fluid should flow east.
        /// </summary>
        East = 0,

        /// <summary>
        /// The fluid should flow north-east.
        /// </summary>
        Northeast = 45,

        /// <summary>
        /// The fluid should flow north.
        /// </summary>
        North = 90,

        /// <summary>
        /// The fluid should flow north-west.
        /// </summary>
        Northwest = 135,

        /// <summary>
        /// The fluid should flow west.
        /// </summary>
        West = 180,

        /// <summary>
        /// The fluid should flow south-west.
        /// </summary>
        Southwest = 225,

        /// <summary>
        /// The fluid should flow south.
        /// </summary>
        South = 270,

        /// <summary>
        /// The fluid should flow south-east.
        /// </summary>
        Southeast = 315
    }

    /// <summary>
    /// Extensions for the FluidFlowDirection enumeration.
    /// </summary>
    public static class FluidFlowDirectionExtensions
    {
        /// <summary>
        /// Parses an enumeration constant from a string. If no equivalent is found,
        /// the constant representing none, null, empty or default will be returned.
        /// </summary>
        /// <param name="enumConstant">The string to parse.</param>
        /// <returns>See summary.</returns>
        public static FluidFlowDirection ParseFromString(string enumConstant)
        {
            return EnumHelper.ParseFromString<FluidFlowDirection>(enumConstant, FluidFlowDirection.East);
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
            return EnumHelper.GetSortedList<FluidFlowDirection>(FluidFlowDirection.East);
        }
    }
}
