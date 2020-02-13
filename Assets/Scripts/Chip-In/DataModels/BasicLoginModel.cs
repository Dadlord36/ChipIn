using DataModels.Interfaces;
using JetBrains.Annotations;

namespace DataModels
{
    public class BasicLoginModel : IBasicLoginModel
    {
        [CanBeNull] public string Email { get; set; }
        public string Password { get; set; }
    }
}