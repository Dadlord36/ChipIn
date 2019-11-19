using System;
using Common;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public sealed class SlidingToggle : BaseUIToggle, IProgress<float>, IPointerClickHandler
    {
        [SerializeField, Range(0f, 0.5f)] private double handleDockingPositionPercentage;
        [SerializeField] private RectTransform handleTransform;

        private ITimeline _timeline;
        private float _onPosX, _offPosX;
        private Vector3 tempHandlePosition;

        private IToggle[] _toggles;

        protected override void Awake()
        {
            Assert.IsNotNull(handleTransform);
            _timeline = GetComponent<ITimeline>();
            SubscribeGraphicsFadeSwitchersToTimelineProgression();
            CollectAllToggles();
            CalculateMovementBounds();

            base.Awake();
        }

        protected override void InitializeToggle(float percentage)
        {
            SetHandlePositionAlongSlide(percentage);
        }

        private void CollectAllToggles()
        {
            _toggles = GetComponentsInChildren<IToggle>();
        }

        private void SubscribeGraphicsFadeSwitchersToTimelineProgression()
        {
            _timeline.Progressing += (this as IProgress<float>).Report;
            var progressReceivers = GetComponentsInChildren<IProgress<float>>();
            for (int i = 0; i < progressReceivers.Length; i++)
            {
                _timeline.Progressing += progressReceivers[i].Report;
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            Assert.IsNotNull(handleTransform);
            CalculateMovementBounds();
            base.OnValidate();
        }
#endif

        private void CalculateMovementBounds()
        {
            var toggleRect = GetComponent<RectTransform>();
            var toggleSizeX = toggleRect.sizeDelta.x;

            _onPosX = (toggleSizeX / 2) * (float) handleDockingPositionPercentage;
            _offPosX = _onPosX * -1;
        }

        void IProgress<float>.Report(float value)
        {
            SetHandlePositionAlongSlide(InversePercentage(value));
        }

        private void SetHandlePositionAlongSlide(float percentage)
        {
            tempHandlePosition.x = Mathf.Lerp(_offPosX, _onPosX, percentage);
            handleTransform.localPosition = tempHandlePosition;
        }

        private void PropagateConditionChange()
        {
            OnToggleSwitched();
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].Condition = Condition;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Condition = !Condition;
            PropagateConditionChange();
            _timeline.RestartTimer();
        }
    }
}