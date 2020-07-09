using DataModels.Common;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IPaginatedResponse
    {
        [JsonProperty("pagination")] PaginatedResponseData Paginated { get; set; }
    }
}