using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Microsoft.VisualBasic.CompilerServices;
using static Core.Enum;
using static Core.Global.Command;

namespace Server
{

    public class Server
    {
        private static bool consoleExit;
        private static Thread threadConsole;

        public static async Task Main()
        {
            threadConsole = new Thread(new ThreadStart(() => ConsoleThreadAsync().GetAwaiter().GetResult()));
            threadConsole.Start();

            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;

            // Spin up the server on the main thread
            await General.InitServerAsync();
        }

        private static void ProcessExitHandler(object sender, EventArgs e)
        {
            var loopTo = NetworkConfig.Socket?.HighIndex;
            for (int i = 0; i < loopTo; i++)
            {
                Player.LeftGame(i);
            }
            
            consoleExit = Conversions.ToBoolean(1);
            threadConsole.Join();
        }

        private static async Task ConsoleThreadAsync()
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
                            Console.WriteLine("/shutdown, shuts down the server");
                            break;
                        }

                    #endregion

                    case "/shutdown":
                        {
                            #region Body
                            if (General.GetShutDownTimer.IsRunning)
                            {
                                General.GetShutDownTimer.Stop();
                                Console.WriteLine("Server shutdown has been cancelled!");
                                NetworkSend.GlobalMsg("Server shutdown has been cancelled!");
                            }
                            else
                            {
                                if (General.GetShutDownTimer.ElapsedTicks > 0L)
                                {
                                    General.GetShutDownTimer.Restart();
                                }
                                else
                                {
                                    General.GetShutDownTimer.Start();
                                }
                                Console.WriteLine("Server shutdown in " + SettingsManager.Instance.ServerShutdown + " seconds!");
                                NetworkSend.GlobalMsg("Server shutdown in " + SettingsManager.Instance.ServerShutdown + " seconds!");
                            }

                            break;
                        }
                    #endregion

                    case "/exit":
                        {

                            #region  Body 

                            await General.DestroyServerAsync();
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
                                            NetworkSend.PlayerMsg(Pindex, "Your access has been set to Player!", (int)ColorType.BrightCyan);
                                            Console.WriteLine("Successfully set the access level to " + Access + " for player " + Name);
                                            break;
                                        }
                                    case (byte)AccessType.Moderator:
                                        {
                                            SetPlayerAccess(Pindex, Access);
                                            NetworkSend.SendPlayerData(Pindex);
                                            NetworkSend.PlayerMsg(Pindex, "Your access has been set to Moderator!", (int)ColorType.BrightCyan);
                                            Console.WriteLine("Successfully set the access level to " + Access + " for player " + Name);
                                            break;
                                        }
                                    case (byte)AccessType.Mapper:
                                        {
                                            SetPlayerAccess(Pindex, Access);
                                            NetworkSend.SendPlayerData(Pindex);
                                            NetworkSend.PlayerMsg(Pindex, "Your access has been set to Mapper!", (int)ColorType.BrightCyan);
                                            Console.WriteLine("Successfully set the access level to " + Access + " for player " + Name);
                                            break;
                                        }
                                    case (byte)AccessType.Developer:
                                        {
                                            SetPlayerAccess(Pindex, Access);
                                            NetworkSend.SendPlayerData(Pindex);
                                            NetworkSend.PlayerMsg(Pindex, "Your access has been set to Developer!", (int)ColorType.BrightCyan);
                                            Console.WriteLine("Successfully set the access level to " + Access + " for player " + Name);
                                            break;
                                        }
                                    case (byte)AccessType.Owner:
                                        {
                                            SetPlayerAccess(Pindex, Access);
                                            NetworkSend.SendPlayerData(Pindex);
                                            NetworkSend.PlayerMsg(Pindex, "Your access has been set to Owner!", (int)ColorType.BrightCyan);
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
                            Clock.Instance.GameSpeed = speed;
                            SettingsManager.Instance.TimeSpeed = speed;
                            SettingsManager.Save();
                            Console.WriteLine("Set GameSpeed to " + Clock.Instance.GameSpeed + " secs per seconds");
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