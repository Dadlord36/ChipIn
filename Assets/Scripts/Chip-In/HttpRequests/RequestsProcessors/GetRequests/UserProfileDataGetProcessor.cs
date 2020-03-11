using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserProfileDataGetProcessor : BaseRequestProcessor<object, UserProfileResponseModel , IUserProfileDataWebModel>
    {
        public UserProfileDataGetProcessor(IRequestHeaders requestHeaders) : base(
            ApiCategories.Profile, HttpMethod.Get, requestHeaders, null)
        {
        }
    }
}