namespace Petroules.Synteza.Web
{
    using System;
    using System.IO;
    using System.Text;
    using System.Web;

    /// <summary>
    /// A semi-generic <see cref="Stream"/> implementation for <see cref="HttpResponse.Filter"/> with an event
    /// interface for handling content transformations via <see cref="Stream"/> or <see cref="string"/>.
    /// </summary>
    /// <remarks>Use with care as this implementation copies the output into a memory stream which increases memory usage.</remarks>
    public class ResponseFilterStream : Stream
    {
        /// <summary>
        /// The original stream.
        /// </summary>
        private Stream stream;

        /// <summary>
        /// Stream that original content is read into and then passed to the <see cref="TransformStream"/> function.
        /// </summary>
        private MemoryStream cacheStream = new MemoryStream(5000);

        /// <summary>
        /// Internal pointer that that keeps track of the size of the <see cref="cacheStream"/>.
        /// </summary>
        private int cachePointer = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseFilterStream"/> class.
        /// </summary>
        /// <param name="responseStream">The HTTP response stream to modify.</param>
        /// <exception cref="ArgumentNullException"><paramref name="responseStream"/> is null.</exception>
        public ResponseFilterStream(Stream responseStream)
        {
            if (responseStream == null)
            {
                throw new ArgumentNullException("responseStream");
            }

            this.stream = responseStream;
        }

        /// <summary>
        /// Event that captures response output and makes it available as a <see cref="MemoryStream"/>. Output is captured but will not affect response output.
        /// </summary>
        public event Action<MemoryStream> CaptureStream;

        /// <summary>
        /// Event that captures response output and makes it available as a string. Output is captured but will not affect response output.
        /// </summary>
        public event Action<string> CaptureString;

        /// <summary>
        /// Event that allows transformation of the stream as each chunk of the output is written in the <see cref="Stream.Write"/> method of the stream.
        /// This means that it is possible (and likely) that the input buffer will not contain the full response output but only one of potentially many chunks.
        /// This event is fired as part of the filter stream's <see cref="Write"/> operation.
        /// </summary>
        public event Func<byte[], byte[]> TransformWrite;

        /// <summary>
        /// Event that allows transformation of the response stream as each chunk of byte[] output is written during the stream's write operation.
        /// This means that it is possible (and likely) that the string passed to the handler will only contain a portion of the full output.
        /// Typical buffer chunks are approximately 16 kilobytes in length. This event is fired as part of the stream's Write operation.
        /// </summary>
        public event Func<string, string> TransformWriteString;

        /// <summary>
        /// This event allows capturing and transformation of the entire output stream by caching all write operations and delaying final
        /// response output until <see cref="Stream.Flush"/> is called on the stream.
        /// </summary>
        public event Func<MemoryStream, MemoryStream> TransformStream;

        /// <summary>
        /// Event that can be hooked up to handle <see cref="HttpResponse.Filter"/> transformation. Passes a string that can be modified and
        /// returned back as a return value. The modified content will become the final output.
        /// </summary>
        public event Func<string, string> TransformString;

