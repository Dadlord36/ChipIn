using System.Collections.Specialized;
using System.Net.Http;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class SponsoredAdvertsGetRequestProcessor : RequestWithoutBodyProcessor<SponsoredAdvertsResponseDataModel, ISponsoredAdvertsResponseModel>
    {
        public SponsoredAdvertsGetRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, string requestSuffix,
            HttpMethod requestMethod, IRequestHeaders requestHeaders, PaginatedRequestData paginatedRequestData) : base(out cancellationTokenSource,
            requestSuffix, requestMethod, requestHeaders, paginatedRequestData)
        {
        }
    }

    public class SponsoredPostersGetProcessor : RequestWithoutBodyProcessor<SponsorsPostersResponseDataModel, ISponsorsPostersResponseModel>
    {
        public SponsoredPostersGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            PaginatedRequestData paginatedRequestData, bool sad, bool reserved) : base(out cancellationTokenSource, ApiCategories.Posters,
            HttpMethod.Get, requestHeaders, paginatedRequestData, FormRequestProperties(sad, reserved))
        {
        }

        private static NameValueCollection FormRequestProperties(bool sad, bool reserved)
        {
            return new NameValueCollection
            {
                {"sad", Utilities.PropertiesUtility.BoolToString(sad)},
                {"reserved", Utilities.PropertiesUtility.BoolToString(reserved)}
            };
        }
    }
}