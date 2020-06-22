using System;
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
using Debug = System.Diagnostics.Debug;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(UserInterestPagesPaginatedRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                   + nameof(Paginated) + "/" + nameof(UserInterestPagesPaginatedRepository), order = 0)]
    public class UserInterestPagesPaginatedRepository : PaginatedItemsListRepository<InterestPagePageDataModel, InterestsPagesPagesResponseDataModel,
        IInterestsPagesResponseModel>
    {
        [SerializeField] private SelectedUserInterestRepository selectedUserInterestRepository;
        protected override string Tag => nameof(UserInterestPagesPaginatedRepository);
        public int? SelectedCommunityId => selectedUserInterestRepository.SelectedInterestId;
        

        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
        
        protected override Task<BaseRequestProcessor<object, InterestsPagesPagesResponseDataModel, IInterestsPagesResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            Debug.Assert(SelectedCommunityId != null, nameof(SelectedCommunityId) + " != null");
            return CommunitiesInterestsStaticProcessor.GetCommunityClientsInterests(out cancellationTokenSource, authorisationDataRepository, 
                (int) SelectedCommunityId, paginatedRequestData);
        }

        protected override List<InterestPagePageDataModel> GetItemsFromResponseModelInterface(IInterestsPagesResponseModel pagesResponseModelInterface)
        {
            return new List<InterestPagePageDataModel>(pagesResponseModelInterface.Interests);
        }
    }
}