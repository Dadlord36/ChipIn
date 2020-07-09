using Repositories.Local;
using UnityEngine;
using Utilities;
using ViewModels.UI.Elements.ScrollBars;
using Views;
using Views.Bars.BarItems;

namespace ViewModels
{
    public class MerchantInterestDetailsViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private TitledItemsList itemsList;
        [SerializeField] private OfferCreationRepository offerCreationRepository;
        [SerializeField] private ScrollBarOfTitlesViewModel scrollBar;


        private string _currentCategory;

        public MerchantInterestDetailsViewModel() : base(nameof(MerchantInterestDetailsViewModel))
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            scrollBar.NewItemSelected += ScrollBarOnNewItemSelected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            scrollBar.NewItemSelected -= ScrollBarOnNewItemSelected;
        }

        private void Start()
        {
            scrollBar.Initialize();


            //Todo: remove next line
            itemsList.SubscribeOnElementsSelection(OnItemSelected);
        }

        private void ScrollBarOnNewItemSelected(ITitled selectedCategoryTitle)
        {
            _currentCategory = selectedCategoryTitle.Title;
        }

        private void RefillList()
        {
            itemsList.Fill(null, OnItemSelected);
        }

        private void OnItemSelected(string selectedItemFromListTitle)
        {
            LogUtility.PrintLog(Tag, selectedItemFromListTitle);
            offerCreationRepository[_currentCategory] = selectedItemFromListTitle;
        }
    }
}