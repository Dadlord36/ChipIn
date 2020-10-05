using System;
using System.Threading.Tasks;
using Controllers;
using GlobalVariables;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects;
using UnityEngine;
using Utilities;

namespace Repositories.Local
{
    public interface ILoginState
    {
        bool IsLoggedIn { get; }
    }

    public interface ISessionStateRepository : ILoginState
    {
        event Action SigningOut;
        event Action SignedIn;
        string UserRole { get; }
        void SetLoginState(in string loginAsRole);
        Task SignOut();
        void ConfirmSingingIn();
    }

    [CreateAssetMenu(fileName = nameof(SessionStateRepository),
        menuName = nameof(Repositories) + "/" + nameof(Local) + "/" + nameof(SessionStateRepository), order = 0)]
    public sealed class SessionStateRepository : AsyncOperationsScriptableObject, ISessionStateRepository
    {
        private const string Tag = nameof(SessionStateRepository);
        
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
            try
            {
                await SessionStaticProcessor.TryLogOut(out TasksCancellationTokenSource, authorisationDataRepository, DeviceUtility.BaseDeviceData);

                cachingController.ClearCache();
                OnSigningOut();
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
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