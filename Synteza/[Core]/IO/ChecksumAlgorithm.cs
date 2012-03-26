namespace Petroules.Synteza.IO
{
    /// <summary>
    /// Enumerates possible algorithms to use for performing checksum operations on a file.
    /// </summary>
    public enum ChecksumAlgorithm
    {
        /// <summary>
        /// Specifies the MD5 algorithm.
        /// </summary>
        MD5,

        /// <summary>
        /// Specifies the RIPEMD160 algorithm.
        /// </summary>
        RIPEMD160,

        /// <summary>
        /// Specifies the SHA1 algorithm.
        /// </summary>
        SHA1,

        /// <summary>
        /// Specifies the SHA256 algorithm.
        /// </summary>
        SHA256,

        /// <summary>
        /// Specifies the SHA384 algorithm.
        /// </summary>
        SHA384,

        /// <summary>
        /// Specifies the SHA512 algorithm.
        /// </summary>
        SHA512
    }
}
