using System;
using System.Collections.Generic;
using Controllers;
using DataModels;
using Repositories.Remote.Paginated;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    [Binding]
    public class CommunityInterestLabelsViewModel : BaseMenuViewModel<CommunityInterestLabelsView>
    {
        private const string Tag = nameof(CommunityInterestLabelsViewModel);

        [SerializeField] private CommunitiesDataPaginatedListRepository communitiesDataRepository;
        private PaginatedDataExplorer<CommunityBasicDataModel> _paginatedDataExplorer;
        
        
        protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeOnEvents();
            communitiesDataRepository.DataWasLoaded += UpdateItems;
            _paginatedDataExplorer = new PaginatedDataExplorer<CommunityBasicDataModel>(communitiesDataRepository);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromEvents();
            communitiesDataRepository.DataWasLoaded -= UpdateItems;
        }

        private  void Start()
        {
            TryToFillWithCurrentPageItems();
        }

        private void SubscribeOnEvents()
        {
            RelatedView.SwipedLeft += TryToSwitchToNextPage;
            RelatedView.SwipedRight += TryToSwitchToPreviousPage ;
        }

        private void UnsubscribeFromEvents()
        {
            RelatedView.SwipedLeft -= TryToSwitchToNextPage ;
            RelatedView.SwipedRight -= TryToSwitchToPreviousPage;
        }

        [Binding]
        public void Button_StartAnInterest_OnClick()
        {
        }

        private async void TryToFillWithCurrentPageItems()
        {
            try
            {
                FillGridViewWithItems(await _paginatedDataExplorer.TryToGetCurrentPageItems());
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private async void TryToSwitchToNextPage()
        {
            try
            {
                FillGridViewWithItems(await _paginatedDataExplorer.TryToGetNextPageItems());
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private async void TryToSwitchToPreviousPage()
        {
            try
            {
                FillGridViewWithItems(await _paginatedDataExplorer.TryToGetPreviousPageItems());
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void FillGridViewWithItems(IReadOnlyList<CommunityBasicDataModel> communityBasicDataModels)
        {
            if (communityBasicDataModels == null)
            {
                LogUtility.PrintLog(Tag, "There is no items left");
                return;
            }

            RelatedView.UpdateGridItemsContent(communityBasicDataModels);
        }
    }
}