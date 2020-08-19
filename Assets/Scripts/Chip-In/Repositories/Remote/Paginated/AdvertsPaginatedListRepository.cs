using System;
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
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(Paginated) + "/" + "Create " + nameof(AdvertsPaginatedListRepository),
        fileName = nameof(AdvertsPaginatedListRepository), order = 0)]
    public class AdvertsPaginatedListRepository : PaginatedItemsListRepository<AdvertItemDataModel, AdvertsListResponseDataModel, IAdvertsListResponseModel>
    {
        protected override string Tag => nameof(AdvertsPaginatedListRepository);

        protected override Task<BaseRequestProcessor<object, AdvertsListResponseDataModel, IAdvertsListResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return AdvertStaticRequestsProcessor.ListAdverts(out cancellationTokenSource, authorisationDataRepository, paginatedRequestData);
        }

        protected override List<AdvertItemDataModel> GetItemsFromResponseModelInterface(IAdvertsListResponseModel responseModelInterface)
        {
            return responseModelInterface.Adverts.ToList();
        }
    }
}