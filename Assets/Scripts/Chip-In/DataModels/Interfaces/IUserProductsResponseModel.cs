using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IUserProductsResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("user_products")] ProductDataModel[] ProductsData { get; set; }
    }
}