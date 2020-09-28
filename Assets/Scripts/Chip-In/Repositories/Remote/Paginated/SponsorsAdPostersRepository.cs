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
    [CreateAssetMenu(fileName = nameof(SponsorsAdPostersRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(Paginated) + "/" + nameof(SponsorsAdPostersRepository), order = 0)]
    public class SponsorsAdPostersRepository : PaginatedItemsListRepository<SponsoredPosterDataModel, SponsorsPostersResponseDataModel,
        ISponsorsPostersResponseModel>
    {
        protected override string Tag => nameof(SponsorsAdPostersRepository);

        protected override Task<BaseRequestProcessor<object, SponsorsPostersResponseDataModel, ISponsorsPostersResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return AdvertStaticRequestsProcessor.GetAllSponsoredAdPosters(out cancellationTokenSource, authorisationDataRepository, paginatedRequestData);
        }

        protected override List<SponsoredPosterDataModel> GetItemsFromResponseModelInterface(ISponsorsPostersResponseModel responseModelInterface)
        {
            return new List<SponsoredPosterDataModel>(responseModelInterface.SponsorPosters);
        }
    }
}