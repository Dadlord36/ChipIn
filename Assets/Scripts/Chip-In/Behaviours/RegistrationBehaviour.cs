using DataModels;
using UnityEngine;
using ViewModels;

namespace Behaviours
{
    public class RegistrationBehaviour : MonoBehaviour
    {
        [SerializeField] private RegistrationViewModel registrationViewModel;

        private void Start()
        {
            registrationViewModel.RegistrationSuccessfullyComplete += delegate(UserProfileModel model) {  };
        }
    }
}