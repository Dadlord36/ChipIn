using Common;
using DataModels;
using Repositories.Remote.Paginated;
using Views.ViewElements.Fields;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public sealed class MerchantInterestPagesListAdapter : SelectableElementsPagesListAdapter<MerchantInterestPagesPaginatedRepository,
        MerchantInterestPageDataModel, DefaultFillingViewPageViewHolder<NameAndNumberSelectableFieldFillingData>,
        NameAndNumberSelectableFieldFillingData,MerchantInterestPagesListAdapter.MerchantInterestPageDataAdapter>
    {
        public class MerchantInterestPageDataAdapter : FillingViewAdapter<MerchantInterestPageDataModel, NameAndNumberSelectableFieldFillingData>
        {
            public override NameAndNumberSelectableFieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                MerchantInterestPageDataModel data, uint dataIndexInRepository)
            {
                return new NameAndNumberSelectableFieldFillingData(data.Name, dataIndexInRepository, data.JoinedCount);
            }
        }
    }
}