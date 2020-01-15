using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebOperationUtilities;

namespace Views
{
    public class CommunityInterestGridItemView : BaseView
    {
        public struct CommunityInterestGridItemData
        {
            public readonly int InterestId;
            public readonly string ItemName;
            public readonly byte[] IconTextureData;

            public CommunityInterestGridItemData(int interestId, string itemName, byte[] iconTextureData)
            {
                InterestId = interestId;
                ItemName = itemName;
                IconTextureData = iconTextureData;
            }
        }

        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text textField;

        private int _interestId;

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

        public void SetItemImageAndText(in CommunityInterestGridItemData gridItemData)
        {
            ItemImageSprite = SpritesUtility.CreateSpriteWithDefaultParameters( gridItemData.IconTextureData);
            ItemName = gridItemData.ItemName;
            _interestId = gridItemData.InterestId;
        }
        
        public void SetItemImageAndText(int id,string itemName,Sprite sprite)
        {
            ItemImageSprite = sprite;
            ItemName = itemName;
            _interestId = id;
        }
    }
}