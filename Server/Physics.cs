namespace MMO3D.Server
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;
    using MMO3D.Engine;
    using MMO3D.NetworkInterface;

    /// <summary>
    /// Contains physics functions related to movement, collision and pathfinding.
    /// </summary>
    public static class Physics
    {
        /// <summary>
        /// Attempts to move the player to the position specified by <paramref name="targetPosition"/>.
        /// </summary>
        /// <param name="player">The player to move.</param>
        /// <param name="targetPosition">The position to move the player to.</param>
        public static void CheckCollision(PlayerHandler player, Vector3 targetPosition)
        {
            if (player != null)
            {
                List<GameObjectBase> allClients = new List<GameObjectBase>();
                allClients.Add(player);
                for (int i = 0; i < GameServer.Instance.Clients.Count; i++)
                {
                    allClients.Add(GameServer.Instance.Clients[i]);
                }

                bool impossibleX = false;
                bool impossibleY = false;

                // Save the current position so we can reset afterwards
                Vector3 oldPosition = player.Position;

                for (int i = 0; i < allClients.Count; i++)
                {
                    if (!impossibleX)
                    {
                        player.Position = new Vector3(targetPosition.X, oldPosition.Y, targetPosition.Z);
                        if ((player.Collides || allClients[i].Collides) && Collision.Colliding(player, allClients[i]))
                        {
                            impossibleX = true;
                        }
                    }

                    if (!impossibleY)
                    {
                        player.Position = new Vector3(impossibleX ? oldPosition.X : targetPosition.X, targetPosition.Y, targetPosition.Z);
                        if ((player.Collides || allClients[i].Collides) && Collision.Colliding(player, allClients[i]))
                        {
                            impossibleY = true;
                        }
                    }

                    if (impossibleX && impossibleY)
                    {
                        player.Position = oldPosition;
                        return;
                    }
                }
            }
        }
    }
}
