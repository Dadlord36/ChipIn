using DataModels.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class CommunityInterestGridItemView : BaseView
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text textField;

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
    }
}