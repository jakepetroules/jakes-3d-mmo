namespace Petroules.Synteza.Security
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Provides public key encryption services using the RSA algorithm.
    /// </summary>
    public sealed class RsaEncryption : IDisposable
    {
        /// <summary>
        /// The RSA cryptography object.
        /// </summary>
        private RSACryptoServiceProvider rsa;

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaEncryption"/> class with the specified key size.
        /// </summary>
        /// <param name="keySize">The size of the key. This should be a multiple of 8 in the range 384 to 16384. The default is 2048 bits.</param>
        public RsaEncryption(int keySize)
        {
            this.rsa = new RSACryptoServiceProvider(keySize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaEncryption"/> class with key information from the specified XML string.
        /// </summary>
        /// <param name="xml">The XML containing the key information.</param>
        public RsaEncryption(string xml)
        {
            this.rsa = new RSACryptoServiceProvider();
            this.rsa.FromXmlString(xml);
        }

        /// <summary>
        /// Gets the private and public encryption keys.
        /// </summary>
        public string PrivateKey
        {
            get { return this.rsa.ToXmlString(true); }
        }

        /// <summary>
        /// Gets the public encryption key alone.
        /// </summary>
        public string PublicKey
        {
            get { return this.rsa.ToXmlString(false); }
        }

        /// <summary>
        /// Encrypts data.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        public byte[] Encrypt(byte[] data)
        {
            return this.Crypt(data, true);
        }

        /// <summary>
        /// Decrypts data.
        /// </summary>
        /// <param name="data">The data to decrypt.</param>
        public byte[] Decrypt(byte[] data)
        {
            return this.Crypt(data, false);
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
                this.rsa.Clear();
            }
        }

        /// <summary>
        /// Encrypts or decrypts data.
        /// </summary>
        /// <param name="data">The data to encrypt or decrypt.</param>
        /// <param name="encrypt"><c>true</c> to encrypt; <c>false</c> to decrypt.</param>
        private byte[] Crypt(byte[] data, bool encrypt)
        {
            // Create a stream to hold our encrypted output
            MemoryStream stream = new MemoryStream();

            // The maximum block length is the length of the modulus in bytes (e.g. the key size divided by 8), minus 11 bytes for padding if we're encrypting
            int maxBlockLength = this.rsa.ExportParameters(false).Modulus.Length - (encrypt ? 11 : 0);

            // Calculate the number of blocks we'll need to process our data - this is the length of our data divided by the maximum block length.
            // If the length of our data is evenly divisible by the maximum block length, we need just that many blocks, however if it's not, we'll
            // need an extra one since integer division is floored
            int blocks = data.Length % maxBlockLength == 0 ? data.Length / maxBlockLength : (data.Length / maxBlockLength) + 1;

            for (int i = 0; i < blocks; i++)
            {
                // Calculates the block length for the current iteration - for most iterations, the block length is equal to the maximum block length,
                // however for the last iteration it is the length of our data minus the offset of the last block (obviously) so we don't overflow
                int blockLength = i == (blocks - 1) ? data.Length - (i * maxBlockLength) : maxBlockLength;

                // Allocate a new block of that size and copy data from the main buffer
                byte[] block = new byte[blockLength];
                Array.Copy(data, i * maxBlockLength, block, 0, block.Length);

                // Encrypt or decrypt the block and write it to the output stream
                byte[] cryptedBytes = encrypt ? this.rsa.Encrypt(block, false) : this.rsa.Decrypt(block, false);
                stream.Write(cryptedBytes, 0, cryptedBytes.Length);
            }

            return stream.ToArray();
        }
    }
}
