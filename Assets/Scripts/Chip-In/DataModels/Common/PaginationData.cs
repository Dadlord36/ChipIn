using Newtonsoft.Json;

namespace DataModels.Common
{
    public struct PaginationData
    {
        [JsonProperty("total")] public int Total;
        [JsonProperty("page")] public int Page;
        [JsonProperty("per_page")] public int PerPage;
    }
}