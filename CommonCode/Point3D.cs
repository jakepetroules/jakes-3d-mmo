namespace MMO3D.CommonCode
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Defines a point in 3D space.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Point3D : IEquatable<Point3D>
    {
        /// <summary>
        /// The point (0,0,0).
        /// </summary>
        private static readonly Point3D zero = new Point3D();

        /// <summary>
        /// Initializes a new instance of the Point3D struct.
        /// </summary>
        /// <param name="x">The x-coordinate of the Point3D.</param>
        /// <param name="y">The y-coordinate of the Point3D.</param>
        /// <param name="z">The z-coordinate of the Point3D.</param>
        public Point3D(int x, int y, int z)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Gets the point (0,0,0).
        /// </summary>
        /// <value>The point (0,0,0).</value>
        public static Point3D Zero
        {
            get { return Point3D.zero; }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the Point3D.
        /// </summary>
        /// <value>See summary.</value>
        public int X
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the Point3D.
        /// </summary>
        /// <value>See summary.</value>
        public int Y
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the z-coordinate of the Point3D.
        /// </summary>
        /// <value>See summary.</value>
        public int Z
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
        public static bool operator ==(Point3D obj1, Point3D obj2)
        {
            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Determines whether the specified instances are inequal.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator !=(Point3D obj1, Point3D obj2)
        {
            return !obj1.Equals(obj2);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">Another object to compare to.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(Point3D other)
        {
            if (other == null)
            {
                return false;
            }

            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
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

            return this.Equals((Point3D)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode();
        }

        /// <summary>
        /// Returns a System.String that represents the current Point3D.
        /// </summary>
        /// <returns>A System.String that represents the current Point3D.</returns>
        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2}}}", new object[] { this.X.ToString(currentCulture), this.Y.ToString(currentCulture), this.Z.ToString(currentCulture) });
        }
    }
}
