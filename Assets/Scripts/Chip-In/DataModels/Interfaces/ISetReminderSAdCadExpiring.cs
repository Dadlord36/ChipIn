using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ISetReminderSAdCadExpiring
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.SetReminderSAdCAdExpiring)]
        bool SetReminderSAdCAdExpiring { get; set; }
    }
}