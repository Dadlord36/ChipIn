using ScriptableObjects.SwitchBindings;
using Utilities;

namespace Views.ViewElements.ViewsPlacers
{
    public class OneSlotMultiViewsPlacer : MultiViewsPlacer
    {
        private ViewSlot _viewSlot;
        private const string ViewSlotName = "ViewSlot";

        private void Awake()
        {
            _viewSlot = GameObjectsUtility.FindOrAttach<ViewSlot>(transform, ViewSlotName);
        }

        protected override void ReplaceCurrentViewWithGiven(MultiViewsSwitchingBinding.DualViewsSwitchData dualViewsSwitchData)
        {
            // ToDo: Implement dual slots Views Switching
            ReleaseSingleSlot(_viewSlot);
            _viewSlot.AttachView(dualViewsSwitchData.toView);
            dualViewsSwitchData.toView.Show();
        }
    }
}