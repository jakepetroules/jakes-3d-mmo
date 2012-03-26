namespace MMO3D.Engine
{
    /// <summary>
    /// Enumerates the positions of the vertices on a <see cref="QuadNode"/>.
    /// </summary>
    public enum NodeVertex
    {
        /// <summary>
        /// The center vertex of the <see cref="QuadNode"/>.
        /// </summary>
        Center = 0,

        /// <summary>
        /// The west vertex of the <see cref="QuadNode"/>.
        /// </summary>
        West = 1,

        /// <summary>
        /// The north vertex of the <see cref="QuadNode"/>.
        /// </summary>
        North = 2,

        /// <summary>
        /// The east vertex of the <see cref="QuadNode"/>.
        /// </summary>
        East = 3,

        /// <summary>
        /// The south vertex of the <see cref="QuadNode"/>.
        /// </summary>
        South = 4,

        /// <summary>
        /// The northwest vertex of the <see cref="QuadNode"/>.
        /// </summary>
        NorthWest = 5,

        /// <summary>
        /// The northeast vertex of the <see cref="QuadNode"/>.
        /// </summary>
        NorthEast = 6,

        /// <summary>
        /// The southeast vertex of the <see cref="QuadNode"/>.
        /// </summary>
        SouthEast = 7,

        /// <summary>
        /// The southwest vertex of the <see cref="QuadNode"/>.
        /// </summary>
        SouthWest = 8
    }
}
