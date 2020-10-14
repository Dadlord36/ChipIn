using DataModels;
using Repositories.Remote;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public class FavoriteInterestsListAdapter : RepositoryBasedListAdapter<FavoriteInterestsRemoteRepository, UserInterestPageDataModel>
    {
    }
}