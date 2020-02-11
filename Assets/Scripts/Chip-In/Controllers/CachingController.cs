using Repositories.Remote;
using UnityEngine;
using ViewModels.SwitchingControllers;
using Views;

namespace Controllers
{
    public interface IClearable
    {
        void Clear();
    }

    [CreateAssetMenu(fileName = nameof(CachingController),
        menuName = nameof(Controllers) + "/" + nameof(CachingController), order = 0)]
    public class CachingController : ScriptableObject
    {
        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;
        [SerializeField] private UserProfileRemoteRepository userProfileRemoteRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        public void ClearCache()
        {
            ClearVaultCash(userProfileRemoteRepository);
            ClearVaultCash(authorisationDataRepository);
        }

        private static void ClearVaultCash(IClearable vault)
        {
            vault.Clear();
        }

        public void LogOut()
        {
            ClearCache();
            viewsSwitchingController.RequestSwitchToView(nameof(LoginView));
        }
    }
}