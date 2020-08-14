using System;
using System.Threading.Tasks;
using DataModels;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(OffersRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(OffersRemoteRepository), order = 0)]
    public class OffersRemoteRepository : BaseNotPaginatedListRepository<ChallengingOfferWithIdentifierModel>
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        public override async Task LoadDataFromServer()
        {
            try
            {
                var result = await OffersStaticRequestProcessor.TryGetListOfOffers(out TasksCancellationTokenSource, authorisationDataRepository);
                ItemsLiveData.AddRange(result.ResponseModelInterface.Offers);
                ConfirmDataLoading();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }
    }
}