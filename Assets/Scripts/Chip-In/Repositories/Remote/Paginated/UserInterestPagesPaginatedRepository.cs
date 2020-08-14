using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using Repositories.Local.SingleItem;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(UserInterestPagesPaginatedRepository), menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                                                                                         + nameof(Paginated) + "/"
                                                                                         + nameof(UserInterestPagesPaginatedRepository), order = 0)]
    public class UserInterestPagesPaginatedRepository : PaginatedItemsListRepository<UserInterestPageDataModel, UserInterestPagesResponseDataModel,
        IUserInterestPagesResponseModel>
    {
        [SerializeField] private SelectedUserInterestRepository selectedUserInterestRepository;
        protected override string Tag => nameof(UserInterestPagesPaginatedRepository);
        private Task<int?> SelectedCommunityId => selectedUserInterestRepository.SelectedUserInterestId;
        

        protected override Task<BaseRequestProcessor<object, UserInterestPagesResponseDataModel, IUserInterestPagesResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {

            DisposableCancellationTokenSource cancellationTokenSourceLocal = null;

            var task = SelectedCommunityId.ContinueWith(selectedCommunityIdGetTask =>
                    CommunitiesInterestsStaticProcessor.GetClientsInterestPages(out cancellationTokenSourceLocal,
                        authorisationDataRepository, selectedCommunityIdGetTask.GetAwaiter().GetResult().Value, paginatedRequestData),
                TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap();

            cancellationTokenSource = cancellationTokenSourceLocal;
            return task;
        }

        protected override List<UserInterestPageDataModel> GetItemsFromResponseModelInterface(IUserInterestPagesResponseModel pagesResponseModelInterface)
        {
            return new List<UserInterestPageDataModel>(pagesResponseModelInterface.Interests);
        }
    }
}