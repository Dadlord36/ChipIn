using System;
using DataModels.Interfaces;
using Newtonsoft.Json;


namespace DataModels.ResponsesModels
{
    
    public sealed class
        CommunityInterestLabelDataRequestResponse : ICommunityInterestLabelDataRequestResponse
    {
        [JsonObject(MemberSerialization.OptIn)]
        public struct CommunityInterestLabelData
        {
            [JsonProperty("id")] public int Id;
            [JsonProperty("name")] public string Name;
            [JsonProperty("poster")] public string PosterUrl;

            public bool IsValid => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(PosterUrl);
        }
        
        public struct PaginationData
        {
            [JsonProperty("total")] public int Total;
            [JsonProperty("page")] public int Page;
            [JsonProperty("per_page")] public int PerPage;
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