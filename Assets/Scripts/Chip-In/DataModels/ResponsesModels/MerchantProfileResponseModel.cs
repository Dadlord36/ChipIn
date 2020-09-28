
using Newtonsoft.Json;
using Repositories.Interfaces;
using Repositories.Remote;

namespace DataModels.ResponsesModels
{
    public interface IMerchantProfileResponseModel :  ISuccess
    {
        [JsonProperty("user")] MerchantProfileSettingsDataModel User { get; set; }
    }
    
    public sealed class MerchantProfileResponseModel : IMerchantProfileResponseModel
    {
        public bool Success { get; set; }
        public MerchantProfileSettingsDataModel User { get; set; }
    }
}