using System;
using System.Net.Mail;

namespace DataModels
{
    [Serializable]
    public class UserRegistrationModel
    {
        public MailAddress email;
        public string password;
        public string gender;
        public string role;
    }
}