using System;
using Views;

namespace ScriptableObjects.Interfaces
{
    public interface IViewsSwitchingBinding
    {
        event Action<ViewsSwitchingBinding.ViewsSwitchData> ViewSwitchingRequested;

        /// <summary>
        /// Switches between current and given views
        /// </summary>
        /// <param name="currentView">Current view, shown on screen</param>
        /// <param name="viewIdToSwitchTo">View to be shown on screen</param>
        /// <returns>Instance of the switched view</returns>
        void SwitchViews(BaseView currentView, in string viewIdToSwitchTo);
    }
}