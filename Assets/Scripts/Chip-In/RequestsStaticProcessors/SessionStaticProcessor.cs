using System.Threading;
using System.Threading.Tasks;
using DataModels.HttpRequestsHeadersModels;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.DeleteRequests;
using HttpRequests.RequestsProcessors.PostRequests;
using Newtonsoft.Json;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class SessionStaticProcessor
    {
        private const string Tag = nameof(SessionStaticProcessor);

        public static Task<BaseRequestProcessor<IUserLoginRequestModel, LoginResponseModel, ILoginResponseModel>.HttpResponse>
            TryLogin(out CancellationTokenSource cancellationTokenSource, IUserLoginRequestModel userLoginRequestModel)
        {
            LogUtility.PrintLog(Tag, $"Login request model: {JsonConvert.SerializeObject(userLoginRequestModel)}");
            return new LoginRequestProcessor(out cancellationTokenSource, userLoginRequestModel).SendRequest("User was LoggedIn");
        }

        public static Task<BaseRequestProcessor<IBaseDeviceData, SignOutResponseModel, ISignOutResponseModel>.HttpResponse> TryLogOut(
            out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, IBaseDeviceData deviceData)
        {
            return new SignOutRequestProcessor(out cancellationTokenSource, requestHeaders, deviceData).SendRequest("User");
        }
    }
}