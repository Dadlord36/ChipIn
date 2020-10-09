using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels.Cards
{
    [Binding]
    public class SearchForCommunityViewModel : BaseSearchForItemsViewModel
    {
        private const string Tag = nameof(SearchForCommunityViewModel);

        [SerializeField] private UserInterestsLabelsSimpleListAdapter userInterestsLabelsSimpleListAdapter;
        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;


        protected override async Task RefillListViewAsync(string nameToSearch)
        {
            AsyncOperationCancellationController.CancelOngoingTask();
            var response = await CommunitiesStaticRequestsProcessor.GetCommunitiesListByName(
                    out AsyncOperationCancellationController.TasksCancellationTokenSource, userAuthorisationDataRepository, nameToSearch)
                .ConfigureAwait(false);
            if (!response.Success)
            {
                LogUtility.PrintLog(Tag, response.Error);
                return;
            }

            RestItems(response.ResponseModelInterface.Communities);
        }

        private void RestItems(IList<InterestBasicDataModel> items)
        {
            userInterestsLabelsSimpleListAdapter.SetItems(items);
        }

        public override void Clear()
        {
            base.Clear();
            userInterestsLabelsSimpleListAdapter.ClearRemainListItems();
        }
    }
}