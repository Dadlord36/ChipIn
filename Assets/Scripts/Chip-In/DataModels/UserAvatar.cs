using Newtonsoft.Json;

namespace DataModels
{
    public interface IUserAvatarImage
    {
        [JsonProperty("url")]
        string Url { get; set; }

        [JsonProperty("thumb")]
        UserAvatar.Thumb Thumb { get; set; }
    }
    
    public class UserAvatar
    {
        public class Thumb
        {

            [JsonProperty("url")]
            public string Url { get; set; }
        }

        public class Avatar : IUserAvatarImage
        {
            public string Url { get; set; }
            public Thumb Thumb { get; set; }
        }
        
    }
}