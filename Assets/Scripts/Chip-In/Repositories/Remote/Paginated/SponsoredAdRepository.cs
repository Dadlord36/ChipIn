using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using RequestsStaticProcessors;

namespace Repositories.Remote.Paginated
{
    public class SponsoredAdRepository : PaginatedItemsListRepository<SponsoredAdDataModel, SponsoredAdvertsResponseDataModel,
        ISponsoredAdvertsResponseModel>
    {
        public SponsoredAdRepository() : base(nameof(SponsoredAdRepository))
        {
        }

        protected override Task<BaseRequestProcessor<object, SponsoredAdvertsResponseDataModel, ISponsoredAdvertsResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return AdvertStaticRequestsProcessor.GetListOfSponsoredAdverts(out cancellationTokenSource, AuthorisationDataRepositoryHeaders,
                paginatedRequestData);
        }

        protected override List<SponsoredAdDataModel> GetItemsFromResponseModelInterface(ISponsoredAdvertsResponseModel responseModelInterface)
        {
            return new List<SponsoredAdDataModel>(responseModelInterface.SponsoredAdverts);
        }
    }
}