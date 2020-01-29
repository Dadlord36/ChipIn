using GlobalVariables;
using UnityEngine;

namespace Repositories.Local
{
    public interface ILoginState
    {
        bool IsLoggedIn { get; }
    }

    [CreateAssetMenu(fileName = nameof(LoginStateRepository),
        menuName = nameof(Repositories) + "/" + nameof(Local) + "/" + nameof(LoginStateRepository), order = 0)]
    public class LoginStateRepository : ScriptableObject, ILoginState
    {
        private bool _isLoggedIn;
        private string _userRole;

        public bool IsLoggedIn => _isLoggedIn;

        public void SetLoginState(in string loginAsRole)
        {
            _isLoggedIn = loginAsRole != MainNames.Guest;
            _userRole = loginAsRole;
        }
    }
}