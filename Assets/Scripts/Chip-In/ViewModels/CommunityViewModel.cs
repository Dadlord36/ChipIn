using UnityEngine;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class CommunityViewModel : ViewsSwitchingViewModel
    {
        [Binding]
        public void SwitchToCommunityStatisticsView()
        {
            
            PrintLog("Switching to CommunityStatisticsView");
        }

        [Binding]
        public void SwitchToCommunityInterestView()
        {
            SwitchToView(nameof(CommunityInterestLabelsView));
            PrintLog("Switching to SwitchToCommunityInterestView");
        }

        private static void PrintLog(string message)
        {
            Debug.unityLogger.Log(LogType.Log, SwitchingViewTag, message);
        }
    }
}