using System;
using System.Collections.Generic;
using GlobalVariables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public sealed class CreateOfferView : BaseView
    {
        [SerializeField] private Image avatarIconImage;

        [SerializeField] private TMP_Text validityPeriodTextField;
        [SerializeField] private TMP_Text startingTimerField;

        [SerializeField] private TMP_Dropdown categoriesDropdown;
        [SerializeField] private TMP_Dropdown challengeTypeDropdown;
        [SerializeField] private TMP_Dropdown offerTypeDropdown;

        public event Action<string> NewCategorySelected;
        public event Action<string> NewGameTypeSelected;
        public event Action<string> NewOfferTypeSelected; 

        protected override void OnEnable()
        {
            base.OnEnable();
            ResetDropdowns();
            categoriesDropdown.onValueChanged.AddListener(OnNewCategoryItemSelected);
            challengeTypeDropdown.onValueChanged.AddListener(OnNewGameTypeSelected);
            offerTypeDropdown.onValueChanged.AddListener(OnNewOfferTypeSelected);
            
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            categoriesDropdown.onValueChanged.RemoveListener(OnNewCategoryItemSelected);
            challengeTypeDropdown.onValueChanged.RemoveListener(OnNewGameTypeSelected);
            offerTypeDropdown.onValueChanged.RemoveListener(OnNewOfferTypeSelected);
        }

        private void ResetDropdowns()
        {
            categoriesDropdown.ClearOptions();
            categoriesDropdown.AddOptions(new List<string>(MainNames.OfferSegments.OffersSegmentsArray));
            OnNewCategoryItemSelected(0);

            challengeTypeDropdown.ClearOptions();
            challengeTypeDropdown.AddOptions(new List<string>(MainNames.ChallengeTypes.ChallengeTypesArray));
            OnNewGameTypeSelected(0);
            
            offerTypeDropdown.ClearOptions();
            offerTypeDropdown.AddOptions(new List<string>(MainNames.OfferCategories.OfferCategoriesArray));
            OnNewOfferTypeSelected(0);
        }

        private void OnNewOfferTypeSelected(int index)
        {
            OnNewOfferTypeSelected(MainNames.OfferCategories.OfferCategoriesArray[index]);
        }
        
        private void OnNewCategoryItemSelected(int index)
        {
            OnNewCategorySelected(MainNames.OfferSegments.OffersSegmentsArray[index]);
        }

        private void OnNewGameTypeSelected(int index)
        {
            OnNewGameTypeSelected(MainNames.ChallengeTypes.ChallengeTypesArray[index]);
        }

        public DateTime ValidityPeriod
        {
            set => validityPeriodTextField.text = value.ToShortDateString();
        }

        public DateTime StartingTime
        {
            set => startingTimerField.text = $"{value.ToLongDateString()} : {value.ToShortTimeString()}";
        }

        public Sprite AvatarIconSprite
        {
            get => avatarIconImage.sprite;
            set
            {
                avatarIconImage.gameObject.SetActive(true);
                avatarIconImage.sprite = value;
            }
        }

        private void OnNewCategorySelected(string obj)
        {
            NewCategorySelected?.Invoke(obj);
        }

        private void OnNewGameTypeSelected(string obj)
        {
            NewGameTypeSelected?.Invoke(obj);
        }

        private void OnNewOfferTypeSelected(string obj)
        {
            NewOfferTypeSelected?.Invoke(obj);
        }
    }
}