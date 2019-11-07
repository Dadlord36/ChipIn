using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public class CheckEmailViewModel : BaseViewModel
    {
        [Binding]
        public void SwitchToLoginView()
        {
            viewsSwitchingBinding.SwitchView<LoginViewModel>(View);
        }
    }
}