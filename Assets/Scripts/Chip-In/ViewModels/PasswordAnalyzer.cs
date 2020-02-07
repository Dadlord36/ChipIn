﻿using System;
using ScriptableObjects.Validations;
using UnityEngine;

namespace ViewModels
{
    [Serializable]
    public class PasswordAnalyzer
    {
        [SerializeField] private PasswordValidation passwordValidation;
        
        private string _originalPassword;
        private string _repeatedPassword;
        
        public string OriginalPassword
        {
            get => _originalPassword;
            set => _originalPassword = value;
        }

        public string RepeatedPassword
        {
            get => _repeatedPassword;
            set => _repeatedPassword = value;
        }

        public bool PasswordIsValid()
        {
            return passwordValidation.CheckIsValid(_originalPassword);
        }

        public bool CheckPasswordsAreMatch()
        {
            return OriginalPassword == RepeatedPassword;
        }

        public bool CheckIfPasswordsAreMatchAndItIsValid()
        {
            return CheckPasswordsAreMatch() && PasswordIsValid();
        }
    }
}