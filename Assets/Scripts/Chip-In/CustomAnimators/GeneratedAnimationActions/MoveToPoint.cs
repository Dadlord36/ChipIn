using UnityEngine;

namespace CustomAnimators.GeneratedAnimationActions
{
    public sealed class MoveToPoint : ProgressiveAction
    {
        private readonly Transform _transform;
        private readonly Vector2 _startPoint;
        private readonly Vector2 _endPoint;


        public MoveToPoint(Transform transform, Vector2 startPoint, Vector2 endPoint, AnimationCurve speedCurve, float time)
            : base(speedCurve, time)
        {
            _transform = transform;
            _startPoint = startPoint;
            _endPoint = endPoint;
        }

        private Vector2 CalculatePointPositionFromPathPercentage(float percentage)
        {
            return Vector2.Lerp(_startPoint, _endPoint, percentage);
        }

        protected override void ProgressUpdate(float progressPercentage)
        {
            _transform.position = CalculatePointPositionFromPathPercentage(progressPercentage);
        }
    }
}