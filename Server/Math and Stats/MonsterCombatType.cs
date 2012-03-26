namespace MMO3D.Server
{
    /// <summary>
    /// Enumerates the combat types of monsters.
    /// </summary>
    public enum MonsterCombatType
    {
        /// <summary>
        /// Only one player is required to defeat the monster.
        /// </summary>
        Single,

        /// <summary>
        /// A party is required to defeat the monster.
        /// </summary>
        Party,

        /// <summary>
        /// A guild is required to defeat the monster.
        /// </summary>
        Guild,

        /// <summary>
        /// As many players as possible are needed to defeat the monster.
        /// </summary>
        Everyone
    }

    /// <summary>
    /// Extensions for the MonsterCombatType enumeration.
    /// </summary>
    public static class MonsterCombatTypeExtensions
    {
        /// <summary>
        /// Gets the symbol for a monster of this combat type.
        /// </summary>
        /// <param name="monsterCombatType">The combat type of the monster.</param>
        /// <returns>See summary.</returns>
        public static string GetSymbol(this MonsterCombatType monsterCombatType)
        {
            switch (monsterCombatType)
            {
                case MonsterCombatType.Single:
                    return string.Empty;
                case MonsterCombatType.Party: // TODO: 4-8 players
                    return "☠";
                case MonsterCombatType.Guild: // TODO: 10-30 players
                    return "☢";
                case MonsterCombatType.Everyone: // TODO: 50-100 players
                    return "☣";
            }

            return string.Empty;
        }
    }
}
