using DataModels;

namespace HttpRequests.RequestsProcessors
{
    public class TestApiRegistrationProcessor : PostRequestProcessor<TestApiUserRegistrationModel, TestApiUserProfileModel>
    {
        public TestApiRegistrationProcessor() : base("api/users")
        {
        }
    }
}