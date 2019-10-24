using System;

namespace DataModels
{
    [Serializable]
    public class TestApiUserProfileModel : TestApiUserRegistrationModel
    {
        public int id;

        public override string ToString()
        {
            return $"{base.ToString()}Id: {id.ToString()}";
        }
    }
}