using System;
using UnityEngine;

namespace CustomAnimators.GeneratedAnimationActions
{
    public sealed class CanvasGroupFading : ProgressiveAction
    {
        public enum FadingType
        {
            Appear,
            Disappear
        }

        [Serializable]
        public struct AnimationParameters
        {
            public AnimationCurve speedCurve;
            public float animationTime;

            public AnimationParameters(AnimationCurve speedCurve, float animationTime)
            {
                this.speedCurve = speedCurve;
                this.animationTime = animationTime;
            }
        }

        private readonly CanvasGroup _canvasGroup;

        private const float MinAlpha = 0;
        private const float MaxAlpha = 1f;

        private float _startAlpha;
        private float _endAlpha;

        private float CanvasGroupAlpha
        {
            get => _canvasGroup.alpha;
            set => _canvasGroup.alpha = value;
        }

        public CanvasGroupFading(AnimationCurve speedCurve, in float time, CanvasGroup canvasGroup, FadingType fadingType = FadingType.Appear) :
            base(speedCurve, in time)
        {
            _canvasGroup = canvasGroup;
            SwitchFadingProgressParameters(fadingType);
        }

        public CanvasGroupFading(AnimationParameters parameters, CanvasGroup canvasGroup, FadingType fadingType = FadingType.Appear) :
            base(parameters.speedCurve, parameters.animationTime)
        {
            _canvasGroup = canvasGroup;
            SwitchFadingProgressParameters(fadingType);
        }

        private void SwitchFadingProgressParameters(FadingType fadingType)
        {
            _startAlpha = CanvasGroupAlpha;
            switch (fadingType)
            {
                case FadingType.Appear:
                    _endAlpha = MaxAlpha;
                    return;
                case FadingType.Disappear:
                    _endAlpha = MinAlpha;
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fadingType), fadingType, null);
            }
        }

        protected override void ProgressUpdate(float progressPercentage)
        {
            CanvasGroupAlpha = Mathf.Lerp(_startAlpha, _endAlpha, progressPercentage);
        }
    }
}