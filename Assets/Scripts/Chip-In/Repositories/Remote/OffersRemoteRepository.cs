using UnityEngine;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(ScriptableOffersRemoteRepository), menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" 
                                                                                     + nameof(ScriptableOffersRemoteRepository), order = 0)]
    public class ScriptableOffersRemoteRepository : ScriptableObject
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
    }
}