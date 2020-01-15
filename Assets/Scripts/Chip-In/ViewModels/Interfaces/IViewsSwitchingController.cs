namespace ViewModels.Interfaces
{
    public interface IViewsSwitchingController
    {
        void RequestSwitchToView(in string typeOfView);
        void SwitchToPreviousView();
    }
}