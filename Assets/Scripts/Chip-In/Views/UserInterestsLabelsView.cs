using System;
using UnityEngine;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace Views
{
    public sealed class UserInterestsLabelsView : BaseView
    {
        [SerializeField] private UserInterestsLabelsGridAdapter labelsGridAdapter;

        public event Action<int?> NewInterestSelected;
        public UserInterestsLabelsView() : base(nameof(UserInterestsLabelsView))
        {
        }

        protected override async void OnBeingSwitchedTo()
        {
            base.OnBeingSwitchedTo();
            try
            {
                await labelsGridAdapter.Initialize();
                SubscribeOnEvents();
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
            UnsubscribeFromEvents();
        }
        
        private void SubscribeOnEvents()
        {
            labelsGridAdapter.NewInterestSelected += OnNewInterestSelected;
        }

        private void UnsubscribeFromEvents()
        {
            labelsGridAdapter.NewInterestSelected -= OnNewInterestSelected;
        }

        private void OnNewInterestSelected(int? interestIndex)
        {
            NewInterestSelected?.Invoke(interestIndex);
        }
    }
}