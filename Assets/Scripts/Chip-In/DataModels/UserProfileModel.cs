using System;
using System.Numerics;

namespace DataModels
{
    [Serializable]
    public class UserProfileModel
    {
        public int id;
        public string email;
        public string name;
        public string role;
        public int tokensBalance;
        public string gender;
        public Vector2 location;
        public string avatar;
    }
}