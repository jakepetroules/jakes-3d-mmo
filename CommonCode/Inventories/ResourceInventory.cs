namespace MMO3D.CommonCode
{
    using System;

    /// <summary>
    /// Class for managing resource inventory. This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class ResourceInventory : Inventory
    {
        /// <summary>
        /// The size of a resource's inventory.
        /// </summary>
        public const int InventorySize = 24;

        /// <summary>
        /// Initializes a new instance of the ResourceInventory class.
        /// </summary>
        public ResourceInventory()
            : base(ResourceInventory.InventorySize)
        {
        }
    }
}
