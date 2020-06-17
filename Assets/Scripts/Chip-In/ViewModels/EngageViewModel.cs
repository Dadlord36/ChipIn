using System;
using System.Threading.Tasks;
using DataModels;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;
using Utilities;
using ViewModels.Cards;
using Views;

namespace ViewModels
{
    public class EngageViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private OfferCreationRepository offerCreationRepository;
        [SerializeField] private CommunitiesDetailsDataRepository communitiesDetailsDataRepository;

        private EngageView RelativeView => View as EngageView;

        public EngageViewModel() : base(nameof(EngageViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await RefillInterestsList();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
        }

        private async Task RefillInterestsList()
        {
            RelativeView.ClearScrollList();
            var itemsData = communitiesDetailsDataRepository.ItemsData;
            var tasks = new Task<EngageCardViewModel>[itemsData.Count];

            for (int i = 0; i < itemsData.Count; i++)
            {
                tasks[i] = CreateAndAddEngageCardToScrollList(itemsData[i]);
            }

            try
            {
                var engageCards = await Task.WhenAll(tasks);

                for (int i = 0; i < engageCards.Length; i++)
                {
                    engageCards[i].CardWasSelected += OnNewCommunityInterestCardSelected;
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private Task<EngageCardViewModel> CreateAndAddEngageCardToScrollList(ICommunityDetailsDataModel interestGridData)
        {
            return RelativeView.AddCardToScrollList(interestGridData);
        }

        private void OnNewCommunityInterestCardSelected(EngageCardDataModel engageCardDataModel)
        {
            offerCreationRepository.SelectedInterestData = engageCardDataModel;
            SwitchToView(nameof(MerchantInterestView));
        }
    }
}