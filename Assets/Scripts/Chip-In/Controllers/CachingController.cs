using Repositories.Remote;
using UnityEngine;

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
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private UserCoinsAmountRepository coinsAmountRepository;

        public void ClearCache()
        {
            ClearVaultCash(authorisationDataRepository);
            ClearVaultCash(coinsAmountRepository);
        }

        private static void ClearVaultCash(IClearable vault)
        {
            vault.Clear();
        }
    }
}