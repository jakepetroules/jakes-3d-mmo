namespace Petroules.Synteza.Web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mail;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// This is a test class for <see cref="WebUtilities"/> and is intended to contain all <see cref="WebUtilities"/> Unit Tests.
    /// </summary>
    public static class WebUtilities
    {
        /// <summary>
        /// Formats XHTML as an XML document with perfect indentation.
        /// </summary>
        /// <param name="html">The XHTML to format.</param>
        /// <returns>The formatted XHTML.</returns>
        public static string FormatHtml(string html)
        {
            // Create a memory stream to store the formatted output
            using (MemoryStream memoryStream = new MemoryStream())
            {
                try
                {
                    // Get the HTML output from the HTTP response stream and load it into an XML document
                    XmlDocument document = new XmlDocument();
                    document.PreserveWhitespace = false;
                    document.LoadXml(html);
                    document.Normalize();

                    // Write the document to the memory stream
                    XmlTextWriter writer = new XmlTextWriter(memoryStream, Encoding.UTF8);
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;
                    document.Save(writer);

                    // Return the formatted XHTML
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
                catch (XmlException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Creates an SEO-compatible title from the specified string (alphanumeric with separating dashes).
        /// </summary>
        /// <param name="title">The title to SEO-optimize.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="title"/> is null or an empty string.</exception>
        public static string CreateSeoTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("title");
            }

            const char Separator = '-';
            StringBuilder builder = new StringBuilder();

            // Loop through every character of the given title
            for (int i = 0; i < title.Length; i++)
            {
                // If it's a letter or digit, simply insert it
                if (char.IsLetterOrDigit(title[i]))
                {
                    builder.Append(char.ToLowerInvariant(title[i]));
                }
                else
                {
                    // If it's not a letter or digit, and:
                    // - The last character was NOT the separator char
                    // - The builder is not empty (there's at least one letter or number)
                    // - This is not the last character
                    // then add a separator character
                    if (builder[builder.Length - 1] != Separator && builder.Length != 0 && i != title.Length - 1)
                    {
                        builder.Append(Separator);
                    }
                }
            }

            // Normally, return the builder
            if (builder.Length > 0)
            {
                return builder.ToString();
            }
            else
            {
                // But if we STILL didn't get anything, our result
                // will simply be one of the separator characters
                return Separator.ToString();
            }
        }

        /// <summary>
        /// Checks whether the specified email address has valid syntax.
        /// </summary>
        /// <param name="emailAddress">The email address to check.</param>
        /// <returns>See summary.</returns>
        public static bool IsValidEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException("emailAddress");
            }

            try
            {
                new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a list of timezones where the keys are the GMT offsets and daylight savings time use, and the values are the timezone names.
        /// </summary>
        /// <returns>See summary.</returns>
        public static Dictionary<string, string> GetTimeZones()
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("-12:00,0", "(-12:00) International Date Line West");
            map.Add("-11:00,0", "(-11:00) Midway Island, Samoa");
            map.Add("-10:00,0", "(-10:00) Hawaii");
            map.Add("-09:00,1", "(-09:00) Alaska");
            map.Add("-08:00,1", "(-08:00) Pacific Time (US & Canada)");
            map.Add("-07:00,0", "(-07:00) Arizona");
            map.Add("-07:00,1", "(-07:00) Mountain Time (US & Canada)");
            map.Add("-06:00,0", "(-06:00) Central America, Saskatchewan");
            map.Add("-06:00,1", "(-06:00) Central Time (US & Canada), Guadalajara, Mexico city");
            map.Add("-05:00,0", "(-05:00) Indiana, Bogota, Lima, Quito, Rio Branco");
            map.Add("-05:00,1", "(-05:00) Eastern time (US & Canada)");
            map.Add("-04:00,1", "(-04:00) Atlantic time (Canada), Manaus, Santiago");
            map.Add("-04:00,0", "(-04:00) Caracas, La Paz");
            map.Add("-03:30,1", "(-03:30) Newfoundland");
            map.Add("-03:00,1", "(-03:00) Greenland, Brasilia, Montevideo");
            map.Add("-03:00,0", "(-03:00) Buenos Aires, Georgetown");
            map.Add("-02:00,1", "(-02:00) Mid-Atlantic");
            map.Add("-01:00,1", "(-01:00) Azores");
            map.Add("-01:00,0", "(-01:00) Cape Verde Is.");
            map.Add("00:00,0", "(00:00) Casablanca, Monrovia, Reykjavik");
            map.Add("00:00,1", "(00:00) GMT: Dublin, Edinburgh, Lisbon, London");
            map.Add("+01:00,1", "(+01:00) Amsterdam, Berlin, Rome, Vienna, Prague, Brussels");
            map.Add("+01:00,0", "(+01:00) West Central Africa");
            map.Add("+02:00,1", "(+02:00) Amman, Athens, Istanbul, Beirut, Cairo, Jerusalem");
            map.Add("+02:00,0", "(+02:00) Harare, Pretoria");
            map.Add("+03:00,1", "(+03:00) Baghdad, Moscow, St. Petersburg, Volgograd");
            map.Add("+03:00,0", "(+03:00) Kuwait, Riyadh, Nairobi, Tbilisi");
            map.Add("+03:30,0", "(+03:30) Tehran");
            map.Add("+04:00,0", "(+04:00) Abu Dhadi, Muscat");
            map.Add("+04:00,1", "(+04:00) Baku, Yerevan");
            map.Add("+04:30,0", "(+04:30) Kabul");
            map.Add("+05:00,1", "(+05:00) Ekaterinburg");
            map.Add("+05:00,0", "(+05:00) Islamabad, Karachi, Tashkent");
            map.Add("+05:30,0", "(+05:30) Chennai, Kolkata, Mumbai, New Delhi, Sri Jayawardenepura");
            map.Add("+05:45,0", "(+05:45) Kathmandu");
            map.Add("+06:00,0", "(+06:00) Astana, Dhaka");
            map.Add("+06:00,1", "(+06:00) Almaty, Nonosibirsk");
            map.Add("+06:30,0", "(+06:30) Yangon (Rangoon)");
            map.Add("+07:00,1", "(+07:00) Krasnoyarsk");
            map.Add("+07:00,0", "(+07:00) Bangkok, Hanoi, Jakarta");
            map.Add("+08:00,0", "(+08:00) Beijing, Hong Kong, Singapore, Taipei");
            map.Add("+08:00,1", "(+08:00) Irkutsk, Ulaan Bataar, Perth");
            map.Add("+09:00,1", "(+09:00) Yakutsk");
            map.Add("+09:00,0", "(+09:00) Seoul, Osaka, Sapporo, Tokyo");
            map.Add("+09:30,0", "(+09:30) Darwin");
            map.Add("+09:30,1", "(+09:30) Adelaide");
            map.Add("+10:00,0", "(+10:00) Brisbane, Guam, Port Moresby");
            map.Add("+10:00,1", "(+10:00) Canberra, Melbourne, Sydney, Hobart, Vladivostok");
            map.Add("+11:00,0", "(+11:00) Magadan, Solomon Is., New Caledonia");
            map.Add("+12:00,1", "(+12:00) Auckland, Wellington");
            map.Add("+12:00,0", "(+12:00) Fiji, Kamchatka, Marshall Is.");
            map.Add("+13:00,0", "(+13:00) Nuku'alofa");
            return map;
        }
    }
}
