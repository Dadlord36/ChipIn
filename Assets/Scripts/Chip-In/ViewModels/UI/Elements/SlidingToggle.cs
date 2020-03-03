using System;
using System.Linq;
using Common;
using Common.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityWeld.Binding;
using ViewModels.UI.Interfaces;

namespace ViewModels.UI.Elements
{
    [Binding]
    public sealed class SlidingToggle : BaseAnimatedToggle, IProgress<float>
    {
        [SerializeField, Range(0f, 0.5f)] private double handleDockingPositionPercentage;
        [SerializeField] private RectTransform handleTransform;
        [SerializeField] private PointClickRetranslator clickRetranslator;
        
        private float _onPosX, _offPosX;
        private Vector3 _tempHandlePosition;
        private ITimeline _timeline;
        [SerializeField] private BaseAnimatedToggle[] toggles;
        


        protected override void Start()
        {
            base.Start();
            SetToggleInitState();
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].Condition = !Condition;
                toggles[i].SetToggleInitState();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Assert.IsNotNull(handleTransform);

            _timeline = GetComponent<ITimeline>();
            _timeline.Initialize();

            CalculateMovementBounds();

            SubscribeChangeableSliderPartsToTimelineProgression();
            SubscribeRelatedGraphicsSwitchers();
            SubscribeOnPointClickRetranslator();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeChangeableSliderPartsFromTimelineProgression();
            UnsubscribeRelatedGraphicsSwitchers();
            UnsubscribeFromPointClickRetranslator();
        }

        private void SubscribeOnPointClickRetranslator()
        {
            clickRetranslator.PointerClicked += SwitchCondition;
        }

        private void UnsubscribeFromPointClickRetranslator()
        {
            clickRetranslator.PointerClicked -= SwitchCondition;
        }

        private void SubscribeRelatedGraphicsSwitchers()
        {
            GroupItemsConnector.ConnectGroupTo(this, toggles.Cast<IOneOfAGroup>().ToArray());
        }

        private void UnsubscribeRelatedGraphicsSwitchers()
        {
            GroupItemsConnector.DisconnectGroupFrom(this, toggles.Cast<IOneOfAGroup>().ToArray());
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

        protected override void OnToggleSwitched()
        {
            base.OnToggleSwitched();
            if (enabled)
                _timeline.RestartTimer();
        }
    }
}