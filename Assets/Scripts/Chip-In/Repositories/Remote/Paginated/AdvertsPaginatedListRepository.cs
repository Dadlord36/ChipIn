using System.Collections.Generic;
using System.Linq;
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
    public class AdvertsPaginatedListRepository : PaginatedItemsListRepository<AdvertItemDataModel, AdvertsListResponseDataModel, IAdvertsListResponseModel>
    {
        protected override string Tag => nameof(AdvertsPaginatedListRepository);

        public AdvertsPaginatedListRepository(IUserAuthorisationDataRepository authorisationDataRepositoryInterface) : base(authorisationDataRepositoryInterface)
        {
        }

        protected override Task<BaseRequestProcessor<object, AdvertsListResponseDataModel, IAdvertsListResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return AdvertStaticRequestsProcessor.ListAdverts(out cancellationTokenSource, AuthorisationDataRepository, paginatedRequestData);
        }

        protected override List<AdvertItemDataModel> GetItemsFromResponseModelInterface(IAdvertsListResponseModel responseModelInterface)
        {
            return responseModelInterface.Adverts.ToList();
        }
    }
}