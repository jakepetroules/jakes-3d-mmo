namespace MMO3D.Engine
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Contains collision-handling code.
    /// </summary>
    public static class Collision
    {
        /// <summary>
        /// Checks to see if any of the bounding spheres of the specified meshes collide.
        /// </summary>
        /// <param name="gameObject1">The first game object in the collision check.</param>
        /// <param name="gameObject2">The second game object in the collision check.</param>
        /// <returns>Whether the models collided.</returns>
        public static bool Colliding(GameObjectBase gameObject1, GameObjectBase gameObject2)
        {
            if (gameObject1 == gameObject2 || (!gameObject1.Collides || !gameObject2.Collides) || (gameObject1 is Player && gameObject2 is Player))
            {
                return false;
            }

            for (int i = 0; i < gameObject1.Model.Meshes.Count; i++)
            {
                BoundingSphere sphere1 = new BoundingSphere(gameObject1.Model.Meshes[i].BoundingSphere.Center + gameObject1.Position, gameObject1.Model.Meshes[i].BoundingSphere.Radius);

                for (int j = 0; j < gameObject2.Model.Meshes.Count; j++)
                {
                    BoundingSphere sphere2 = new BoundingSphere(gameObject2.Model.Meshes[j].BoundingSphere.Center + gameObject2.Position, gameObject2.Model.Meshes[j].BoundingSphere.Radius);

                    if (sphere1.Intersects(sphere2))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
