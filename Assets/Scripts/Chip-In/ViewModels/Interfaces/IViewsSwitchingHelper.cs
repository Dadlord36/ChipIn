namespace ViewModels.Interfaces
{
    public interface IViewsSwitchingHelper
    {
        void RequestSwitchToView(string typeOfView);
        void SwitchToPreviousView();
    }
}