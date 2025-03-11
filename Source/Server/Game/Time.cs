using System;
using Core;
using Mirage.Sharp.Asfw;
using static Core.Packets;

namespace Server
{

    static class Time
    {

        public static void InitTime()
        {
            // Add handlers to time events
            Core.Clock.Instance.OnTimeChanged += (ref Core.Clock source) => HandleTimeChanged(ref source);
            Core.Clock.Instance.OnTimeOfDayChanged += (ref Core.Clock source) => HandleTimeOfDayChanged(ref source);
            Core.Clock.Instance.OnTimeSync += (ref Core.Clock source) => HandleTimeSync(ref source);

            // Prepare the time instance
            Core.Clock.Instance.Time = DateTime.Now;
            Core.Clock.Instance.GameSpeed = 0;
        }

        public static void HandleTimeChanged(ref Core.Clock source)
        {
            General.UpdateCaption();
        }

        public static void HandleTimeOfDayChanged(ref Clock source)
        {
            SendTimeToAll();
        }

        public static void HandleTimeSync(ref Clock source)
        {
            SendGameClockToAll();
        }

        public static void SendGameClockTo(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SClock);
            buffer.WriteInt32((int)Clock.Instance.GameSpeed);
            buffer.WriteBytes(BitConverter.GetBytes(Clock.Instance.Time.Ticks));
            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendGameClockToAll()
        {
            int i;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= loopTo; i++)
            {
                if (NetworkConfig.IsPlaying(i))
                {
                    SendGameClockTo(i);
                }
            }
        }

        public static void SendTimeTo(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.STime);
            buffer.WriteByte((byte)Clock.Instance.TimeOfDay);
            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendTimeToAll()
        {
            int i;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= loopTo; i++)
            {
                if (NetworkConfig.IsPlaying(i))
                {
                    SendTimeTo(i);
                }
            }

        }

    }
}