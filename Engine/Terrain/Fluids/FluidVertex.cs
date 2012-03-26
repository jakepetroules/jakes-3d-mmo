namespace MMO3D.Engine
{
    using System;

    /// <summary>
    /// Enumerates the vertices of a fluid.
    /// </summary>
    [Serializable]
    public enum FluidVertex
    {
        /// <summary>
        /// The southwest vertex; this is the 1st vertex, at index 0.
        /// </summary>
        Southwest = 0,

        /// <summary>
        /// The southeast vertex; this is the 2nd vertex, at index 1.
        /// </summary>
        Southeast = 1,

        /// <summary>
        /// The northwest vertex; this is the 3rd vertex, at index 2.
        /// </summary>
        Northwest = 2,

        /// <summary>
        /// The northeast vertex; this is the 4th vertex, at index 3.
        /// </summary>
        Northeast = 3
    }
}
