namespace Petroules.Synteza.Networking
{
    using System;

    /// <summary>
    /// The result of the authentication request.
    /// </summary>
    [Serializable]
    public enum AuthenticationResult
    {
        /// <summary>
        /// Successful authentication.
        /// </summary>
        Success,

        /// <summary>
        /// Already authenticated.
        /// </summary>
        AlreadyAuthenticated,

        /// <summary>
        /// Failed because the account associated with the specified credentials is disabled.
        /// </summary>
        FailedAccountDisabled,

        /// <summary>
        /// Failed because the supplied user ID was invalid.
        /// </summary>
        FailedInvalidUserName,

        /// <summary>
        /// Failed because the supplied password was invalid.
        /// </summary>
        FailedInvalidPassword,

        /// <summary>
        /// Failed because there were too many invalid authentication attempts for the specified credentials.
        /// </summary>
        FailedExceededAuthenticationAttempts,

        /// <summary>
        /// Failed because the client was not connected to the remote server.
        /// </summary>
        FailedNotConnected,

        /// <summary>
        /// Failed with an unknown error.
        /// </summary>
        FailedUnknown
    }
}
