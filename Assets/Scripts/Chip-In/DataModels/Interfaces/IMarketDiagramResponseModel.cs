using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IMarketDiagramResponseModel : ISuccess
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Market)]
        MarketDiagramDataModel MarketDiagramData { get; set; }
    }
}