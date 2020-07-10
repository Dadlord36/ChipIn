using DataModels;
using Repositories.Remote.Paginated;
using Views.ViewElements.Fields;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public class MerchantInterestListAdapter : SelectableElementsPagesListAdapter
    <
        MarketInterestsPaginatedListRepository,
        MarketInterestDetailsDataModel,
        EngageCardViewHolder, MarketInterestDetailsDataModel,
        DefaultFillingViewAdapter<MarketInterestDetailsDataModel>
    >
    {
        public class MerchantInterestDataAdapter : FillingViewAdapter<MarketInterestDetailsDataModel, NameAndNumberSelectableFieldFillingData>
        {
            public override NameAndNumberSelectableFieldFillingData Convert(MarketInterestDetailsDataModel data, uint dataIndexInRepository)
            {
                return new NameAndNumberSelectableFieldFillingData(data.Name, dataIndexInRepository, data.Size);
            }
        }
    }
}