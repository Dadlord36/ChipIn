using System;
using Common.Interfaces;
using UnityEngine;

namespace Common.Timers
{
    public abstract class BaseBehaviourTimeline : MonoBehaviour, ITimeline
    {
        public event Action OnElapsed;
        public event Action<float> Progressing;

        private float _elapsedTime, _interval;
        public bool AutoReset { get; set; }

        private void Awake()
        {
            enabled = false;
            InitializerTimer(out _interval);
            CheckIfTimerIntervalIsValid();
        }

        private void CheckIfTimerIntervalIsValid()
        {
            if (_interval <= 0) throw new ArgumentOutOfRangeException(nameof(_interval));
        }

        protected abstract void InitializerTimer(out float timerInterval);

        public void StartTimer()
        {
            enabled = true;
        }

        public void StartTimer(float interval)
        {
            _interval = interval;
            RestartTimer();
        }

        public void StopTimer()
        {
            enabled = false;
            _elapsedTime = 0f;
        }

        public void RestartTimer()
        {
            _elapsedTime = 0f;
            enabled = true;
        }

        private float _progress;

        public void Update()
        {
            _elapsedTime += Time.deltaTime;
            _progress = Mathf.Clamp01(_elapsedTime / _interval);
            OnProgressing(_progress);
            if (!(_progress >= 1.0f)) return;
            OnElapsed?.Invoke();
            if (AutoReset)
            {
                RestartTimer();
            }
            else
            {
                StopTimer();
            }
        }

        private void OnProgressing(float percentage)
        {
            Progressing?.Invoke(percentage);
        }
    }
}