namespace MMO3D.CommonCode
{
    using System;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines food.
    /// </summary>
    [Serializable]
    public sealed class Food : Item, IQuality
    {
        /// <summary>
        /// Initializes a new instance of the Food class.
        /// </summary>
        /// <param name="itemTypeId">The ID of the item to create.</param>
        /// <param name="name">The item's name.</param>
        /// <param name="description">The item's description.</param>
        /// <exception cref="System.ArgumentNullException">Any parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">ItemTypeId is less than zero.</exception>
        public Food(long itemTypeId, string name, string description)
            : base(itemTypeId, ItemClass.Food, name, description)
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
