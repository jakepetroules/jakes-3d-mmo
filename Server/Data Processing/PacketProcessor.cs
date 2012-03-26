namespace MMO3D.Server
{
    using System;
    using System.Globalization;
    using Petroules.Synteza.Networking;
    using MMO3D.NetworkInterface;

    /// <summary>
    /// Processes packets in the MMO3D data transfer API.
    /// </summary>
    public static class PacketProcessor
    {
        /// <summary>
        /// Processes an packet sent from a client.
        /// </summary>
        /// <param name="server">A reference to the server object.</param>
        /// <param name="client">The client that sent the data.</param>
        /// <param name="packet">The packet that the client sent.</param>
        /// <exception cref="System.ArgumentNullException">Packet is null.</exception>
        public static void Process(GameServer server, PlayerHandler client, Packet packet)
        {
            if (packet == null)
            {
                throw new ArgumentNullException("packet");
            }

            // If the player's not logged in, process only the login packet
            if (!client.LoggedIn)
            {
                AuthenticationPacket pack = packet as AuthenticationPacket;
                if (pack != null)
                {
                    // Try to log the client in, this will send a packet telling everyone he's logged in
                    if (client.ClientLogOn(pack.UserId, pack.Password))
                    {
                        server.ServerGui.AddStatusText(string.Format(CultureInfo.CurrentCulture, Resources.CommandParserClientLogOnSuccess, pack.UserId, client.PlayerHostName));

                        PacketSender.SendToAll(new OrientationPacket(client));
                    }
                    else
                    {
                        server.ServerGui.AddStatusText(string.Format(CultureInfo.CurrentCulture, Resources.CommandParserClientLogOnFailure, pack.UserId, client.PlayerHostName));
                    }
                }
            }
            else
            {
                // Otherwise process all other messages...

                // Player chat
                ChatPacket chatPacket = packet as ChatPacket;
                if (chatPacket != null)
                {
                    if (chatPacket.IsCommand)
                    {
                        CommandProcessor.Process(server, client, chatPacket.Chat);
                    }
                    else
                    {
                        if (chatPacket.ChatType == ChatType.Global)
                        {
                            PacketSender.SendToAll(new ChatPacket(ChatFilter.CensorString(chatPacket.Chat), client.UserName, ChatType.Global));
                        }
                    }
                }

                OrientationPacket orientationPacket = packet as OrientationPacket;
                if (orientationPacket != null)
                {
                    client.SetOrientation(orientationPacket.Position, orientationPacket.Rotation, orientationPacket.Scaling);

                    PacketSender.SendToAllExcept(new OrientationPacket(client), client);
                }

                SwapSlotsPacket swapSlotsPacket = packet as SwapSlotsPacket;
                if (swapSlotsPacket != null)
                {
                    try
                    {
                        client.Inventory.SwapSlots(swapSlotsPacket.SwapIndex1, swapSlotsPacket.SwapIndex2);
                        client.SendPacket(new InventoryPacket(client.Inventory));
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // Invalid indexes sent by the client... TODO: log this
                        client.Dispose();
                    }
                }

                EquipPacket equipPacket = packet as EquipPacket;
                if (equipPacket != null)
                {
                    if (equipPacket.Equip)
                    {
                        try
                        {
                            if (client.Inventory.Equip(equipPacket.EquipIndex, equipPacket.Slot))
                            {
                                client.SendPacket(new InventoryPacket(client.Inventory));
                            }
                            else
                            {
                                client.SendPacket(new ErrorMessagePacket(Resources.InsufficientInventorySpace));
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            // Invalid indexes sent by the client... TODO: log this
                            client.Dispose();
                        }
                    }
                    else
                    {
                        if (client.Inventory.Remove(equipPacket.Slot))
                        {
                            client.SendPacket(new InventoryPacket(client.Inventory));
                        }
                        else
                        {
                            client.SendPacket(new ErrorMessagePacket(Resources.InsufficientInventorySpace));
                        }
                    }
                }

                DiscardPacket discardPacket = packet as DiscardPacket;
                if (discardPacket != null)
                {
                    try
                    {
                        client.Inventory.GetItems()[discardPacket.DiscardIndex] = null;
                        client.SendPacket(new InventoryPacket(client.Inventory));
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // Invalid indexes sent by the client... TODO: log this
                        client.Dispose();
                    }
                }

                MovementPacket movementPacket = packet as MovementPacket;
                if (movementPacket != null)
                {
                    client.MoveForwardBack(client, 1, movementPacket.MovementDirection, true);
                    client.SendPacket(new OrientationPacket(client));
                }
            }
        }
    }
}
