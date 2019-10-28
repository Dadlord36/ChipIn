using System;
using System.Net.Mail;
using UnityEngine;

namespace DataModels
{
    public interface IUserRegistrationModel : IUserSimpleRegistrationModel
    {
        string Gender { get; set; }
        string Role { get; set; }
    }

    public interface IUserSimpleRegistrationModel
    {
        MailAddress Email { get; set; }
        string Password { get; set; }
    }

    [Serializable]
    public class UserRegistrationModel : UserSimpleRegistrationModel , IUserRegistrationModel
    {
       [SerializeField] private string gender;
       [SerializeField] private string role;

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