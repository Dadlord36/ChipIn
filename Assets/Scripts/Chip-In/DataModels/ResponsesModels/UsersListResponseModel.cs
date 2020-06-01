using DataModels.Common;
using DataModels.Interfaces;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.ResponsesModels
{
    public interface IUserListResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("users")] UserProfileBaseData[] UsersData { get; set; }
    }
    
    public class UsersListResponseDataModel : IUserListResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public UserProfileBaseData[] UsersData { get; set; }
    }
}