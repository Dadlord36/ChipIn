using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(ClientOffersRemoteRepository), menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                                                                                 + nameof(ClientOffersRemoteRepository), order = 0)]
    public class ClientOffersRemoteRepository : PaginatedItemsListRepository<ClientOfferDataModel, GetClientOffersResponseDataModel,
        IGetClientOffersResponseModel>
    {
        protected override string Tag { get; } = nameof(ClientOffersRemoteRepository);

        public int InterestId { get; set; }

        protected override Task<BaseRequestProcessor<object, GetClientOffersResponseDataModel, IGetClientOffersResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return OffersStaticRequestProcessor.GetClientsOffers(out cancellationTokenSource, authorisationDataRepository, InterestId,
                paginatedRequestData);
        }

        protected override List<ClientOfferDataModel> GetItemsFromResponseModelInterface(IGetClientOffersResponseModel responseModelInterface)
        {
            return new List<ClientOfferDataModel>(responseModelInterface.Offers);
        }
    }
}