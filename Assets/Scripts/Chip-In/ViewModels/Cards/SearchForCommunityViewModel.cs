using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using DataModels.ResponsesModels;
using Factories.ReferencesContainers;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels.Cards
{
    [Binding]
    public class SearchForUsersViewModel : SearchItemsList<IUserListResponseModel>
    {
        private const string Tag = nameof(SearchForUsersViewModel);
        
        protected  override async Task<IList<IUserListResponseModel>> RetrieveItems(string value)
        {
            /*var response = await UsersRequestsStaticProcessor.GetUserDataByName(out AsyncOperationCancellationController.TasksCancellationTokenSource,
                MainObjectsReferencesContainer.GetObjectInstance<IUserAuthorisationDataRepository>(), value);

            if (response.Success) return response.ResponseModelInterface.Communities;
            LogUtility.PrintLog(Tag, response.Error);*/
            return null;
        }

        protected override void RestItems(IList<IUserListResponseModel> items)
        {
            throw new NotImplementedException();
        }
    }

    [Binding]
    public class SearchForCommunityViewModel : SearchItemsList<InterestBasicDataModel>
    {
        private const string Tag = nameof(SearchForCommunityViewModel);

        [SerializeField] private UserInterestsLabelsSimpleListAdapter userInterestsLabelsSimpleListAdapter;

        private void Start()
        {
            userInterestsLabelsSimpleListAdapter.itemSelected.AddListener(SyncSelectedIndex);
        }

        private void OnDestroy()
        {
            userInterestsLabelsSimpleListAdapter.itemSelected.RemoveListener(SyncSelectedIndex);
        }

        private void SyncSelectedIndex()
        {
            SelectedItemIndex = userInterestsLabelsSimpleListAdapter.SelectedIndex;
        }

        protected override async Task<IList<InterestBasicDataModel>> RetrieveItems(string value)
        {
            var response = await CommunitiesStaticRequestsProcessor.GetCommunitiesListByName(
                out AsyncOperationCancellationController.TasksCancellationTokenSource,
                MainObjectsReferencesContainer.GetObjectInstance<IUserAuthorisationDataRepository>(), value);

            if (response.Success) return response.ResponseModelInterface.Communities;
            LogUtility.PrintLog(Tag, response.Error);
            return null;
        }

        protected override void RestItems(IList<InterestBasicDataModel> items)
        {
            userInterestsLabelsSimpleListAdapter.SetItems(items);
        }
    }
}