        /// <summary>
        /// Gets a value indicating whether the stream can be read. This property always returns true. See <see cref="Stream.CanRead"/>.
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the stream can be seeked. This property always returns true. See <see cref="Stream.CanSeek"/>.
        /// </summary>
        public override bool CanSeek
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the stream can be written to. This property always returns true. See <see cref="Stream.CanWrite"/>.
        /// </summary>
        public override bool CanWrite
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the value 0. See <see cref="Stream.Length"/>.
        /// </summary>
        public override long Length
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets or sets the current position in the original stream. This property does nothing.
        /// </summary>
        public override long Position
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the stream is captured.
        /// </summary>
        private bool IsCaptured
        {
            get { return this.CaptureStream != null || this.CaptureString != null || this.TransformStream != null || this.TransformString != null; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Write"/> method is outputting data immediately or delaying output until <see cref="Flush"/> is called.
        /// </summary>
        private bool IsOutputDelayed
        {
            get { return this.TransformStream != null || this.TransformString != null; }
        }

        /// <summary>
        /// Sets the position within the underlying stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the origin parameter.</param>
        /// <param name="direction">A value of type <see cref="System.IO.SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <returns>The new position within the current stream.</returns>
        public override long Seek(long offset, SeekOrigin direction)
        {
            return this.stream.Seek(offset, direction);
        }

        /// <summary>
        /// Sets the length of the underlying stream.
        /// </summary>
        /// <param name="length">The length to set.</param>
        public override void SetLength(long length)
        {
            this.stream.SetLength(length);
        }

        /// <summary>
        /// Closes the underlying stream.
        /// </summary>
        public override void Close()
        {
            this.stream.Close();
        }

        /// <summary>
        /// Writes out the cached stream data.
        /// </summary>
        public override void Flush()
        {
            if (this.IsCaptured && this.cacheStream.Length > 0)
            {
                // Check for transform implementations
                this.cacheStream = this.OnTransformCompleteStream(this.cacheStream);
                this.cacheStream = this.OnTransformCompleteStringInternal(this.cacheStream);

                this.OnCaptureStream(this.cacheStream);
                this.OnCaptureStringInternal(this.cacheStream);

                // Write the stream back out if output was delayed
                if (this.IsOutputDelayed)
                {
                    this.stream.Write(this.cacheStream.ToArray(), 0, (int)this.cacheStream.Length);
                }

                // Clear the cache once we've written it out
                this.cacheStream.SetLength(0);
            }

            // Default flush behavior
            this.stream.Flush();
        }

        /// <summary>
        /// Reads from the underlying stream. See <see cref="Stream.Read"/>.
        /// </summary>
        /// <param name="buffer">See <see cref="Stream.Read"/>.</param>
        /// <param name="offset">See <see cref="Stream.Read"/>.</param>
        /// <param name="count">See <see cref="Stream.Read"/>.</param>
        /// <returns>See <see cref="Stream.Read"/>.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.stream.Read(buffer, offset, count);
        }

        /// <summary>
        /// Captures output written by ASP.NET and writes it into a cached stream that is written out later when <see cref="Flush"/> is called.
        /// See <see cref="Stream.Write"/>.
        /// </summary>
        /// <param name="buffer">See <see cref="Stream.Write"/>.</param>
        /// <param name="offset">See <see cref="Stream.Write"/>.</param>
        /// <param name="count">See <see cref="Stream.Write"/>.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.IsCaptured)
            {
                // Copy to holding buffer only - we'll write out later
                this.cacheStream.Write(buffer, 0, count);
                this.cachePointer += count;
            }

            // Hust transform this buffer
            if (this.TransformWrite != null)
            {
                buffer = this.OnTransformWrite(buffer);
            }

            if (this.TransformWriteString != null)
            {
                buffer = this.OnTransformWriteStringInternal(buffer);
            }

            if (!this.IsOutputDelayed)
            {
                this.stream.Write(buffer, offset, buffer.Length);
            }
        }

        /// <summary>
        /// Wrapper method for <see cref="TransformString"/> that handles <see cref="Stream"/> to string and vice versa conversions.
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        internal MemoryStream OnTransformCompleteStringInternal(MemoryStream ms)
        {
            if (this.TransformString == null)
            {
                return ms;
            }

            string content = HttpContext.Current.Response.ContentEncoding.GetString(ms.ToArray());

            content = this.TransformString(content);
            byte[] buffer = HttpContext.Current.Response.ContentEncoding.GetBytes(content);
            ms = new MemoryStream();
            ms.Write(buffer, 0, buffer.Length);

            return ms;
        }

        protected virtual void OnCaptureStream(MemoryStream ms)
        {
            if (this.CaptureStream != null)
            {
                this.CaptureStream(ms);
            }
        }

        protected virtual void OnCaptureString(string output)
        {
            if (this.CaptureString != null)
            {
                this.CaptureString(output);
            }
        }

        protected virtual byte[] OnTransformWrite(byte[] buffer)
        {
            if (this.TransformWrite != null)
            {
                return this.TransformWrite(buffer);
            }

            return buffer;
        }

        protected virtual MemoryStream OnTransformCompleteStream(MemoryStream ms)
        {
            if (this.TransformStream != null)
            {
                return this.TransformStream(ms);
            }

            return ms;
        }

        private void OnCaptureStringInternal(MemoryStream ms)
        {
            if (this.CaptureString != null)
            {
                string content = HttpContext.Current.Response.ContentEncoding.GetString(ms.ToArray());
                this.OnCaptureString(content);
            }
        }

        private byte[] OnTransformWriteStringInternal(byte[] buffer)
        {
            Encoding encoding = HttpContext.Current.Response.ContentEncoding;
            string output = this.OnTransformWriteString(encoding.GetString(buffer));
            return encoding.GetBytes(output);
        }

        private string OnTransformWriteString(string value)
        {
            if (this.TransformWriteString != null)
            {
                return this.TransformWriteString(value);
            }

            return value;
        }

        /// <summary>
        /// Allows transformation of strings.
        /// </summary>
        /// <param name="responseText"></param>
        /// <returns></returns>
        /// <remarks>
        /// Note this handler is internal and not meant to be overridden as the <see cref="TransformString"/> event has to
        /// be hooked up in order for this handler to even fire to avoid the overhead of string conversion on every passthrough.
        /// </remarks>
        private string OnTransformCompleteString(string responseText)
        {
            if (this.TransformString != null)
            {
                this.TransformString(responseText);
            }

            return responseText;
        }
    }
}
