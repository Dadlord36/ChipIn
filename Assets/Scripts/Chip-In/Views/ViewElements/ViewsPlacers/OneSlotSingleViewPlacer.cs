using Utilities;

namespace Views.ViewElements.ViewsPlacers
{
    public class OneSlotSingleViewPlacer : SingleViewPlacer
    {
        private ViewSlot _viewSlot;
        private const string ViewSlotName = "ViewSlot";

        private void Awake()
        {
            _viewSlot = GameObjectsUtility.FindOrAttach<ViewSlot>(transform, ViewSlotName);
        }

        protected override void ReplaceCurrentViewWithGiven(BaseView givenView)
        {
            ReleaseSingleSlot(_viewSlot);
            _viewSlot.AttachView(givenView);
            givenView.Show();
        }
    }
}