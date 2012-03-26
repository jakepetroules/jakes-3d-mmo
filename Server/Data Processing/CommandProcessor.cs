namespace MMO3D.Server
{
    using System;
    using System.Globalization;
    using System.Text;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;
    using MMO3D.NetworkInterface;

    /// <summary>
    /// Processes text commands.
    /// </summary>
    public static class CommandProcessor
    {
        /// <summary>
        /// Processes an text command sent from a client.
        /// </summary>
        /// <param name="server">A reference to the server object.</param>
        /// <param name="player">The player that sent the data.</param>
        /// <param name="command">The command text that the client sent.</param>
        /// <exception cref="System.ArgumentNullException">Command is null.</exception>
        public static void Process(GameServer server, PlayerHandler player, string command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command", "Command text cannot be null.");
            }

            ServerCommand cmd = ServerCommand.Parse(command);

            switch (cmd.CommandName)
            {
                case "item":
                    if (player.UserLevel.HasPermission(UserPermissionLevel.Administrator))
                    {
                        bool error = false;

                        try
                        {
                            int id = Convert.ToInt32(cmd.Parameters[0], CultureInfo.InvariantCulture);

                            if (player.Inventory.Add(ItemCreator.CreateFromId(id)))
                            {
                                PacketSender.Send(new InventoryPacket(player.Inventory), player);
                            }
                            else
                            {
                                PacketSender.Send(new ErrorMessagePacket(Resources.InsufficientInventorySpace), player);
                            }
                        }
                        catch (FormatException)
                        {
                            error = true;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            error = true;
                        }
                        catch (Exception e)
                        {
                            System.Console.WriteLine(e);
                        }

                        if (error)
                        {
                            PacketSender.Send(new ErrorMessagePacket(CommandProcessor.ShowUsage(cmd, "item-ID")), player);
                        }
                    }
                    else
                    {
                        PacketSender.Send(new ErrorMessagePacket(Resources.MustBeAdministrator), player);
                    }

                    break;
                case "tele":
                    if (player.UserLevel.HasPermission(UserPermissionLevel.Administrator))
                    {
                        bool error = false;

                        try
                        {
                            float x = Convert.ToSingle(cmd.Parameters[0], CultureInfo.InvariantCulture);
                            float y = Convert.ToSingle(cmd.Parameters[1], CultureInfo.InvariantCulture);

                            player.Position = new Vector3(x, y, player.Position.Z);

                            PacketSender.Send(new OrientationPacket(player), player);
                        }
                        catch (FormatException)
                        {
                            error = true;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            error = true;
                        }

                        if (error)
                        {
                            PacketSender.Send(new ErrorMessagePacket(CommandProcessor.ShowUsage(cmd, "x", "y")), player);
                        }
                    }
                    else
                    {
                        PacketSender.Send(new ErrorMessagePacket(Resources.MustBeAdministrator), player);
                    }

                    break;
            }
        }

        /// <summary>
        /// Gets a string showing the proper usage of a command.
        /// </summary>
        /// <param name="cmd">The command to retrieve the command name of.</param>
        /// <param name="parameterValues">The expected values of the parameters.</param>
        /// <returns>See summary.</returns>
        private static string ShowUsage(ServerCommand cmd, params string[] parameterValues)
        {
            StringBuilder sb = new StringBuilder(string.Format(CultureInfo.InvariantCulture, "Usage: {0}{1}", ServerCommand.CommandPrefix, cmd.CommandName));

            for (int i = 0; i < parameterValues.Length; i++)
            {
                sb.AppendFormat(" {0}", parameterValues[i].Replace(' ', '-'));
            }

            return sb.ToString();
        }
    }
}
