using System;
using DataModels.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views
{
    public sealed class CommunityInterestGridItemView : BaseView, IPointerClickHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text textField;

        public event Action<int?> ItemSelected;

        private int? _interestId;

        private Sprite ItemImageSprite
        {
            get => itemImage.sprite;
            set => itemImage.sprite = value;
        }

        private string ItemName
        {
            get => textField.text;
            set => textField.text = value;
        }

        public CommunityInterestGridItemView() : base(nameof(CommunityInterestGridItemView))
        {
        }

        public void SetImage(Sprite icon)
        {
            ItemImageSprite = icon;
        }

        public void SetItemImageAndText(IIndexedAndNamed gridItemData, Sprite icon)
        {
            SetItemText(gridItemData);
            SetImage(icon);
        }

        public void SetItemText(IIndexedAndNamed gridItemData)
        {
            ItemName = gridItemData.Name;
            _interestId = gridItemData.Id;
        }

        public void SetItemImageAndText(int id, string itemName, Sprite sprite)
        {
            ItemImageSprite = sprite;
            ItemName = itemName;
            _interestId = id;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnItemSelected();
        }

        private void OnItemSelected()
        {
            ItemSelected?.Invoke(_interestId);
        }
    }
}