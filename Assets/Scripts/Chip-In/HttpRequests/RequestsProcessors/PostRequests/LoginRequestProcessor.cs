using System.Net.Http;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public class LoginRequestProcessor : BaseRequestProcessor<IUserLoginRequestModel, LoginResponseModel, ILoginResponseModel>
    {
        public LoginRequestProcessor(IUserLoginRequestModel requestBodyModel) : base(ApiCategories.SignIn,
            HttpMethod.Post, null, requestBodyModel)
        {
        }
    }
}