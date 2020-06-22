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
    public class UserInterestLabelsPaginatedDataExplorer : PaginatedDataExplorer<CommunitiesDataPaginatedListRepository, InterestBasicDataModel>
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
    public sealed class CommunityInterestLabelsViewModel : BasicPaginatedListViewModel<CommunityBasicDataModelListUnityEvent, InterestBasicDataModel,
        UserInterestLabelsPaginatedDataExplorer, CommunitiesDataPaginatedListRepository>, INotifyPropertyChanged
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
        
        public CommunityInterestLabelsViewModel() : base(nameof(CommunityInterestLabelsViewModel))
        {
        }

        private void SwitchToPagesView()
        {
            SwitchToView(nameof(UserInterestPagesView));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}