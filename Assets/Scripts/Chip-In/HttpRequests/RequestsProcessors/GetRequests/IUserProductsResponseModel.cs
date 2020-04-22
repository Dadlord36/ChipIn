using DataModels;
using DataModels.Common;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public interface IUserProductsResponseModel : ISuccess, IPaginated
    {
        [JsonProperty("user_products")] ProductDataModel[] ProductsData { get; set; }
    }
}