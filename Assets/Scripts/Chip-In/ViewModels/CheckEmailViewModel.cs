using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class CheckEmailViewModel : ViewsSwitchingViewModel
    {
        [Binding]
        public void SwitchToLoginView()
        {
            SwitchToView(nameof(LoginView));
        }
    }
}