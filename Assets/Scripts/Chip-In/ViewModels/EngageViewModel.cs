using System;
using Repositories.Local;
using Repositories.Local.SingleItem;
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
        [SerializeField] private SelectedMerchantInterestRepository selectedMerchantInterestRepository;

        public EngageViewModel() : base(nameof(EngageViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await merchantInterestListAdapter.Initialize();
                merchantInterestListAdapter.ItemSelected += MerchantInterestListAdapterOnItemSelected;
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
            merchantInterestListAdapter.ItemSelected -= MerchantInterestListAdapterOnItemSelected;
        }

        private void MerchantInterestListAdapterOnItemSelected(uint index)
        {
            selectedMerchantInterestRepository.SelectedInterestRepositoryIndex = index;
            SwitchToView(nameof(MerchantInterestView));
        }
    }
}