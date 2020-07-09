using System;
using Common.Interfaces;
using UnityEngine;

namespace Common.Timers
{
    public abstract class BaseBehaviourTimeline : MonoBehaviour, ITimeline
    {
        private const string Tag = nameof(BaseBehaviourTimeline);

        private readonly ITimeline _timeline = new TimerController();

        public event Action<float> Progressing
        {
            add => _timeline.Progressing += value;
            remove => _timeline.Progressing -= value;
        }

        public event Action Elapsed
        {
            add => _timeline.Elapsed += value;
            remove => _timeline.Elapsed -= value;
        }

        public bool AutoReset { get; set; }

        public float Interval
        {
            get => _timeline.Interval;
            set => _timeline.Interval = value;
        }

        public void Initialize()
        {
            enabled = false;
            InitializeTimer(out var interval);
            Interval = interval;
        }

        public void StartTimer()
        {
            Elapsed += StopTimer;
            _timeline.StartTimer();
            enabled = true;
        }

        public void StartTimer(float interval)
        {
            Elapsed += StopTimer;
            _timeline.StartTimer(interval);
            enabled = true;
        }

        public void StopTimer()
        {
            enabled = false;
            _timeline.Elapsed -= StopTimer;
            _timeline.StopTimer();
        }

        public void RestartTimer()
        {
            _timeline.RestartTimer();
            Elapsed += StopTimer;
            enabled = true;
        }

        protected abstract void InitializeTimer(out float timerInterval);
        
        public void Update()
        {
            _timeline.Update();
        }
    }
}