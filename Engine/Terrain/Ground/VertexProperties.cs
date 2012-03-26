namespace MMO3D.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;

    /// <summary>
    /// Encapsulates a vertex coordinate, along with the ID of the patch it resides on,
    /// and the height and texture to set on it.
    /// </summary>
    public struct VertexProperties : IEquatable<VertexProperties>
    {
        /// <summary>
        /// Initializes a new instance of the VertexProperties struct.
        /// </summary>
        /// <param name="globalCoordinate">The global coordinate we are setting the properties of.</param>
        /// <param name="heightLevel">The height level of the terrain patch we are setting the properties of.</param>
        /// <param name="height">The change in height of this vertex.</param>
        /// <param name="terrainType">The terrain type to set on this vertex.</param>
        public VertexProperties(Point globalCoordinate, int heightLevel, float? height, TerrainType? terrainType)
            : this()
        {
            this.MainPatchId = TerrainManager.GetTerrainPatchId(MathExtensions.PointToVector3(globalCoordinate), heightLevel);
            this.GlobalCoordinate = globalCoordinate;
            this.Height = height;
            this.TerrainType = terrainType;
        }

        /// <summary>
        /// Gets the main patch ID this vertex belongs to.
        /// </summary>
        /// <value>See summary.</value>
        public Point3D MainPatchId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the global coordinate we are setting the properties of.
        /// </summary>
        /// <value>See summary.</value>
        public Point GlobalCoordinate
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the change in height of this vertex.
        /// </summary>
        /// <value>See summary.</value>
        public float? Height
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the terrain type to set on this vertex.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainType? TerrainType
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the specified instances are equal.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator ==(VertexProperties obj1, VertexProperties obj2)
        {
            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Determines whether the specified instances are inequal.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator !=(VertexProperties obj1, VertexProperties obj2)
        {
            return !obj1.Equals(obj2);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">Another object to compare to.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(VertexProperties other)
        {
            if (other == null)
            {
                return false;
            }

            return this.MainPatchId == other.MainPatchId && this.GlobalCoordinate == other.GlobalCoordinate && this.Height.GetValueOrDefault() == other.Height.GetValueOrDefault() && this.TerrainType.GetValueOrDefault() == other.TerrainType.GetValueOrDefault();
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>True if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((VertexProperties)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.MainPatchId.GetHashCode() ^ this.GlobalCoordinate.GetHashCode() ^ this.Height.GetHashCode() ^ this.TerrainType.GetHashCode();
        }
    }
}