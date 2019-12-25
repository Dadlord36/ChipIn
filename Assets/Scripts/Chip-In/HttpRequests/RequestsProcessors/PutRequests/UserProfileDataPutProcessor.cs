using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.PutRequests
{
    public class UserProfileDataPutProcessor : BaseRequestProcessor<IUserProfileDataWebModel, UserProfileDataWebModel,IUserProfileDataWebModel>
    {
        public UserProfileDataPutProcessor(IRequestHeaders requestHeaders, IUserProfileDataWebModel requestBodyModel) :
            base(RequestsSuffixes.Profile, HttpMethod.Put,
                requestHeaders, requestBodyModel)
        {
        }
    }
}