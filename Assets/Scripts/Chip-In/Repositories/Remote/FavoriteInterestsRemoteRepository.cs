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
    [CreateAssetMenu(fileName = nameof(FavoriteInterestsRemoteRepository), menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                                                                                      + nameof(Paginated) + "/"
                                                                                      + nameof(FavoriteInterestsRemoteRepository), order = 0)]
    public class FavoriteInterestsRemoteRepository : PaginatedItemsListRepository<UserInterestPageDataModel, UserInterestPagesResponseDataModel,
        IUserInterestPagesResponseModel>
    {
        protected override string Tag { get; } = nameof(FavoriteInterestsRemoteRepository);

        protected override Task<BaseRequestProcessor<object, UserInterestPagesResponseDataModel, IUserInterestPagesResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesInterestsStaticProcessor.GetAllFavoritesInterests(out cancellationTokenSource, authorisationDataRepository,
                paginatedRequestData);
        }

        protected override List<UserInterestPageDataModel> GetItemsFromResponseModelInterface(IUserInterestPagesResponseModel responseModelInterface)
        {
            return new List<UserInterestPageDataModel>(responseModelInterface.Interests);
        }
    }
}