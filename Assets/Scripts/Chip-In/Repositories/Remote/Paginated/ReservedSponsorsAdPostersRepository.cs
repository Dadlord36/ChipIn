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
    public class ReservedSponsorsAdPostersRepository : PaginatedItemsListRepository<SponsoredPosterDataModel, SponsorsPostersResponseDataModel,
        ISponsorsPostersResponseModel>
    {
        public ReservedSponsorsAdPostersRepository() : base(nameof(ReservedSponsorsAdPostersRepository))
        {
        }

        protected override Task<BaseRequestProcessor<object, SponsorsPostersResponseDataModel, ISponsorsPostersResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return AdvertStaticRequestsProcessor.GetAllReservedAdPosters(out cancellationTokenSource, AuthorisationDataRepositoryHeaders,
                paginatedRequestData);
        }

        protected override List<SponsoredPosterDataModel> GetItemsFromResponseModelInterface(ISponsorsPostersResponseModel responseModelInterface)
        {
            return new List<SponsoredPosterDataModel>(responseModelInterface.SponsorPosters);
        }
    }
}