namespace Petroules.Synteza.Web
{
    /// <summary>
    /// Represents a result from the W3C Markup Validation Service.
    /// </summary>
    /// <remarks>
    /// Derived from code found at: http://damianedwards.wordpress.com/2008/10/06/adding-html-validity-checking-to-your-aspnet-web-site-via-unit-tests/
    /// </remarks>
    public sealed class W3CValidityCheckResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="W3CValidityCheckResult"/> class.
        /// </summary>
        internal W3CValidityCheckResult()
        {
        }

        /// <summary>
        /// Gets a value indicating whether the result of the validity check is valid.
        /// </summary>
        public bool IsValid
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the number of warnings in the checked document.
        /// </summary>
        public int WarningCount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the number of errors in the checked document.
        /// </summary>
        public int ErrorCount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the body of the W3C Markup Validator's response.
        /// </summary>
        public string Body
        {
            get;
            internal set;
        }
    }
}
