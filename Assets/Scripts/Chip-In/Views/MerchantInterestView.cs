using System;
using Common.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace Views
{
    public sealed class MerchantInterestView : BaseView
    {
        [SerializeField] private MerchantInterestPagesListAdapter merchantInterestPagesListAdapter;
        public UnityEvent beingSwitchedTo;
        public UnityEvent beingSwitchedFrom;

        public MerchantInterestView() : base(nameof(MerchantInterestView))
        {
        }

        protected override async void OnBeingSwitchedTo()
        {
            base.OnBeingSwitchedTo();
            OnSwitchTo();
            try
            {
                await merchantInterestPagesListAdapter.Initialize().ConfigureAwait(true);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
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