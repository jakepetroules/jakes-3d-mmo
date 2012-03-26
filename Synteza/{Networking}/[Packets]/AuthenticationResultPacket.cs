namespace Petroules.Synteza.Networking
{
    using System;

    /// <summary>
    /// Represents the result of an authentication.
    /// </summary>
    [Serializable]
    public sealed class AuthenticationResultPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationResultPacket"/> class.
        /// </summary>
        /// <param name="result">The result of the authentication request.</param>
        public AuthenticationResultPacket(AuthenticationResult result)
            : base()
        {
            this.Result = result;
        }

        /// <summary>
        /// Gets the "success" authentication status packet.
        /// </summary>
        public static AuthenticationResultPacket Success
        {
            get { return new AuthenticationResultPacket(AuthenticationResult.Success); }
        }

        /// <summary>
        /// Gets the "already authenticated" authentication status packet.
        /// </summary>
        public static AuthenticationResultPacket AlreadyLoggedIn
        {
            get { return new AuthenticationResultPacket(AuthenticationResult.AlreadyAuthenticated); }
        }

        /// <summary>
        /// Gets the "failed - account disabled" authentication status packet.
        /// </summary>
        public static AuthenticationResultPacket FailedAccountDisabled
        {
            get { return new AuthenticationResultPacket(AuthenticationResult.FailedAccountDisabled); }
        }

        /// <summary>
        /// Gets the "failed - invalid user name" authentication status packet.
        /// </summary>
        public static AuthenticationResultPacket FailedInvalidUserName
        {
            get { return new AuthenticationResultPacket(AuthenticationResult.FailedInvalidUserName); }
        }

        /// <summary>
        /// Gets the "failed - invalid password" authentication status packet.
        /// </summary>
        public static AuthenticationResultPacket FailedInvalidPassword
        {
            get { return new AuthenticationResultPacket(AuthenticationResult.FailedInvalidPassword); }
        }

        /// <summary>
        /// Gets the "failed - too many failed authentication attempts" authentication status packet.
        /// </summary>
        public static AuthenticationResultPacket FailedTooManyIncorrectLogins
        {
            get { return new AuthenticationResultPacket(AuthenticationResult.FailedExceededAuthenticationAttempts); }
        }

        /// <summary>
        /// Gets the "failed - unknown error" authentication status packet.
        /// </summary>
        public static AuthenticationResultPacket FailedUnknown
        {
            get { return new AuthenticationResultPacket(AuthenticationResult.FailedUnknown); }
        }

        /// <summary>
        /// Gets the result of the authentication request.
        /// </summary>
        public AuthenticationResult Result
        {
            get;
            private set;
        }
    }
}
