﻿using System.Net.Http;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class MerchantInterestsPagesPaginatedGetProcessor : RequestWithoutBodyProcessor<MerchantInterestPagesResponseDataModel,
        IMerchantInterestPagesResponseModel>
    {
        public MerchantInterestsPagesPaginatedGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int selectedCommunityId, PaginatedRequestData paginatedRequestData) : base(out cancellationTokenSource, ApiCategories.Communities,
            HttpMethod.Get, requestHeaders, new[] {selectedCommunityId.ToString(), ApiCategories.Subcategories.Interests},
            paginatedRequestData?.ConvertPaginationToNameValueCollection())
        {
        }
    }
}