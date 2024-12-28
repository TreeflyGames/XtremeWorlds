using System;
using System.Runtime.CompilerServices;
using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw.Network;
using static Core.Global.Command;
using static Core.Packets;

namespace Server
{

    internal static class NetworkConfig
    {
        private static NetworkServer _Socket;

        internal static NetworkServer Socket
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

        internal static void InitNetwork()
        {
            if (Socket is not null)
                return;

            // Establish some Rulez
            Socket = new NetworkServer((int)Packets.ClientPackets.Count, 8192, Core.Constant.MAX_PLAYERS)
            {
                BufferLimit = 2048000, // <- this is 2mb Core.Constant.MAX data storage
                MinimumIndex = 1, // <- this prevents the network from giving us 0 as an index
                PacketAcceptLimit = 500, // Dunno what is a reasonable cap right now so why not? :P
                PacketDisconnectCount = 100 // If the other thing was even remotely reasonable, this is DEFINITELY spam count!
            };
            // END THE ESTABLISHMENT! WOOH ANARCHY! ~SpiceyWolf

            NetworkReceive.PacketRouter(); // Need them packet ids boah!
        }

        internal static void DestroyNetwork()
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

        public static bool IsMultiAccounts(int index, string login)
        {
            for (int i = 0, loopTo = Socket.HighIndex; i <= (int)loopTo; i++)
            {
                if (i != index)
                {
                    if (login != "" && Core.Type.Account[i].Login.ToLower() != "")
                    {
                        if (Core.Type.Account[i].Login.ToLower() != login)
                        {
                            if (Socket.ClientIp(i) == Socket.ClientIp(index))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            
            return false;
        }

        public static void CheckMultiAccounts(int index, string login)
        {
            for (int i = 0, loopTo = Socket.HighIndex; i <= (int)loopTo; i++)
            {
                if (login != "" && Core.Type.Account[i].Login.ToLower() != "")
                {
                    if (Core.Type.Account[i].Login.ToLower() == login)
                    {
                        Player.LeftGame(i);
                    }
                }
            }
        }

        internal static void SendDataToAll(ref byte[] data, int head)
        {
            for (int i = 0, loopTo = Socket.HighIndex; i <= (int)loopTo; i++)
                Socket.SendDataTo(i, data, head);
        }

        public static void SendDataToAllBut(int index, ref byte[] data, int head)
        {
            for (int i = 0, loopTo = Socket.HighIndex; i <= (int)loopTo; i++)
            {
                if (i != index)
                {
                    Socket.SendDataTo(i, data, head);
                }
            }
        }

        public static void SendDataToMapBut(int index, int mapNum, ref byte[] data, int head)
        {
            for (int i = 0, loopTo = Socket.HighIndex; i <= (int)loopTo; i++)
            {
                if (GetPlayerMap(i) == mapNum & i != index)
                {
                    Socket.SendDataTo(i, data, head);
                }
            }
        }

        public static void SendDataToMap(int MapNum, ref byte[] data, int head)
        {
            int i;

            var loopTo = Socket.HighIndex;
            for (i = 0; i <= (int)loopTo; i++)
            {

                if (GetPlayerMap(i) == MapNum)
                {
                    Socket.SendDataTo(i, data, head);
                }

            }

        }

        public static void SendDataTo(int index, ref byte[] data, int head)
        {
            Socket.SendDataTo(index, data, head);
        }

        #region Events

        internal static void Socket_ConnectionReceived(int index)
        {
            Console.WriteLine("Connection received on index[" + index + "] - IP[" + Socket.ClientIp(index) + "]");
            NetworkSend.SendKeyPair(index);
        }

        internal static void Socket_ConnectionLost(int index)
        {
            Console.WriteLine("Connection lost on index [" + index + "] - IP[" + Socket.ClientIp(index) + "]");
            Player.LeftGame(index);
        }

        internal static void Socket_CrashReport(int index, string err)
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