﻿using System.Net.Http;
using System.Threading;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public interface ICommunityItemResponseModel : ISuccess
    {
        [JsonProperty("community")] CommunityDetailsDataModel LabelDetailsDataModel { get; set; }
    }

    public sealed class CommunityDetailsGetProcessor : RequestWithoutBodyProcessor<CommunityItemResponseDataModel,
        ICommunityItemResponseModel>
    {
        public CommunityDetailsGetProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int communityId) : base(out cancellationTokenSource, ApiCategories.Communities, HttpMethod.Get, requestHeaders,
            new[] {communityId.ToString()})
        {
        }
    }
}