using System;
using Common.Interfaces;
using Common.Structures.ProgressionTwiners;
using UnityEngine;

namespace CustomAnimators
{
    public sealed class MoveToPoint : IUpdatableProgress
    {
        public event Action ProgressReachesEnd;

        private readonly Transform _transform;
        private readonly AnimationCurve _speedCurve;
        private readonly Vector2 _startPoint;
        private readonly Vector2 _endPoint;
        private FloatProgressionTwiner timeProgression;

        public MoveToPoint(Transform transform, AnimationCurve speedCurve, Vector2 startPoint, Vector2 endPoint, float time)
        {
            _transform = transform;
            _speedCurve = speedCurve;
            _startPoint = startPoint;
            _endPoint = endPoint;
            timeProgression = new FloatProgressionTwiner(0f, time);
        }

        private Vector2 CalculatePointPositionFromPathPercentage(float percentage)
        {
            return Vector2.Lerp(_startPoint, _endPoint, percentage);
        }

        private float _progressPercentage;

        public void Update()
        {
            _progressPercentage = timeProgression.PreProgress(Time.deltaTime * _speedCurve.Evaluate(_progressPercentage));

            if (Math.Abs(_progressPercentage - 1f) < float.Epsilon)
            {
                OnProgressReachesEnd();
                return;
            }

            _transform.position = CalculatePointPositionFromPathPercentage(_progressPercentage);
        }

        private void OnProgressReachesEnd()
        {
            ProgressReachesEnd?.Invoke();
        }
    }
}