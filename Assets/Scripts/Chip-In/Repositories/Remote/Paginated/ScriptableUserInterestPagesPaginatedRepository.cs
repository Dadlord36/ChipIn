using DataModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(ScriptableUserInterestPagesPaginatedRepository), menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                                                                                                   + nameof(Paginated) + "/"
                                                                                                   + nameof(ScriptableUserInterestPagesPaginatedRepository
                                                                                                   ), order = 0)]
    public class ScriptableUserInterestPagesPaginatedRepository : ScriptablePaginatedItemsListRepository<UserInterestPageDataModel,
        UserInterestPagesResponseDataModel, IUserInterestPagesResponseModel, UserInterestPagesPaginatedRepository>
    {
        public int SelectedCommunityId
        {
            get => RemoteRepository.SelectedCommunityId;
            set => RemoteRepository.SelectedCommunityId = value;
        }

        public int SelectedFilterIndex
        {
            get => RemoteRepository.SelectedFilterIndex;
            set => RemoteRepository.SelectedFilterIndex = value;
        }
    }
}