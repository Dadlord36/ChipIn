using System.Net.Http;
using DataModels;
using DataModels.RequestsModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public sealed class RegistrationRequestProcessor : BaseRequestProcessor<RegistrationRequestModel,
        UserProfileDataWebModel, IUserProfileDataWebModel>
    {
        public RegistrationRequestProcessor(RegistrationRequestModel requestBodyModel) : base(
            RequestsSuffixes.SignUp, HttpMethod.Post, null, requestBodyModel)
        {
        }
    }

    public sealed class
        SimpleRegistrationRequestProcessor : BaseRequestProcessor<SimpleRegistrationRequestModel,
            UserProfileDataWebModel, IUserProfileDataWebModel>
    {
        public SimpleRegistrationRequestProcessor(SimpleRegistrationRequestModel requestBodyModel) : base(
            RequestsSuffixes.SignUp, HttpMethod.Post, null, requestBodyModel)
        {
        }
    }
}