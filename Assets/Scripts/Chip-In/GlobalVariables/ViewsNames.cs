using System.Collections.Generic;
using Views;

namespace GlobalVariables
{
    public static class ViewsNames
    {
        public static readonly List<string> MainViewsNames = new List<string>
        {
            nameof(MarketplaceView), nameof(MyChallengeView), nameof(CartView),
            nameof(CommunityView), nameof(SettingsView)
        };

        static ViewsNames()
        {
            MainViewsNames.Sort();
        }

        public static bool IsMainView(string viewName)
        {
            return MainViewsNames.BinarySearch(viewName) >= 0;
        }
    }
}