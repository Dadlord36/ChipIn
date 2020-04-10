using DataModels;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;
using Views;
using Views.Bars;

namespace ViewModels
{
    public class EngageViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private OfferCreationRepository offerCreationRepository;
        [SerializeField] private ItemsScrollBarView itemsScrollBarView;

        [SerializeField] private MerchantCommunityInterestsRepository merchantCommunityInterestsRepository;
        private EngageView RelativeView => View as EngageView;


        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            itemsScrollBarView.Activate();
            RefillInterestsList();
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            itemsScrollBarView.Deactivate();
        }

        private void RefillInterestsList()
        {
            RelativeView.ClearScrollList();

            var itemsData = merchantCommunityInterestsRepository.ItemsData;
            for (int i = 0; i < itemsData.Count; i++)
            {
                CreateAndAddEngageCardToScrollList(itemsData[i]);
            }
        }

        private void CreateAndAddEngageCardToScrollList(EngageCardDataModel engageCardDataModel)
        {
            RelativeView.AddCardToScrollList(engageCardDataModel).CardWasSelected += OnNewCommunityInterestCardSelected;
        }

        private void OnNewCommunityInterestCardSelected(EngageCardDataModel engageCardDataModel)
        {
            offerCreationRepository.SelectedInterestData = engageCardDataModel;
        }
        
    }
}