namespace MMO3D.Engine
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Encapsulates XML strings containing public and private RSA keys.
    /// </summary>
    public struct KeyPair : IEquatable<KeyPair>
    {
        /// <summary>
        /// Initializes a new instance of the KeyPair struct.
        /// </summary>
        /// <param name="secretKeys">An XML string containing the private and public keys.</param>
        /// <param name="publicKey">An XML string containing the public key.</param>
        /// <param name="length">The bit length of the keys.</param>
        public KeyPair(string secretKeys, string publicKey, int length)
            : this()
        {
            this.SecretKeys = secretKeys;
            this.PublicKey = publicKey;
            this.Length = length;
        }

        /// <summary>
        /// Gets an XML string containing the private and public keys.
        /// </summary>
        /// <value>See summary.</value>
        public string SecretKeys
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets an XML string containing the public key.
        /// </summary>
        /// <value>See summary.</value>
        public string PublicKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the bit length of the keys.
        /// </summary>
        /// <value>See summary.</value>
        public int Length
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the specified instances are equal.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator ==(KeyPair obj1, KeyPair obj2)
        {
            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Determines whether the specified instances are inequal.
        /// </summary>
        /// <param name="obj1">The first instance.</param>
        /// <param name="obj2">The second instance.</param>
        /// <returns>See summary.</returns>
        public static bool operator !=(KeyPair obj1, KeyPair obj2)
        {
            return !obj1.Equals(obj2);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">Another object to compare to.</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(KeyPair other)
        {
            if (other == null)
            {
                return false;
            }

            return this.SecretKeys == other.SecretKeys && this.PublicKey == other.PublicKey && this.Length == other.Length;
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

            return this.Equals((KeyPair)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.SecretKeys.GetHashCode() ^ this.PublicKey.GetHashCode() ^ this.Length;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> representing the current <see cref="MMO3D.Engine.KeyPair"/>.
        /// </summary>
        /// <returns>See summary.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{{{0} Key Length: {1}}}", this.GetType().Name, this.Length);
        }
    }
}
