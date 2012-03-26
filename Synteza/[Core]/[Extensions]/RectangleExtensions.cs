namespace Petroules.Synteza
{
    using System.Drawing;

    /// <summary>
    /// Provides extensions to the <see cref="Rectangle"/> class.
    /// </summary>
    public static class RectangleExtensions
    {
        /// <summary>
        /// Adds a point, specified by the integer arguments <paramref name="newX"/>
        /// and <paramref name="newY"/>, to this <see cref="Rectangle"/>. The resulting
        /// <see cref="Rectangle"/> is the smallest <see cref="Rectangle"/> that contains
        /// both the original <see cref="Rectangle"/> and the specified point.
        /// </summary>
        /// <param name="rectangle">The rectangle to add the point to.</param>
        /// <param name="newX">The X coordinate of the new point.</param>
        /// <param name="newY">The Y coordinate of the new point.</param>
        /// <returns>See summary.</returns>
        public static Rectangle Add(this Rectangle rectangle, int newX, int newY)
        {
            int x1 = System.Math.Min(rectangle.X, newX);
            int x2 = System.Math.Max(rectangle.X + rectangle.Width, newX);
            int y1 = System.Math.Min(rectangle.Y, newY);
            int y2 = System.Math.Max(rectangle.Y + rectangle.Height, newY);

            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Adds the specified <see cref="Point"/> to this <see cref="Rectangle"/>.
        /// The resulting <see cref="Rectangle"/> is the smallest <see cref="Rectangle"/>
        /// that contains both the original <see cref="Rectangle"/> and the specified
        /// <see cref="Point"/>.
        /// </summary>
        /// <param name="rectangle">The rectangle to add the point to.</param>
        /// <param name="point">The new point to add to this rectangle.</param>
        /// <returns>See summary.</returns>
        public static Rectangle Add(this Rectangle rectangle, Point point)
        {
            return rectangle.Add(point.X, point.Y);
        }
    }
}
