namespace MMO3D.Server
{
    using MMO3D.Engine;

    /// <summary>
    /// Stationary objects that serve only as decoration and/or obstacles and do nothing special.
    /// </summary>
    public sealed class BasicObject : GameObjectBase
    {
        /// <summary>
        /// Initializes a new instance of the BasicObject class.
        /// </summary>
        /// <param name="model">The model to use for this game object.</param>
        public BasicObject(ExtendedModel model)
            : base(model)
        {
        }
    }
}
