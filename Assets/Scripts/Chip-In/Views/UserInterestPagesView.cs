using System;
using Controllers.SlotsSpinningControllers.RecyclerView;
using UnityEngine;
using Utilities;

namespace Views
{
    public sealed class UserInterestPagesView : BaseView
    {
        [SerializeField] private RecyclerView recyclerView;

        public UserInterestPagesView() : base(nameof(UserInterestPagesView))
        {
        }

        protected override async void OnBeingSwitchedTo()
        {
            base.OnBeingSwitchedTo();
            try
            {
                await recyclerView.Initialize();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
        
    }
}