using Newtonsoft.Json;

namespace DataModels.Common
{
    public class PaginationData
    {
        [JsonProperty("total")] public int Total;
        [JsonProperty("page")] public int Page;
        [JsonProperty("per_page")] public int PerPage;
    }

    public interface IPaginated
    {
        PaginationData Pagination { get; set; }
    }
}