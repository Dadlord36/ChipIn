using System.Net.Http;
using System.Threading;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserProfileDataGetProcessor : BaseRequestProcessor<object, UserProfileResponseModel, IUserProfileDataWebModel>
    {
        public UserProfileDataGetProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders) : base(
            out cancellationTokenSource, ApiCategories.Profile, HttpMethod.Get, requestHeaders, null)
        {
        }
    }
}