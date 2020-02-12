namespace ViewModels.Interfaces
{
    public interface IViewsSwitchingController
    {
        void RequestSwitchToView(string fromViewName,string toViewName);
        void SwitchToPreviousView();
    }
}