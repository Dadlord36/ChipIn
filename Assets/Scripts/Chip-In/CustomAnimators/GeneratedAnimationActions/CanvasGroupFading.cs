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

        public const float MinAlpha = 0, MaxAlpha = 1f;

        private float _startAlpha;
        private float _endAlpha;

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
            switch (fadingType)
            {
                case FadingType.Appear:
                    _startAlpha = MinAlpha;
                    _endAlpha = MaxAlpha;
                    return;
                case FadingType.Disappear:
                    _startAlpha = MaxAlpha;
                    _endAlpha = MinAlpha;
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fadingType), fadingType, null);
            }
        }

        protected override void ProgressUpdate(float progressPercentage)
        {
            _canvasGroup.alpha = Mathf.Lerp(_startAlpha,_endAlpha, progressPercentage);
        }
    }
}