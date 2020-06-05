using System.Net.Http;
using Common;
using DataModels;
using DataModels.RequestsModels;
using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public interface IRegistrationResponseDataModel : ISuccess
    {
        [JsonProperty("user")] UserProfileDataWebModel UserData { get; set; }
        [JsonProperty("auth")] AuthorisationModel AuthorisationData { get; set; }
    }

    public sealed class RegistrationResponseDataModel : IRegistrationResponseDataModel
    {
        public bool Success { get; set; }
        public UserProfileDataWebModel UserData { get; set; }
        public AuthorisationModel AuthorisationData { get; set; }
    }

    public sealed class RegistrationRequestProcessor : BaseRequestProcessor<RegistrationRequestModel,
        RegistrationResponseDataModel, IRegistrationResponseDataModel>
    {
        public RegistrationRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, RegistrationRequestModel requestBodyModel)
            : base(out cancellationTokenSource, ApiCategories.SignUp, HttpMethod.Post, null, requestBodyModel)
        {
        }
    }

    public sealed class
        SimpleRegistrationRequestProcessor : BaseRequestProcessor<SimpleRegistrationRequestModel,
            UserProfileDataWebModel, IUserProfileDataWebModel>
    {
        public SimpleRegistrationRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource,
            SimpleRegistrationRequestModel requestBodyModel) : base(out cancellationTokenSource, ApiCategories.SignUp, HttpMethod.Post,
            null, requestBodyModel)
        {
        }
    }
}