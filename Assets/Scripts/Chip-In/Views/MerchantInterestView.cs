using System;
using Common.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace Views
{
    public sealed class MerchantInterestView : BaseView, IIdentifiedSelection
    {
        [SerializeField] private MerchantInterestPagesListAdapter merchantInterestPagesListAdapter;
        public UnityEvent beingSwitchedTo;
        public UnityEvent beingSwitchedFrom;

        public event Action<uint> ItemSelected
        {
            add => merchantInterestPagesListAdapter.ItemSelected += value;
            remove => merchantInterestPagesListAdapter.ItemSelected -= value;
        }

        public MerchantInterestView() : base(nameof(MerchantInterestView))
        {
        }

        protected override async void OnBeingSwitchedTo()
        {
            base.OnBeingSwitchedTo();
            OnSwitchTo();
            try
            {
                await merchantInterestPagesListAdapter.Initialize();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        protected override void OnBeingSwitchedSwitchedFrom()
        {
            base.OnBeingSwitchedSwitchedFrom();
            OnSwitchedFrom();
        }

        private void OnSwitchTo()
        {
            beingSwitchedTo.Invoke();
        }

        private void OnSwitchedFrom()
        {
            beingSwitchedFrom.Invoke();
        }
    }
}