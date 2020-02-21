using System;
using ScriptableObjects.SwitchBindings;
using Views;

namespace ScriptableObjects.Interfaces
{
    public interface IViewsSwitchingBinding
    {
        event Action<ViewsSwitchData> ViewSwitchingRequested;

        /// <summary>
        /// Switches between current and given views
        /// </summary>
        /// <param name="viewNameToSwitchTo">View to be shown on screen</param>
        /// <param name="viewAppearingSide"></param>
        /// <param name="currentViewName">Current view, shown on screen</param>
        /// <returns>Instance of the switched view</returns>
        void SwitchViews(in string viewNameToSwitchTo, ViewsSwitchData.AppearingSide viewAppearingSide);
    }
}