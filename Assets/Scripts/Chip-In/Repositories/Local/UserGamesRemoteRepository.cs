using System;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using DataModels.Interfaces;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(UserGamesRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(UserGamesRemoteRepository), order = 0)]
    public class UserGamesRemoteRepository : BaseNotPaginatedListRepository<GameDataModel>
    {
        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;

        public override async Task LoadDataFromServer()
        {
            ItemsLiveData.Clear();
            ItemsLiveData.AddRange(await UserGamesStaticProcessor.GetUserGames(userAuthorisationDataRepository));
        }

        public GameDataModel GetGameDataByGameId(int gameId)
        {
            return ItemsData.First(gameData => gameData.Id == gameId);
        }

        public int? GetCorrespondingToTheGameIdOfferId(int gameId) => GetGameDataByGameId(gameId).GameableData.Id;

        public bool UserHasSubscribedToGivenOffer(int offerId)
        {
            return ItemsData.Any(gameData => gameData.GameableData.Id == offerId);
        }

        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }

        public Task<IOfferDetailsResponseModel> GetOfferDataForGivenGameId(int selectedGameId)
        {
            var offerId = GetCorrespondingToTheGameIdOfferId(selectedGameId);
            return OffersStaticRequestProcessor.GetOfferDetails(userAuthorisationDataRepository, offerId);
        }
    }
}