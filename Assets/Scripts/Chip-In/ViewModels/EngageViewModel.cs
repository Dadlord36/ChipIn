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
        [SerializeField] private CommunityInterestRemoteRepository interestRemoteRepository;

        private EngageView RelativeView => View as EngageView;


        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            RefillInterestsList();
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
        }

        private void RefillInterestsList()
        {
            RelativeView.ClearScrollList();

            var itemsData = interestRemoteRepository.ItemsData;

            for (int i = 0; i < itemsData.Count; i++)
            {
                CreateAndAddEngageCardToScrollList(itemsData[i]);
            }
        }

        private void CreateAndAddEngageCardToScrollList(in CommunityInterestGridItemView.CommunityInterestGridItemData
            interestGridData)
        {
            RelativeView.AddCardToScrollList(interestGridData).CardWasSelected += OnNewCommunityInterestCardSelected;
        }

        private void OnNewCommunityInterestCardSelected(EngageCardDataModel engageCardDataModel)
        {
            offerCreationRepository.SelectedInterestData = engageCardDataModel;
            SwitchToView(nameof(MerchantInterestView));
        }
    }
}