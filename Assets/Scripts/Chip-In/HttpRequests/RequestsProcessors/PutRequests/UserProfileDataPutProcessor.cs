using System.Net.Http;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;
using Newtonsoft.Json;

namespace HttpRequests.RequestsProcessors.PutRequests
{
    public class UserProfileDataPutProcessor : BaseRequestProcessor<IUserProfileDataWebModel, UserProfileDataWebModel,
        IUserProfileDataWebModel>
    {
        public UserProfileDataPutProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            IUserProfileDataWebModel requestBodyModel) : base(out cancellationTokenSource, ApiCategories.Profile, HttpMethod.Put,
            requestHeaders, requestBodyModel)
        {
        }
    }


    public interface IUserProfilePasswordChangeModel
    {
        [JsonProperty("password")] string Password { get; set; }

        [JsonProperty("password_confirmation")]
        string PasswordConfirmation { get; set; }

        [JsonProperty("current_password")] string CurrentPassword { get; set; }
    }

    public class UserProfilePasswordChangingModel : IUserProfilePasswordChangeModel
    {
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string CurrentPassword { get; set; }
    }

    public interface IUserName
    {
        [JsonProperty("name")] string Name { get; set; }
    }

    public class DummyData : IUserName
    {
        public DummyData(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class UserProfilePasswordChangePutProcessor : BaseRequestProcessor<IUserProfilePasswordChangeModel,
        UserProfileResponseModel, IUserProfileResponseModel>
    {
        public UserProfilePasswordChangePutProcessor(out DisposableCancellationTokenSource cancellationTokenSource,
            IRequestHeaders requestHeaders, IUserProfilePasswordChangeModel requestBodyModel) : base(out cancellationTokenSource,
            ApiCategories.Profile, HttpMethod.Put, requestHeaders, requestBodyModel)
        {
        }
    }

    public class UserProfilePasswordChangeDummyPutProcessor : BaseRequestProcessor<IUserName,
        UserProfileResponseModel, IUserProfileResponseModel>
    {
        public UserProfilePasswordChangeDummyPutProcessor(out DisposableCancellationTokenSource cancellationTokenSource,
            IRequestHeaders requestHeaders, IUserName requestBodyModel) : base(out cancellationTokenSource, ApiCategories.Profile,
            HttpMethod.Put, requestHeaders, requestBodyModel)
        {
        }
    }
}