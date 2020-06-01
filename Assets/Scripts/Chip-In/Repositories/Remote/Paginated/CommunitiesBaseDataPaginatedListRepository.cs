using System;
using System.Collections.Generic;
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
        menuName = nameof(Repositories) + "/" + nameof(Remote) + nameof(CommunitiesBaseDataPaginatedListRepository),
        fileName = "Create " + nameof(CommunitiesBaseDataPaginatedListRepository), order = 0)]
    public class CommunitiesBaseDataPaginatedListRepository : BasePaginatedItemsListRepository<CommunityBasicDataModel,
        CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>
    {
        protected override string Tag => nameof(CommunitiesBaseDataPaginatedListRepository);

        protected override Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse,
                ICommunitiesBasicDataRequestResponse>.HttpResponse>
            CreateLoadPaginatedItemsTask(PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesStaticRequestsProcessor.GetPaginatedCommunitiesList(authorisationDataRepository,
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