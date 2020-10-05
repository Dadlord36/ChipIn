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

namespace Repositories.Remote.Paginated
{
    public class MerchantInterestPagesPaginatedRepository : PaginatedItemsListRepository<MerchantInterestPageDataModel,
        MerchantInterestPagesResponseDataModel, IMerchantInterestPagesResponseModel>
    {
        public int SelectedCommunityId { get; set; }

        public MerchantInterestPagesPaginatedRepository() : base(nameof(MerchantInterestPagesPaginatedRepository))
        {
        }

        protected override Task<BaseRequestProcessor<object, MerchantInterestPagesResponseDataModel, IMerchantInterestPagesResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesInterestsStaticProcessor.GetMerchantInterestPages(out cancellationTokenSource, AuthorisationDataRepositoryHeaders,
                SelectedCommunityId, paginatedRequestData);
        }

        protected override List<MerchantInterestPageDataModel> GetItemsFromResponseModelInterface(
            IMerchantInterestPagesResponseModel responseModelInterface)
        {
            return new List<MerchantInterestPageDataModel>(responseModelInterface.Interests);
        }
    }
}