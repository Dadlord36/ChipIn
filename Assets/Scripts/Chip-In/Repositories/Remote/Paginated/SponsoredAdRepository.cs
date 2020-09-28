using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(SponsoredAdRepository), menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                                                                          + nameof(Paginated) + "/" + nameof(SponsoredAdRepository), order = 0)]
    public class SponsoredAdRepository : PaginatedItemsListRepository<SponsoredAdDataModel, SponsoredAdvertsResponseDataModel,
        ISponsoredAdvertsResponseModel>
    {
        protected override string Tag => nameof(SponsoredAdRepository);

        protected override Task<BaseRequestProcessor<object, SponsoredAdvertsResponseDataModel, ISponsoredAdvertsResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return AdvertStaticRequestsProcessor.GetListOfSponsoredAdverts(out cancellationTokenSource, authorisationDataRepository, paginatedRequestData);
        }

        protected override List<SponsoredAdDataModel> GetItemsFromResponseModelInterface(ISponsoredAdvertsResponseModel responseModelInterface)
        {
            return new List<SponsoredAdDataModel>(responseModelInterface.SponsoredAdverts);
        }
    }
}