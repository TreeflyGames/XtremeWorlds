using System;
using Core.Common;
using Mirage.Sharp.Asfw;
using static Core.Packets;

namespace Server
{

    static class Time
    {

        public static void InitTime()
        {
            // Add handlers to time events
            TimeType.Instance.OnTimeChanged += (ref Core.Common.TimeType source) => HandleTimeChanged(ref source);
            TimeType.Instance.OnTimeOfDayChanged += (ref Core.Common.TimeType source) => HandleTimeOfDayChanged(ref source);
            TimeType.Instance.OnTimeSync += (ref Core.Common.TimeType source) => HandleTimeSync(ref source);

            // Prepare the time instance
            TimeType.Instance.Time = DateTime.Now;
            TimeType.Instance.GameSpeed = 0;
        }

        public static void HandleTimeChanged(ref TimeType source)
        {
            General.UpdateCaption();
        }

        public static void HandleTimeOfDayChanged(ref TimeType source)
        {
            SendTimeToAll();
        }

        public static void HandleTimeSync(ref TimeType source)
        {
            SendGameClockToAll();
        }

        public static void SendGameClockTo(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((byte) ServerPackets.SClock);
            buffer.WriteInt32((int)TimeType.Instance.GameSpeed);
            buffer.WriteBytes(BitConverter.GetBytes(TimeType.Instance.Time.Ticks));
            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendGameClockToAll()
        {
            int i;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= (int)loopTo; i++)
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

            buffer.WriteInt32((byte) ServerPackets.STime);
            buffer.WriteByte((byte)TimeType.Instance.TimeOfDay);
            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendTimeToAll()
        {
            int i;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= (int)loopTo; i++)
            {
                if (NetworkConfig.IsPlaying(i))
                {
                    SendTimeTo(i);
                }
            }

        }

    }
}