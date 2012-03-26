namespace MMO3D.CommonCode
{
    using System;
    using System.Globalization;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Provides mathematical helper methods.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Gets the normal vector of a triangle.
        /// </summary>
        /// <param name="point1">The first point of the triangle.</param>
        /// <param name="point2">The second point of the triangle.</param>
        /// <param name="point3">The third point of the triangle.</param>
        /// <returns>See summary.</returns>
        public static Vector3 GetNormal(Vector3 point1, Vector3 point2, Vector3 point3)
        {
            Vector3 side1 = point2 - point1; // 1 - 3
            Vector3 side2 = point1 - point3; // 1 - 2

            Vector3 normal = Vector3.Cross(side1, side2);
            normal.Normalize();

            return normal;
        }

        /// <summary>
        /// Converts a System.String in the format "{X:0 Y:0 Z:0}" to a MMO3D.Engine.Point3D.
        /// </summary>
        /// <param name="data">The System.String to convert.</param>
        /// <returns>See summary.</returns>
        public static Point3D StringToPoint(string data)
        {
            try
            {
                string[] split = data.Trim('{', '}').Split(' ');

                return new Point3D(Convert.ToInt32(split[0].Replace("X:", string.Empty), CultureInfo.InvariantCulture), Convert.ToInt32(split[1].Replace("Y:", string.Empty), CultureInfo.InvariantCulture), Convert.ToInt32(split[2].Replace("Z:", string.Empty), CultureInfo.InvariantCulture));
            }
            catch
            {
                throw new ArgumentException("Must be in the format: {X:0 Y:0 Z:0}", "data");
            }
        }

        /// <summary>
        /// Gets the index in a one-dimensional array, from an X and X coordinate.
        /// </summary>
        /// <param name="location">The Vector2 containing the X and Y coordinates.</param>
        /// <param name="squareRootHeights">The square root of the length of the heights array.</param>
        /// <returns>See summary.</returns>
        public static int GetIndex(Vector2 location, int squareRootHeights)
        {
            return MathExtensions.GetIndex((int)location.X, (int)location.Y, squareRootHeights);
        }

        /// <summary>
        /// Gets the index in a one-dimensional array, from an X and X coordinate.
        /// </summary>
        /// <param name="location">The Point containing the X and Y coordinates.</param>
        /// <param name="squareRootHeights">The square root of the length of the heights array.</param>
        /// <returns>See summary.</returns>
        public static int GetIndex(Point location, int squareRootHeights)
        {
            return MathExtensions.GetIndex((int)location.X, (int)location.Y, squareRootHeights);
        }

        /// <summary>
        /// Gets the index in a one-dimensional array, from an X and Y coordinate.
        /// </summary>
        /// <param name="locationX">The X coordinate.</param>
        /// <param name="locationY">The Y coordinate.</param>
        /// <param name="squareRootHeights">The square root of the length of the heights array.</param>
        /// <returns>See summary.</returns>
        public static int GetIndex(int locationX, int locationY, int squareRootHeights)
        {
            return locationX + (locationY * squareRootHeights);
        }

        /// <summary>
        /// Gets the exact height of any point on a <b>square</b> grid using linear interpolation.
        /// If any out of range or null reference exception occurs, 0 is returned.
        /// </summary>
        /// <param name="location">The location on the grid.</param>
        /// <param name="heights">
        /// The heights on the grid. The values in this parameter must be arranged
        /// by their X values, and then by their Y values. For example, index 0
        /// would be X:0,Y:0, index 1 would be X:0:Y:1 and index 2 would be X:1:Y0.</param>
        /// <returns>See summary.</returns>
        public static float GetExactHeight(Vector2 location, float[] heights)
        {
            try
            {
                int lowerX = (int)location.X;
                int higherX = lowerX + 1;
                float relativeX = (location.X - lowerX) / ((float)higherX - (float)lowerX);

                int lowerY = (int)location.Y;
                int higherY = lowerY + 1;
                float relativeY = (location.Y - lowerY) / ((float)higherY - (float)lowerY);

                float heightLxLy = heights[MathExtensions.GetIndex(lowerX, lowerY, (int)Math.Sqrt(heights.Length))];
                float heightLxHy = heights[MathExtensions.GetIndex(lowerX, higherY, (int)Math.Sqrt(heights.Length))];
                float heightHxLy = heights[MathExtensions.GetIndex(higherX, lowerY, (int)Math.Sqrt(heights.Length))];
                float heightHxHy = heights[MathExtensions.GetIndex(higherX, higherY, (int)Math.Sqrt(heights.Length))];

                bool cameraAboveLowerTriangle = (relativeX + relativeY) < 1;

                float finalHeight;
                if (cameraAboveLowerTriangle)
                {
                    finalHeight = heightLxLy;
                    finalHeight += relativeY * (heightLxHy - heightLxLy);
                    finalHeight += relativeX * (heightHxLy - heightLxLy);
                }
                else
                {
                    finalHeight = heightHxHy;
                    finalHeight += (1.0f - relativeY) * (heightHxLy - heightHxHy);
                    finalHeight += (1.0f - relativeX) * (heightLxHy - heightHxHy);
                }

                return finalHeight;
            }
            catch (IndexOutOfRangeException)
            {
                return 0;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the average of an arbitrary number of arguments.
        /// </summary>
        /// <param name="arguments">The arguments to determine the average of.</param>
        /// <returns>See summary.</returns>
        public static float Average(params float[] arguments)
        {
            float sum = 0;

            for (int i = 0; i < arguments.Length; i++)
            {
                sum += arguments[i];
            }

            return sum / arguments.Length;
        }

        /// <summary>
        /// Adds two Microsoft.Xna.Framework.Point objects.
        /// </summary>
        /// <param name="point1">The first Microsoft.Xna.Framework.Point to add.</param>
        /// <param name="point2">The second Microsoft.Xna.Framework.Point to add.</param>
        /// <returns>The sum of the two Microsoft.Xna.Framework.Point objects.</returns>
        public static Point AddPoints(Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }

        /// <summary>
        /// Adds two MMO3D.Engine.Point3D objects.
        /// </summary>
        /// <param name="point1">The first MMO3D.Engine.Point3D to add.</param>
        /// <param name="point2">The second MMO3D.Engine.Point3D to add.</param>
        /// <returns>The sum of the two MMO3D.Engine.Point3D objects.</returns>
        public static Point3D AddPoints(Point3D point1, Point3D point2)
        {
            return new Point3D(point1.X + point2.X, point1.Y + point2.Y, point1.Z + point2.Z);
        }

        /// <summary>
        /// Gets the angle of rotation on the X axis.
        /// </summary>
        /// <param name="angle">The angle, in radians.</param>
        /// <returns>See summary.</returns>
        public static float GetRotationX(float angle)
        {
            return (float)Math.Cos(angle);
        }

        /// <summary>
        /// Gets the angle of rotation on the Y axis.
        /// </summary>
        /// <param name="angle">The angle, in radians.</param>
        /// <returns>See summary.</returns>
        public static float GetRotationY(float angle)
        {
            return (float)Math.Sin(angle);
        }

        /// <summary>
        /// Reduces the value so it fits in the range 0 to amount - 1.
        /// This is mainly for degree calculations, e.g. passing 360 or 720
        /// and 360 for amount both yield 0. 719 yields 359, etc...
        /// </summary>
        /// <param name="value">The value to modulate.</param>
        /// <param name="amount">The amount to modulate by.</param>
        /// <returns>The modulated value.</returns>
        public static double Modulate(double value, double amount)
        {
            if (value < 0)
            {
                value += (double)Math.Ceiling(Math.Abs(value) / amount) * amount;
            }

            return value % amount;
        }

        /// <summary>
        /// Reduces the value so it fits in the range 0 to amount - 1.
        /// This is mainly for degree calculations, e.g. passing 360 or 720
        /// and 360 for amount both yield 0. 719 yields 359, etc...
        /// </summary>
        /// <param name="vector">A vector containing the values to modulate.</param>
        /// <param name="amount">The amount to modulate by.</param>
        /// <returns>The modulated value.</returns>
        public static Vector2 Modulate(Vector2 vector, double amount)
        {
            return new Vector2((float)MathExtensions.Modulate(vector.X, amount), (float)MathExtensions.Modulate(vector.Y, amount));
        }

        /// <summary>
        /// Reduces the value so it fits in the range 0 to amount - 1.
        /// This is mainly for degree calculations, e.g. passing 360 or 720
        /// and 360 for amount both yield 0. 719 yields 359, etc...
        /// </summary>
        /// <param name="vector">A vector containing the values to modulate.</param>
        /// <param name="amount">The amount to modulate by.</param>
        /// <returns>The modulated value.</returns>
        public static Vector3 Modulate(Vector3 vector, double amount)
        {
            return new Vector3((float)MathExtensions.Modulate(vector.X, amount), (float)MathExtensions.Modulate(vector.Y, amount), (float)MathExtensions.Modulate(vector.Z, amount));
        }

        /// <summary>
        /// Reduces the value so it fits in the range 0 to amount - 1.
        /// This is mainly for degree calculations, e.g. passing 360 or 720
        /// and 360 for amount both yield 0. 719 yields 359, etc...
        /// </summary>
        /// <param name="vector">A vector containing the values to modulate.</param>
        /// <param name="amount">The amount to modulate by.</param>
        /// <returns>The modulated value.</returns>
        public static Vector4 Modulate(Vector4 vector, double amount)
        {
            return new Vector4((float)MathExtensions.Modulate(vector.X, amount), (float)MathExtensions.Modulate(vector.Y, amount), (float)MathExtensions.Modulate(vector.Z, amount), (float)MathExtensions.Modulate(vector.W, amount));
        }

        /// <summary>
        /// Truncates a Vector3 to a Vector2.
        /// </summary>
        /// <param name="vector">The vector to truncate.</param>
        /// <returns>See summary.</returns>
        public static Vector2 TruncateVector(Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        /// <summary>
        /// Truncates a Vector4 to a Vector3.
        /// </summary>
        /// <param name="vector">The vector to truncate.</param>
        /// <returns>See summary.</returns>
        public static Vector3 TruncateVector(Vector4 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Converts a <see cref="Microsoft.Xna.Framework.Vector2"/> into a <see cref="Microsoft.Xna.Framework.Point"/>.
        /// </summary>
        /// <param name="vector">The vector to convert to a point.</param>
        /// <returns>See summary.</returns>
        public static Point VectorToPoint(Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        /// <summary>
        /// Converts a <see cref="Microsoft.Xna.Framework.Vector3"/> into a <see cref="Microsoft.Xna.Framework.Point"/>.
        /// </summary>
        /// <param name="vector">The vector to convert to a point.</param>
        /// <returns>See summary.</returns>
        public static Point VectorToPoint(Vector3 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        /// <summary>
        /// Converts a <see cref="Microsoft.Xna.Framework.Point"/> into a <see cref="Microsoft.Xna.Framework.Vector2"/>.
        /// </summary>
        /// <param name="point">The point to convert to a vector.</param>
        /// <returns>See summary.</returns>
        public static Vector2 PointToVector2(Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        /// <summary>
        /// Converts a <see cref="Microsoft.Xna.Framework.Point"/> into a <see cref="Microsoft.Xna.Framework.Vector3"/>.
        /// </summary>
        /// <param name="point">The point to convert to a vector.</param>
        /// <returns>See summary.</returns>
        public static Vector3 PointToVector3(Point point)
        {
            return new Vector3(point.X, point.Y, 0);
        }
    }
}
