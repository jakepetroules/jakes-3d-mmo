namespace MMO3D.Server
{
    using System;
    using System.Linq;
    using Petroules.Synteza.Networking;

    /// <summary>
    /// Contains methods for sending data to clients.
    /// </summary>
    public static class PacketSender
    {
        /// <summary>
        /// Reference to the server object.
        /// </summary>
        private static GameServer server;

        /// <summary>
        /// Initializes the packet sender.
        /// </summary>
        /// <param name="server">Reference to the server object.</param>
        public static void Initialize(GameServer server)
        {
            PacketSender.server = server;
        }

        /// <summary>
        /// Sends a packet to a specific player or players.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        /// <param name="players">The player(s) to send the packet to.</param>
        /// <returns>Whether the data was successfully sent to all the player(s).</returns>
        public static bool Send(Packet packet, params PlayerHandler[] players)
        {
            return PacketSender.InternalSend(packet, players, new PlayerHandler[] { });
        }

        /// <summary>
        /// Sends a packet to all clients currently connected.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        /// <returns>Whether the data was successfully sent to all the players.</returns>
        public static bool SendToAll(Packet packet)
        {
            return PacketSender.Send(packet, PacketSender.server.Clients.ToArray());
        }

        /// <summary>
        /// Sends a packet to all clients currently connected, except the one(s) specified.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        /// <param name="exceptions">The clients NOT to send the packet to.</param>
        /// <returns>Whether the data was successfully sent to all the players.</returns>
        public static bool SendToAllExcept(Packet packet, params PlayerHandler[] exceptions)
        {
            return PacketSender.InternalSend(packet, PacketSender.server.Clients.ToArray(), exceptions);
        }

        /// <summary>
        /// Sends a packet to the specified clients in the first array,
        /// except the one(s) specified in the second array.
        /// TODO: This does nothing if a packet fails to be sent to a client,
        /// and also silently discards any parameters being null.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        /// <param name="players">The players to send the packet to.</param>
        /// <param name="exceptions">The players NOT to send the packet to.</param>
        /// <returns>Whether the data was successfully sent to all the players included.</returns>
        private static bool InternalSend(Packet packet, PlayerHandler[] players, PlayerHandler[] exceptions)
        {
            if (packet != null && players != null && exceptions != null)
            {
                // Assume success; set to false if there was a failure was caught
                bool success = true;

                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i] != null && !exceptions.Contains(players[i]))
                    {
                        if (!players[i].SendPacket(packet))
                        {
                            success = false;
                        }
                    }
                }

                return success;
            }

            return false;
        }
    }
}
