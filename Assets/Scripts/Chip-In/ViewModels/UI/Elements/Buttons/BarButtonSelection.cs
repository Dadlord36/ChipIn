using ScriptableObjects.Parameters;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityWeld.Binding;
using ViewModels.UI.Interfaces;

namespace ViewModels.UI.Elements.Buttons
{
    [Binding]
    public sealed class BarButtonSelection : UIBehaviour, IOneOfAGroup, IGroupAction
    {
#if UNITY_EDITOR
        public Vector2 IconSize
        {
            get => stateSwitchableButton.image.rectTransform.sizeDelta;
            set => stateSwitchableButton.image.rectTransform.sizeDelta = value;
        }

        public Sprite Icon
        {
            get => stateSwitchableButton.image.sprite;
            set => stateSwitchableButton.image.sprite = value;
        }

        public bool IsIconValid => stateSwitchableButton != null;
        public bool shouldShowReferencesFields;
#endif

        [SerializeField] private Image[] selectionViewElements;
        [SerializeField] private FloatParameter crossFadeColorTime;
        [SerializeField] private StateSwitchableButton stateSwitchableButton;
        [SerializeField] private Button interactiveButton;


        public UnityAction onClick;
        public event UnityAction GroupActionPerformed;

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

        protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeOnEvents();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeOnEvent();
        }

        private void SubscribeOnEvents()
        {
            interactiveButton.onClick.AddListener(onClick);
        }

        private void UnsubscribeOnEvent()
        {
            interactiveButton.onClick.RemoveListener(onClick);
        }

        private void ShowUpCrossFaded()
        {
            for (var i = 0; i < selectionViewElements.Length; i++)
            {
                MakeImageVisible(selectionViewElements[i]);
                CrossFadeImageColor(selectionViewElements[i], 1.0f);
            }
        }

        public static void MakeImageVisible(Graphic image)
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

        public void OnOtherOnePerformGroupAction()
        {
            Hide();
        }

        void IGroupAction.PerformGroupAction()
        {
            Show();
            GroupActionPerformed?.Invoke();
        }
    }
}