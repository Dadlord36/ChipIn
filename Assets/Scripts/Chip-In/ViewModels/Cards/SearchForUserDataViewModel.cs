using System;
using System.Threading.Tasks;
using Repositories.Remote.Paginated;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class SearchForUserDataViewModel : BaseSearchForItemsViewModel
    {
        [SerializeField] private UsersDataPaginatedListRepository usersDataPaginatedListRepository;
        [SerializeField] private UsersDataLabelsPaginatedListAdapter usersDataLabelsPaginatedListAdapter;


        protected override async Task RefillListViewAsync(string nameToSearch)
        {
            try
            {
                usersDataPaginatedListRepository.UserName = nameToSearch;
                await usersDataLabelsPaginatedListAdapter.ResetAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public override void Clear()
        {
            base.Clear();
            usersDataLabelsPaginatedListAdapter.ClearRemainListItems();
        }
    }
}