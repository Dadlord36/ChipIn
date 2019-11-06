using ScriptableObjects;
using Utilities;

namespace Views.ViewElements
{
    public class OneSlotViewsPlace : ViewPlacer
    {
        private ViewSlot _viewSlot;
        private const string ViewSlotName = "ViewSlot";

        private void Awake()
        {
            _viewSlot = GameObjectsUtility.FindOrAttach<ViewSlot>(transform, ViewSlotName);
        }

        protected override void ReplaceCurrentViewsWithGiven(ViewsSwitchingBinding.ViewsSwitchData viewsSwitchData)
        {
            ReleaseSingleSlot(_viewSlot);
            _viewSlot.AttachView(viewsSwitchData.toView);
            viewsSwitchData.toView.Show();
        }
    }
}