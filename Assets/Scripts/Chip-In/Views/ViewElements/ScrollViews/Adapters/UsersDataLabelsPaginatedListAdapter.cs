using Common;
using DataModels;
using Repositories.Remote.Paginated;
using UnityWeld.Binding;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class UsersDataLabelsPaginatedListAdapter : SelectableElementsPagesListAdapter<UsersDataPaginatedListRepository, UserProfileBaseData,
        DefaultFillingViewPageViewHolder<UserDataItemViewModel.FieldFillingData, UserProfileBaseData>, UserProfileBaseData,
        UserDataItemViewModel.FieldFillingData, UsersDataLabelsPaginatedListAdapter.FillingViewAdapter>
    {
        public class FillingViewAdapter : FillingViewAdapter<UserProfileBaseData, UserDataItemViewModel.FieldFillingData>
        {
            public override UserDataItemViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                UserProfileBaseData data, uint dataIndexInRepository)
            {
                return new UserDataItemViewModel.FieldFillingData(data);
            }
        }
    }
}