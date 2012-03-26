namespace MMO3D.Engine
{
    using System;

    /// <summary>
    /// Enumerates the vertices of a <see cref="QuadNode"/>.
    /// </summary>
    [Flags]
    public enum NodeContents
    {
        /// <summary>
        /// Default value.
        /// </summary>
        None = 0,

        /// <summary>
        /// The northwest vertex of the <see cref="QuadNode"/>.
        /// </summary>
        NorthwestVertex = 1,

        /// <summary>
        /// The northeast vertex of the <see cref="QuadNode"/>.
        /// </summary>
        NortheastVertex = 2,

        /// <summary>
        /// The southeast vertex of the <see cref="QuadNode"/>.
        /// </summary>
        SoutheastVertex = 4,

        /// <summary>
        /// The southwest vertex of the <see cref="QuadNode"/>.
        /// </summary>
        SouthwestVertex = 8,

        /// <summary>
        /// The center vertex of the <see cref="QuadNode"/>.
        /// </summary>
        CenterVertex = 16,

        /// <summary>
        /// The west vertex of the <see cref="QuadNode"/>.
        /// </summary>
        WestVertex = 32,

        /// <summary>
        /// The north vertex of the <see cref="QuadNode"/>.
        /// </summary>
        NorthVertex = 64,

        /// <summary>
        /// The east vertex of the <see cref="QuadNode"/>.
        /// </summary>
        EastVertex = 128,

        /// <summary>
        /// The south vertex of the <see cref="QuadNode"/>.
        /// </summary>
        SouthVertex = 256,

        /// <summary>
        /// The northwest child of the <see cref="QuadNode"/>.
        /// </summary>
        NorthwestChild = 512,

        /// <summary>
        /// The northeast child of the <see cref="QuadNode"/>.
        /// </summary>
        NortheastChild = 1024,

        /// <summary>
        /// The southwest child of the <see cref="QuadNode"/>.
        /// </summary>
        SouthwestChild = 2048,

        /// <summary>
        /// The southeast child of the <see cref="QuadNode"/>.
        /// </summary>
        SoutheastChild = 4096
    }
}
