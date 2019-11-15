namespace ViewModels.Helpers
{
    public interface IViewsSwitchingHelper
    {
        void SwitchToView(string typeOfView);
        void SwitchToPreviousView();
    }
}