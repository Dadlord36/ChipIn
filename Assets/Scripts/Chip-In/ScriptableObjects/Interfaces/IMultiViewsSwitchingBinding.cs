using System;
using ScriptableObjects.SwitchBindings;
using Views;

namespace ScriptableObjects.Interfaces
{
    public interface ISingleViewSwitchingBinding
    {
        event Action<BaseView> ViewSwitchingRequested;
        void SwitchViews(in string viewNameToSwitchTo);
    }
    
    public interface IMultiViewsSwitchingBinding
    {
        event Action<MultiViewsSwitchingBinding.DualViewsSwitchData> ViewSwitchingRequested;

        /// <summary>
        /// Switches between current and given views
        /// </summary>
        /// <param name="currentViewName">Current view, shown on screen</param>
        /// <param name="viewNameToSwitchTo">View to be shown on screen</param>
        /// <returns>Instance of the switched view</returns>
        void SwitchViews(in string currentViewName, in string viewNameToSwitchTo);
    }
}