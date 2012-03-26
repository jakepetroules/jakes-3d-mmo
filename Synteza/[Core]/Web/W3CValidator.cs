namespace Petroules.Synteza.Web
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Provides an interface to the W3C Markup Validator service.
    /// </summary>
    /// <remarks>
    /// Derived from code found at: http://damianedwards.wordpress.com/2008/10/06/adding-html-validity-checking-to-your-aspnet-web-site-via-unit-tests/
    /// </remarks>
    public static class W3CValidator
    {
        /// <summary>
        /// Used by <see cref="ResetBlocker"/>.
        /// </summary>
        private static AutoResetEvent validatorBlock = new AutoResetEvent(true);

        /// <summary>
        /// Gets the base URL of the W3C Markup Validator service.
        /// </summary>
        public static Uri MarkupValidatorUrl
        {
            get { return new Uri("http://validator.w3.org/check"); }
        }

        /// <summary>
        /// Determines whether the page at the specified URL returns valid markup by checking the response against the W3C Markup Validator.
        /// </summary>
        /// <param name="url">The absolute URL of the resource to check.</param>
        /// <returns>An object representing indicating whether the markup generated is valid.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is null.</exception>
        public static W3CValidityCheckResult ReturnsValidMarkup(Uri url)
        {
            return W3CValidator.ReturnsValidMarkup(url, false);
        }

        /// <summary>
        /// Determines whether the specified markup is valid by checking the response against the W3C Markup Validator.
        /// </summary>
        /// <param name="markup">The page markup to check.</param>
        /// <returns>An object representing indicating whether the markup generated is valid.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="markup"/> is null or an empty string.</exception>
        public static W3CValidityCheckResult ReturnsValidMarkup(string markup)
        {
            if (string.IsNullOrEmpty(markup))
            {
                throw new ArgumentNullException("markup");
            }

            return W3CValidator.ReturnsValidMarkup(null, markup, true);
        }

        /// <summary>
        /// Determines whether the page at the specified URL returns valid markup by checking the response against the W3C Markup Validator.
        /// </summary>
        /// <param name="url">The absolute URL of the resource to check.</param>
        /// <param name="useDirect"><c>true</c> to fetch the page markup manually and send it to the validator; <c>false</c> to let the validator fetch it. The default is <c>false</c>.</param>
        /// <returns>An object representing indicating whether the markup generated is valid.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is null.</exception>
        public static W3CValidityCheckResult ReturnsValidMarkup(Uri url, bool useDirect)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            return W3CValidator.ReturnsValidMarkup(url, null, useDirect);
        }

        /// <summary>
        /// Determines whether the page at the specified URL returns valid markup by checking the response against the W3C Markup Validator.
        /// </summary>
        /// <param name="url">The absolute URL of the resource to check.</param>
        /// <param name="markup">The page markup to check.</param>
        /// <param name="useDirect"><c>true</c> to fetch the page markup manually and send it to the validator; <c>false</c> to let the validator fetch it. The default is <c>false</c>.</param>
        /// <returns>An object representing indicating whether the markup generated is valid.</returns>
        private static W3CValidityCheckResult ReturnsValidMarkup(Uri url, string markup, bool useDirect)
        {
            var result = new W3CValidityCheckResult();
            WebHeaderCollection validatorResponseHeaders = new WebHeaderCollection();

            using (var webClient = new WebClient())
            {
                // Send to W3C validator
                webClient.Encoding = Encoding.UTF8;
                var values = new NameValueCollection();

                if (url != null)
                {
                    // If we're getting the markup ourselves...
                    if (useDirect)
                    {
                        // The markup to validate
                        values.Add("fragment", W3CValidator.GetPageMarkup(webClient, url));
                    }
                    else
                    {
                        // The URI to validate
                        values.Add("uri", url.ToString());
                    }
                }
                else if (markup != null)
                {
                    values.Add("fragment", markup);
                }

                // Detect the character set automatically
                values.Add("charset", "(detect automatically)");

                // Detect the DOCTYPE automatically
                values.Add("doctype", "Inline");

                // Validate the full document, not a fragment
                values.Add("prefill", "0");

                // List error messages sequentially rather than by type
                values.Add("group", "0");

                // Also validate error pages
                values.Add("No200", "1");

                try
                {
                    W3CValidator.validatorBlock.WaitOne();

                    byte[] validatorRawResponse = webClient.UploadValues(W3CValidator.MarkupValidatorUrl, values);
                    result.Body = Encoding.UTF8.GetString(validatorRawResponse);

                    validatorResponseHeaders.Add(webClient.ResponseHeaders);
                }
                finally
                {
                    // Reset on background thread
                    ThreadPool.QueueUserWorkItem(W3CValidator.ResetBlocker);
                }
            }

            // Extract result from response headers
            int warnings = -1;
            int errors = -1;
            int.TryParse(validatorResponseHeaders["X-W3C-Validator-Warnings"], out warnings);
            int.TryParse(validatorResponseHeaders["X-W3C-Validator-Errors"], out errors);
            string status = validatorResponseHeaders["X-W3C-Validator-Status"];

            result.WarningCount = warnings;
            result.ErrorCount = errors;
            result.IsValid = !string.IsNullOrEmpty(status) && status.Equals("Valid", StringComparison.OrdinalIgnoreCase);

            return result;
        }

        /// <summary>
        /// Gets the response body of the resource located at the specified URL.
        /// </summary>
        /// <param name="webClient">The <see cref="WebClient"/> used to download the data.</param>
        /// <param name="url">The URL to download the resource from.</param>
        /// <returns>See summary.</returns>
        private static string GetPageMarkup(WebClient webClient, Uri url)
        {
            // Pretend to be Firefox 3.6 so that ASP.NET renders compliant HTML
            webClient.Headers["User-Agent"] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2) Gecko/20100115 Firefox/3.6 (.NET CLR 3.5.30729)";
            webClient.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            webClient.Headers["Accept-Language"] = "en-au,en-us;q=0.7,en;q=0.3";

            using (var responseStream = webClient.OpenRead(url))
            using (var streamReader = new StreamReader(responseStream))
            {
                return streamReader.ReadToEnd();
            }
        }

        /// <summary>
        /// Ensures that W3C Validator service is not called more than once a second.
        /// </summary>
        /// <param name="state">The parameter is not used.</param>
        private static void ResetBlocker(object state)
        {
            Thread.Sleep(1000);
            W3CValidator.validatorBlock.Set();
        }
    }
}
