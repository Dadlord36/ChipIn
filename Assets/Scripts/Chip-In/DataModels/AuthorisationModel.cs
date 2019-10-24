using System;

namespace DataModels
{
    [Serializable]
    public class AuthorisationModel
    {
        public string accessToken;
        public string client;
        public string tokenType;
        public string uid;
    }
}
