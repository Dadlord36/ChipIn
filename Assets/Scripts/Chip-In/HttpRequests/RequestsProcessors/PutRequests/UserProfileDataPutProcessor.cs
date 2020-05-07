using System.Net.Http;
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
        public UserProfileDataPutProcessor(IRequestHeaders requestHeaders, IUserProfileDataWebModel requestBodyModel) :
            base(ApiCategories.Profile, HttpMethod.Put, requestHeaders, requestBodyModel)
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
        public UserProfilePasswordChangePutProcessor(IRequestHeaders requestHeaders,
            IUserProfilePasswordChangeModel requestBodyModel) : base(ApiCategories.Profile,
            HttpMethod.Put, requestHeaders, requestBodyModel)
        {
        }
    }
    
    public class UserProfilePasswordChangeDummyPutProcessor : BaseRequestProcessor<IUserName,
        UserProfileResponseModel, IUserProfileResponseModel>
    {
        public UserProfilePasswordChangeDummyPutProcessor(IRequestHeaders requestHeaders,
            IUserName requestBodyModel) : base(ApiCategories.Profile,
            HttpMethod.Put, requestHeaders, requestBodyModel)
        {
        }
    }
}