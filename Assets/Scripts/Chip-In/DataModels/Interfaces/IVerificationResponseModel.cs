using System.Collections.Generic;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IVerificationResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("verification")] IList<VerificationDataModel> Verification { get; set; }
    }
}