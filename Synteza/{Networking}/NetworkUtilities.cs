namespace Petroules.Synteza.Networking
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// Provides various utility methods related to networking.
    /// </summary>
    public static class NetworkUtilities
    {
        /// <summary>
        /// Gets the size of the content at the specified URL.
        /// </summary>
        /// <param name="contentUrl">The URL of the content.</param>
        /// <returns>The size of the remote content, in bytes.</returns>
        public static long GetContentSize(Uri contentUrl)
        {
            using (HttpWebResponse response = (HttpWebResponse)HttpWebRequest.Create(contentUrl).GetResponse())
            {
                return response.ContentLength;
            }
        }

        /// <summary>
        /// Gets the list of IP addresses associated with the local computer's host name.
        /// </summary>
        public static IPAddress[] GetAddressList()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList;
        }

        /// <summary>
        /// Gets the remote IP address of a <see cref="Socket"/> as a <see cref="long"/>.
        /// </summary>
        /// <param name="socket">The socket to get the remote IP address of.</param>
        /// <exception cref="ArgumentNullException"><paramref name="socket"/> is <c>null</c>.</exception>
        public static long GetRemoteIP(Socket socket)
        {
            if (socket == null)
            {
                throw new ArgumentNullException("socket");
            }

            byte[] addressBytes = ((IPEndPoint)socket.RemoteEndPoint).Address.GetAddressBytes();

            if (addressBytes.Length == sizeof(int))
            {
                return BitConverter.ToInt32(addressBytes, 0);
            }
            else if (addressBytes.Length == sizeof(long))
            {
                return BitConverter.ToInt64(addressBytes, 0);
            }

            return 0;
        }
    }
}
