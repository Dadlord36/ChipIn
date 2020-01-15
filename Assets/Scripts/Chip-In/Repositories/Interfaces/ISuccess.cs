using Newtonsoft.Json;

namespace Repositories.Interfaces
{
    public interface ISuccess
    {
        [JsonProperty("success")]  bool Success { get; set; }
    }
}