using System.ComponentModel;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using DataModels;
using JetBrains.Annotations;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public class RegistrationViewModel : BaseViewModel, IUserRegistrationModel, INotifyPropertyChanged
    {
        private IUserRegistrationModel _registrationModel = new UserRegistrationModel();
        [Binding]
        public MailAddress Email
        {
            get => _registrationModel.Email;
            set
            {
                _registrationModel.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        [Binding]
        public string Password
        {
            get => _registrationModel.Password;
            set
            {
                _registrationModel.Password = value; 
                OnPropertyChanged(nameof(Password));
            }
        }
        [Binding]
        public string Gender
        {
            get => _registrationModel.Gender;
            set
            {
                _registrationModel.Gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        [Binding]
        public string Role
        {
            get => _registrationModel.Role;
            set
            {
                _registrationModel.Role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}