using System;
using Newtonsoft.Json;

namespace Common.Structures
{
    [Serializable]
    public class GeoLocation
    {
        [JsonProperty("lon")] public float longitude;
        [JsonProperty("lat")] public float latitude;
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}