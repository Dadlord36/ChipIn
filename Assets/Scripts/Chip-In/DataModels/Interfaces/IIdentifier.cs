using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IIdentifier
    {
        [JsonProperty("id")] int Id { get; set; }
    }
}