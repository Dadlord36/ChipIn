﻿using System;
using Controllers;
using Repositories.Local;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Views.Cards.Settings;
using Views.ViewElements.ViewsPlacers;

namespace ViewModels
{
    [Binding]
    public sealed class SettingsViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private SessionStateRepository sessionStateRepository;
        [SerializeField] private TwoSlotsViewsPlacer viewsPlacer;
        
        
        
        private void Start()
        {
            viewsPlacer.Initialize();    
            ShowMyProfile();
        }
        
        [Binding]
        public void LogOut_OnClick()
        {
            LogOut();
        }

        [Binding]
        public void ShowMyProfile()
        {
            SwitchToView(nameof(UserProfileView));
        }

        [Binding]
        public void ShowMyWallet()
        {
            SwitchToView(nameof(TokenBalanceView));
        }

        [Binding]
        public void ShowMyInterest()
        {
        }

        private void LogOut()
        {
            sessionStateRepository.SignOut();
            Destroy(gameObject);
        }
    }
}