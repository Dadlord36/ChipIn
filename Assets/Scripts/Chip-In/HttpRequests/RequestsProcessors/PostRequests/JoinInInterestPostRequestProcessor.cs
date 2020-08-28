using System.Collections.Specialized;
using System.Net.Http;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public class SupportInterestPostRequestProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public SupportInterestPostRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
            : base(out cancellationTokenSource, ApiCategories.Subcategories.Interests, HttpMethod.Post, requestHeaders,
                new[] {interestId.ToString(), MainNames.CommonActions.Support})
        {
        }
    }

    public class WatchInterestPostRequestProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public WatchInterestPostRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
            : base(out cancellationTokenSource, ApiCategories.Subcategories.Interests, HttpMethod.Post, requestHeaders,
                new[] {interestId.ToString(), MainNames.CommonActions.Watch})
        {
        }
    }

    public class FundInterestPostRequestProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public FundInterestPostRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId,
            int tokensAmount)
            : base(out cancellationTokenSource, ApiCategories.Subcategories.Interests, HttpMethod.Post, requestHeaders,
                new[] {interestId.ToString(), MainNames.CommonActions.Fund},CreateFundParameter(tokensAmount))
        {
        }

        private static NameValueCollection CreateFundParameter(int tokensAmount)
        {
            return new NameValueCollection {{MainNames.CommonActions.Fund, tokensAmount.ToString()}};
        }
    }

    public class JoinInInterestPostRequestProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public JoinInInterestPostRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
            : base(out cancellationTokenSource, ApiCategories.Subcategories.Interests, HttpMethod.Post, requestHeaders,
                new[] {interestId.ToString(), MainNames.CommonActions.Join})
        {
        }
    }
}