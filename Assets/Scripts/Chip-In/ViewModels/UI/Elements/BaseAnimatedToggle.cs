using UnityEngine;

namespace ViewModels.UI.Elements
{
    public abstract class BaseAnimatedToggle : ToggleBase
    {
        private float _basicValue;

        public override bool Condition
        {
            get => base.Condition;
            set
            {
                _basicValue = value ? 0 : -1.0f;
                base.Condition = value;
            }
        }


        private float GetPathPercentageFromCondition()
        {
            return Condition ? 1.0f : 0f;
        }

        public void SetToggleInitState()
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