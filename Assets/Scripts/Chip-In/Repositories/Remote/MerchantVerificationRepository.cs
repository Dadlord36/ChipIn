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

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(MerchantVerificationRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(MerchantVerificationRepository), order = 0)]
    public sealed class MerchantVerificationRepository : PaginatedItemsListRepository<VerificationDataModel, VerificationResponseDataModel,
        IVerificationResponseModel>
    {
        protected override string Tag => nameof(MerchantVerificationRepository);

        protected override Task<BaseRequestProcessor<object, VerificationResponseDataModel, IVerificationResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return ProfileDataStaticRequestsProcessor.GetVerificationData(out cancellationTokenSource, authorisationDataRepository);
        }

        protected override List<VerificationDataModel> GetItemsFromResponseModelInterface(IVerificationResponseModel responseModelInterface)
        {
            return new List<VerificationDataModel>(responseModelInterface.Verification);
        }
    }
}