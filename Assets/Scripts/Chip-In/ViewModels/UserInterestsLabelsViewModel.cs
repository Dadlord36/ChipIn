using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using Factories;
using Repositories.Interfaces;
using Repositories.Remote.Paginated;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Binding]
    public sealed class UserInterestsLabelsViewModel : CorrespondingViewsSwitchingViewModel<UserInterestsLabelsView>
    {
        [SerializeField] private InterestsBasicDataPaginatedListRepository interestsBasicDataPaginatedListRepository;
        [SerializeField] private UserInterestPagesPaginatedRepository userInterestPagesPaginatedRepository;
        [SerializeField] private UserInterestsListAdapter userInterestsListAdapter;
        [SerializeField] private LastViewedInterestsListAdapter lastViewedInterestsListAdapter;

        private static ILastViewedInterestsRepository ViewedInterestsRepository => SimpleAutofac.GetInstance<ILastViewedInterestsRepository>();

        private InterestBasicDataModel _selectedInterestData;

        [Binding]
        public InterestBasicDataModel SelectedInterest
        {
            get => _selectedInterestData;
            set
            {
                _selectedInterestData = value;
                SelectedInterestIndex = (int) value.Id;
            }
        }

        private int SelectedInterestIndex
        {
            get => userInterestPagesPaginatedRepository.SelectedCommunityId;
            set
            {
                userInterestPagesPaginatedRepository.SelectedCommunityId = value;
                OnNewInterestSelected();
            }
        }

        public UserInterestsLabelsViewModel() : base(nameof(UserInterestsLabelsViewModel))
        {
        }

        private async void SetSelectedCommunityIdAsync(int listIndex)
        {
            try
            {
                userInterestPagesPaginatedRepository.SelectedCommunityId = await GetDataByIndexAsync((uint) listIndex)
                    .ConfigureAwait(false);
                OnNewInterestSelected();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void OnNewInterestSelected()
        {
            ViewedInterestsRepository.AddUniqueItemAtStartAsync(SelectedInterest);
            SwitchToPagesView();
        }

        private async Task<int> GetDataByIndexAsync(uint index)
        {
            var itemData = await interestsBasicDataPaginatedListRepository.GetItemWithIndexAsync(index)
                .ConfigureAwait(false);
            return (int) itemData.Id;
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await userInterestsListAdapter.ResetAsync().ConfigureAwait(false);
                await ViewedInterestsRepository.Restore().ConfigureAwait(false);
                RefillLastViewedItemsList(ViewedInterestsRepository.LastViewedInterestsList);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void RefillLastViewedItemsList(IList<InterestBasicDataModel> itemsData)
        {
            lastViewedInterestsListAdapter.SetItems(itemsData);
        }

        private void SwitchToPagesView()
        {
            SwitchToView(nameof(UserInterestPagesView));
        }
    }
}