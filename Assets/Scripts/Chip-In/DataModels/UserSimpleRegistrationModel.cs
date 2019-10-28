using System;
using UnityEngine;

namespace DataModels
{
    [Serializable]
    public class UserSimpleRegistrationModel
    {
        [SerializeField] private string email;
        [SerializeField] private string password;

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }
    }
}