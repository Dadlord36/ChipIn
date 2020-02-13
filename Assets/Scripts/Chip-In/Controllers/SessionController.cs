using System;
using System.Threading.Tasks;
using DataModels.RequestsModels;
using GlobalVariables;
using Repositories;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;
using ViewModels.SwitchingControllers;
using Views;

namespace Controllers
{
    [CreateAssetMenu(fileName = nameof(SessionController), menuName = nameof(Controllers) + "/", order = 0)]
    public class SessionController : ScriptableObject
    {
        [SerializeField] private RemoteRepositoriesController repositoriesController;
        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;

        public async Task<bool> TryToSignIn(IUserLoginRequestModel userLoginRequestModel)
        {
            try
            {
                var response = await SessionStaticProcessor.Login(userLoginRequestModel);


                if (response.ResponseModelInterface != null && response.ResponseModelInterface.Success)
                {
                    repositoriesController.SetAuthorisationDataAndInvokeRepositoriesLoading(response
                        .ResponseModelInterface);
                    if (response.ResponseModelInterface.UserProfileData.Role == MainNames.UserRoles.Client)
                    {
                        SwitchToMiniGame();
                    }
                    
                    return true;
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }

            return false;
        }

        private void SwitchToMiniGame()
        {
            SwitchToView(nameof(CoinsGameView));
        }

        private void SwitchToBusinessMainMenu()
        {
            // SwitchToView();
        }

        private void SwitchToView(string toViewName)
        {
            viewsSwitchingController.RequestSwitchToView(null, toViewName);
        }
    }
}