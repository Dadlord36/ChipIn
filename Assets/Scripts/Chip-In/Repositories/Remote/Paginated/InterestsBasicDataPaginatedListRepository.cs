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
    public class InterestsBasicDataPaginatedListRepository : PaginatedItemsListRepository<InterestBasicDataModel, CommunitiesBasicDataRequestResponse,
        ICommunitiesBasicDataRequestResponse>
    {
        public InterestsBasicDataPaginatedListRepository() : base(nameof(InterestsBasicDataPaginatedListRepository))
        {
        }

        protected override Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesStaticRequestsProcessor.GetUserCommunitiesPaginatedList(out cancellationTokenSource, AuthorisationDataRepositoryHeaders,
                paginatedRequestData);
        }

        protected override List<InterestBasicDataModel> GetItemsFromResponseModelInterface(ICommunitiesBasicDataRequestResponse responseModelInterface)
        {
            return new List<InterestBasicDataModel>(responseModelInterface.Communities);
        }
    }
}