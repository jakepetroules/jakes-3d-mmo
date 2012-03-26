namespace Petroules.Synteza.Networking
{
    using System;
    using System.IO;
    using Petroules.Synteza.IO;
    using Petroules.Synteza.Security;

    /// <summary>
    /// Defines the base class of network packets.
    /// </summary>
    /// <remarks>
    /// Operation of this class is as follows: All data that will be transferred over
    /// the network should be encapsulated in a class inheriting from NetUtilities.Networking.Packet.
    /// The Serializable attribute should be added to all applicable members.
    /// Packets are serialized to a binary byte array. The byte array retrieved from the serialization
    /// of the packets is converted to a short array, and a sentinel value of -1 is used to signal the end
    /// of a packet when reading and writing from and to the stream, since bytes only contain positive numbers.
    /// This does not interfere with Unicode strings because serialization takes care of converting data from and to arrays of bytes.
    /// Data transferred over the network is encrypted using the RSA algorithm.
    /// </remarks>
    [Serializable]
    public abstract class Packet
    {
        /// <summary>
        /// The value that signals the end of a packet in the stream. Must be less than zero.
        /// </summary>
        private const short Sentinel = -1;

        /// <summary>
        /// Initializes a new instance of the Packet class.
        /// </summary>
        protected Packet()
        {
        }

        /// <summary>
        /// Reads a packet from a stream. This method blocks until a packet (which can be null) is read.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public static Packet ReadFromStream(BinaryReader stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            byte[] data = Packet.ReadBytesFromStream(stream);
            if (data != null)
            {
                return SerializationHelper.DeserializeBytes<Packet>(data);
            }

            return null;
        }

        /// <summary>
        /// Reads a packet from a stream, decrypting it. This method blocks until a packet (which can be null) is read.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="rsa">The RSA encryption object used for secure communication.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c> -or- <paramref name="rsa"/> is <c>null</c>.</exception>
        public static Packet ReadFromStream(BinaryReader stream, RsaEncryption rsa)
        {
            if (stream == null || rsa == null)
            {
                throw new ArgumentNullException(stream == null ? "stream" : "rsa");
            }

            byte[] data = Packet.ReadBytesFromStream(stream);
            if (data != null)
            {
                return SerializationHelper.DeserializeBytes<Packet>(rsa.Decrypt(data));
            }

            return null;
        }

        /// <summary>
        /// Writes the packet to a stream through which serialized data is being passed.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <returns>A value indicating whether the packet was sent successfully.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public bool WriteToStream(BinaryWriter stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            return this.WriteToStream(stream, SerializationHelper.SerializeBytes(this));
        }

        /// <summary>
        /// Writes the packet to a stream through which serialized data is being passed, and encrypts it.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="rsa">The RSA encryption object used for secure communication.</param>
        /// <returns>A value indicating whether the packet was sent successfully.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c> -or- <paramref name="rsa"/> is <c>null</c>.</exception>
        public bool WriteToStream(BinaryWriter stream, RsaEncryption rsa)
        {
            if (stream == null || rsa == null)
            {
                throw new ArgumentNullException(stream == null ? "stream" : "rsa");
            }

            return this.WriteToStream(stream, rsa.Encrypt(SerializationHelper.SerializeBytes(this)));
        }

        /// <summary>
        /// Reads a chunk of bytes from a stream. This method blocks until a chunk of bytes (which can be null) is read.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        private static byte[] ReadBytesFromStream(BinaryReader stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            lock (stream)
            {
                MemoryStream memoryStream = new MemoryStream();

                try
                {
                    short readByte;
                    while ((readByte = stream.ReadInt16()) != Packet.Sentinel)
                    {
                        memoryStream.WriteByte((byte)readByte);
                    }

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    return memoryStream.ToArray();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
                finally
                {
                    memoryStream.Close();
                    memoryStream.Dispose();
                }
            }
        }

        /// <summary>
        /// Writes the specified data to a stream through which serialized data is being passed.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="bytes">The data to write.</param>
        /// <returns>A value indicating whether the packet was sent successfully.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c> -or- <paramref name="bytes"/> is <c>null</c>.</exception>
        private bool WriteToStream(BinaryWriter stream, byte[] bytes)
        {
            if (stream == null || bytes == null)
            {
                throw new ArgumentNullException(stream == null ? "stream" : "bytes");
            }

            try
            {
                lock (stream)
                {
                    // Write the bytes we retrived as shorts
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        stream.Write((short)bytes[i]);
                    }

                    // Write the sentinel and send it off
                    stream.Write(Packet.Sentinel);
                    stream.Flush();

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
