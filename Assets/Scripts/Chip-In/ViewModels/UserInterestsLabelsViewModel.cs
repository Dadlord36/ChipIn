using System;
using System.Threading.Tasks;
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

        private int _selectedIndex;

        [Binding]
        public uint SelectedIndex
        {
            get => (uint) _selectedIndex;
            set => SelectedInterestIndex = _selectedIndex = (int) value;
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
                userInterestPagesPaginatedRepository.SelectedCommunityId = await GetDataByIndex((uint) listIndex);
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
            SwitchToPagesView();
        }

        private async Task<int> GetDataByIndex(uint index)
        {
            var itemData = await interestsBasicDataPaginatedListRepository.CreateGetItemWithIndexTask(index);
            return (int) itemData.Id;
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await userInterestsListAdapter.ResetAsync();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void SwitchToPagesView()
        {
            SwitchToView(nameof(UserInterestPagesView));
        }
    }
}