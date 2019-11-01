using ScriptableObjects;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public class WelcomeViewModel : BaseViewModel
    {
        [SerializeField] private ViewsSwitcherBinding switcherBinding;
        
        [Binding]
        public void SwitchToLoginWindow()
        {
            switcherBinding.SwitchView<LoginViewModel>(this);
        }
        
        [Binding]
        public void LoginAsGuest()
        {
//            switcherBinding.SwitchView<>(this);
        }
    }
}