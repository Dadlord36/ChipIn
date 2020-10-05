using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.Interfaces;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using RequestsStaticProcessors;

namespace Repositories.Remote
{
    public class UserProductsRepository : PaginatedItemsListRepository<ProductDataModel, UserProductsResponseDataModel, IUserProductsResponseModel>
    {
        public int CurrentlySelectedIndex { get; set; }
        public Task<ProductDataModel> GetCurrentlySelectedProductAsync => GetItemWithIndexAsync((uint) CurrentlySelectedIndex);

        public UserProductsRepository() : base(nameof(UserProductsRepository))
        {
        }

        protected override Task<BaseRequestProcessor<object, UserProductsResponseDataModel, IUserProductsResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return UserProductsStaticRequestsProcessor.GetUserProducts(out cancellationTokenSource, AuthorisationDataRepositoryHeaders,
                paginatedRequestData);
        }

        protected override List<ProductDataModel> GetItemsFromResponseModelInterface(IUserProductsResponseModel responseModelInterface)
        {
            return new List<ProductDataModel>(responseModelInterface.ProductsData);
        }
    }
}