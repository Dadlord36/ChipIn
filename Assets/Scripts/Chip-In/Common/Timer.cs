using System;

namespace Common
{
    public class Timer :  IDisposable
    {
        public event Action OnElapsed;
        private readonly System.Timers.Timer _timer;

        public bool AutoReset { get; set; }

        public Timer(double interval, bool autoReset)
        {
            AutoReset = autoReset;

            _timer = new System.Timers.Timer
            {
                Interval = interval, AutoReset = autoReset, Enabled = false
            };
            _timer.Elapsed += delegate { OnElapsed?.Invoke(); };
        }

        public void SetTimer(float interval, bool autoReset = false)
        {
            _timer.Interval = interval;
            _timer.AutoReset = autoReset;
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}