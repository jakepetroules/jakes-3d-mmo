namespace Petroules.Synteza.IO
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    /// <summary>
    /// Provides utility methods to assist with the application update process.
    /// </summary>
    public static class ChecksumUtilities
    {
        /// <summary>
        /// Performs a checksum of the specified file and returns a value indicating whether it is intact.
        /// </summary>
        /// <param name="fileName">The name of the file to perform the checksum on.</param>
        /// <param name="checksum">The valid checksum to compare to the actual file's checksum.</param>
        /// <param name="algorithm">The algorithm used to calculate the checksums.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> is <c>null</c> or white space. -or- <paramref name="checksum"/> is <c>null</c>.</exception>
        public static bool PerformChecksum(string fileName, string checksum, ChecksumAlgorithm algorithm)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(checksum))
            {
                throw new ArgumentNullException(string.IsNullOrEmpty(fileName) ? "fileName" : "checksum");
            }

            return ChecksumUtilities.GetChecksumString(fileName, algorithm).ToUpperInvariant() == checksum.ToUpperInvariant();
        }

        /// <summary>
        /// Performs a checksum of the specified file and returns a value indicating whether it is intact.
        /// </summary>
        /// <param name="fileName">The name of the file to perform the checksum on.</param>
        /// <param name="checksum">The valid checksum to compare to the actual file's checksum.</param>
        /// <param name="algorithm">The algorithm used to calculate the checksums.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> is <c>null</c> or white space. -or- <paramref name="checksum"/> is <c>null</c>.</exception>
        public static bool PerformChecksum(string fileName, byte[] checksum, ChecksumAlgorithm algorithm)
        {
            if (string.IsNullOrEmpty(fileName) || checksum == null)
            {
                throw new ArgumentNullException(string.IsNullOrEmpty(fileName) ? "fileName" : "checksum");
            }

            return ChecksumUtilities.GetChecksumBytes(fileName, algorithm).SequenceEqual(checksum);
        }

        /// <summary>
        /// Gets a checksum of the specified file using the specified algorithm.
        /// </summary>
        /// <param name="fileName">The name of the file to perform the checksum on.</param>
        /// <param name="algorithm">The algorithm used to calculate the checksums.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> is <c>null</c> or white space.</exception>
        public static string GetChecksumString(string fileName, ChecksumAlgorithm algorithm)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            return BitConverter.ToString(ChecksumUtilities.GetChecksumBytes(fileName, algorithm)).Replace("-", string.Empty);
        }

        /// <summary>
        /// Gets a checksum of the specified file using the specified algorithm.
        /// </summary>
        /// <param name="stream">The stream to perform the checksum on.</param>
        /// <param name="algorithm">The algorithm used to calculate the checksums.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public static string GetChecksumString(Stream stream, ChecksumAlgorithm algorithm)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            return BitConverter.ToString(ChecksumUtilities.GetChecksumBytes(stream, algorithm)).Replace("-", string.Empty);
        }

        /// <summary>
        /// Gets a checksum of the specified file using the specified algorithm.
        /// </summary>
        /// <param name="fileName">The name of the file to perform the checksum on.</param>
        /// <param name="algorithm">The algorithm used to calculate the checksums.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> is <c>null</c> or white space.</exception>
        public static byte[] GetChecksumBytes(string fileName, ChecksumAlgorithm algorithm)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            using (FileStream file = new FileStream(fileName, FileMode.Open))
            {
                return ChecksumUtilities.GetChecksumBytes(file, algorithm);
            }
        }

        /// <summary>
        /// Gets a checksum of the specified file using the specified algorithm.
        /// </summary>
        /// <param name="stream">The stream to perform the checksum on.</param>
        /// <param name="algorithm">The algorithm used to calculate the checksums.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidEnumArgumentException"><paramref name="algorithm"/> was not a valid member of the <see cref="ChecksumAlgorithm"/> enumeration.</exception>
        public static byte[] GetChecksumBytes(Stream stream, ChecksumAlgorithm algorithm)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            using (HashAlgorithm hash = HashAlgorithm.Create(algorithm.ToString()))
            {
                if (hash != null)
                {
                    return hash.ComputeHash(stream);
                }
                else
                {
                    throw new InvalidEnumArgumentException("algorithm", (int)algorithm, typeof(ChecksumAlgorithm));
                }
            }
        }
    }
}
