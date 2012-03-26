namespace MMO3D.Engine
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Data buffers.
    /// </summary>
    public struct BuffersData : IDisposable, IEquatable<BuffersData>
    {
        /// <summary>
        /// Gets or sets the associated vertex buffer.
        /// </summary>
        /// <value>See summary.</value>
        public VertexBuffer VertexBuffer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the associated index buffer.
        /// </summary>
        /// <value>See summary.</value>
        public IndexBuffer IndexBuffer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of vertices.
        /// </summary>
        /// <value>See summary.</value>
        public int NumberOfVertices
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of indexes.
        /// </summary>
        /// <value>See summary.</value>
        public int NumberOfIndexes
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
        public static bool operator ==(BuffersData obj1, BuffersData obj2)
        {
            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Determines whether the specified instances are inequal.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator !=(BuffersData obj1, BuffersData obj2)
        {
            return !obj1.Equals(obj2);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">Another object to compare to.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(BuffersData other)
        {
            if (other == null)
            {
                return false;
            }

            return this.VertexBuffer == other.VertexBuffer && this.IndexBuffer == other.IndexBuffer && this.NumberOfVertices == other.NumberOfVertices && this.NumberOfIndexes == other.NumberOfIndexes;
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

            return this.Equals((BuffersData)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.VertexBuffer.GetHashCode() ^ this.IndexBuffer.GetHashCode() ^ this.NumberOfVertices ^ this.NumberOfIndexes;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.VertexBuffer != null)
                {
                    this.VertexBuffer.Dispose();
                }

                if (this.IndexBuffer != null)
                {
                    this.IndexBuffer.Dispose();
                }
            }
        }
    }
}
