namespace MMO3D.Server
{
    using MMO3D.Engine;

    /// <summary>
    /// Defines an NPC that players can engage in combat with.
    /// </summary>
    public sealed class CombatNpc : Npc
    {
        /// <summary>
        /// Initializes a new instance of the CombatNpc class.
        /// </summary>
        /// <param name="model">The model to use for this game object.</param>
        public CombatNpc(ExtendedModel model)
            : base(model)
        {
        }
    }
}
