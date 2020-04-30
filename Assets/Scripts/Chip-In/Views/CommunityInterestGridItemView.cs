using DataModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebOperationUtilities;

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

        public void SetItemImageAndText(in CommunityBasicDataModel gridItemData, Sprite icon)
        {
            ItemImageSprite = icon;
            ItemName = gridItemData.Name;
            _interestId = gridItemData.Id;
        }
        
        public void SetItemImageAndText(int id,string itemName,Sprite sprite)
        {
            ItemImageSprite = sprite;
            ItemName = itemName;
            _interestId = id;
        }
    }
}