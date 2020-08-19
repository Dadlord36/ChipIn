using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.Interfaces;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(UserProductsRepository), menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(UserProductsRepository),
        order = 0)]
    public class UserProductsRepository : PaginatedItemsListRepository<ProductDataModel, UserProductsResponseDataModel, IUserProductsResponseModel>
    {
        // [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        protected override string Tag => nameof(UserProductsRepository);
        public int CurrentlySelectedIndex { get; set; }
        public Task<ProductDataModel> GetCurrentlySelectedProductAsync => CreateGetItemWithIndexTask((uint) CurrentlySelectedIndex);


        protected override Task<BaseRequestProcessor<object, UserProductsResponseDataModel, IUserProductsResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return UserProductsStaticRequestsProcessor.GetUserProducts(out cancellationTokenSource, authorisationDataRepository, paginatedRequestData);
        }

        protected override List<ProductDataModel> GetItemsFromResponseModelInterface(IUserProductsResponseModel responseModelInterface)
        {
            return new List<ProductDataModel>(responseModelInterface.ProductsData);
        }
    }
}