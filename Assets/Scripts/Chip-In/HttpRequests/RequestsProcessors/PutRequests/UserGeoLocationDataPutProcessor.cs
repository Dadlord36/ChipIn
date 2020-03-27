using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.PutRequests
{

    public class UserGeoLocationDataPutProcessor : BaseRequestProcessor<IUserGeoLocation, UserProfileDataWebModel,IUserProfileDataWebModel>
    {
        public UserGeoLocationDataPutProcessor(IRequestHeaders requestHeaders, IUserGeoLocation requestBodyModel) :
            base(ApiCategories.Profile, HttpMethod.Put, requestHeaders, requestBodyModel)
        {
        }
    }
}