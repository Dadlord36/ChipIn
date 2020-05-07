using System.Threading.Tasks;
using DataModels;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;
using ViewModels.Cards;
using Views;

namespace ViewModels
{
    public class EngageViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private OfferCreationRepository offerCreationRepository;
        [SerializeField] private CommunitiesDetailsDataRepository communitiesDetailsDataRepository;

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

        private async void RefillInterestsList()
        {
            RelativeView.ClearScrollList();
            var itemsData = communitiesDetailsDataRepository.ItemsData;
            var tasks = new Task<EngageCardViewModel>[itemsData.Count];

            for (int i = 0; i < itemsData.Count; i++)
            {
                tasks[i] = CreateAndAddEngageCardToScrollList(itemsData[i]);
            }

            var engageCards = await Task.WhenAll(tasks);

            for (int i = 0; i < engageCards.Length; i++)
            {
                engageCards[i].CardWasSelected += OnNewCommunityInterestCardSelected;
            }
        }

        private async Task<EngageCardViewModel> CreateAndAddEngageCardToScrollList(ICommunityDetailsDataModel interestGridData)
        {
            return await RelativeView.AddCardToScrollList(interestGridData);
        }

        private void OnNewCommunityInterestCardSelected(EngageCardDataModel engageCardDataModel)
        {
            offerCreationRepository.SelectedInterestData = engageCardDataModel;
            SwitchToView(nameof(MerchantInterestView));
        }
    }
}