using DataModels.Interfaces;

namespace DataModels.Common
{
    public class UserInterestAttribute : IUserId
    {
        public int UserId { get; set; }
    }
}