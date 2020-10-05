using DataModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using UnityEngine;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(ScriptableMerchantVerificationRepository), menuName = nameof(Repositories) + "/"
        + nameof(Remote) + "/"
        + nameof(ScriptableMerchantVerificationRepository), order = 0)]
    public sealed class ScriptableMerchantVerificationRepository : ScriptablePaginatedItemsListRepository<VerificationDataModel,
        VerificationResponseDataModel, IVerificationResponseModel,MerchantVerificationRepository>
    {
    }
}