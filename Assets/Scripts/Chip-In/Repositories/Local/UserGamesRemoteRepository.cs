using System.Threading.Tasks;
using DataModels;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(UserGamesRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) +"/"  + nameof(UserGamesRemoteRepository), order = 0)]
    public class UserGamesRemoteRepository : BaseItemsListRepository<GameModelModel>
    {
        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
        public override async Task LoadDataFromServer()
        {
            ItemsLiveData.Clear();
            ItemsLiveData.AddRange(await UserGamesStaticProcessor.GetUserGames(userAuthorisationDataRepository));
        }
        
        public GameModelModel  this[int index] => ItemsData[index];

        public override Task SaveDataToServer()
        {
            throw new System.NotImplementedException();
        }
    }
}