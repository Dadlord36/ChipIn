using System;
using ScriptableObjects.Parameters;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common
{
    public sealed class BehaviourTimeline : MonoBehaviour, ITimeline
    {
        public event Action OnElapsed;
        public event Action<float> Progressing;

        [SerializeField] private FloatParameter intervalParameter;

        private float _elapsedTime, _interval;
        public bool AutoReset { get; set; }

        private void Awake()
        {
            enabled = false;
            Assert.IsNotNull(intervalParameter, $"There is no {nameof(FloatParameter)} on: {name}");
            SetTimer(intervalParameter.value);
        }

        public void SetTimer(float interval, bool autoReset = false)
        {
            _interval = interval;
            AutoReset = autoReset;
        }

        public void StartTimer()
        {
            enabled = true;
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