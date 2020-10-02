using Common;
using DataModels;
using Repositories.Remote;
using UnityWeld.Binding;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class UserProductsListAdapter : RepositoryBasedListAdapter<UserProductsRepository, ProductDataModel,
        DefaultFillingViewPageViewHolder<SponsoredAdCardViewModel.FieldFillingData>, SponsoredAdCardViewModel.FieldFillingData,
        UserProductsListAdapter.UserProductFillingViewAdapter>
    {
        public class UserProductFillingViewAdapter : FillingViewAdapter<ProductDataModel, SponsoredAdCardViewModel.FieldFillingData>
        {
            public override SponsoredAdCardViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                ProductDataModel data, uint dataIndexInRepository)
            {
                return new SponsoredAdCardViewModel.FieldFillingData(data.PosterUri);
            }
        }
    }
}