using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        menuName = nameof(Repositories) + "/" + nameof(Remote) + nameof(CommunitiesDataPaginatedListRepository),
        fileName = "Create " + nameof(CommunitiesDataPaginatedListRepository), order = 0)]
    public class CommunitiesDataPaginatedListRepository : PaginatedItemsListRepository<CommunityBasicDataModel,
        CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>
    {
        protected override string Tag => nameof(CommunitiesDataPaginatedListRepository);

        protected override Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>
            CreateLoadPaginatedItemsTask(out CancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesStaticRequestsProcessor.GetPaginatedCommunitiesList(out cancellationTokenSource, authorisationDataRepository,
                paginatedRequestData);
        }

        protected override List<CommunityBasicDataModel> GetItemsFromResponseModelInterface(
            ICommunitiesBasicDataRequestResponse responseModelInterface)
        {
            return new List<CommunityBasicDataModel>(responseModelInterface.Communities);
        }

        public override async Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}