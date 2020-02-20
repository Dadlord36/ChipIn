using ScriptableObjects.SwitchBindings;

namespace ViewModels.Interfaces
{
    public interface IViewsSwitchingController
    {
        void RequestSwitchToView(string fromViewName, string toViewName,
            ViewsSwitchData.AppearingSide viewAppearingSide);

        void SwitchToPreviousView();
    }
}