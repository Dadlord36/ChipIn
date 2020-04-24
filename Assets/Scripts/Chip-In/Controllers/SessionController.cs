using System;
using System.Threading.Tasks;
using DataModels.RequestsModels;
using GlobalVariables;
using Repositories;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using Utilities;
using ViewModels.SwitchingControllers;
using Views;

namespace Controllers
{
    [CreateAssetMenu(fileName = nameof(SessionController),
        menuName = nameof(Controllers) + "/" + nameof(SessionController), order = 0)]
    public sealed class SessionController : ScriptableObject
    {
        public enum SessionMode
        {
            User,
            Merchant
        }

        private const string Tag = nameof(SessionController);


        [SerializeField] private RemoteRepositoriesController repositoriesController;
        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private AlertCardController alertCardController;
        [SerializeField] private CachingController cachingController;
        [SerializeField] private SessionStateRepository sessionStateRepository;

        public event Action<SessionMode> SwitchingToMode;

        public async Task TryToSignIn(IUserLoginRequestModel userLoginRequestModel)
        {
            var response = await SessionStaticProcessor.TryLogin(userLoginRequestModel);

            if (response.ResponseModelInterface == null)
            {
                LogUtility.PrintLog(Tag, "SignIn response model is null");
                alertCardController.ShowAlertWithText(response.Error);
                return;
            }

            if (response.ResponseModelInterface.Success)
            {
                repositoriesController.SetAuthorisationDataAndInvokeRepositoriesLoading(response.ResponseModelInterface);

                SaveUserAuthentication(response.ResponseModelInterface.UserProfileData.Role);
                SwitchToViewCorrespondingToUseRole();
            }
            else
            {
                alertCardController.ShowAlertWithText(response.Error);
            }
        }

        public async void SignOut()
        {
            await sessionStateRepository.SignOut();
            SwitchToLoginView();
        }

        private void SwitchToViewCorrespondingToUseRole()
        {
            switch (authorisationDataRepository.UserRole)
            {
                case MainNames.UserRoles.Client:
                case MainNames.UserRoles.Guest:
                    OnSwitchingToMode(SessionMode.User);
                    SwitchToMiniGame();
                    break;
                case MainNames.UserRoles.BusinessOwner:
                    OnSwitchingToMode(SessionMode.Merchant);
                    SwitchToBusinessMainMenu();
                    break;
            }
        }

        private void SaveUserAuthentication(string role)
        {
            authorisationDataRepository.SetUserRole(role);
            //Save authentication data, so that it will be used on next app launch and user won't have to sign in again
            authorisationDataRepository.TrySaveDataLocally();
        }

        public void ProcessAppLaunching()
        {
            if (authorisationDataRepository.CheckIfUserWasLoggedInPreviously())
            {
                authorisationDataRepository.TryLoadLocalData();
                repositoriesController.InvokeRepositoriesLoading();
                SwitchToViewCorrespondingToUseRole();
            }
            else
            {
                cachingController.ClearCache();
                SwitchToWelcomeView();
            }
        }

        private void SwitchToWelcomeView()
        {
            SwitchToView(nameof(WelcomeView));
        }

        private void SwitchToLoginView()
        {
            SwitchToView(nameof(LoginView));
        }

        private void SwitchToMiniGame()
        {
            SwitchToView(nameof(CoinsGameView));
        }

        private void SwitchToBusinessMainMenu()
        {
            SwitchToView(nameof(MarketView));
        }

        private void SwitchToView(string toViewName)
        {
            viewsSwitchingController.RequestSwitchToView(null, toViewName);
        }

        public async Task<bool> TryRegisterAndLoginAsGuest()
        {
            var authorisationModel = await GuestRegistrationStaticProcessor.TryRegisterUserAsGuest();
            var result = authorisationModel;
            if (!result.Success)
            {
                LogUtility.PrintLog(Tag, "Failed to register user as Guest");
                alertCardController.ShowAlertWithText(result.Error);
                return false;
            }

            repositoriesController.SetGuestAuthorisationDataAndInvokeRepositoriesLoading(result.ResponseModelInterface.AuthorisationData);
            SaveUserAuthentication(MainNames.UserRoles.Guest);
            return true;
        }

        private void OnSwitchingToMode(SessionMode obj)
        {
            SwitchingToMode?.Invoke(obj);
        }
    }
}