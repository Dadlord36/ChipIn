using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using HttpRequests.RequestsProcessors;
using RequestsStaticProcessors;

namespace Repositories.Remote.Paginated
{
    public class MarketInterestsPaginatedListRepository : PaginatedItemsListRepository<MarketInterestDetailsDataModel, MerchantInterestsResponseDataModel,
        IMerchantInterestsResponseModel>
    {
        public MarketInterestsPaginatedListRepository() : base(nameof(MarketInterestsPaginatedListRepository))
        {
        }

        protected override Task<BaseRequestProcessor<object, MerchantInterestsResponseDataModel, IMerchantInterestsResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesStaticRequestsProcessor.GetOwnersCommunityPaginatedList(out cancellationTokenSource, AuthorisationDataRepositoryHeaders,
                paginatedRequestData);
        }

        protected override List<MarketInterestDetailsDataModel> GetItemsFromResponseModelInterface(IMerchantInterestsResponseModel responseModelInterface)
        {
            return new List<MarketInterestDetailsDataModel>(responseModelInterface.LabelDetailsDataModel);
        }
    }
}