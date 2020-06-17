using System;
using System.Threading.Tasks;
using Controllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.Cards;

namespace ViewModels
{
    [Binding]
    public class AdminViewModel : ViewsSwitchingViewModel, IAdminProperties
    {
        [SerializeField] private SessionController sessionController;
        
        private IAdminProperties AdminViewProperties => View as IAdminProperties;
        

        public uint TokenBallance
        {
            get => AdminViewProperties.TokenBallance;
            set => AdminViewProperties.TokenBallance = value;
        }

        public uint AdSpendField
        {
            get => AdminViewProperties.AdSpendField;
            set => AdminViewProperties.AdSpendField = value;
        }

        public uint SalesFromThisApp
        {
            get => AdminViewProperties.SalesFromThisApp;
            set => AdminViewProperties.SalesFromThisApp = value;
        }

        public uint SalesCommisions
        {
            get => AdminViewProperties.SalesCommisions;
            set => AdminViewProperties.SalesCommisions = value;
        }

        public uint ReturnOnInvestments
        {
            get => AdminViewProperties.ReturnOnInvestments;
            set => AdminViewProperties.ReturnOnInvestments = value;
        }

        public AdminViewModel() : base(nameof(AdminViewModel))
        {
        }
        
        [Binding]
        public async void LogOut_OnClick()
        {
            try
            {
                await LogOut();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        [Binding]
        public void TokenBallance_OnClick()
        {
        }

        [Binding]
        public void Access_OnClick()
        {
        }

        [Binding]
        public void Verification_OnClick()
        {
        }

        [Binding]
        public void Library_OnClick()
        {
        }
        
        private Task LogOut()
        {
           return sessionController.SignOut();
        }
    }
}