using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels.RequestsModels;
using JetBrains.Annotations;
using Repositories;
using RequestsStaticProcessors;
using ScriptableObjects.ActionsConnectors;
using ScriptableObjects.Validations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class LoginViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private UserProfileRemoteRepository remoteRepository;
        
        [SerializeField] private LoginModelValidation loginModelValidation;
        [SerializeField] private ActionConnector loginActionConnector;
        private readonly UserLoginRequestModel _userLoginRequestModel = new UserLoginRequestModel();

        [Binding]
        public string UserEmail
        {
            get => _userLoginRequestModel.Email;
            set
            {
                _userLoginRequestModel.Email = value;
                OnPropertyChanged();
                ValidateLoginData();
            }
        }

        [Binding]
        public string UserPassword
        {
            get => _userLoginRequestModel.Password;
            set
            {
                _userLoginRequestModel.Password = value;
                OnPropertyChanged();
                ValidateLoginData();
            }
        }

        private bool _canLogin;

        [Binding]
        public bool CanLogin
        {
            get => _canLogin;
            set
            {
                _canLogin = value;
                OnPropertyChanged();
            }
        }

        private bool _pendingLogin;

        [Binding]
        public bool IsPendingLogin
        {
            get => _pendingLogin;
            set
            {
                _pendingLogin = value;
                OnPropertyChanged();
            }
        }

        [Binding] public bool CanReceiveInput { get; private set; } = true;

        private void ValidateLoginData()
        {
            CanLogin = loginModelValidation.CheckIsValid(_userLoginRequestModel);
            if (CanLogin)
                Debug.Log("Now can login", this);
        }

        [Binding]
        public void SwitchToRegistrationView()
        {
            SwitchToView(nameof(RegistrationView));
        }

        [Binding]
        public async void LoginButton_Click()
        {
            await ProcessLogin();
        }

        private async Task ProcessLogin()
        {
            IsPendingLogin = true;
            var response = await LoginStaticProcessor.Login(_userLoginRequestModel);

            authorisationDataRepository.Set(response.ResponseModelInterface.AuthorisationData);
            // authorisationDataRepository.Expiry = int.Parse(GetFirstValue(response.Headers, "expiry"));
            IsPendingLogin = false;
            SwitchToMiniGame();
            await remoteRepository.LoadDataFromServer();
            // loginActionConnector.InvokeAction();
        }

        private static string GetFirstValue(HttpHeaders headers, string valueName)
        {
            Assert.IsTrue(headers.Contains(valueName));
            return headers.GetValues(valueName).FirstOrDefault();
        }

        private void SwitchToMiniGame()
        {
            SwitchToView(nameof(CoinsGameView));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}