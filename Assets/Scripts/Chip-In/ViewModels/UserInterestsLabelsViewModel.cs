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
    public class UserInterestLabelsPaginatedDataExplorer : PaginatedDataExplorer<UserInterestsBasicDataPaginatedListRepository, InterestBasicDataModel>
    {
        public UserInterestLabelsPaginatedDataExplorer() : base(nameof(UserInterestLabelsPaginatedDataExplorer))
        {
        }
    }

    [Serializable]
    public class CommunityBasicDataModelListUnityEvent : ReadOnlyListUnityEvent<InterestBasicDataModel>
    {
    }

    [Binding]
    public sealed class UserInterestsLabelsViewModel : CorrespondingViewsSwitchingViewModel<UserInterestsLabelsView>, INotifyPropertyChanged
    {
        [SerializeField] private SelectedUserInterestRepository selectedUserInterestRepository;
        
        [Binding]
        public void Button_StartAnInterest_OnClick()
        {
        }

        public void SetNewSelectedInterest(int? interestId)
        {
            selectedUserInterestRepository.SelectedInterestId = interestId;
            SwitchToPagesView();
        }
        
        public UserInterestsLabelsViewModel() : base(nameof(UserInterestsLabelsViewModel))
        {
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


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}