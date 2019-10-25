using DataModels;
using DefaultNamespace;
using HttpRequests;
using ScriptableObjects.Validations;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public class LoginViewModel : BaseViewModel, IInitialize
    {
        private UserLoginModel _userLoginModel;
        
        [Binding] public bool CanLogin { get; private set; }
        [Binding] public bool CanReceiveInput { get;} = true;
        
        [SerializeField] private LoginModelValidation loginModelValidation;

        public void Initialize()
        {
            _userLoginModel = new UserLoginModel();
            _userLoginModel.PropertyChanged += delegate
            {
                CanLogin = loginModelValidation.CheckIsValid(_userLoginModel);
            };
        }

        public void LoginToAccount()
        {
            ProcessLogin();
        }

        private async void ProcessLogin()
        {
            var profileModel = await new LoginRequestProcessor().SendRequest(_userLoginModel);

            Debug.Log($"Is response successful? : {profileModel.success.ToString()}");

            Debug.Log(profileModel.user.email);
            Debug.Log(profileModel.user.name);
            Debug.Log(profileModel.user.gender);
        }
    }
}