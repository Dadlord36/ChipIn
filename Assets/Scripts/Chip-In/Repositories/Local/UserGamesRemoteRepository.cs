using System.Linq;
using System.Threading.Tasks;
using DataModels;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(UserGamesRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(UserGamesRemoteRepository), order = 0)]
    public class UserGamesRemoteRepository : BaseItemsListRepository<GameDataModel>
    {
        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;

        public override async Task LoadDataFromServer()
        {
            ItemsLiveData.Clear();
            ItemsLiveData.AddRange(await UserGamesStaticProcessor.GetUserGames(userAuthorisationDataRepository));
        }

        public GameDataModel this[int index] => ItemsData[index];


        
        public bool UserHasSubscribedToGivenOffer(int offerId)
        {
            return ItemsData.Any(gameData => gameData.GameableData.Id == offerId);
        }

        public override Task SaveDataToServer()
        {
            throw new System.NotImplementedException();
        }
    }
}