using System;
using Common;
using ScriptableObjects.DataSets;
using Shapes2D;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Bars.BarItems
{
    public sealed class ScrollBarItemView : BaseView, IScrollBarItem
    {
        public event Action<string> Selected;

        [SerializeField] private Image iconImage;
        [SerializeField] private Shape backgroundShape;
        [SerializeField] private TMP_Text textField;

        private PointClickRetranslator PointClickRetranslatorComponent => GetComponent<PointClickRetranslator>();

        public Sprite IconSprite
        {
            get => iconImage.sprite;
            set => iconImage.sprite = value;
        }

        public string Title
        {
            get => textField.text;
            set => textField.text = value;
        }

        public Color BackgroundGradientColor1
        {
            get => backgroundShape.settings.fillColor;
            set => backgroundShape.settings.fillColor = value;
        }

        public Color BackgroundGradientColor2
        {
            get => backgroundShape.settings.fillColor2;
            set => backgroundShape.settings.fillColor2 = value;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            PointClickRetranslatorComponent.pointerClicked.AddListener(OnPointerClick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PointClickRetranslatorComponent.pointerClicked.RemoveListener(OnPointerClick);
        }

        private void OnPointerClick()
        {
            OnSelected();
        }

        public void Set(IScrollBarItem scrollBarItemData)
        {
            IconSprite = scrollBarItemData.IconSprite;
            Title = scrollBarItemData.Title;
            BackgroundGradientColor1 = scrollBarItemData.BackgroundGradientColor1;
            BackgroundGradientColor2 = scrollBarItemData.BackgroundGradientColor2;
        }

        private void OnSelected()
        {
            Selected?.Invoke(Title);
        }
    }
}