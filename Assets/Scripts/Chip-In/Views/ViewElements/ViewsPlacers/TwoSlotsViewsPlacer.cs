using System;
using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Views.ViewElements.ViewsPlacers
{
    public class TwoSlotsViewsPlacer : MultiViewsPlacer
    {
        [Serializable]
        public class ViewsSwitchingEvent : UnityEvent<ViewsSwitchData.AppearingSide>
        {
        }

        public ViewsSwitchingEvent viewsBeingReplaced;
        [SerializeField] private bool stretchSlotsToScreenSize;
        [SerializeField] private bool overrideSortingOrderInSlots;
        [SerializeField] private int lowestSortingOrderSortingOrder = 0;

        private ViewSlot _previousSlot, _nextSlot;

        private const string SwitchingFromViewContainerName = "SwitchingFromViewContainer",
            SwitchingToViewContainerName = "SwitchingToViewContainer";

        private void Awake()
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

        protected override void ReplaceCurrentViewWithGiven(ViewsSwitchData viewsSwitchData)
        {
            PlaceView(viewsSwitchData.ViewToSwitchTo);
            viewsBeingReplaced?.Invoke(viewsSwitchData.ScrollSide);
        }

        private void PlaceView(BaseView view)
        {
            if (_nextSlot.Occupied)
            {
                if (_previousSlot.Occupied)
                    ReleaseSingleSlot(_previousSlot);

                _previousSlot.AttachView(_nextSlot.DetachView());
            }

            _nextSlot.AttachView(view);
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