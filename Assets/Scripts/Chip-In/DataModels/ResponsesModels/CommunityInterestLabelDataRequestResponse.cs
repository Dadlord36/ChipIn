using System;
using DataModels.Common;
using DataModels.Interfaces;
using Newtonsoft.Json;


namespace DataModels.ResponsesModels
{
    public sealed class CommunityInterestLabelDataRequestResponse : ICommunityInterestLabelDataRequestResponse
    {
        [JsonObject(MemberSerialization.OptIn)]
        public class CommunityInterestLabelData
        {
            [JsonProperty("id")] public int Id;
            [JsonProperty("name")] public string Name;
            [JsonProperty("poster")] public string PosterUri;

            public bool IsValid => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(PosterUri);
        }


        public bool Success { get; set; }
        public PaginationData Pagination { get; set; }
        public CommunityInterestLabelData[] Communities { get; set; }

        public CommunityInterestLabelDataRequestResponse(bool success, PaginationData paginationData,
            CommunityInterestLabelData[] communities)
        {
            Success = success;
            Pagination = paginationData;
            Communities = communities;
        }
    }
}