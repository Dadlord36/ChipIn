using System;
using Common.UnityEvents;
using Controllers.PaginationControllers;
using DataModels;
using Repositories.Remote.Paginated;
using ViewModels.Basic;

namespace ViewModels
{
    [Serializable]
    public class UserInterestPagesPaginatedDataExplorer : PaginatedDataExplorer<UserInterestPagesPaginatedRepository, InterestPagePageDataModel>
    {
        public UserInterestPagesPaginatedDataExplorer() : base(nameof(UserInterestPagesPaginatedDataExplorer))
        {
        }
    }

    [Serializable]
    public class CommunityInterestDataModelListUnityEvent : ReadOnlyListUnityEvent<InterestPagePageDataModel>
    {
    }

    public class UserInterestPagesViewModel : BasicPaginatedListViewModel<CommunityInterestDataModelListUnityEvent, InterestPagePageDataModel,
        UserInterestPagesPaginatedDataExplorer, UserInterestPagesPaginatedRepository>
    {
        public UserInterestPagesViewModel() : base(nameof(UserInterestPagesViewModel))
        {
        }
        
        /*protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeOnEvents();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromEvents();
        }

        private void SubscribeOnEvents()
        {
            /*paginatedDataExplorer.NextPageItemsReceived += OnListedForward;
            paginatedDataExplorer.PreviousPageItemsReceived += OnListedBackward;#1#
        }

        private void UnsubscribeFromEvents()
        {
            /*paginatedDataExplorer.NextPageItemsReceived -= OnListedForward;
            paginatedDataExplorer.PreviousPageItemsReceived -= OnListedBackward;#1#
        }*/
 
    }
}