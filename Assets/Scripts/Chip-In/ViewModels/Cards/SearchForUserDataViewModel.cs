using System;
using Behaviours;
using Repositories.Remote.Paginated;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels.Cards
{
    [Binding]
    public class SearchForUserDataViewModel : AsyncOperationsMonoBehaviour
    {
        [SerializeField] private UsersDataPaginatedListRepository usersDataPaginatedListRepository;
        [SerializeField] private UsersDataLabelsPaginatedListAdapter usersDataLabelsPaginatedListAdapter;
        
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

        private async void RefillListView(string value)
        {
            usersDataPaginatedListRepository.UserName = value;
            await usersDataLabelsPaginatedListAdapter.ResetAsync().ConfigureAwait(false);
        }
    }
}