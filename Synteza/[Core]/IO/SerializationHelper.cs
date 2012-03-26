namespace Petroules.Synteza.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security;
    using System.Text;

    /// <summary>
    /// Provides methods to help with simple binary serialization and deserialization.
    /// </summary>
    public static class SerializationHelper
    {
        /// <summary>
        /// Serializes an object to a byte array.
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="obj"/> is null.</exception>
        /// <exception cref="SerializationException">An error has occurred during serialization, such as if an object in the graph parameter is not marked as serializable.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        public static byte[] SerializeBytes<T>(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="obj"/> is null.</exception>
        /// <exception cref="SerializationException">An error has occurred during serialization, such as if an object in the graph parameter is not marked as serializable.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        public static string SerializeString<T>(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            StringBuilder builder = new StringBuilder();
            byte[] bytes = SerializationHelper.SerializeBytes(obj);
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append((char)bytes[i]);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Deserializes a byte array to an object.
        /// </summary>
        /// <typeparam name="T">The object type to deserialize.</typeparam>
        /// <param name="data">The byte array to deserialize.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="SerializationException">The serializationStream supports seeking, but its length is 0.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This design is intended to avoid type-casting.")]
        public static T DeserializeBytes<T>(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Seek(0, SeekOrigin.Begin);

                return (T)new BinaryFormatter().Deserialize(stream);
            }
        }

        /// <summary>
        /// Deserializes a string to an object.
        /// </summary>
        /// <typeparam name="T">The object type to deserialize.</typeparam>
        /// <param name="data">The string to deserialize.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="SerializationException">The serializationStream supports seeking, but its length is 0.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This design is intended to avoid type-casting.")]
        public static T DeserializeString<T>(string data)
        {
            byte[] bytes = new byte[data.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)data[i];
            }

            return SerializationHelper.DeserializeBytes<T>(bytes);
        }
    }
}
