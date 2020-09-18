using System.Net.Http;
using Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserProfileDataGetProcessor : RequestWithoutBodyProcessor<UserProfileResponseModel, IUserProfileModel>
    {
        public UserProfileDataGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders) : base(
            out cancellationTokenSource, ApiCategories.Profile, HttpMethod.Get, requestHeaders)
        {
        }
    }

    public class MerchantProfileDataGetProcessor : RequestWithoutBodyProcessor<MerchantProfileResponseModel, IMerchantProfileResponseModel>
    {
        public MerchantProfileDataGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource,  IRequestHeaders requestHeaders) 
            : base(out cancellationTokenSource, ApiCategories.Profile, HttpMethod.Get, requestHeaders)
        {
        }
    }
}