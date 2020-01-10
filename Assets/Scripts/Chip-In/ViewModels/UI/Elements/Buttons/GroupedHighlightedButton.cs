using ScriptableObjects.Parameters;
using TMPro;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Elements.Buttons
{
    public class GroupedHighlightedButton : Button, ISelectableObject
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private TMP_FontAsset normalFont, highlightedFont;
        [SerializeField] private ColorParameter normalTextColor, highlightedTextColor;

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
            onClick.AddListener(SelectAsOneOfGroup);
        }

        public void OnOtherItemSelected()
        {
            SetTextFontAsset(normalFont, normalTextColor);
        }

        public void SelectAsOneOfGroup()
        {
            SetTextFontAsset(highlightedFont, highlightedTextColor);
        }

        public void SubscribeOnMainEvent(UnityAction onOtherItemInGroupSelected)
        {
            onClick.AddListener(onOtherItemInGroupSelected);
        }
    }
}