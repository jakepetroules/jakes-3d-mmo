namespace MMO3D.CommonCode
{
    using System;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a material.
    /// </summary>
    [Serializable]
    public sealed class Material : Item, IQuality
    {
        /// <summary>
        /// Initializes a new instance of the Material class.
        /// </summary>
        /// <param name="itemTypeId">The ID of the item to create.</param>
        /// <param name="name">The item's name.</param>
        /// <param name="description">The item's description.</param>
        /// <exception cref="System.ArgumentNullException">Any parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">ItemTypeId is less than zero.</exception>
        public Material(long itemTypeId, string name, string description)
            : base(itemTypeId, ItemClass.Material, name, description)
        {
        }

        /// <summary>
        /// Gets or sets the quality of the item.
        /// </summary>
        /// <value>See summary.</value>
        public ItemQuality Quality
        {
            get;
            set;
        }
    }
}
