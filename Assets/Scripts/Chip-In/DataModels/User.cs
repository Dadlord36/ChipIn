using DataModels.Interfaces;
using Newtonsoft.Json;

namespace DataModels
{
    public interface IUser
    {
        [JsonProperty("user")] User UserName { get; set; }
    }
    public class User : INamed
    {
        public string Name { get; set; }
    }
}