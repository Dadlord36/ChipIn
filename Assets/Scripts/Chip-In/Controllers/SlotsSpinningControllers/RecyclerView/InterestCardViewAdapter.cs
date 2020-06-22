using DataModels;
using Repositories.Remote.Paginated;
using Views.Cards;

namespace Controllers.SlotsSpinningControllers.RecyclerView
{
    class InterestCardViewAdapter : PaginatedRepositoryRecyclerViewAdapter<InterestCardView, InterestPagePageDataModel,
        UserInterestPagesPaginatedRepository>
    {
    }
}