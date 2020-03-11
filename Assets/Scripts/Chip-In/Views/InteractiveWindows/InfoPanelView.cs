using System.Threading.Tasks;
using DataModels.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.InteractiveWindows.Interfaces;
using WebOperationUtilities;

namespace Views.InteractiveWindows
{
    public interface IInfoPanelView
    {
        void FillCardWithData(IInfoPanelData data);
        void ShowInfoCard();
        void HideInfoCard();
    }
    
    public class InfoPanelView : BaseView, IInfoPanelData,IInfoPanelView
    {
        public class InfoPanelData : IInfoPanelData
        {
            public InfoPanelData(Sprite itemLabel, string itemName, string itemType, string itemDescription)
            {
                ItemLabel = itemLabel;
                ItemName = itemName;
                ItemType = itemType;
                ItemDescription = itemDescription;
            }

            public Sprite ItemLabel { get; set; }
            public string ItemName { get; set; }
            public string ItemType { get; set; }
            public string ItemDescription { get; set; }
        }
        
        
        [SerializeField] private Image productIconImage;
        [SerializeField] private TMP_Text itemNameField;
        [SerializeField] private TMP_Text itemTypeField;
        [SerializeField] private TMP_Text itemDescriptionField;


        public Sprite ItemLabel
        {
            get => productIconImage.sprite;
            set => productIconImage.sprite = value;
        }

        public string ItemName
        {
            get => itemNameField.text;
            set => itemNameField.text = value;
        }

        public string ItemType
        {
            get => itemTypeField.text;
            set => itemTypeField.text = value;
        }

        public string ItemDescription
        {
            get => itemDescriptionField.text;
            set => itemDescriptionField.text = value;
        }

        public void FillCardWithData(IInfoPanelData data)
        {
            ItemLabel = data.ItemLabel;
            ItemName = data.ItemName;
            ItemType = data.ItemType;
            ItemDescription = data.ItemDescription;
        }

        public void ShowInfoCard()
        {
            gameObject.SetActive(true);
        }

        public void HideInfoCard()
        {
            gameObject.SetActive(false);
        }
        
        public static async Task FillWithData(IInfoPanelView infoPanelView, IOfferWithGameModel offerWithGameModel)
        {
            var label = SpritesUtility.CreateSpriteWithDefaultParameters(await ImagesDownloadingUtility.TryDownloadImageAsync(offerWithGameModel.PosterUri));

            infoPanelView.FillCardWithData(new InfoPanelView.InfoPanelData(label, offerWithGameModel.Title, offerWithGameModel.Category, offerWithGameModel.Description));
        }
    }
}