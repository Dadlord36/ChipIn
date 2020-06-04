using System.Net.Http;
using System.Threading;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.PutRequests
{

    public class UserGeoLocationDataPutProcessor : BaseRequestProcessor<IUserGeoLocation, UserProfileDataWebModel,IUserProfileDataWebModel>
    {
        public UserGeoLocationDataPutProcessor(out CancellationTokenSource cancellationTokenSource ,IRequestHeaders requestHeaders, IUserGeoLocation requestBodyModel) :
            base(out cancellationTokenSource, ApiCategories.Profile, HttpMethod.Put, requestHeaders, requestBodyModel)
        {
        }
    }
}