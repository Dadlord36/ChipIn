using Controllers;
using UnityEngine;
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

        protected override void Start()
        {
            base.Start();
            labelsGridAdapter.Initialize();
        }
        
    }
}