using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class Timer : IDisposable
    {
        public event Action OnElapsed;

        private CancellationTokenSource _cancellationTokenSource;
        public bool AutoReset { get; set; }
        private int _interval;

        public Timer(int interval, bool autoReset)
        {
            SetTimer(interval, autoReset);
        }

        ~Timer()
        {
            Dispose(false);
        }

        public void SetTimer(int millisecondsInterval, bool autoReset = false)
        {
            _interval = millisecondsInterval;
            AutoReset = autoReset;
        }

        public async Task StartTimer()
        {
            try
            {
                while (true)
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    await Task.Delay(_interval, _cancellationTokenSource.Token);
                    OnOnElapsed();
                    if (AutoReset) continue;
                    break;
                }
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }

        public void StopTimer()
        {
            _cancellationTokenSource.Cancel();
        }

        private void ReleaseUnmanagedResources()
        {
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                _cancellationTokenSource?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void OnOnElapsed()
        {
            OnElapsed?.Invoke();
        }
    }
}