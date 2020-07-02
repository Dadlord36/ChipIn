using System;
using Common.Interfaces;
using Common.Structures.ProgressionTwiners;
using UnityEngine;

namespace CustomAnimators.GeneratedAnimationActions
{
    public abstract class ProgressiveAction : IUpdatableProgress
    {
        public event Action ProgressReachesEnd;
        private FloatProgressionTwiner _timeProgression;
        private readonly AnimationCurve _speedCurve;

        protected ProgressiveAction(AnimationCurve speedCurve, in float time)
        {
            _speedCurve = speedCurve;
            _timeProgression = new FloatProgressionTwiner(0f, time);
        }

        private float _progressPercentage;


        public void Update()
        {
            _progressPercentage = _timeProgression.PreProgress(Time.deltaTime * _speedCurve.Evaluate(_progressPercentage));
            ProgressUpdate(_progressPercentage);
            if (_progressPercentage >= 1f)
            {
                OnProgressReachesEnd();
            }
        }

        protected abstract void ProgressUpdate(float progressPercentage);

        private void OnProgressReachesEnd()
        {
            ProgressReachesEnd?.Invoke();
        }
    }
}