namespace MMO3D.CommonCode
{
    /// <summary>
    /// Enumerates reasons a player's account was disabled.
    /// </summary>
    public enum DisableReason
    {
        /// <summary>
        /// Indicates that the user's account is not disabled.
        /// </summary>
        NotDisabled = 0,

        /// <summary>
        /// Indicates that a user failed to authenticate too many times.
        /// </summary>
        ExcessLogins = 1,

        /// <summary>
        /// Indicates that the user's account has been temporarily banned.
        /// </summary>
        TemporaryBan = 2,

        /// <summary>
        /// Indicates that the user's account has been permanently banned.
        /// </summary>
        PermanentBan = 3
    }
}
