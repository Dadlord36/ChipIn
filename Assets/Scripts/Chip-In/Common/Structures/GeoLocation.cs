using Newtonsoft.Json;

namespace Common.Structures
{
    public class GeoLocation
    {
        [JsonProperty("lon")] public float Longitude;
        [JsonProperty("lat")] public float Latitude;
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}