namespace MMO3D.Engine
{
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines a terrain vertex.
    /// </summary>
    public sealed class TerrainVertex
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainVertex"/> class.
        /// </summary>
        /// <param name="value">The back vertex value.</param>
        public TerrainVertex(VertexPositionNormalMultipleTexture value)
        {
            this.BufferIndex = 0;
            this.LastUsedIteration = -1;
            this.References = new Vector<QuadNode>();
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the index of the current vertex inside the vertex buffer since the last iteration.
        /// </summary>
        /// <value>See summary.</value>
        public int BufferIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last iteration identifier where the current vertex was used.
        /// </summary>
        /// <value>See summary.</value>
        public int LastUsedIteration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the current vertex is enabled.
        /// </summary>
        /// <value>See summary.</value>
        /// <remarks>A vertex is enabled when referenced by one node minimum.</remarks>
        public bool Enabled
        {
            get { return this.References.Count > 0; }
        }

        /// <summary>
        /// Gets or sets the back vertex value.
        /// </summary>
        /// <value>See summary.</value>
        public VertexPositionNormalMultipleTexture Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of nodes that reference the current <see cref="TerrainVertex"/>.
        /// </summary>
        /// <value>See summary.</value>
        public Vector<QuadNode> References
        {
            get;
            private set;
        }

        /// <summary>
        /// Adds a reference to a <see cref="QuadNode"/>.
        /// </summary>
        /// <param name="node">The <see cref="QuadNode"/> to get a reference to.</param>
        public void AddReferenceTo(QuadNode node)
        {
            if (this.References.IndexOf(node) >= 0)
            {
                return;
            }

            this.References.Add(node);
        }

        /// <summary>
        /// Removes the reference from a <see cref="QuadNode"/>.
        /// </summary>
        /// <param name="node">The <see cref="QuadNode"/> to remove the reference from.</param>
        public void RemoveReferenceFrom(QuadNode node)
        {
            this.References.Remove(node);
        }
    }
}
