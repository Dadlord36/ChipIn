using System;
using DataModels;
using HttpRequests;
using UnityEngine;
using ViewModels;

namespace Behaviours
{
    public class RegistrationBehaviour : MonoBehaviour
    {
        [SerializeField] private RegistrationViewModel registrationViewModel;

        private void Start()
        {
            registrationViewModel.OnTryToRegister += delegate(UserRegistrationModel model)
            {
                TryToRegister(model);
                
            };
        }

        async void TryToRegister(UserRegistrationModel model)
        {
            var response = await new RegistrationRequestProcessor().SendRequest(model);
        }
    }
}