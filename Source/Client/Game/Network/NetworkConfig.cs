using System;
using System.Runtime.CompilerServices;
using Core;
using Mirage.Sharp.Asfw.Network;

namespace Client
{

    public class NetworkConfig
    {
        private static NetworkClient _Socket;

        public static NetworkClient Socket
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
                    _Socket.ConnectionSuccess -= Socket_ConnectionSuccess;
                    _Socket.ConnectionFailed -= Socket_ConnectionFailed;
                    _Socket.ConnectionLost -= Socket_ConnectionLost;
                    _Socket.CrashReport -= Socket_CrashReport;
                    _Socket.TrafficReceived -= Socket_TrafficReceived;
                    _Socket.PacketReceived -= Socket_PacketReceived;
                }

                _Socket = value;
                if (_Socket != null)
                {
                    _Socket.ConnectionSuccess += Socket_ConnectionSuccess;
                    _Socket.ConnectionFailed += Socket_ConnectionFailed;
                    _Socket.ConnectionLost += Socket_ConnectionLost;
                    _Socket.CrashReport += Socket_CrashReport;
                    _Socket.TrafficReceived += Socket_TrafficReceived;
                    _Socket.PacketReceived += Socket_PacketReceived;
                }
            }
        }

        public static void InitNetwork()
        {
            try
            {
                // Initialize the network client with packet count and buffer size.
                Socket = new NetworkClient((int)Packets.ServerPackets.COUNT, 8192);

                // Start the connection attempt.
                Socket.ConnectionSuccess += OnConnectionSuccess;

                Socket.Connect(Settings.Instance.IP, Settings.Instance.Port); // Adjust IP and port as needed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Network initialization failed: {ex.Message}");
            }
        }

        private static void OnConnectionSuccess()
        {
            Console.WriteLine("Connection established. Starting packet router...");
            NetworkReceive.PacketRouter();
        }

        public static void DestroyNetwork()
        {
            // Calling a disconnect is not necessary when using destroy network as
            // Dispose already calls it and cleans up the memory internally.
            Socket?.Dispose();
        }

        #region  Events 

        private static void Socket_ConnectionSuccess()
        {
            Console.WriteLine("Connection success.");
        }

        private static void Socket_ConnectionFailed()
        {
            Console.WriteLine("Failed to connect to the server. Retrying...");
            InitNetwork();
        }

        private static void Socket_ConnectionLost()
        {
            Console.WriteLine("Connection lost.");
        }

        private static void Socket_CrashReport(string err)
        {
            GameLogic.LogoutGame();
            GameLogic.DialogueAlert((byte)Core.Enum.DialogueMsg.Crash);

            var currentDateTime = DateTime.Now;
            string timestampForFileName = currentDateTime.ToString("yyyyMMdd_HHmmss");
            string logFileName = $"{timestampForFileName}.txt";

            Core.Log.Add(err, logFileName);
        }

        private static void Socket_TrafficReceived(int size, ref byte[] data)
        {
            Console.WriteLine("Traffic Received : [Size: " + size + "]");
            byte[] tmpData = data;
            // Put breakline on tmpData to look at what is contained in data at runtime in the VS logger.
        }

        private static void Socket_PacketReceived(int size, int header, ref byte[] data)
        {
            Console.WriteLine("Packet Received : [Size: " + size + "| Packet: " + ((Packets.ServerPackets)header).ToString() + "]");
            byte[] tmpData = data;
            // Put breakline on tmpData to look at what is contained in data at runtime in the VS logger.
        }
        #endregion

    }
}