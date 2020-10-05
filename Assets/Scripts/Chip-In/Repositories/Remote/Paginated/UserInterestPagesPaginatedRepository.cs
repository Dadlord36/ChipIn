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

namespace Repositories.Remote.Paginated
{
    public class UserInterestPagesPaginatedRepository : PaginatedItemsListRepository<UserInterestPageDataModel, UserInterestPagesResponseDataModel,
        IUserInterestPagesResponseModel>
    {
        public int SelectedCommunityId { get; set; }
        public int SelectedFilterIndex { get; set; }

        private string SelectedCategory => ((MainNames.InterestCategory) SelectedFilterIndex).ToString();

        public UserInterestPagesPaginatedRepository() : base(nameof(UserInterestPagesPaginatedRepository))
        {
        }

        protected override Task<BaseRequestProcessor<object, UserInterestPagesResponseDataModel, IUserInterestPagesResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesInterestsStaticProcessor.GetAllClientsInterestPages(out cancellationTokenSource, AuthorisationDataRepositoryHeaders,
                SelectedCommunityId, SelectedCategory, paginatedRequestData);
        }

        protected override List<UserInterestPageDataModel> GetItemsFromResponseModelInterface(IUserInterestPagesResponseModel responseModelInterface)
        {
            return responseModelInterface.Interests == null ? null : new List<UserInterestPageDataModel>(responseModelInterface.Interests);
        }
    }
}