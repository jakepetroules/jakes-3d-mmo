namespace MMO3D.CommonCode
{
    using System;

    /// <summary>
    /// Represents a pseudo-random number generator, a device that produces a sequence
    /// of numbers that meet certain statistical requirements for randomness. This class
    /// is based off of System.Random and modified to suit the needs of a game.
    /// </summary>
    public static class GameRandom
    {
        /// <summary>
        /// The internal instance of random to be used for the entire lifetime of the program.
        /// </summary>
        private static Random internalRandom;

        /// <summary>
        /// Gets the internal instance of <see cref="System.Random"/>.
        /// </summary>
        /// <value>See summary.</value>
        private static Random Instance
        {
            get { return GameRandom.internalRandom ?? (GameRandom.internalRandom = new Random()); }
        }

        /// <summary>
        /// Returns a nonnegative random number.
        /// </summary>
        /// <returns>A 32-bit signed integer greater than or equal to zero and less than System.Int32.MaxValue.</returns>
        public static int Next()
        {
            return GameRandom.Instance.Next();
        }

        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number to be generated.
        /// maxValue must be greater than or equal to zero.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to zero, and less than maxValue;
        /// that is, the range of return values ordinarily includes zero but not maxValue.
        /// However, if maxValue equals zero, maxValue is returned.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">maxValue is less than zero.</exception>
        public static int Next(int maxValue)
        {
            return GameRandom.Instance.Next(maxValue);
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to minValue and less than maxValue;
        /// that is, the range of return values includes minValue but not maxValue.
        /// If minValue equals maxValue, minValue is returned.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">minValue is greater than maxValue.</exception>
        public static int Next(int minValue, int maxValue)
        {
            return GameRandom.Instance.Next(minValue, maxValue);
        }

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        /// <exception cref="System.ArgumentNullException">buffer is null.</exception>
        public static void NextBytes(byte[] buffer)
        {
            GameRandom.Instance.NextBytes(buffer);
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns>A double-precision floating point number greater than or equal to 0.0, and less than 1.0.</returns>
        public static double NextDouble()
        {
            return GameRandom.Instance.NextDouble();
        }
    }
}
