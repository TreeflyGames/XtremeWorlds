using System;
using System.Threading;
using Core;
using Core.Common;
using Microsoft.VisualBasic.CompilerServices;
using static Core.Enum;
using static Core.Global.Command;

namespace Server
{

    static class Server
    {
        private static bool consoleExit;
        private static Thread threadConsole;

        public static void Main()
        {
            threadConsole = new Thread(new ThreadStart(ConsoleThread));
            threadConsole.Start();

            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;

            // Spin up the server on the main thread
            General.InitServer();
        }

        private static void ProcessExitHandler(object sender, EventArgs e)
        {
            Loop.UpdateSavePlayers();
            consoleExit = Conversions.ToBoolean(1);
            threadConsole.Join();
        }

        private static void ConsoleThread()
        {
            string line;
            string[] parts;

            Console.WriteLine("Initializing Console Loop");

            while (!consoleExit)
            {
                try
                {
                    line = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    break;
                }

                parts = line.Split(" ");
                if (parts.Length < 1)
                    continue;

                switch (parts[0].ToLower() ?? "")
                {
                    case "/help":
                        {
                            #region  Body 

                            Console.WriteLine("/help, shows this message.");
                            Console.WriteLine("/exit, closes down the server.");
                            Console.WriteLine("/access, sets player access level, use with '/access name level goes from 1 for Player, to 5 to Owner.");
                            Console.WriteLine("/kick, kicks user from server, use with '/kick name'");
                            Console.WriteLine("/ban, bans user from server, use with '/ban name'");
                            Console.WriteLine("/shutdown, shuts down the server after 60 seconds or a value you specify in seconds");
                            break;
                        }

                    #endregion

                    case "/shutdown":
                        {
                            #region Body
                            if (parts.Length < 2)
                            {
                                General.shutDownDuration = 60;
                            }
                            else
                            {
                                General.shutDownDuration = Conversions.ToInteger(parts[1]);
                            }

                            if (General.shutDownTimer.IsRunning)
                            {
                                General.shutDownTimer.Stop();
                                General.shutDownDuration = 0;
                                Console.WriteLine("Server shutdown has been cancelled!");
                                NetworkSend.GlobalMsg("Server shutdown has been cancelled!");
                            }
                            else
                            {
                                if (General.shutDownTimer.ElapsedTicks > 0L)
                                {
                                    General.shutDownTimer.Restart();
                                }
                                else
                                {
                                    General.shutDownTimer.Start();
                                }

                                Console.WriteLine("Server shutdown in " + General.shutDownDuration + " seconds!");
                                NetworkSend.GlobalMsg("Server shutdown in " + General.shutDownDuration + " seconds!");
                            }

                            break;
                        }
                    #endregion

                    case "/exit":
                        {

                            #region  Body 

                            General.DestroyServer();
                            break;
                        }

                    #endregion

                    case "/access":
                        {
                            #region Body
                            if (parts.Length < 3)
                                continue;

                            string Name = parts[1];
                            int Pindex = GameLogic.FindPlayer(Name);
                            int Access;
                            int.TryParse(parts[2], out Access);

                            if (Pindex == -1)
                            {
                                Console.WriteLine("Player name is empty or invalid. [Name not found]");
                            }
                            else
                            {
                                switch (Access)
                                {
                                    case (byte)AccessType.Player:
                                        {
                                            SetPlayerAccess(Pindex, Access);
                                            NetworkSend.SendPlayerData(Pindex);
                                            NetworkSend.PlayerMsg(Pindex, "Your access has been set to Player!", (int) ColorType.BrightCyan);
                                            Console.WriteLine("Successfully set the access level to " + Access + " for player " + Name);
                                            break;
                                        }
                                    case (byte) AccessType.Moderator:
                                        {
                                            SetPlayerAccess(Pindex, Access);
                                            NetworkSend.SendPlayerData(Pindex);
                                            NetworkSend.PlayerMsg(Pindex, "Your access has been set to Moderator!", (int) ColorType.BrightCyan);
                                            Console.WriteLine("Successfully set the access level to " + Access + " for player " + Name);
                                            break;
                                        }
                                    case (byte) AccessType.Mapper:
                                        {
                                            SetPlayerAccess(Pindex, Access);
                                            NetworkSend.SendPlayerData(Pindex);
                                            NetworkSend.PlayerMsg(Pindex, "Your access has been set to Mapper!", (int) ColorType.BrightCyan);
                                            Console.WriteLine("Successfully set the access level to " + Access + " for player " + Name);
                                            break;
                                        }
                                    case (byte) AccessType.Developer:
                                        {
                                            SetPlayerAccess(Pindex, Access);
                                            NetworkSend.SendPlayerData(Pindex);
                                            NetworkSend.PlayerMsg(Pindex, "Your access has been set to Developer!", (int) ColorType.BrightCyan);
                                            Console.WriteLine("Successfully set the access level to " + Access + " for player " + Name);
                                            break;
                                        }
                                    case (byte) AccessType.Owner:
                                        {
                                            SetPlayerAccess(Pindex, Access);
                                            NetworkSend.SendPlayerData(Pindex);
                                            NetworkSend.PlayerMsg(Pindex, "Your access has been set to Owner!", (int) ColorType.BrightCyan);
                                            Console.WriteLine("Successfully set the access level to " + Access + " for player " + Name);
                                            break;
                                        }

                                    default:
                                        {
                                            Console.WriteLine("Failed to set the access level to " + Access + " for player " + Name);
                                            break;
                                        }
                                }
                            }

                            break;
                        }

                    #endregion

                    case "/kick":
                        {
                            #region Body
                            if (parts.Length < 2)
                                continue;

                            string Name = parts[1];
                            int Pindex = GameLogic.FindPlayer(Name);
                            if (Pindex == -1)
                            {
                                Console.WriteLine("Player name is empty or invalid.");
                            }
                            else
                            {
                                NetworkSend.AlertMsg(Pindex, (int)DialogueMsg.Kicked);
                                Player.LeftGame(Pindex);
                            }

                            break;
                        }
                    #endregion

                    case "/ban":
                        {
                            #region Body
                            if (parts.Length < 2)
                                continue;

                            string Name = parts[1];
                            int Pindex = GameLogic.FindPlayer(Name);
                            if (Pindex == -1)
                            {
                                Console.WriteLine("Player name is empty or invalid. [Name not found]");
                            }
                            else
                            {
                                Database.ServerBanindex(Pindex);
                            }

                            break;
                        }

                    #endregion

                    case "/timespeed":
                        {
                            #region  Body 
                            if (parts.Length < 2)
                                return;

                            double speed;
                            double.TryParse(parts[1], out speed);
                            TimeType.Instance.GameSpeed = speed;
                            Settings.Instance.TimeSpeed = speed;
                            Settings.Save();
                            Console.WriteLine("Set GameSpeed to " + TimeType.Instance.GameSpeed + " secs per seconds");
                            break;
                        }

                    case var case5 when case5 == "":
                        {

                            #endregion

                            continue;
                        }

                    default:
                        {
                            Console.WriteLine("Invalid  If you are unsure of the functions type '/help'.");
                            break;
                        }
                }
            }
        }

    }
}