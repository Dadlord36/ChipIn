using System;
using UnityEngine;

namespace Common
{
    public class Timer : MonoBehaviour
    {
        public event Action OnElapsed; 
        
        private float _elapsedTime, _interval;

        private bool _autoReset;
        
        private void Awake()
        {
            enabled = false;
        }

        public void SetTimer(float interval, bool autoReset = false)
        {
            _interval = interval;
            _autoReset = autoReset;
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

        public void Update()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _interval)
            {
                OnElapsed?.Invoke();
                if (_autoReset)
                {
                    _elapsedTime = 0f;
                }
                else
                {
                    StopTimer();
                }
            }
        }
    }
}