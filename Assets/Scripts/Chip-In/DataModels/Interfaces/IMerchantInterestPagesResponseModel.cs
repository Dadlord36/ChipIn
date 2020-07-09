﻿using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IMerchantInterestPagesResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("interests")] MerchantInterestPageDataModel[] Interests { get; set; }
    }
}