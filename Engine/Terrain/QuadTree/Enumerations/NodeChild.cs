namespace MMO3D.Engine
{
    /// <summary>
    /// Enumerates the children of a <see cref="QuadNode"/>.
    /// </summary>
    public enum NodeChild
    {
        /// <summary>
        /// The northwest child of the <see cref="QuadNode"/>.
        /// </summary>
        Northwest = 0,

        /// <summary>
        /// The northeast child of the <see cref="QuadNode"/>.
        /// </summary>
        Northeast = 1,

        /// <summary>
        /// The southwest child of the <see cref="QuadNode"/>.
        /// </summary>
        Southwest = 2,

        /// <summary>
        /// The southeast child of the <see cref="QuadNode"/>.
        /// </summary>
        Southeast = 3
    }
}
