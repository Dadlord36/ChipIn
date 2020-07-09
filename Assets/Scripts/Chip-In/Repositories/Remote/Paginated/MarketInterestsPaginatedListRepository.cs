using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using HttpRequests.RequestsProcessors;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(MarketInterestsPaginatedListRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(MarketInterestsPaginatedListRepository), order = 0)]
    public class MarketInterestsPaginatedListRepository : PaginatedItemsListRepository<MarketInterestDetailsDataModel,
        MerchantInterestsResponseDataModel, IMerchantInterestsResponseModel>
    {
        protected override string Tag => nameof(MarketInterestsPaginatedListRepository);
        protected override  Task<BaseRequestProcessor<object, MerchantInterestsResponseDataModel, IMerchantInterestsResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesStaticRequestsProcessor.GetOwnersCommunityPaginatedList(out cancellationTokenSource, authorisationDataRepository,
                paginatedRequestData);
        }

        protected override List<MarketInterestDetailsDataModel> GetItemsFromResponseModelInterface(IMerchantInterestsResponseModel
            responseModelInterface)
        {
           return new List<MarketInterestDetailsDataModel>(responseModelInterface.LabelDetailsDataModel);
        }
        
        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}