using System;
using ScriptableObjects.SwitchBindings;
using Views;

namespace ScriptableObjects.Interfaces
{
    public interface ISingleViewSwitchingBinding
    {
        event Action<string> ViewSwitchingRequested;
        void SwitchViews(in string viewNameToSwitchTo);
    }
    
    public interface IMultiViewsSwitchingBinding
    {
        event Action<MultiViewsSwitchingBinding.ViewsSwitchData> ViewSwitchingRequested;

        /// <summary>
        /// Switches between current and given views
        /// </summary>
        /// <param name="currentView">Current view, shown on screen</param>
        /// <param name="viewNameToSwitchTo">View to be shown on screen</param>
        /// <returns>Instance of the switched view</returns>
        void SwitchViews(BaseView currentView, in string viewNameToSwitchTo);
    }
}