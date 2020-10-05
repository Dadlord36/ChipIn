using DataModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(ScriptableMerchantInterestPagesPaginatedRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                   + nameof(Paginated) + "/" + nameof(ScriptableMerchantInterestPagesPaginatedRepository), order = 0)]
    public class ScriptableMerchantInterestPagesPaginatedRepository : ScriptablePaginatedItemsListRepository<MerchantInterestPageDataModel,
        MerchantInterestPagesResponseDataModel, IMerchantInterestPagesResponseModel, MerchantInterestPagesPaginatedRepository>
    {
        public int SelectedCommunityId
        {
            get => RemoteRepository.SelectedCommunityId;
            set => RemoteRepository.SelectedCommunityId = value;
        }
    }
}