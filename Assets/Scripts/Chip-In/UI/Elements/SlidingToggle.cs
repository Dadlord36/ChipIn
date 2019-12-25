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

        private float _onPosX, _offPosX;
        private Vector3 tempHandlePosition;
        private ITimeline _timeline;
        private IToggle[] _toggles;

        protected override void Awake()
        {
            base.Awake();

            Assert.IsNotNull(handleTransform);
            _timeline = GetComponent<ITimeline>();


            CalculateMovementBounds();
            // SetToggleInitState();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            CollectAllToggles();
            SubscribeChangeableSliderPartsToTimelineProgression();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeChangeableSliderPartsFromTimelineProgression();
        }

        private void CollectAllToggles()
        {
            _toggles = GetComponentsInChildren<IToggle>();
        }

        private void SubscribeChangeableSliderPartsToTimelineProgression()
        {
            _timeline.Progressing += (this as IProgress<float>).Report;
            var progressReceivers = GetComponentsInChildren<IProgress<float>>();
            for (int i = 0; i < progressReceivers.Length; i++)
            {
                _timeline.Progressing += progressReceivers[i].Report;
            }
        }

        private void UnsubscribeChangeableSliderPartsFromTimelineProgression()
        {
            _timeline.Progressing -= (this as IProgress<float>).Report;
            var progressReceivers = GetComponentsInChildren<IProgress<float>>();
            for (int i = 0; i < progressReceivers.Length; i++)
            {
                _timeline.Progressing -= progressReceivers[i].Report;
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

        protected override void SetHandlePositionAlongSlide(float percentage)
        {
            tempHandlePosition.x = Mathf.Lerp(_offPosX, _onPosX, percentage);
            handleTransform.localPosition = tempHandlePosition;
        }

        private void PropagateConditionChange()
        {
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].Condition = Condition;
            }
        }

        protected override void OnConditionChanger()
        {
            _timeline.RestartTimer();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Condition = !Condition;
            OnToggleSwitched();
            PropagateConditionChange();
        }
    }
}