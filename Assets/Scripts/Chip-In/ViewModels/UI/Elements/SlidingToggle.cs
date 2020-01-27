using System;
using System.Linq;
using Common;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using ViewModels.UI.Interfaces;

namespace ViewModels.UI.Elements
{
    public sealed class SlidingToggle : BaseAnimatedToggle, IProgress<float>, IPointerClickHandler
    {
        [SerializeField, Range(0f, 0.5f)] private double handleDockingPositionPercentage;
        [SerializeField] private RectTransform handleTransform;

        private float _onPosX, _offPosX;
        private Vector3 _tempHandlePosition;
        private ITimeline _timeline;
        [SerializeField] private BaseAnimatedToggle[] toggles;

        protected override void OnEnable()
        {
            base.OnEnable();
            Assert.IsNotNull(handleTransform);
            
            _timeline = GetComponent<ITimeline>();
            CalculateMovementBounds();
            SubscribeChangeableSliderPartsToTimelineProgression();
            SubscribeRelatedGraphicsSwitchers();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeChangeableSliderPartsFromTimelineProgression();
            UnsubscribeRelatedGraphicsSwitchers();
        }

        private void SubscribeRelatedGraphicsSwitchers()
        {
            GroupItemsConnector.ConnectGroupItems(toggles.Cast<IOneOfAGroup>().ToArray());
        }

        private void UnsubscribeRelatedGraphicsSwitchers()
        {
            GroupItemsConnector.DisconnectGroupItems(toggles.Cast<IOneOfAGroup>().ToArray());
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

            _onPosX = toggleSizeX / 2 * (float) handleDockingPositionPercentage;
            _offPosX = _onPosX * -1;
        }

        void IProgress<float>.Report(float value)
        {
            SetHandlePositionAlongSlide(InversePercentage(value));
        }

        protected override void SetHandlePositionAlongSlide(float percentage)
        {
            _tempHandlePosition.x = Mathf.Lerp(_offPosX, _onPosX, percentage);
            handleTransform.localPosition = _tempHandlePosition;
        }
        

        /*private void PropagateConditionChange()
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].SetCondition(Condition,false);
            }
        }*/

        /*protected override void OnConditionChanger()
        {
            
            /*PropagateConditionChange();#1#
        }*/

        public void OnPointerClick(PointerEventData eventData)
        {
            Condition = !Condition;
            OnToggleSwitched();
            _timeline.RestartTimer();
        }
    }
}