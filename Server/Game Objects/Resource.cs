namespace MMO3D.Server
{
    using MMO3D.CommonCode;
    using MMO3D.Engine;

    /// <summary>
    /// Defines a resource, like a tree or rock, that players can gather from.
    /// </summary>
    /// <remarks>
    /// These harvestable resources should work the same as item drops.
    /// They'll have some indicator so players know they are harvestable,
    /// and when they are clicked, a window opens showing what items are
    /// in the resource that can be taken. Then the player can pick the
    /// desired items, and his/her character then chops/mines it if they
    /// have the appropriate tool on their tool belt. This will dim it
    /// on the menu so other people can't take what the player is already
    /// trying to take.
    /// </remarks>
    public sealed class Resource : GameObjectBase
    {
        /// <summary>
        /// Gets the resource's inventory.
        /// </summary>
        /// <value>See summary.</value>
        public ResourceInventory Inventory
        {
            get;
            private set;
        }
    }
}
