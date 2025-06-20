using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Mirage.Sharp.Asfw;
using static Core.Packets;

namespace Server
{
    public class TimeManager
    {
        private readonly Clock _clock;
        private readonly List<TimeZoneInfo> _supportedTimeZones;
        private readonly object _syncLock = new object();

        public TimeManager()
        {
            _clock = Clock.Instance;
            _supportedTimeZones = new List<TimeZoneInfo>
            {
                TimeZoneInfo.Utc,
                TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"),
                TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")
                // Add more time zones as needed
            };

            // Initialize event handlers
            _clock.OnTimeChanged += HandleTimeChanged;
            _clock.OnTimeOfDayChanged += HandleTimeOfDayChanged;
            _clock.OnTimeSync += HandleTimeSync;

            // Set initial time and game speed
            _clock.Time = DateTime.UtcNow;
            _clock.GameSpeed = 0;
        }

        // Public method to add a new time zone
        public void AddTimeZone(TimeZoneInfo timeZone)
        {
            if (timeZone == null)
            {
                throw new ArgumentNullException(nameof(timeZone));
            }
            lock (_syncLock)
            {
                if (!_supportedTimeZones.Contains(timeZone))
                {
                    _supportedTimeZones.Add(timeZone);
                }
            }
        }

        // Public method to remove a time zone
        public bool RemoveTimeZone(TimeZoneInfo timeZone)
        {
            if (timeZone == null)
            {
                throw new ArgumentNullException(nameof(timeZone));
            }
            lock (_syncLock)
            {
                return _supportedTimeZones.Remove(timeZone);
            }
        }

        // Public method to get the current time in a specific time zone
        public DateTime GetTimeInZone(TimeZoneInfo timeZone)
        {
            if (timeZone == null)
            {
                throw new ArgumentNullException(nameof(timeZone));
            }
            if (!_supportedTimeZones.Contains(timeZone))
            {
                throw new InvalidOperationException("Time zone not supported.");
            }
            return TimeZoneInfo.ConvertTime(_clock.Time, timeZone);
        }

        // Public method to synchronize time across multiple servers
        public async Task SynchronizeTimeAsync(IEnumerable<Server> servers)
        {
            if (servers == null)
            {
                throw new ArgumentNullException(nameof(servers));
            }
            foreach (var server in servers)
            {
                //TODO: Add server packet sending logic here
                //await server.SendTimeAsync(_clock.Time);
            }
        }

        // Event handler for time changes
        private void HandleTimeChanged(ref Clock source)
        {
            General.UpdateCaption();
        }

        // Event handler for time of day changes
        private void HandleTimeOfDayChanged(ref Clock source)
        {
            SendTimeToAll();
        }

        // Event handler for time synchronization
        private void HandleTimeSync(ref Clock source)
        {
            SendGameClockToAll();
        }

        // Send game clock to a specific index
        public void SendGameClockTo(int index)
        {
            using (var buffer = new ByteStream(4))
            {
                buffer.WriteInt32((int)ServerPackets.SClock);
                buffer.WriteInt32((int)_clock.GameSpeed);
                buffer.WriteBytes(BitConverter.GetBytes(_clock.Time.Ticks));
                NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            }
        }

        // Send game clock to all connected clients
        public void SendGameClockToAll()
        {
            var highIndex = NetworkConfig.Socket.HighIndex;
            for (int i = 0; i < highIndex; i++)
            {
                if (NetworkConfig.IsPlaying(i))
                {
                    SendGameClockTo(i);
                }
            }
        }

        // Send time to a specific index
        public void SendTimeTo(int index)
        {
            using (var buffer = new ByteStream(4))
            {
                buffer.WriteInt32((int)ServerPackets.STime);
                buffer.WriteByte((byte)_clock.TimeOfDay);
                NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            }
        }

        // Send time to all connected clients
        public void SendTimeToAll()
        {
            var highIndex = NetworkConfig.Socket.HighIndex;
            for (int i = 0; i < highIndex; i++)
            {
                if (NetworkConfig.IsPlaying(i))
                {
                    SendTimeTo(i);
                }
            }
        }

        // Helper method to get the current time of day
        public TimeOfDay GetTimeOfDay()
        {
            return _clock.TimeOfDay;
        }

        // Helper method to set the game speed
        public void SetGameSpeed(int speed)
        {
            if (speed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speed), "Game speed cannot be negative.");
            }
            _clock.GameSpeed = speed;
        }

        // Helper method to adjust the clock time
        public void AdjustTime(TimeSpan adjustment)
        {
            _clock.Time = _clock.Time.Add(adjustment);
        }
    }
}
