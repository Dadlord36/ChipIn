using System;
using System.Threading.Tasks;
using DataModels;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(OffersRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(OffersRemoteRepository), order = 0)]
    public class OffersRemoteRepository : BaseNotPaginatedListRepository<ChallengingOfferWithIdentifierModel>
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        public override async Task LoadDataFromServer()
        {
            ItemsLiveData = await OffersStaticRequestProcessor.TryGetListOfOffers(authorisationDataRepository);
            ConfirmDataLoading();
        }

        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}