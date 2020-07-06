using DataModels;
using Repositories.Remote.Paginated;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public class UserInterestPagesListAdapter : RepositoryBasedListAdapter<UserInterestPagesPaginatedRepository, UserInterestPageDataModel,
        DefaultFillingViewPageViewHolder<UserInterestPageDataModel>>
    {
    }
}