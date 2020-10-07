using System;
using System.Threading.Tasks;
using GlobalVariables;
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
    public class UserInterestPagesViewModel : CorrespondingViewsSwitchingViewModel<UserInterestPagesView>
    {
        [SerializeField] private UserInterestPagesPaginatedRepository userInterestPagesPaginatedRepository;

        #region Controlled List Adapters

        [SerializeField] private UserInterestPagesListAdapter allInterestPagesListAdapter;
        [SerializeField] private UserInterestPagesListAdapter joinInInterestPagesListAdapter;
        [SerializeField] private UserInterestPagesListAdapter myInterestPagesListAdapter;

        #endregion

        [Binding]
        public int SelectedFilterIndex
        {
            get => userInterestPagesPaginatedRepository.SelectedFilterIndex;
            set
            {
                userInterestPagesPaginatedRepository.SelectedFilterIndex = value;
                try
                {
                    RefreshCorrespondingListViewAsync(value);
                }
                catch (Exception e)
                {
                    LogUtility.PrintLogException(e);
                    throw;
                }
            }
        }

        private int SelectedCommunityId => userInterestPagesPaginatedRepository.SelectedCommunityId;

        public UserInterestPagesViewModel() : base(nameof(UserInterestPagesViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await RefreshCorrespondingListViewAsync(SelectedFilterIndex).ConfigureAwait(false);
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

        [Binding]
        public void StartAnInterestButton_OnClick()
        {
            SwitchToView(nameof(StartInterestView), new FormsTransitionBundle(SelectedCommunityId));
        } 
        
        private Task RefreshCorrespondingListViewAsync(int selectedFilterIndex)
        {
            UserInterestPagesListAdapter controllingAdapter;
            switch ((MainNames.InterestCategory) selectedFilterIndex)
            {
                case MainNames.InterestCategory.all:
                    controllingAdapter = allInterestPagesListAdapter;
                    break;
                case MainNames.InterestCategory.@join:
                    controllingAdapter = joinInInterestPagesListAdapter;
                    break;
                case MainNames.InterestCategory.my:
                    controllingAdapter = myInterestPagesListAdapter;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return controllingAdapter.ResetAsync();
        }
    }
}