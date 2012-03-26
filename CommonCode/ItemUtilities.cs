namespace MMO3D.CommonCode
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Security;

    /// <summary>
    /// Provides item-related utility methods.
    /// </summary>
    public static class ItemUtilities
    {
        /// <summary>
        /// Gets the image for a particular item.
        /// </summary>
        /// <param name="itemId">The ID of the item to retrieve the image of.</param>
        /// <returns>See summary.</returns>
        public static Image GetItemImage(long itemId)
        {
            try
            {
                HttpWebRequest wreq = (HttpWebRequest)WebRequest.Create(WebResourceUrls.GetItemSpriteUrl(itemId));
                wreq.AllowWriteStreamBuffering = true;

                using (HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse())
                {
                    using (Stream mystream = wresp.GetResponseStream())
                    {
                        return new Bitmap(mystream);
                    }
                }
            }
            catch (NotSupportedException)
            {
            }
            catch (SecurityException)
            {
            }
            catch (WebException)
            {
            }
            catch (ProtocolViolationException)
            {
            }
            catch (ArgumentException)
            {
            }

            return new Bitmap(GlobalValues.ItemThumbnailSize.Width, GlobalValues.ItemThumbnailSize.Height);
        }
    }
}
