using System;
using System.Threading.Tasks;
using DataModels;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using GlobalVariables;
using HttpRequests;
using Repositories;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects;
using ScriptableObjects.CardsControllers;
using Tasking;
using UnityEngine;
using Utilities;
using ViewModels.SwitchingControllers;
using Views;

namespace Controllers
{
    public interface ISessionController
    {
        event Action<SessionController.SessionMode> SwitchingToMode;
        bool IsSignedIn { get; set; }
        Task TryToSignIn(IUserLoginRequestModel userLoginRequestModel);
        Task TryRegisterAndLoginAsGuest();
        Task SignOut();
        Task ProcessTokenInvalidationCase();

        void ProcessAppLaunching();
    }

    [CreateAssetMenu(fileName = nameof(SessionController),
        menuName = nameof(Controllers) + "/" + nameof(SessionController), order = 0)]
    public sealed class SessionController : AsyncOperationsScriptableObject, ISessionController
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

        private bool _processingAppRestarting;
        public event Action<SessionMode> SwitchingToMode;

        public bool IsSignedIn { get; set; }
        private bool RestartAppIfUnauthorizedRequestHappens { get; set; } = true;

        private void OnEnable()
        {
            _processingAppRestarting = false;
        }

        public async Task TryToSignIn(IUserLoginRequestModel userLoginRequestModel)
        {
            try
            {
                RestartAppIfUnauthorizedRequestHappens = false;
                var response = await SessionStaticProcessor.TryLogin(out TasksCancellationTokenSource, userLoginRequestModel)
                    .ConfigureAwait(false);
                RestartAppIfUnauthorizedRequestHappens = true;

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
                RestartAppIfUnauthorizedRequestHappens = false;
                var result = await GuestRegistrationStaticProcessor.TryRegisterUserAsGuest(out TasksCancellationTokenSource)
                    .ConfigureAwait(false);
                RestartAppIfUnauthorizedRequestHappens = true;

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
            IsSignedIn = true;
            repositoriesController.SetAuthorisationDataAndInvokeRepositoriesLoading(loginResponseModel);
            SaveRoAndInvokeSwitchingToCorrespondingView(role);
        }


        private void ProceedWithGivenAuthorisationData(IAuthorisationModel authorisationModel, string role)
        {
            IsSignedIn = true;
            repositoriesController.SetGuestAuthorisationDataAndInvokeRepositoriesLoading(authorisationModel);
            SaveRoAndInvokeSwitchingToCorrespondingView(role);
        }

        private void SaveRoAndInvokeSwitchingToCorrespondingView(in string role)
        {
            SaveUserAuthentication(role);
            SwitchToViewCorrespondingToUseRole();
            sessionStateRepository.ConfirmSingingIn();
        }


        public async Task SignOut()
        {
            InvokeAppRestartingProcess();

            //TODO: figure out should app sing-out if it was logged in as guest
            if (sessionStateRepository.UserRole != MainNames.UserRoles.Guest)
            {
                await sessionStateRepository.SignOut().ConfigureAwait(false);
            }

            ClearUserAuthentication();
        }

        private void InvokeAppRestartingProcess()
        {
            IsSignedIn = false;

            ApiHelper.StopAllOngoingRequests();
            viewsSwitchingController.ClearSwitchingHistory(nameof(WelcomeView));
            DestroyView(nameof(CoinsGameView));
            SwitchToWelcomeView();
        }


        public async Task ProcessTokenInvalidationCase()
        {
            if (!RestartAppIfUnauthorizedRequestHappens || _processingAppRestarting) return;
            _processingAppRestarting = true;
            ApiHelper.StopAllOngoingRequests();
            if (IsSignedIn)
            {
                await SignOut().ConfigureAwait(false);
                ClearUserAuthentication();
            }

            InvokeAppRestartingProcess();
            ClearUserAuthentication();
            _processingAppRestarting = false;
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

        private void ClearUserAuthentication()
        {
            authorisationDataRepository.Clear();
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
            viewsSwitchingController.RequestSwitchToView(null, toViewName,false);
        }

        private void OnSwitchingToMode(SessionMode obj)
        {
            TasksFactories.ExecuteOnMainThread(() => { SwitchingToMode?.Invoke(obj); });
        }
    }
}