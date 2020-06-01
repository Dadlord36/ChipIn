using DataModels.Interfaces;

namespace DataModels
{
    public class UserProfileBaseData : IUserProfileBaseModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
    }
}