using System.Net.Http;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class RadarData
    {
        [JsonProperty("max")] public float Max { get; set; }
        [JsonProperty("points")]public float[,] Points { get; set; }
    }

    public interface IRadarModel : ISuccess
    {
        [JsonProperty("data")] RadarData Data { get; set; }
    }

    public class RadarDataModel : IRadarModel
    {
        public bool Success { get; set; }
        public RadarData Data { get; set; }
    }

    public class MerchantMarketRadarRequestsProcessor : BaseRequestProcessor<object, RadarDataModel, IRadarModel>
    {
        public MerchantMarketRadarRequestsProcessor(IRequestHeaders requestHeaders) :
            base(new BaseRequestProcessorParameters(ApiCategories.Profile, HttpMethod.Get, requestHeaders, null,
                new[] {ApiCategories.Radar}))
        {
        }
    }
}