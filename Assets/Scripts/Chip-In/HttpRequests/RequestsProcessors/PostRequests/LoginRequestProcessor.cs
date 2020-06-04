using System.Net.Http;
using System.Threading;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public class LoginRequestProcessor : BaseRequestProcessor<IUserLoginRequestModel, LoginResponseModel, ILoginResponseModel>
    {
        public LoginRequestProcessor(out CancellationTokenSource cancellationTokenSource, IUserLoginRequestModel requestBodyModel) :
            base(out cancellationTokenSource, ApiCategories.SignIn, HttpMethod.Post, null, requestBodyModel)
        {
        }
    }
}