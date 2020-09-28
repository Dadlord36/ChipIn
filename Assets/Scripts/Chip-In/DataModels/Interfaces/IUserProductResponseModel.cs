using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IUserProductResponseModel : ISuccess
    {
        [JsonProperty("user_product")] ProductDataModel ProductsData { get; set; }
    }
}