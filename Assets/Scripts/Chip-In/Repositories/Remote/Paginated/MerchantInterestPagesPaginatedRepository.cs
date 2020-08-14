using System;
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
    [CreateAssetMenu(fileName = nameof(MerchantInterestPagesPaginatedRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                   + nameof(Paginated) + "/" + nameof(MerchantInterestPagesPaginatedRepository), order = 0)]
    public class MerchantInterestPagesPaginatedRepository : PaginatedItemsListRepository<MerchantInterestPageDataModel,
        MerchantInterestPagesResponseDataModel, IMerchantInterestPagesResponseModel>
    {
        protected override string Tag => nameof(MerchantInterestPagesPaginatedRepository);

        public int SelectedCommunityId { get; set; }


        protected override Task<BaseRequestProcessor<object, MerchantInterestPagesResponseDataModel, IMerchantInterestPagesResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesInterestsStaticProcessor.GetMerchantInterestPages(out cancellationTokenSource, authorisationDataRepository,
                SelectedCommunityId, paginatedRequestData);
        }


        protected override List<MerchantInterestPageDataModel> GetItemsFromResponseModelInterface(IMerchantInterestPagesResponseModel
            responseModelInterface)
        {
            return new List<MerchantInterestPageDataModel>(responseModelInterface.Interests);
        }
    }
}