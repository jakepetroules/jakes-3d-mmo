namespace Petroules.Synteza
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Provides extensions to the <see cref="Stream"/> class.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Converts a stream to a string array, casting each byte to a character. This method seeks to the beginning of the stream.
        /// </summary>
        /// <param name="stream">The stream to convert to a string.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public static string ToByteString(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            StringBuilder builder = new StringBuilder();
            stream.Position = 0;
            for (int i = 0; i < stream.Length; i++)
            {
                builder.Append((char)stream.ReadByte());
            }

            return builder.ToString();
        }
    }
}
