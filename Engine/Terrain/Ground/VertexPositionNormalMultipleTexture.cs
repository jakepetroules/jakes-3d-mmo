namespace MMO3D.Engine
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MMO3D.CommonCode;

    /// <summary>
    /// Describes a custom vertex format structure that contains position, normal data, and one set of texture coordinates whose Z component specifies a texture index.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionNormalMultipleTexture : IEquatable<VertexPositionNormalMultipleTexture>
    {
        /// <summary>
        /// An array of three vertex elements describing the position, normal, and texture coordinate of this vertex.
        /// </summary>
        private static readonly VertexElement[] vertexElements = new VertexElement[]
        {
            new VertexElement(0, 0, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Position, 0),
            new VertexElement(0, 12, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Normal, 0),
            new VertexElement(0, 24, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 0)
        };

        /// <summary>
        /// The vertex position.
        /// </summary>
        private Vector3 position;

        /// <summary>
        /// The vertex normal.
        /// </summary>
        private Vector3 normal;

        /// <summary>
        /// The texture coordinates.
        /// </summary>
        private Vector3 textureCoordinate;

        /// <summary>
        /// The terrain type.
        /// </summary>
        private TerrainType terrainType;

        /// <summary>
        /// Initializes a new instance of the VertexPositionNormalMultipleTexture struct.
        /// </summary>
        /// <param name="position">Position of the vertex.</param>
        /// <param name="normal">The vertex normal.</param>
        /// <param name="textureCoordinate">The texture coordinate.</param>
        public VertexPositionNormalMultipleTexture(Vector3 position, Vector3 normal, Vector2 textureCoordinate)
            : this()
        {
            this.position = position;
            this.normal = normal;
            this.textureCoordinate = new Vector3(textureCoordinate, 0);
            this.TerrainType = TerrainType.UndefinedTerrain;
        }

        /// <summary>
        /// Gets the size of the VertexPositionNormalMultipleTexture struct.
        /// </summary>
        /// <value>The size of the struct, in bytes.</value>
        public static int SizeInBytes
        {
            get { return Marshal.SizeOf(typeof(VertexPositionNormalMultipleTexture)); }
        }

        /// <summary>
        /// Gets or sets the vertex position.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        /// <summary>
        /// Gets or sets the vertex normal.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 Normal
        {
            get { return this.normal; }
            set { this.normal = value; }
        }

        /// <summary>
        /// Gets or sets the vertex texture coordinates.
        /// </summary>
        /// <value>See summary.</value>
        public Vector2 TextureCoordinate
        {
            get
            {
                return MathExtensions.TruncateVector(this.textureCoordinate);
            }

            set
            {
                this.textureCoordinate = new Vector3(value, (int)this.TerrainType);
            }
        }

        /// <summary>
        /// Gets or sets the terrain type to use for this vertex.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainType TerrainType
        {
            get
            {
                return this.terrainType;
            }

            set
            {
                this.terrainType = value;
                this.textureCoordinate.Z = (int)value;
            }
        }

        /// <summary>
        /// Gets an array of three vertex elements describing the position, normal, and texture coordinate of this vertex.
        /// </summary>
        /// <returns>See summary.</returns>
        public static VertexElement[] GetVertexElements()
        {
            return VertexPositionNormalMultipleTexture.vertexElements.Clone() as VertexElement[];
        }

        /// <summary>
        /// Determines whether the specified instances are equal.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator ==(VertexPositionNormalMultipleTexture obj1, VertexPositionNormalMultipleTexture obj2)
        {
            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Determines whether the specified instances are inequal.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator !=(VertexPositionNormalMultipleTexture obj1, VertexPositionNormalMultipleTexture obj2)
        {
            return !obj1.Equals(obj2);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">Another object to compare to.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(VertexPositionNormalMultipleTexture other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Position == other.Position && this.Normal == other.Normal && this.TerrainType == other.TerrainType && this.TextureCoordinate == other.TextureCoordinate;
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

            return this.Equals((VertexPositionNormalMultipleTexture)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.Position.GetHashCode() ^ this.Normal.GetHashCode() ^ (int)this.TerrainType ^ this.TextureCoordinate.GetHashCode();
        }

        /// <summary>
        /// Retrieves a string representation of this object.
        /// </summary>
        /// <returns>String representation of this object.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Position: {0} Normal: {1} TerrainType: {2} TextureCoordinate: {3}}}", this.Position, this.Normal, this.TerrainType, this.TextureCoordinate);
        }
    }
}
