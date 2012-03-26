namespace MMO3D.Engine
{
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Contains cryptographic functions.
    /// </summary>
    public static class SimpleCryptography
    {
        /// <summary>
        /// Gets the cryptographic hash of a string using the MD5 algorithm.
        /// </summary>
        /// <param name="data">The string to compute the cryptographic hash of.</param>
        /// <returns>A hexadecimal string representing the encrypted string.</returns>
        public static string MD5(string data)
        {
            MD5CryptoServiceProvider crypto = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            bytes = crypto.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2", CultureInfo.InvariantCulture));
            }

            return builder.ToString();
        }
    }
}
