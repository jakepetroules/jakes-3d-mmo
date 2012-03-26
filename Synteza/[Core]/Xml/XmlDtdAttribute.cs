namespace Petroules.Synteza.Xml
{
    using System;

    /// <summary>
    /// Encapsulates properties of an XML document type definition, used for serialization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class XmlDtdAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDtdAttribute"/> class.
        /// </summary>
        /// <param name="publicId">The public identifier of the <acronym title="Document Type Definition">DTD</acronym>.</param>
        /// <param name="systemId">The system identifier (URL) of the <acronym title="Document Type Definition">DTD</acronym>.</param>
        public XmlDtdAttribute(string publicId, string systemId)
        {
            this.PublicId = publicId;
            this.SystemId = systemId;
        }

        /// <summary>
        /// Gets the public identifier of the <acronym title="Document Type Definition">DTD</acronym>.
        /// </summary>
        public string PublicId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the system identifier (URL) of the <acronym title="Document Type Definition">DTD</acronym>.
        /// </summary>
        public string SystemId
        {
            get;
            private set;
        }
    }
}
