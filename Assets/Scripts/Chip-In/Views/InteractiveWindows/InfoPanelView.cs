using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.InteractiveWindows.Interfaces;

namespace Views.InteractiveWindows
{
    public class InfoPanelView : BaseView,IInfoPanelData
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
    }
}