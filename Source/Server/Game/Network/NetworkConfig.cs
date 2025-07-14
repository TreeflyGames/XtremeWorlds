using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
using Mirage.Sharp.Asfw.Network;
using Reoria.Engine.Common.Security.Encryption;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using static Core.Enum;
using static Core.Global.Command;
using static Core.Packets;

namespace Server
{

    public class NetworkConfig
    {
        private static NetworkServer _Socket;

        public static NetworkServer Socket
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Socket;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Socket != null)
                {
                    _Socket.ConnectionReceived -= Socket_ConnectionReceived;
                    _Socket.ConnectionLost -= Socket_ConnectionLost;
                    _Socket.CrashReport -= Socket_CrashReport;
                    _Socket.TrafficReceived -= Socket_TrafficReceived;
                    _Socket.PacketReceived -= Socket_PacketReceived;
                }

                _Socket = value;
                if (_Socket != null)
                {
                    _Socket.ConnectionReceived += Socket_ConnectionReceived;
                    _Socket.ConnectionLost += Socket_ConnectionLost;
                    _Socket.CrashReport += Socket_CrashReport;
                    _Socket.TrafficReceived += Socket_TrafficReceived;
                    _Socket.PacketReceived += Socket_PacketReceived;
                }
            }
        }

        public static void InitNetwork()
        {
            if (Socket is not null)
                return;

            // Establish some Rulez
            Socket = new NetworkServer((int)Packets.ClientPackets.Count, 8192, Core.Constant.MAX_PLAYERS)
            {
                BufferLimit = 5048000, // <- this is 2mb MAX data storage
                MinimumIndex = 0, // <- this prevents the network from giving us 0 as an index
            };
            // END THE ESTABLISHMENT! WOOH ANARCHY! ~SpiceyWolf

            NetworkReceive.PacketRouter(); // Need them packet ids boah!
        }

        public static void DestroyNetwork()
        {
            Socket.Dispose();
        }

        public static bool IsLoggedIn(int index)
        {
            return Core.Type.Account[index].Login.Length > 0;
        }

        public static bool IsPlaying(int index)
        {
            return Core.Type.TempPlayer[index].InGame;
        }

        public static bool IsMultiLogin(int index, string login)
        {
            for (int i = 0, loopTo = Socket.HighIndex; i <= loopTo; i++)
            {
                if (i != index)
                {
                    if (login != "" && Core.Type.Account[i].Login.ToLower() != "")
                    {
                        if (Core.Type.Account[i].Login.ToLower() != login)
                        {
                            if (Socket.ClientIP(i) == Socket.ClientIP(index))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            
            return false;
        }

        public static async Task LoadAccount(int index, string login, byte slot)
        {
            for (int i = 0, loopTo = Socket.HighIndex; i <= loopTo; i++)
            {
                if (login != "" && Core.Type.Account[i].Login.ToLower() != "")
                {
                    if (Core.Type.Account[i].Login.ToLower() == login)
                    {
                        if (index != i)
                        {
                            await Player.LeftGame(i);
                            break;
                        }
                    }
                }
            }

            Database.LoadCharacter(index, slot);
            Database.LoadBank(index);

            // Check if character data has been created
            if (Strings.Len(Core.Type.Player[index].Name) > 0)
            {
                // we have a char!                        
                Player.HandleUseChar(index);
            }
            else
            {
                NetworkSend.AlertMsg(index, (byte)DialogueMsg.Database, (byte)MenuType.Chars);
            }
        }

        public static void SendDataToAll(ReadOnlySpan<byte> data, int head)
        {
            for (int i = 0, loopTo = Socket.HighIndex; i <= loopTo; i++)
                Socket.SendDataTo(i, data, head);
        }

        public static void SendDataToAllBut(int index, ReadOnlySpan<byte> data, int head)
        {
            for (int i = 0, loopTo = Socket.HighIndex; i <= loopTo; i++)
            {
                if (i != index)
                {
                    Socket.SendDataTo(i, data, head);
                }
            }
        }

        public static void SendDataToMapBut(int index, int mapNum, ReadOnlySpan<byte> data, int head)
        {
            for (int i = 0, loopTo = Socket.HighIndex; i <= loopTo; i++)
            {
                if (IsPlaying(i))
                {
                    if (i != index)
                    {
                        if (GetPlayerMap(i) == mapNum)
                        {
                            Socket.SendDataTo(i, data, head);
                        }
                    }
                }
            }
        }

        public static void SendDataToMap(int mapNum, ReadOnlySpan<byte> data, int head)
        {
            int i;

            var loopTo = Socket.HighIndex;
            for (i = 0; i <= loopTo; i++)
            {
                if (IsPlaying(i))
                {
                    if (GetPlayerMap(i) == mapNum)
                    {
                        Socket.SendDataTo(i, data, head);
                    }
                }
            }

        }

        public static void SendDataTo(int index, ReadOnlySpan<byte> data, int head)
        {
            Socket.SendDataTo(index, data, head);
        }

        #region Events

        public static void Socket_ConnectionReceived(int index)
        {
            Console.WriteLine("Connection received on index [" + index + "] - IP[" + Socket.ClientIP(index) + "]");
            General.Aes.TryAdd(index, new AesEncryption());
            NetworkSend.AesKeyIV(index);

        }

        public static void Socket_ConnectionLost(int index)
        {
            Console.WriteLine("Connection lost on index [" + index + "] - IP[" + Socket.ClientIP(index) + "]");
            Player.LeftGame(index);
            General.Aes.Remove(index);
        }

        public static void Socket_CrashReport(int index, string err)
        {
            Console.WriteLine("There was a network error index [" + index + "]");
            Console.WriteLine("Report: " + err);
        }

        private static void Socket_TrafficReceived(int size, ref byte[] data)
        {
            if (Conversions.ToInteger(Global.DebugTxt) == 1)
            {
                Console.WriteLine("Traffic Received: [Size: " + size + "]");
            }
        }

        private static void Socket_PacketReceived(int size, int header, ref byte[] data)
        {
            if (Conversions.ToInteger(Global.DebugTxt) == 1)
            {
                Console.WriteLine("Packet Received: [Size: " + size + "| Packet: " + ((ClientPackets)header).ToString() + "]");
            }
        }

        #endregion

    }
}