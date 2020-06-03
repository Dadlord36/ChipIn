using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ICategory
    {
        [JsonProperty("category")] string Category { get; set; }
    }
}