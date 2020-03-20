using TMPro;
using UnityEngine;
using ViewModels.UI.Elements.Icons;

namespace Views.Cards
{
    public class EngageCardView : BaseView
    {
        [SerializeField] private TMP_Text titleTextField;
        [SerializeField] private TMP_Text descriptionAgeTextField;
        [SerializeField] private TMP_Text marketAgeTextField;
        [SerializeField] private TMP_Text marketSizeTextField;
        [SerializeField] private TMP_Text marketCapTextField;
        [SerializeField] private TMP_Text marketSpiritTextField;
        [SerializeField] private UserAvatarIcon icon;


        public string Title
        {
            get => titleTextField.text;
            set => descriptionAgeTextField.text = value;
        }
        
        public Sprite IconSprite
        {
            get => icon.AvatarSprite;
            set => icon.AvatarSprite = value;
        }

        public string MarketAge
        {
            get => marketAgeTextField.text;
            set => marketAgeTextField.text = value;
        }

        public string MarketSize
        {
            get => marketSizeTextField.text;
            set => marketSizeTextField.text = value;
        }

        public string MarketCap
        {
            get => marketCapTextField.text;
            set => marketCapTextField.text = value;
        }

        public string MarketSpirit
        {
            get => marketSpiritTextField.text;
            set => marketSpiritTextField.text = value;
        }
    }
}