namespace MMO3D.Engine
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Cryptography;

    /// <summary>
    /// Provides RSA encryption services mainly for use by the engine on network packets.
    /// </summary>
    public sealed class KeyEncryption : IDisposable
    {
        /// <summary>
        /// The default key length.
        /// </summary>
        public const int KeyLength = 4096;

        /// <summary>
        /// The RSA object used for encryption and decryption.
        /// </summary>
        private RSACryptoServiceProvider rsa;

        /// <summary>
        /// Initializes a new instance of the KeyEncryption class.
        /// </summary>
        public KeyEncryption()
        {
            this.rsa = new RSACryptoServiceProvider(KeyEncryption.KeyLength);
        }

        /// <summary>
        /// Generates a new key pair.
        /// </summary>
        /// <param name="keyLength">The size in bits, of the key.</param>
        /// <returns>See summary.</returns>
        public static KeyPair GenerateKeys(int keyLength)
        {
            return KeyEncryption.GetKeys(new RSACryptoServiceProvider(keyLength));
        }

        /// <summary>
        /// Encrypts a byte array.
        /// </summary>
        /// <param name="data">The array of bytes to encrypt.</param>
        /// <returns>The encrypted byte array.</returns>
        public byte[] Encrypt(byte[] data)
        {
            byte[] encryptedBytes = this.rsa.Encrypt(data, false);
            Array.Reverse(encryptedBytes);
            return encryptedBytes;
        }

        /// <summary>
        /// Decrypts a byte array.
        /// </summary>
        /// <param name="data">The array of bytes to decrypt.</param>
        /// <returns>The decrypted byte array.</returns>
        public byte[] Decrypt(byte[] data)
        {
            byte[] decryptedBytes = this.rsa.Decrypt(data, false);
            Array.Reverse(decryptedBytes);
            return decryptedBytes;
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
        /// Retrieves the current key pair from the specified RSA object.
        /// </summary>
        /// <param name="rsa">The RSA object to retrieve keys from.</param>
        /// <returns>See summary.</returns>
        private static KeyPair GetKeys(RSACryptoServiceProvider rsa)
        {
            return new KeyPair(rsa.ToXmlString(true), rsa.ToXmlString(false), rsa.KeySize);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        [SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "rsa", Justification = "RSACryptoServiceProvider.Clear() calls RSACryptoServiceProvider.Dispose()")]
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.rsa.Clear();
            }
        }
    }
}
