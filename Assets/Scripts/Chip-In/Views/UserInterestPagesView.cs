using System;
using UnityEngine;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace Views
{
    public sealed class UserInterestPagesView : BaseView
    {
        [SerializeField] private UserInterestPagesListAdapter userInterestPagesListAdapter;
        
        public UserInterestPagesView() : base(nameof(UserInterestPagesView))
        {
        }

        protected override async void OnBeingSwitchedTo()
        {
            base.OnBeingSwitchedTo();
            try
            {
                await userInterestPagesListAdapter.Initialize();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}