using System;

namespace DataModels
{
    [Serializable]
    public class TestApiUserRegistrationModel
    {
        public string name;
        public string job;

        public override string ToString()
        {
            return $"Name: {name}; Job: {job}";
        }
    }
}