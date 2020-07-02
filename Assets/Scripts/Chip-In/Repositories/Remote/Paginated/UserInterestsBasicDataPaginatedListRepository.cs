﻿using System;
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
    [CreateAssetMenu(
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(Paginated) + "/" + nameof(UserInterestsBasicDataPaginatedListRepository),
        fileName = "Create " + nameof(UserInterestsBasicDataPaginatedListRepository), order = 0)]
    public class UserInterestsBasicDataPaginatedListRepository : PaginatedItemsListRepository<InterestBasicDataModel,
        CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>
    {
        protected override string Tag => nameof(UserInterestsBasicDataPaginatedListRepository);

        protected override Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesStaticRequestsProcessor.GetPaginatedCommunitiesList(out cancellationTokenSource, authorisationDataRepository,
                paginatedRequestData);
        }

        protected override List<InterestBasicDataModel> GetItemsFromResponseModelInterface(ICommunitiesBasicDataRequestResponse responseModelInterface)
        {
            return new List<InterestBasicDataModel>(responseModelInterface.Communities);
        }

        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}