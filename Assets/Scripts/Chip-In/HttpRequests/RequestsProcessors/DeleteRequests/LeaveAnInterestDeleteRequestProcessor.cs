using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.DeleteRequests
{
    public class LeaveAnInterestDeleteRequestProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public LeaveAnInterestDeleteRequestProcessor(IRequestHeaders requestHeaders, int interestId) :
            base(ApiCategories.Subcategories.Interests, HttpMethod.Delete, requestHeaders, 
                new []{interestId.ToString(), MainNames.CommonActions.Leave})
        {
        }
    }
}