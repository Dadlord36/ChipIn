using System;
using System.Threading.Tasks;
using DataModels;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using GlobalVariables;
using Repositories;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using Utilities;
using ViewModels.SwitchingControllers;
using Views;

namespace Controllers
{
    [CreateAssetMenu(fileName = nameof(SessionController),
        menuName = nameof(Controllers) + "/" + nameof(SessionController), order = 0)]
    public sealed class SessionController : AsyncOperationsScriptableObject
    {
        public enum SessionMode
        {
            User,
            Merchant,
            Guest
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
            try
            {
                var response = await SessionStaticProcessor.TryLogin(out TasksCancellationTokenSource, userLoginRequestModel)
                    .ConfigureAwait(true);
                var responseInterface = response.ResponseModelInterface;
                if (responseInterface == null)
                {
                    LogUtility.PrintLog(Tag, "SignIn response model is null");
                    alertCardController.ShowAlertWithText(response.Error);
                    return;
                }

                if (responseInterface.Success)
                {
                    ProceedWithGivenAuthorisationData(responseInterface, responseInterface.UserProfileData.Role);
                }
                else
                {
                    alertCardController.ShowAlertWithText(response.Error);
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public async Task TryRegisterAndLoginAsGuest()
        {
            try
            {
                var result = await GuestRegistrationStaticProcessor.TryRegisterUserAsGuest(out TasksCancellationTokenSource)
                    .ConfigureAwait(true);

                if (!result.Success)
                {
                    LogUtility.PrintLog(Tag, "Failed to register user as Guest");
                    alertCardController.ShowAlertWithText(result.Error);
                }

                var responseInterface = result.ResponseModelInterface;

                ProceedWithGivenAuthorisationData(responseInterface.AuthorisationData, responseInterface.UserData.Role);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void ProceedWithGivenAuthorisationData(ILoginResponseModel loginResponseModel, string role)
        {
            repositoriesController.SetAuthorisationDataAndInvokeRepositoriesLoading(loginResponseModel);
            SaveRoAndInvokeSwitchingToCorrespondingView(role);
        }

        private void ProceedWithGivenAuthorisationData(IAuthorisationModel authorisationModel, string role)
        {
            repositoriesController.SetGuestAuthorisationDataAndInvokeRepositoriesLoading(authorisationModel);
            SaveRoAndInvokeSwitchingToCorrespondingView(role);
        }

        private void SaveRoAndInvokeSwitchingToCorrespondingView(in string role)
        {
            SaveUserAuthentication(role);
            SwitchToViewCorrespondingToUseRole();
            sessionStateRepository.ConfirmSingingIn();
        }
        
        public Task SignOut()
        {
            viewsSwitchingController.ClearSwitchingHistory();
            DestroyView(nameof(CoinsGameView));
            SwitchToLoginView();

            //TODO: figure out should app sing-out if it was logged in as guest
            return sessionStateRepository.UserRole != MainNames.UserRoles.Guest ? sessionStateRepository.SignOut() : Task.CompletedTask;
        }

        private void DestroyView(string coinsGameViewName)
        {
            viewsSwitchingController.RemoveExistingViewInstance(coinsGameViewName);
        }

        private void SwitchToViewCorrespondingToUseRole()
        {
            switch (authorisationDataRepository.UserRole)
            {
                case MainNames.UserRoles.Guest:
                    OnSwitchingToMode(SessionMode.Guest);
                    SwitchToMiniGame();
                    break;
                case MainNames.UserRoles.Client:
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


        private void OnSwitchingToMode(SessionMode obj)
        {
            SwitchingToMode?.Invoke(obj);
        }
    }
}