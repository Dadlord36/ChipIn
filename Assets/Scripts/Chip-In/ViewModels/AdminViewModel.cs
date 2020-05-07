using Controllers;
using UnityEngine;
using UnityWeld.Binding;
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

        [Binding]
        public void LogOut_OnClick()
        {
            LogOut();
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
        
        private void LogOut()
        {
            sessionController.SignOut();
        }
    }
}