using System;
using DataModels;
using DefaultNamespace;
using HttpRequests;
using ScriptableObjects.Validations;
using UnityEngine;

namespace ViewModels
{
    [CreateAssetMenu(fileName = nameof(LoginViewModel), menuName = "ViewModels/" + nameof(LoginViewModel),
        order = 0)]
    public class LoginViewModel : BaseViewModel, IInitialize
    {
        public event Action<bool> OnCanLoginStateChanged;
        private UserLoginModel UserLoginModel { get; set; }
        
        [NonSerialized] private bool _canLogin;
        public bool CanLogin
        {
            get => _canLogin;
            private set
            {
                _canLogin = value;
                OnCanLoginStateChanged?.Invoke(value);
            }
        }
        public bool CanReceiveInput { get;} = true;
        
        [SerializeField] private LoginModelValidation loginModelValidation;

        public void Initialize()
        {
            UserLoginModel = new UserLoginModel();
            UserLoginModel.PropertyChanged += delegate
            {
                CanLogin = loginModelValidation.CheckIsValid(UserLoginModel);
            };
        }

        // Scriptable Objects are saving their fields values by default, so it might be needed to reset them
        private void ResetValues()
        {
            _canLogin = false;
        }
        
        public void SetUserEmail(string email)
        {
            UserLoginModel.Email = email;
            Debug.Log($"Inputted email: {email}");
        }

        public void SetUserPassword(string password)
        {
            UserLoginModel.Password = password;
            Debug.Log($"Inputted password: {password}");
        }

        public void LoginToAccount()
        {
            ProcessLogin();
        }

        private async void ProcessLogin()
        {
            var profileModel = await new LoginRequestProcessor().SendRequest(UserLoginModel);

            Debug.Log($"Is response successful? : {profileModel.success.ToString()}");

            Debug.Log(profileModel.user.email);
            Debug.Log(profileModel.user.name);
            Debug.Log(profileModel.user.gender);
        }
    }
}