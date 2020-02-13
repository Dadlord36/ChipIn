﻿using ScriptableObjects.Parameters;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ViewModels.UI.Interfaces;

namespace ViewModels.UI.Elements.Buttons
{
    public class GroupedHighlightedButton : Button, IOneOfAGroup, IGroupAction
    {
        public event UnityAction GroupActionPerformed
        {
            add => onClick.AddListener(value);
            remove => onClick.RemoveListener(value);
        }

        [SerializeField,HideInInspector] private bool shouldStayHighlighted;
        [SerializeField,HideInInspector] private TMP_Text text;
        [SerializeField,HideInInspector] private TMP_FontAsset normalFont, highlightedFont;
        [SerializeField,HideInInspector] private ColorParameter normalTextColor, highlightedTextColor;

#if UNITY_EDITOR
        public string ShouldStayHighlightedName => nameof(shouldStayHighlighted);
        public string TextFieldName => nameof(text);
        public string NormalFontFieldName => nameof(normalFont);
        public string HighlightedFontFieldName => nameof(highlightedFont);
        public string NormalTextColorFieldName => nameof(normalTextColor);
        public string HighlightedTextColorFieldName => nameof(highlightedTextColor);
#endif

        private void SetTextFontAsset(TMP_FontAsset newFontAsset, ColorParameter textColor)
        {
            text.font = newFontAsset;
            text.color = textColor.value;
        }

        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(text, $"There is no Text under button: {name}");
            Assert.IsNotNull(normalTextColor);
            Assert.IsNotNull(highlightedTextColor);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (shouldStayHighlighted)
                SwitchToHighlightedStyle();
        }

        public void OnOtherOnePerformGroupAction()
        {
            SetTextFontAsset(normalFont, normalTextColor);
        }

        private void SwitchToHighlightedStyle()
        {
            SetTextFontAsset(highlightedFont, highlightedTextColor);
        }

        public void PerformGroupActionWithoutNotification()
        {
            SwitchToHighlightedStyle();
        }
    }
}