using DataModels.Common;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IPagination
    {
        [JsonProperty("pagination")] PaginationData Pagination { get; set; }
    }
}