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

        public override string ToString()
        {
            return
                $"Id: {id.ToString()} Email: {email} Name: {name} Role: {role} TokenBalance: {tokensBalance.ToString()} Gender :{gender} Location: {location.ToString()}";
        }
    }
}