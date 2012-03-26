namespace MMO3D.Engine
{
    using System.Globalization;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;

    /// <summary>
    /// Encapsulates a terrain patch ID and vertex coordinate.
    /// </summary>
    public struct TerrainPatchVertexCoordinates
    {
        /// <summary>
        /// The vertex coordinate.
        /// </summary>
        private Point vertexCoordinate;

        /// <summary>
        /// Initializes a new instance of the TerrainPatchVertexCoordinates struct.
        /// </summary>
        /// <param name="patchId">The ID of the terrain patch this vertex belongs to.</param>
        /// <param name="vertexCoordinate">The vertex coordinate.</param>
        public TerrainPatchVertexCoordinates(Point3D patchId, Point vertexCoordinate)
            : this()
        {
            this.PatchId = patchId;
            this.VertexCoordinate = vertexCoordinate;
        }

        /// <summary>
        /// Gets or sets the ID of the terrain patch this vertex belongs to.
        /// </summary>
        /// <value>See summary.</value>
        public Point3D PatchId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the vertex coordinate.
        /// </summary>
        /// <value>See summary.</value>
        public Point VertexCoordinate
        {
            get { return this.vertexCoordinate; }
            set { this.vertexCoordinate = new Point((int)MathHelper.Clamp(value.X, 0, TerrainPatch.PatchSize + 1), (int)MathHelper.Clamp(value.Y, 0, TerrainPatch.PatchSize + 1)); }
        }

        /// <summary>
        /// Determines whether the specified instances are equal.
        /// </summary>
        /// <param name="terrainPatchVertexCoordinates1">The first instance.</param>
        /// <param name="terrainPatchVertexCoordinates2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator ==(TerrainPatchVertexCoordinates terrainPatchVertexCoordinates1, TerrainPatchVertexCoordinates terrainPatchVertexCoordinates2)
        {
            return terrainPatchVertexCoordinates1.PatchId == terrainPatchVertexCoordinates2.PatchId && terrainPatchVertexCoordinates1.VertexCoordinate == terrainPatchVertexCoordinates2.VertexCoordinate;
        }

        /// <summary>
        /// Determines whether the specified instances are inequal.
        /// </summary>
        /// <param name="terrainPatchVertexCoordinates1">The first instance.</param>
        /// <param name="terrainPatchVertexCoordinates2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator !=(TerrainPatchVertexCoordinates terrainPatchVertexCoordinates1, TerrainPatchVertexCoordinates terrainPatchVertexCoordinates2)
        {
            return !(terrainPatchVertexCoordinates1 == terrainPatchVertexCoordinates2);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>True if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return (TerrainPatchVertexCoordinates)obj == this;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.PatchId.GetHashCode() ^ this.VertexCoordinate.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> representing the current <see cref="MMO3D.Engine.TerrainPatchVertexCoordinates"/>.
        /// </summary>
        /// <returns>See summary.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{{Patch ID: {0} Vertex Coordinate: {1}}}", this.PatchId, this.VertexCoordinate);
        }
    }
}
