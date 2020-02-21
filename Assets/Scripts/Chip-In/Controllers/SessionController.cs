using System;
using System.Threading.Tasks;
using DataModels.RequestsModels;
using GlobalVariables;
using Repositories;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;
using ViewModels;
using ViewModels.SwitchingControllers;
using Views;

namespace Controllers
{
    [CreateAssetMenu(fileName = nameof(SessionController),
        menuName = nameof(Controllers) + "/" + nameof(SessionController), order = 0)]
    public class SessionController : ScriptableObject
    {
        private const string Tag = nameof(SessionController);

        [SerializeField] private RemoteRepositoriesController repositoriesController;
        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;

        public async Task TryToSignIn(IUserLoginRequestModel userLoginRequestModel)
        {
            try
            {
                var response = await SessionStaticProcessor.TryLogin(userLoginRequestModel);


                if (response.ResponseModelInterface != null && response.ResponseModelInterface.Success)
                {
                    repositoriesController.SetAuthorisationDataAndInvokeRepositoriesLoading(response
                        .ResponseModelInterface);
                    var role = response.ResponseModelInterface.UserProfileData.Role;

                    switch (role)
                    {
                        case MainNames.UserRoles.Client:
                        case MainNames.UserRoles.Guest:
                            SwitchToMiniGame();
                            break;
                        case MainNames.UserRoles.BusinessOwner:
                            SwitchToBusinessMainMenu();
                            break;
                    }
                }
            }
            catch (Exception)
            {
                LogUtility.PrintLog(nameof(LoginViewModel), "Was not able to sign in");
            }
        }

        private void SwitchToMiniGame()
        {
            SwitchToView(nameof(CoinsGameView));
        }

        private void SwitchToBusinessMainMenu()
        {
            SwitchToView(nameof(MainBusinessMenuView));
        }

        private void SwitchToView(string toViewName)
        {
            viewsSwitchingController.RequestSwitchToView(null, toViewName);
        }
    }
}