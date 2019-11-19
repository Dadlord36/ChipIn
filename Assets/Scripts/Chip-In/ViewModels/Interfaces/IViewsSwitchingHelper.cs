namespace ViewModels.Interfaces
{
    public interface IViewsSwitchingHelper
    {
        void RequestSwitchToView(in string typeOfView);
        void SwitchToPreviousView();
    }
}