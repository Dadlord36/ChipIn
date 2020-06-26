using System;
using Controllers;
using UnityEngine;
using Utilities;
using Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Grids;

namespace Views
{
    public sealed class UserInterestsLabelsView : BaseView
    {
        [SerializeField] private UserInterestsLabelsGridAdapter labelsGridAdapter;
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

        public UserInterestsLabelsView() : base(nameof(UserInterestsLabelsView))
        {
        }

        protected override async void Start()
        {
            base.Start();
            try
            {
                await labelsGridAdapter.Initialize();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}