using System;
using DataModels;
using Repositories.Local;
using UnityEngine;
using Utilities;
using Views;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    public class EngageViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private OfferCreationRepository offerCreationRepository;
        [SerializeField] private MerchantInterestListAdapter merchantInterestListAdapter;

        public EngageViewModel() : base(nameof(EngageViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await merchantInterestListAdapter.Initialize();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void OnNewCommunityInterestCardSelected(EngageCardDataModel engageCardDataModel)
        {
            offerCreationRepository.SelectedInterestData = engageCardDataModel;
            SwitchToView(nameof(MerchantInterestView));
        }
    }
}