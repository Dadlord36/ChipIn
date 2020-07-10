using System;
using System.Collections.Generic;
using GlobalVariables;
using Repositories.Local.SingleItem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Views
{
    public sealed class CreateOfferView : BaseView
    {
        [SerializeField] private Image avatarIconImage;
        [SerializeField] private TMP_Text validityPeriodTextField;
        
        
        public event Action<string> NewCategorySelected;
        public event Action<string> NewGameTypeSelected;
        public event Action<string> NewOfferTypeSelected;


        public CreateOfferView() : base(nameof(CreateOfferView))
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ResetDropdowns();
        }
        

        private void ResetDropdowns()
        {
            OnNewCategoryItemSelected(0);
            OnNewGameTypeSelected(0);
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