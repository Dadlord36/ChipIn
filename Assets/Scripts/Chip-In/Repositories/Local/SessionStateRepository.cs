using System.Threading.Tasks;
using Controllers;
using GlobalVariables;
using Repositories.Remote;
using RequestsStaticProcessors;
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
    public class SessionStateRepository : ScriptableObject, ILoginState
    {
        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private CachingController cachingController;
        
        private bool _isLoggedIn;
        private string _userRole;

        public bool IsLoggedIn => _isLoggedIn;

        public void SetLoginState(in string loginAsRole)
        {
            _isLoggedIn = loginAsRole != MainNames.UserRoles.Guest;
            _userRole = loginAsRole;
        }

        public async Task SignOut()
        {
            await SessionStaticProcessor.TryLogOut(authorisationDataRepository,DeviceUtility.BaseDeviceData);
            cachingController.ClearCache();
            viewsSwitchingController.RequestSwitchToView("",nameof(LoginView));
        }
    }
}