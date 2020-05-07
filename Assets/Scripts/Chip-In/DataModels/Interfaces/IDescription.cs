using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IDescription
    {
        [JsonProperty("description")] string Description { get; set; }
    }
}