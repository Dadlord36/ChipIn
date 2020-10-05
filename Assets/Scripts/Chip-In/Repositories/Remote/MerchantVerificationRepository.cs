using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using RequestsStaticProcessors;

namespace Repositories.Remote
{
    public class MerchantVerificationRepository : PaginatedItemsListRepository<VerificationDataModel, VerificationResponseDataModel,
        IVerificationResponseModel>
    {
        public MerchantVerificationRepository() : base(nameof(MerchantVerificationRepository))
        {
        }
        
        protected override Task<BaseRequestProcessor<object, VerificationResponseDataModel, IVerificationResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return ProfileDataStaticRequestsProcessor.GetVerificationData(out cancellationTokenSource, AuthorisationDataRepositoryHeaders);
        }

        protected override List<VerificationDataModel> GetItemsFromResponseModelInterface(IVerificationResponseModel responseModelInterface)
        {
            return new List<VerificationDataModel>(responseModelInterface.Verification);
        }
    }
}