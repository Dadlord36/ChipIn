using System;
using System.Threading.Tasks;
using DataModels;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;

namespace Repositories.Remote
{
    public class OffersRemoteRepository : BaseNotPaginatedListRepository<ChallengingOfferWithIdentifierModel>
    {
        public override async Task LoadDataFromServer()
        {
            try
            {
                var result = await OffersStaticRequestProcessor.TryGetListOfOffers(out TasksCancellationTokenSource, AuthorisationDataRepository);
                ItemsLiveData.AddRange(result.ResponseModelInterface.Offers);
                ConfirmDataLoading();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        public OffersRemoteRepository(IUserAuthorisationDataRepository authorisationDataRepositoryInterface) : base(authorisationDataRepositoryInterface)
        {
        }
    }
}