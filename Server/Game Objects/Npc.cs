namespace MMO3D.Server
{
    using MMO3D.Engine;

    /// <summary>
    /// Defines a regular, non-combat NPC that players can interact with.
    /// </summary>
    public class Npc : GameObjectBase
    {
        /// <summary>
        /// Initializes a new instance of the Npc class.
        /// </summary>
        /// <param name="model">The model to use for this game object.</param>
        public Npc(ExtendedModel model)
            : base(model)
        {
        }
    }
}
