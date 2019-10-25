using System;
using System.Net.Mail;

namespace DataModels
{
    public interface IUserRegistrationModel
    {
        MailAddress Email { get; set; }
        string Password { get; set; }
        string Gender { get; set; }
        string Role { get; set; }
    }

    [Serializable]
    public class UserRegistrationModel : IUserRegistrationModel
    {
        private MailAddress email;
        private string password;
        private string gender;
        private string role;

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

        public string Gender
        {
            get => gender;
            set => gender = value;
        }

        public string Role
        {
            get => role;
            set => role = value;
        }
    }
}