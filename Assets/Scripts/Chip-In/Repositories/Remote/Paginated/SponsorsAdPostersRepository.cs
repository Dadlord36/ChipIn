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
    public class SponsorsAdPostersRepository : PaginatedItemsListRepository<SponsoredPosterDataModel, SponsorsPostersResponseDataModel,
        ISponsorsPostersResponseModel>
    {
        public SponsorsAdPostersRepository() : base(nameof(SponsorsAdPostersRepository))
        {
        }

        protected override Task<BaseRequestProcessor<object, SponsorsPostersResponseDataModel, ISponsorsPostersResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return AdvertStaticRequestsProcessor.GetAllSponsoredAdPosters(out cancellationTokenSource, AuthorisationDataRepositoryHeaders,
                paginatedRequestData);
        }

        protected override List<SponsoredPosterDataModel> GetItemsFromResponseModelInterface(ISponsorsPostersResponseModel responseModelInterface)
        {
            return new List<SponsoredPosterDataModel>(responseModelInterface.SponsorPosters);
        }
    }
}