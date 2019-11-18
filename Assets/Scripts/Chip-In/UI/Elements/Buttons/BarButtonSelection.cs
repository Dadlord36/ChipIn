using System;
using ScriptableObjects.Parameters;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements.Buttons
{
    public sealed class BarButtonSelection : UIBehaviour, IGroupableSelection
    {
        [SerializeField] private Image[] selectionViewElements;
        [SerializeField] private FloatParameter crossFadeColorTime;
        [SerializeField] private StateSwitchableButton stateSwitchableButton;
        private event UnityAction GotSelected;

        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(selectionViewElements);
            ResetAppearance();
        }

        private void ResetAppearance()
        {
            for (var i = 0; i < selectionViewElements.Length; i++)
            {
                var color = selectionViewElements[i].color;
                color.a = 0.0f;
                selectionViewElements[i].color = color;
            }
        }

        private void ShowUpCrossFaded()
        {
            for (var i = 0; i < selectionViewElements.Length; i++)
            {
                MakeImageVisible(selectionViewElements[i]);
                CrossFadeImageColor(selectionViewElements[i], 1.0f);
            }
        }

        public void MakeImageVisible(Graphic image)
        {
            var color = image.color;
            color.a = 1.0f;
            image.color = color;
        }

        private void HideCrossFaded()
        {
            for (var i = 0; i < selectionViewElements.Length; i++)
            {
                CrossFadeImageColor(selectionViewElements[i], 0.0f);
            }
        }

        private void CrossFadeImageColor(Graphic image, float alpha)
        {
            var targetColor = image.color;
            targetColor.a = alpha;
            image.CrossFadeColor(targetColor, crossFadeColorTime.value, false, true);
        }

        private void Show()
        {
            ShowUpCrossFaded();
            stateSwitchableButton.SwitchButtonState(StateSwitchableButton.ButtonSelectionSate.Selected);
        }

        private void Hide()
        {
            HideCrossFaded();
            stateSwitchableButton.SwitchButtonState(StateSwitchableButton.ButtonSelectionSate.Normal);
        }

        public void OnOtherItemSelected()
        {
            Hide();
        }

        public void SelectAsOneOfGroup()
        {
            Show();
            GotSelected?.Invoke();
        }

        public void SubscribeOnMainEvent(UnityAction onOtherItemInGroupSelected)
        {
            GotSelected += onOtherItemInGroupSelected;
        }
    }
}