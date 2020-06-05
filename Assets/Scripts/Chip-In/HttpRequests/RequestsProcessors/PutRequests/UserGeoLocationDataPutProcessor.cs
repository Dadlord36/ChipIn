using System.Net.Http;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.PutRequests
{

    public class UserGeoLocationDataPutProcessor : BaseRequestProcessor<IUserGeoLocation, UserProfileDataWebModel,IUserProfileDataWebModel>
    {
        public UserGeoLocationDataPutProcessor(out DisposableCancellationTokenSource cancellationTokenSource ,IRequestHeaders requestHeaders, IUserGeoLocation requestBodyModel) :
            base(out cancellationTokenSource, ApiCategories.Profile, HttpMethod.Put, requestHeaders, requestBodyModel)
        {
        }
    }
}