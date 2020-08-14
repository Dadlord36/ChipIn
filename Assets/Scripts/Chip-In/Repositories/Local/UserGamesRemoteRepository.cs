using System;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;

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
            try
            {
                var result = await UserGamesStaticProcessor.GetUserGames(out TasksCancellationTokenSource, userAuthorisationDataRepository);
                ItemsLiveData.AddRange(result.ResponseModelInterface.Games);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
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

        public Task<BaseRequestProcessor<object, OfferDetailsResponseModel, IOfferDetailsResponseModel>.HttpResponse> GetOfferDataForGivenGameId(
            int selectedGameId)
        {
            var offerId = GetCorrespondingToTheGameIdOfferId(selectedGameId);
            return OffersStaticRequestProcessor.GetOfferDetails(out TasksCancellationTokenSource, userAuthorisationDataRepository, offerId);
        }
    }
}