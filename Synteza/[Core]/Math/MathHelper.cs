namespace Petroules.Synteza.Math
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Provides various mathematical helper methods.
    /// </summary>
    public static class MathHelper
    {
		/// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
        /// This value is accurate to 35 decimal places, much more so than the value provided by <see cref="Math.PI"/>, which is 3.14159.
        /// </summary>
        public const double π = 3.14159265358979323846264338327950288;
		
        /// <summary>
        /// Defines the value of Pi divided by two as a <see cref="float"/>.
        /// </summary>
        public const float PiOver2 = (float)MathHelper.PiOver2d;

        /// <summary>
        /// Defines the value of Pi divided by two as a <see cref="double"/>.
        /// </summary>
        public const double PiOver2d = π / 2;

        /// <summary>
        /// Defines the value of Pi divided by three as a <see cref="float"/>.
        /// </summary>
        public const float PiOver3 = (float)MathHelper.PiOver3d;

        /// <summary>
        /// Defines the value of Pi divided by three as a <see cref="double"/>.
        /// </summary>
        public const double PiOver3d = π / 3;

        /// <summary>
        /// Definesthe value of  Pi divided by four as a <see cref="float"/>.
        /// </summary>
        public const float PiOver4 = (float)MathHelper.PiOver4d;

        /// <summary>
        /// Definesthe value of  Pi divided by four as a <see cref="double"/>.
        /// </summary>
        public const double PiOver4d = π / 4;

        /// <summary>
        /// Defines the value of Pi divided by six as a <see cref="float"/>.
        /// </summary>
        public const float PiOver6 = (float)MathHelper.PiOver6d;

        /// <summary>
        /// Defines the value of Pi divided by six as a <see cref="double"/>.
        /// </summary>
        public const double PiOver6d = π / 6;

        /// <summary>
        /// Defines the value of Pi multiplied by two as a <see cref="float"/>.
        /// </summary>
        public const float TwoPi = (float)MathHelper.TwoPid;

        /// <summary>
        /// Defines the value of Pi multiplied by two as a <see cref="double"/>.
        /// </summary>
        public const double TwoPid = 2 * π;

        /// <summary>
        /// Defines the value of Pi multiplied by 3 and divided by two as a <see cref="float"/>.
        /// </summary>
        public const float ThreePiOver2 = (float)MathHelper.ThreePiOver2d;

        /// <summary>
        /// Defines the value of Pi multiplied by 3 and divided by two as a <see cref="double"/>.
        /// </summary>
        public const double ThreePiOver2d = (3 * π) / 2;

        /// <summary>
        /// Gets a value indicating whether <paramref name="number"/> is a power of 2.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <returns>
        /// Whether <paramref name="number"/> is a power of 2. This method always
        /// returns false if <paramref name="number"/> is less than or equal to zero.
        /// </returns>
        public static bool IsPowerOfTwo(int number)
        {
            return number > 0 && (number & (number - 1)) == 0;
        }

        /// <summary>
        /// Returns the nearest power of two to <paramref name="number"/>.
        /// </summary>
        /// <param name="number">The number to get the nearest power of two of.</param>
        /// <returns>
        /// The nearest power of two to <paramref name="number"/>. This method always
        /// returns 1 if <paramref name="number"/> is less than or equal to zero.
        /// </returns>
        public static int NearestPowerOfTwo(int number)
        {
            if (number <= 0)
            {
                return 1;
            }

            if (MathHelper.IsPowerOfTwo(number))
            {
                return number;
            }

            return (int)Math.Pow(2, Math.Ceiling(Math.Log(number, 2)));
        }

        /// <summary>
        /// Gets the aspect ratio of <paramref name="size"/>.
        /// </summary>
        /// <param name="size">A <see cref="Size"/> containing the width and height to get the aspect ratio of.</param>
        /// <returns>The aspect ratio of <paramref name="size"/>.</returns>
        public static float AspectRatio(this Size size)
        {
            return MathHelper.AspectRatio(size.Width, size.Height);
        }

        /// <summary>
        /// Gets the aspect ratio of <paramref name="width"/> and <paramref name="height"/>.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>The aspect ratio of <paramref name="width"/> and <paramref name="height"/>.</returns>
        public static float AspectRatio(int width, int height)
        {
            return (float)width / height;
        }

        /// <summary>
        /// Converts degrees to radians using the formula (d * (π / 180)).
        /// </summary>
        /// <param name="degrees">An angle in degrees.</param>
        /// <returns>The angle expressed in radians.</returns>
        public static float DegreesToRadians(float degrees)
        {
            return (float)MathHelper.DegreesToRadians((double)degrees);
        }

        /// <summary>
        /// Converts degrees to radians using the formula (d * (π / 180)).
        /// </summary>
        /// <param name="degrees">An angle in degrees.</param>
        /// <returns>The angle expressed in radians.</returns>
        public static double DegreesToRadians(double degrees)
        {
            return degrees * (π / 180);
        }

        /// <summary>
        /// Converts radians to degrees using the formula (r * (180 / π)).
        /// </summary>
        /// <param name="radians">An angle in radians.</param>
        /// <returns>The angle expressed in degrees.</returns>
        public static float RadiansToDegrees(float radians)
        {
            return (float)MathHelper.RadiansToDegrees((double)radians);
        }

        /// <summary>
        /// Converts radians to degrees using the formula (r * (180 / π)).
        /// </summary>
        /// <param name="radians">An angle in radians.</param>
        /// <returns>The angle expressed in degrees.</returns>
        public static double RadiansToDegrees(double radians)
        {
            return radians * (180 / π);
        }

        /// <summary>
        /// Calculates the factorial of a given natural number.
        /// </summary>
        /// <param name="n">The number to compute the factorial of.</param>
        /// <returns><paramref name="n"/>!</returns>
        public static int Factorial(int n)
        {
            return (int)MathHelper.Factorial((long)n);
        }

        /// <summary>
        /// Calculates the factorial of a given natural number.
        /// </summary>
        /// <param name="n">The number to compute the factorial of.</param>
        /// <returns><paramref name="n"/>!</returns>
        public static long Factorial(long n)
        {
            long result = 1;

            for (; n > 1; n--)
            {
                result *= n;
            }

            return result;
        }

        /// <summary>
        /// Returns the square root of a specified number.
        /// </summary>
        /// <param name="a">A number whose square root is to be found.</param>
        public static decimal Sqrt(decimal a)
        {
            return (decimal)Math.Sqrt((double)a);
        }

        /// <summary>
        /// Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="a">A number whose logarithm is to be found.</param>
        public static decimal Log10(decimal a)
        {
            return (decimal)Math.Log10((double)a);
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x">A number to be raised to a power.</param>
        /// <param name="y">A number that specifies a power.</param>
        public static decimal Pow(decimal x, decimal y)
        {
            return (decimal)Math.Pow((double)x, (double)y);
        }
    }
}
