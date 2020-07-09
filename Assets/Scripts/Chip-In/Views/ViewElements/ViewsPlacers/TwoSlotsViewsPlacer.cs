using Common.Interfaces;
using UnityEngine;
using Utilities;

namespace Views.ViewElements.ViewsPlacers
{
    public class TwoSlotsViewsPlacer : MultiViewsPlacer, IInitialize
    {
        [SerializeField] private bool stretchSlotsToScreenSize;
        [SerializeField] private bool overrideSortingOrderInSlots;
        [SerializeField] private int lowestSortingOrderSortingOrder = 0;

        private ViewSlot _previousSlot, _nextSlot;

        private const string SwitchingFromViewContainerName = "SwitchingFromViewContainer",
                             SwitchingToViewContainerName = "SwitchingToViewContainer";

        public void Initialize()
        {
            InitializeContainers();
        }
        

#if UNITY_EDITOR
        public void Editor_InitializeContainers()
        {
            InitializeContainers();
        }
#endif

        private void InitializeContainers()
        {
            _previousSlot = GameObjectsUtility.FindOrAttach<ViewSlot>(transform, SwitchingFromViewContainerName);
            _nextSlot = GameObjectsUtility.FindOrAttach<ViewSlot>(transform, SwitchingToViewContainerName);


            if (stretchSlotsToScreenSize)
            {
                _previousSlot.ResetTransform();
                _nextSlot.ResetTransform();

                _previousSlot.Stretch();
                _nextSlot.Stretch();
            }

            if (overrideSortingOrderInSlots)
            {
                _previousSlot.CanvasSortingOrder = lowestSortingOrderSortingOrder;
                _nextSlot.CanvasSortingOrder = lowestSortingOrderSortingOrder + 1;
            }
        }

        protected override void ReplaceCurrentViewWithGiven(BaseView viewToSwitchTo)
        {
            PlaceView(viewToSwitchTo);
        }

        private void PlaceView(BaseView view)
        {
            if (_nextSlot.Occupied)
            {
                if (_previousSlot.Occupied)
                    ReleaseSingleSlot(_previousSlot);

                MoveViewFromNextToPreviousSlot();
            }
            
            PlaceViewInMainSlot(view);
        }

        private void PlaceViewInMainSlot(BaseView view)
        {
            _nextSlot.AttachView(view);
            view.ConfirmBeingSwitchedTo();
        }

        private void MoveViewFromNextToPreviousSlot()
        {
            var view = _nextSlot.DetachView();
            _previousSlot.AttachView(view);
            view.ConfirmedBeingSwitchedFrom();
        }
        
        private void ReleaseAllSlots()
        {
            var slots = new[] {_previousSlot, _nextSlot};
            for (int i = 0; i < slots.Length; i++)
            {
                ReleaseSingleSlot(slots[i]);
            }
        }
    }
}