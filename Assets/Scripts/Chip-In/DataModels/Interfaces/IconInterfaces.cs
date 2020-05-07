using Newtonsoft.Json;
using UnityEngine;

namespace DataModels.Interfaces
{
    public interface IIconUrl
    {
        [JsonProperty("icon")] string Icon { get; set; }
    }

    public interface IIconSprite
    {
        Sprite Icon { get; set; }
    }
}