using DataModels;
using Repositories.Remote.Paginated;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class SponsoredAdFullListAdapter : SelectableItemsRepositoryListAdapter<SponsorsAdPostersRepository, SponsoredPosterDataModel>
    {
        protected override void OnScrollPositionChanged(double normPos)
        {
            base.OnScrollPositionChanged(normPos);
            if (!IsInitialized || VisibleItemsCount == 0)
                return;
            FindMiddleElement();
        }
    }
}