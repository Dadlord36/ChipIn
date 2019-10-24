using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace DataModels
{
    /// <summary>
    /// Set of data needed to login to a user account 
    /// </summary>
    [Serializable]
    public class UserLoginModel : INotifyPropertyChanged
    {
        [SerializeField] private string email;
        [SerializeField] private string password;

        public static string NameOfEmailProperty => nameof(email);
        public static string NameOfPasswordProperty => nameof(password);

        public event PropertyChangedEventHandler PropertyChanged;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(password));
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(email));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}