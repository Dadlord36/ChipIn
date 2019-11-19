using ScriptableObjects.SwitchBindings;
using UnityEngine.Events;
using Utilities;

namespace Views.ViewElements.ViewsPlacers
{
    public class TwoSlotsMultiViewsesPlacer : MultiViewsPlacer
    {
        public UnityEvent ViewsBeingReplaced;

        private ViewSlot _previousSlot, _nextSlot;
        private const string PreviousContainerName = "PreviousViewContainer", NextContainerName = "NextViewContainer";

        private void Awake()
        {
            _previousSlot = GameObjectsUtility.FindOrAttach<ViewSlot>(transform, PreviousContainerName);
            _nextSlot = GameObjectsUtility.FindOrAttach<ViewSlot>(transform, NextContainerName);
        }

        protected override void ReplaceCurrentViewWithGiven(MultiViewsSwitchingBinding.DualViewsSwitchData dualViewsSwitchData)
        {
            ReleaseAllSlots();

            PlaceInPreviousContainer(dualViewsSwitchData.fromView);
            PlaceInNextContainer(dualViewsSwitchData.toView);

            dualViewsSwitchData.fromView.Show();
            dualViewsSwitchData.toView.Show();

            ViewsBeingReplaced?.Invoke();
        }

        private void ReleaseAllSlots()
        {
            var slots = new[] {_previousSlot, _nextSlot};
            for (int i = 0; i < slots.Length; i++)
            {
                ReleaseSingleSlot(slots[i]);
            }
        }

        private void PlaceInPreviousContainer(BaseView view)
        {
            _previousSlot.AttachView(view);
        }

        private void PlaceInNextContainer(BaseView view)
        {
            _nextSlot.AttachView(view);
        }
    }
}