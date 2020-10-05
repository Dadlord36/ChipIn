using System;
using System.Collections.Generic;
using Behaviours;
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
    public class SearchForCommunityViewModel : AsyncOperationsMonoBehaviour
    {
        private const string Tag = nameof(SearchForCommunityViewModel);

        [SerializeField] private UserInterestsLabelsSimpleListAdapter userInterestsLabelsSimpleListAdapter;
        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;

        private string _inputText;

        [Binding]
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                try
                {
                    RefillListView(value);
                }
                catch (Exception e)
                {
                    LogUtility.PrintLogException(e);
                    throw;
                }
            }
        }

        private void RestItems(IList<InterestBasicDataModel> items)
        {
            userInterestsLabelsSimpleListAdapter.SetItems(items);
        }

        private async void RefillListView(string value)
        {
            try
            {
                AsyncOperationCancellationController.CancelOngoingTask();
                var response = await CommunitiesStaticRequestsProcessor.GetCommunitiesListByName(
                    out AsyncOperationCancellationController.TasksCancellationTokenSource, userAuthorisationDataRepository, value);
                if (!response.Success)
                {
                    LogUtility.PrintLog(Tag, response.Error);
                    return;
                }

                RestItems(response.ResponseModelInterface.Communities);
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
    }
}