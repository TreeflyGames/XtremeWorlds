using System;
// MIT License
// 
// Copyright (c) 2017 Robert Lodico
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System.Timers;

namespace Core
{

    public enum TimeOfDay : byte
    {
        None = 0,
        Day,
        Night,
        Dawn,
        Dusk
    }

    public delegate void HandleTimeEvent(ref Clock source);

    public class Clock
    {
        private static Clock _mInstance = null;

        public static Clock Instance
        {
            get
            {
                if (_mInstance is null)
                {
                    _mInstance = new Clock();
                }

                return _mInstance;
            }
        }

        public event HandleTimeEvent OnTimeChanged;
        public event HandleTimeEvent OnTimeOfDayChanged;
        public event HandleTimeEvent OnTimeSync;

        private readonly Timer _mTimer;

        private DateTime _mTime;

        public DateTime Time
        {
            get
            {
                return _mTime;
            }
            set
            {
                _mTime = value;

                int arghours = Time.Hour;
                var newTimeOfDay = GetTimeOfDay(ref arghours);
                if (TimeOfDay != newTimeOfDay)
                    TimeOfDay = newTimeOfDay;

                var argsource = this;
                OnTimeChanged?.Invoke(ref argsource);
            }
        }

        private double _mGameSpeed;

        public double GameSpeed
        {
            get
            {
                return _mGameSpeed;
            }
            set
            {
                _mGameSpeed = value;
                var argsource = this;
                OnTimeSync?.Invoke(ref argsource);
            }
        }

        private int mSyncInterval;

        public int SyncInterval
        {
            get
            {
                return mSyncInterval;
            }
            set
            {
                mSyncInterval = value;

                _mTimer.Stop();
                _mTimer.Interval = mSyncInterval;
                _mTimer.Start();
                var argsource = this;
                OnTimeSync?.Invoke(ref argsource);
            }
        }

        private TimeOfDay mTimeOfDay;

        public TimeOfDay TimeOfDay
        {
            get
            {
                return mTimeOfDay;
            }
            set
            {
                mTimeOfDay = value;
                var argsource = this;
                OnTimeOfDayChanged?.Invoke(ref argsource);
            }
        }

        public Clock()
        {
            mSyncInterval = (int)Math.Round(6000.0);

            _mTimer = new Timer(SyncInterval);

            _mTimer.Elapsed += HandleTimerElapsed;

            _mTimer.Start();
        }

        private void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var argsource = this;
            OnTimeSync?.Invoke(ref argsource);
        }

        public override string ToString()
        {
            string argformat = "h:mm:ss tt";
            return ToString(ref argformat);
        }

        public string ToString(ref string format)
        {
            return Time.ToString(format);
        }

        public void Reset()
        {
            Time = new DateTime(0L);
        }

        public void Tick()
        {
            Time = Time.AddSeconds(GameSpeed);
        }

        public static TimeOfDay GetTimeOfDay(ref int hours)
        {
            if (hours < 6)
            {
                return TimeOfDay.Night;
            }
            else if (6 <= hours & hours <= 9)
            {
                return TimeOfDay.Dawn;
            }
            else if (9 < hours & hours < 18)
            {
                return TimeOfDay.Day;
            }
            else
            {
                return TimeOfDay.Dusk;
            }
        }
    }
}