using DataModels.Interfaces;

namespace DataModels.RequestsModels
{
    public interface IUserLoginRequestModel : IBasicLoginModel, IGender, IRole, IDevice
    {
    }

    /// <summary>
    /// Set of data needed to login to a user account 
    /// </summary>
    public class UserLoginRequestModel : IUserLoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public DeviceData Device { get; set; }
    }
}