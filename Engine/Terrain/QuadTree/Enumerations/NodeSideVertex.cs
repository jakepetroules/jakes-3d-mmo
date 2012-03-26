namespace MMO3D.Engine
{
    /// <summary>
    /// Enumerates the positions of the vertices positioned on the side of a <see cref="QuadNode"/>.
    /// </summary>
    public enum NodeSideVertex
    {
        /// <summary>
        /// The east vertex of the <see cref="QuadNode"/>.
        /// </summary>
        East = 0,

        /// <summary>
        /// The north vertex of the <see cref="QuadNode"/>.
        /// </summary>
        North = 1,

        /// <summary>
        /// The west vertex of the <see cref="QuadNode"/>.
        /// </summary>
        West = 2,

        /// <summary>
        /// The south vertex of the <see cref="QuadNode"/>.
        /// </summary>
        South = 3,

        /// <summary>
        /// The center vertex of the <see cref="QuadNode"/>.
        /// </summary>
        Center = 4
    }
}
