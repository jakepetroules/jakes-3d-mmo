namespace MMO3D.CommonCode
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Provides the URLs to various resources from the central game web server.
    /// The URLs are hard-coded.
    /// </summary>
    public static class WebResourceUrls
    {
        /// <summary>
        /// The base URL of the central game web server.
        /// </summary>
        public const string BaseUrl = "http://mmo.petroules.com";

        /// <summary>
        /// The URL of the XML game servers list.
        /// </summary>
        public const string ServerListUrl = WebResourceUrls.BaseUrl + "/Servers.xml";

        /// <summary>
        /// Gets the 32x32 PNG image sprite for the item with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the item to find the sprite of.</param>
        /// <returns>See summary.</returns>
        public static Uri GetItemSpriteUrl(long id)
        {
            return new Uri(string.Format(CultureInfo.InvariantCulture, "{0}/ItemImage.ashx?id={1}", WebResourceUrls.BaseUrl, id));
        }
    }
}
