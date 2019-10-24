using System;

namespace DataModels
{
    [Serializable]
    public class LoginResponseModel
    {
        public bool success;
        public UserProfileModel user;
        public AuthorisationModel auth;
    }
}