namespace ViewModels.Interfaces
{
    public interface IViewsSwitchingController
    {
        void RequestSwitchToView(string toViewName);
        void SwitchToPreviousView();
    }
}