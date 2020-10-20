using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;
using HttpRequests.RequestsProcessors;
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
        protected override string Tag => nameof(UserInterestPagesPaginatedRepository);
        public int SelectedCommunityId { get; set; }
        public int SelectedFilterIndex { get; set; }
        private string SelectedCategory => ((MainNames.InterestCategory) SelectedFilterIndex).ToString();
        

        protected override Task<BaseRequestProcessor<object, UserInterestPagesResponseDataModel, IUserInterestPagesResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesInterestsStaticProcessor.GetAllClientsInterestPages(out cancellationTokenSource, authorisationDataRepository,
                SelectedCommunityId, SelectedCategory, paginatedRequestData);
        }

        protected override List<UserInterestPageDataModel> GetItemsFromResponseModelInterface(IUserInterestPagesResponseModel pagesResponseModelInterface)
        {
            return pagesResponseModelInterface.Interests == null ? null : new List<UserInterestPageDataModel>(pagesResponseModelInterface.Interests);
        }
    }
}