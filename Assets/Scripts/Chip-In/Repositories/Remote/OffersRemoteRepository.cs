using System;
using System.Threading.Tasks;
using DataModels;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Remote
{
    public class OffersModelRemoteRepository : BaseItemsListRepository<ChallengingOfferWithIdentifierModel>
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        public override async Task LoadDataFromServer()
        {
            ItemsLiveData = await OffersStaticRequestProcessor.GetListOfOffers(authorisationDataRepository);
            ConfirmDataLoading();
        }

        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}