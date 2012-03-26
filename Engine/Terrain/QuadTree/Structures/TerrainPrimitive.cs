namespace MMO3D.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines a terrain primitive (a triangle).
    /// </summary>
    public struct TerrainPrimitive : IEquatable<TerrainPrimitive>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainPrimitive"/> struct.
        /// </summary>
        /// <param name="index1">First index of the used vertex for this primitive.</param>
        /// <param name="index2">Second index of the used vertex for this primitive.</param>
        /// <param name="index3">Third index of the used vertex for this primitive.</param>
        public TerrainPrimitive(int index1, int index2, int index3)
            : this()
        {
            this.Index1 = index1;
            this.Index2 = index2;
            this.Index3 = index3;
        }

        /// <summary>
        /// Gets or sets the first index of the used vertex for this primitive.
        /// </summary>
        /// <value>See summary.</value>
        public int Index1
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the second index of the used vertex for this primitive.
        /// </summary>
        /// <value>See summary.</value>
        public int Index2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the third index of the used vertex for this primitive.
        /// </summary>
        /// <value>See summary.</value>
        public int Index3
        {
            get;
            set;
        }

        /// <summary>
        /// Determines whether the specified instances are equal.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator ==(TerrainPrimitive obj1, TerrainPrimitive obj2)
        {
            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Determines whether the specified instances are inequal.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator !=(TerrainPrimitive obj1, TerrainPrimitive obj2)
        {
            return !obj1.Equals(obj2);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">Another object to compare to.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(TerrainPrimitive other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Index1 == other.Index1 && this.Index2 == other.Index2 && this.Index3 == other.Index3;
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

            return this.Equals((TerrainPrimitive)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.Index1 ^ this.Index2 ^ this.Index3;
        }

        /// <summary>
        /// Gets a plane from the set of indices.
        /// </summary>
        /// <param name="vertices">The vertices used to generate the plane.</param>
        /// <returns>See summary.</returns>
        public Plane GetPlane(Vector<VertexPositionNormalMultipleTexture> vertices)
        {
            return new Plane(vertices[this.Index1].Position, vertices[this.Index2].Position, vertices[this.Index3].Position);
        }

        /// <summary>
        /// Gets the normal of a plane from the set of indices.
        /// </summary>
        /// <param name="vertices">The vertices used to generate the plane whose normal we will retrieve.</param>
        /// <returns>See summary.</returns>
        public Vector3 GetNormal(Vector<VertexPositionNormalMultipleTexture> vertices)
        {
            return this.GetPlane(vertices).Normal;
        }

        /// <summary>
        /// Sets the normal on the specified vertices.
        /// </summary>
        /// <param name="vertices">The vertices to set the normal of.</param>
        public void SetNormal(Vector<VertexPositionNormalMultipleTexture> vertices)
        {
            Vector3 normal = this.GetNormal(vertices);

            VertexPositionNormalMultipleTexture vertex = vertices[this.Index1];
            vertex.Normal += normal;
            vertex.Normal.Normalize();
            vertices[this.Index1] = vertex;

            vertex = vertices[this.Index2];
            vertex.Normal += normal;
            vertex.Normal.Normalize();
            vertices[this.Index2] = vertex;

            vertex = vertices[this.Index3];
            vertex.Normal += normal;
            vertex.Normal.Normalize();
            vertices[this.Index3] = vertex;
        }
    }
}
