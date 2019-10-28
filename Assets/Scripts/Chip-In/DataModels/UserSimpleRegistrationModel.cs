using System;
using System.Net.Mail;
using UnityEngine;

namespace DataModels
{
    [Serializable]
    public class UserSimpleRegistrationModel
    {
        [SerializeField] private MailAddress email;
        [SerializeField] private string password;

        public MailAddress Email
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