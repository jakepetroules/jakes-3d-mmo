namespace MMO3D.CommonCode
{
    using MMO3D.CommonCode;

    /// <summary>
    /// An interface for items that have a quality ranking.
    /// </summary>
    public interface IQuality
    {
        /// <summary>
        /// Gets or sets the quality of the item.
        /// </summary>
        /// <value>See summary.</value>
        ItemQuality Quality { get; set; }
    }
}
