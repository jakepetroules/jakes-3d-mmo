namespace MMO3D.CommonCode
{
    using System;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a tool.
    /// </summary>
    [Serializable]
    public sealed class Tool : Item
    {
        /// <summary>
        /// Initializes a new instance of the Tool class.
        /// </summary>
        /// <param name="itemTypeId">The ID of the item to create.</param>
        /// <param name="name">The item's name.</param>
        /// <param name="description">The item's description.</param>
        /// <exception cref="System.ArgumentNullException">Any parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">ItemTypeId is less than zero.</exception>
        public Tool(long itemTypeId, string name, string description)
            : base(itemTypeId, ItemClass.Tool, name, description)
        {
        }
    }
}
