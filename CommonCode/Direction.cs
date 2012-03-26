namespace MMO3D.CommonCode
{
    using System;

    /// <summary>
    /// Used for directional-movement based operations (mostly players and NPCs moving forward and backward).
    /// </summary>
    [Serializable]
    public enum Direction
    {
        /// <summary>
        /// Indicates no movement.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates forward directional movement.
        /// </summary>
        Forward = 1,

        /// <summary>
        /// Indicates backward directional movement.
        /// </summary>
        Backward = 2
    }
}
