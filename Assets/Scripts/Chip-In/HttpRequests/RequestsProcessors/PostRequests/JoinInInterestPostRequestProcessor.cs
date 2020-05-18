using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public class JoinInInterestPostRequestProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public JoinInInterestPostRequestProcessor(IRequestHeaders requestHeaders, int interestId) 
            : base(ApiCategories.Subcategories.Interests, HttpMethod.Post, requestHeaders, 
                new []{interestId.ToString(), MainNames.CommonActions.Join})
        {
        }
    }
}