using UnityEngine;

namespace CustomAnimators.GeneratedAnimationActions
{
    public sealed class CanvasGroupFading : ProgressiveAction
    {
        private readonly CanvasGroup _canvasGroup;
        public CanvasGroupFading(AnimationCurve speedCurve, in float time, CanvasGroup canvasGroup) : base(speedCurve, in time)
        {
            _canvasGroup = canvasGroup;
        }

        protected override void ProgressUpdate(float progressPercentage)
        {
            _canvasGroup.alpha = progressPercentage;
        }
    }
}