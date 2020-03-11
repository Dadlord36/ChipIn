using System.Threading.Tasks;
using DataModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.InteractiveWindows.Interfaces;
using WebOperationUtilities;

namespace Views.InteractiveWindows
{
    public class InfoPanelView : BaseView, IInfoPanelData
    {
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

        public async Task FillWithData(OfferWithGameModel offerDetailsOffer)
        {
            var label = SpritesUtility.CreateSpriteWithDefaultParameters(await ImagesDownloadingUtility.TryDownloadImageAsync(offerDetailsOffer.PosterUri));
            ItemLabel = label;
            ItemName = offerDetailsOffer.Title;
            ItemType = offerDetailsOffer.Category;
            ItemDescription = offerDetailsOffer.Description;
        }
    }
}