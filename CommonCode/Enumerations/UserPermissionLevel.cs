namespace MMO3D.CommonCode
{
    /// <summary>
    /// Represents a user account's permission level.
    /// </summary>
    public enum UserPermissionLevel
    {
        /// <summary>
        /// Represents a disabled account.
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// Represents a normal account with no extra privileges.
        /// </summary>
        Regular = 1,

        /// <summary>
        /// Represents an account with moderation privileges.
        /// </summary>
        Moderator = 2,

        /// <summary>
        /// Represents an account with administration privileges.
        /// </summary>
        Administrator = 3
    }

    /// <summary>
    /// Extensions for the UserPermissionLevel enumeration.
    /// </summary>
    public static class UserPermissionLevelExtensions
    {
        /// <summary>
        /// Determines whether the given user level has the permission of another.
        /// </summary>
        /// <param name="userLevel">The user level to determine the permissions of.</param>
        /// <param name="other">The user level to see if <paramref name="userLevel"/> has the permissions of.</param>
        /// <returns>See summary.</returns>
        public static bool HasPermission(this UserPermissionLevel userLevel, UserPermissionLevel other)
        {
            return (int)userLevel >= (int)other;
        }
    }
}
