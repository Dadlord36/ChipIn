using System;
using System.Threading.Tasks;
using Controllers;
using GlobalVariables;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects;
using UnityEngine;
using Utilities;
using ViewModels.SwitchingControllers;
using Views;

namespace Repositories.Local
{
    public interface ILoginState
    {
        bool IsLoggedIn { get; }
    }

    [CreateAssetMenu(fileName = nameof(SessionStateRepository),
        menuName = nameof(Repositories) + "/" + nameof(Local) + "/" + nameof(SessionStateRepository), order = 0)]
    public sealed class SessionStateRepository : AsyncOperationsScriptableObject, ILoginState
    {
        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private CachingController cachingController;
        
        
        public event Action SigningOut;
        public event Action SignedIn;
        
        private bool _isLoggedIn;
        public string UserRole { get; private set; }
        public bool IsLoggedIn => _isLoggedIn;


        public void SetLoginState(in string loginAsRole)
        {
            _isLoggedIn = loginAsRole != MainNames.UserRoles.Guest;
            UserRole = loginAsRole;
        }

        public async Task SignOut()
        {
            await SessionStaticProcessor.TryLogOut(out TasksCancellationTokenSource,authorisationDataRepository,DeviceUtility.BaseDeviceData);
            cachingController.ClearCache();
            viewsSwitchingController.RequestSwitchToView("",nameof(LoginView));
            OnSigningOut();
        }

        public void ConfirmSingingIn()
        {
            OnSignedIn();
        }

        private void OnSigningOut()
        {
            SigningOut?.Invoke();
        }

        private void OnSignedIn()
        {
            SignedIn?.Invoke();
        }
    }
}