using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.UnityEvents;
using Controllers.PaginationControllers;
using DataModels;
using JetBrains.Annotations;
using Repositories.Local.SingleItem;
using Repositories.Remote.Paginated;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    [Serializable]
    public class UserInterestLabelsPaginatedDataExplorer : PaginatedDataExplorer<InterestsBasicDataPaginatedListRepository, InterestBasicDataModel>
    {
        public UserInterestLabelsPaginatedDataExplorer() : base(nameof(UserInterestLabelsPaginatedDataExplorer))
        {
        }
    }

    [Binding]
    public sealed class UserInterestsLabelsViewModel : CorrespondingViewsSwitchingViewModel<UserInterestsLabelsView>
    {
        [SerializeField] private SelectedUserInterestRepository selectedUserInterestRepository;


        public UserInterestsLabelsViewModel() : base(nameof(UserInterestsLabelsViewModel))
        {
        }

        [Binding]
        public void Button_StartAnInterest_OnClick()
        {
        }

        public void SetNewSelectedInterest(uint interestId)
        {
            selectedUserInterestRepository.SelectedUserInterestRepositoryIndex = interestId;
            SwitchToPagesView();
        }


        private void SwitchToPagesView()
        {
            SwitchToView(nameof(UserInterestPagesView));
        }


        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            SubscribeOnViewEvents();
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            UnsubscribeFromViewEvents();
        }

        private void SubscribeOnViewEvents()
        {
            RelatedView.NewInterestSelected += SetNewSelectedInterest;
        }

        private void UnsubscribeFromViewEvents()
        {
            RelatedView.NewInterestSelected -= SetNewSelectedInterest;
        }
    }
}