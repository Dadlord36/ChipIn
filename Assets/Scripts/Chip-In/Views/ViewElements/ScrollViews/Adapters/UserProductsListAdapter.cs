using DataModels;
using Repositories.Remote;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class UserProductsListAdapter : RepositoryBasedListAdapter<UserProductsRepository, ProductDataModel>
    {
    }
}