namespace ViewModels.Interfaces
{
    public interface IViewSwitcher
    {
        /// <summary>
        /// Switches form current view to the given type one
        /// </summary>
        /// <param name="currentViewModel">View model, from where function is calling</param>
        /// <typeparam name="T">Type of ViewModel corresponding to View to switch to</typeparam>
        void SwitchView<T>(BaseViewModel currentViewModel) where T:BaseViewModel;
    }
}