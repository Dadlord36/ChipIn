﻿using System;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Binding]
    public class EngageViewModel : ViewsSwitchingViewModel
    {
        private uint _selectedCommunityId;

        [Binding]
        public uint SelectedCommunityId
        {
            get => _selectedCommunityId;
            set => MerchantInterestListAdapterOnItemSelected(_selectedCommunityId = value);
        }

        public EngageViewModel() : base(nameof(EngageViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            var adapter = GetComponentInChildren<MerchantCommunitiesDetailsListAdapter>();
            try
            {
                await adapter.ResetAsync().ConfigureAwait(true);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }

            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }


        private void MerchantInterestListAdapterOnItemSelected(uint index)
        {
            SwitchToView(nameof(MerchantInterestView), new FormsTransitionBundle(index));
        }
    }
}