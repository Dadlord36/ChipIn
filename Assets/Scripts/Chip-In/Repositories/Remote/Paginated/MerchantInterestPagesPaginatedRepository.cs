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

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(MerchantInterestPagesPaginatedRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                   + nameof(Paginated) + "/" + nameof(MerchantInterestPagesPaginatedRepository), order = 0)]
    public class MerchantInterestPagesPaginatedRepository : PaginatedItemsListRepository<MerchantInterestPageDataModel,
        MerchantInterestPagesResponseDataModel, IMerchantInterestPagesResponseModel>
    {
        [SerializeField] private SelectedMerchantInterestRepository selectedMerchantInterestRepository;
        protected override string Tag => nameof(MerchantInterestPagesPaginatedRepository);
        private Task<int?> SelectedInterestId => selectedMerchantInterestRepository.SelectedInterestId;

        protected override Task<BaseRequestProcessor<object, MerchantInterestPagesResponseDataModel, IMerchantInterestPagesResponseModel>
                .HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            DisposableCancellationTokenSource innerCancellationTokenSource = null;
            var cratedTask = SelectedInterestId.ContinueWith(task => CommunitiesInterestsStaticProcessor
                .GetMerchantInterestPages(out innerCancellationTokenSource, authorisationDataRepository,
                    task.Result.Value, paginatedRequestData), TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap();
            cancellationTokenSource = innerCancellationTokenSource;
            return cratedTask;
        }

        protected override List<MerchantInterestPageDataModel> GetItemsFromResponseModelInterface(IMerchantInterestPagesResponseModel
            responseModelInterface)
        {
            return new List<MerchantInterestPageDataModel>(responseModelInterface.Interests);
        }

        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}