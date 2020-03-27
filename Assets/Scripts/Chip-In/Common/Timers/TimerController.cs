using System;
using Common.Interfaces;
using UnityEngine;
using Utilities;

namespace Common.Timers
{
    public sealed class TimerController : ITimeline
    {
        private const string Tag = nameof(TimerController);

        private float _elapsedTime, _interval;

        public float Interval
        {
            get => _interval;
            set => _interval = value;
        }

        public event Action Elapsed;
        public event Action<float> Progressing;
        public bool AutoReset { get; set; }
        
        private void CheckIfTimerIntervalIsValid()
        {
            if (_interval <= 0) LogUtility.PrintLogError(Tag, nameof(_interval));
        }

        public void Initialize()
        {
            CheckIfTimerIntervalIsValid();
        }

        private float _progress;

        public void Update()
        {
            _elapsedTime += Time.deltaTime;
            _progress = Mathf.Clamp01(_elapsedTime / _interval);
            OnProgressing(_progress);
            if (!(_progress >= 1.0f)) return;
            OnElapsed();
            if (AutoReset)
            {
                RestartTimer();
            }
            else
            {
                StopTimer();
            }
        }

        public void StartTimer(float interval)
        {
            _interval = interval;
            RestartTimer();
        }

        private void ResetProgress()
        {
            _progress = _elapsedTime = 0f;
        }

        public void StartTimer()
        {
        }

        public void StopTimer()
        {
            ResetProgress();
        }

        public void RestartTimer()
        {
            ResetProgress();
        }

        private void OnProgressing(float percentage)
        {
            Progressing?.Invoke(percentage);
        }

        private void OnElapsed()
        {
            Elapsed?.Invoke();
        }
    }
}