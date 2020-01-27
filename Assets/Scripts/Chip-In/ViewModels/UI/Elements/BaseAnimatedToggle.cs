using UnityEngine;

namespace ViewModels.UI.Elements
{
    public abstract class BaseAnimatedToggle : ToggleBase
    {
        private float _basicValue;

        public override bool Condition
        {
            protected set
            {
                base.Condition = value;
                _basicValue = Condition ? 0 : -1.0f;
            }
        }

        private float GetPathPercentageFromCondition()
        {
            return Condition ? 1.0f : 0f;
        }

        protected void SetToggleInitState()
        {
            SetHandlePositionAlongSlide(GetPathPercentageFromCondition());
        }

        protected abstract void SetHandlePositionAlongSlide(float percentage);

        protected float InversePercentage(float percentage)
        {
            return Mathf.Abs(_basicValue + percentage);
        }
    }
}