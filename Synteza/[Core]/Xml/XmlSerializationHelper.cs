namespace Petroules.Synteza.Xml
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Provides simple methods for serializing and deserializing XML documents.
    /// </summary>
    public static class XmlSerializationHelper
    {
        /// <summary>
        /// Deserializes an object in XML format from a file.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="path">The path of the file to read from.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <c>null</c> or white space.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This design is intended to avoid type-casting.")]
        public static T DeserializeFile<T>(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            return XmlSerializationHelper.DeserializeUri<T>(new Uri(path));
        }

        /// <summary>
        /// Deserializes an object in XML format from a URL.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="url">The URL to read from.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <c>null</c> or white space.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This design is intended to avoid type-casting.")]
        public static T DeserializeUri<T>(Uri url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (Stream stream = url.IsFile ? new FileStream(url.LocalPath, FileMode.Open) : new WebClient().OpenRead(url))
            {
                return (T)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// Serializes an object to a file, in XML format.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="path">The path of the file to write to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="obj"/> is <c>null</c> -or- <paramref name="path"/> is <c>null</c> or white space.</exception>
        public static void SerializeFile<T>(this T obj, string path)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            // Serialize the object to a memory stream
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, obj);

                // Go back to the beginning of the memory stream
                memoryStream.Position = 0;

                // Create an XmlDocument object and load the serialized document from the stream
                XmlDocument document = new XmlDocument();
                document.Load(memoryStream);

                // Search for the root element...
                XmlNodeList nodes = document.GetElementsByTagName(XmlSerializationHelper.GetRootElement(typeof(T)));
                if (nodes.Count == 1)
                {
                    // ...and inject our doctype just before it
                    XmlDocumentType docType = document.CreateDocumentType(XmlSerializationHelper.GetRootElement(typeof(T)), XmlSerializationHelper.GetPublicId(typeof(T)), XmlSerializationHelper.GetSystemId(typeof(T)), null);
                    document.InsertBefore(docType, nodes[0]);
                }

                // Finally, save the document
                document.Save(path);
            }
        }

        /// <summary>
        /// Gets the name of the root element of an object in XML.
        /// </summary>
        /// <param name="type">The type to get the root element of.</param>
        /// <returns>The name of the root element of the given type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        private static string GetRootElement(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            object[] attributes = type.GetCustomAttributes(typeof(XmlRootAttribute), false);
            if (attributes.Length == 1)
            {
                return ((XmlRootAttribute)attributes[0]).ElementName;
            }

            return type.Name;
        }

        /// <summary>
        /// Gets the DTD public identifier of an object in XML.
        /// </summary>
        /// <param name="type">The type to get the DTD public identifier of.</param>
        /// <returns>The DTD public identifier of the given type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        private static string GetPublicId(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            object[] attributes = type.GetCustomAttributes(typeof(XmlDtdAttribute), false);
            if (attributes.Length == 1)
            {
                return ((XmlDtdAttribute)attributes[0]).PublicId;
            }

            return null;
        }

        /// <summary>
        /// Gets the DTD system identifier of an object in XML.
        /// </summary>
        /// <param name="type">The type to get the DTD system identifier of.</param>
        /// <returns>The DTD system identifier of the given type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        private static string GetSystemId(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            
            object[] attributes = type.GetCustomAttributes(typeof(XmlDtdAttribute), false);
            if (attributes.Length == 1)
            {
                return ((XmlDtdAttribute)attributes[0]).SystemId;
            }

            return null;
        }
    }
}
