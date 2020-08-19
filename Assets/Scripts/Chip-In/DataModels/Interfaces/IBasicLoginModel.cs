using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IEmail
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Email)] string Email { get; set; }
    }

    public interface IPassword
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Password)] string Password { get; set; }
    }

    public interface IBasicLoginModel : IEmail, IPassword
    {
    }
}