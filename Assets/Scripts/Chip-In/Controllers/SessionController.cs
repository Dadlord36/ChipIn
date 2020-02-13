﻿using System;
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
    [CreateAssetMenu(fileName = nameof(SessionController),
        menuName = nameof(Controllers) + "/" + nameof(SessionController), order = 0)]
    public class SessionController : ScriptableObject
    {
        [SerializeField] private RemoteRepositoriesController repositoriesController;
        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;

        public async Task TryToSignIn(IUserLoginRequestModel userLoginRequestModel)
        {
            try
            {
                var response = await SessionStaticProcessor.Login(userLoginRequestModel);


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
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
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