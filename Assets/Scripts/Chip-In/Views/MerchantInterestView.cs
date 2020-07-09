using System;
using Common.Interfaces;
using UnityEngine;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace Views
{
    public sealed class MerchantInterestView : BaseView, IIdentifiedSelection
    {
        [SerializeField] private MerchantInterestPagesListAdapter merchantInterestPagesListAdapter;


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
    }
}