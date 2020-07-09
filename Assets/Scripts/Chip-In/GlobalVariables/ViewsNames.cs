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

        public static readonly List<string> BottomBarActiveViews = new List<string>
        {
            nameof(MarketplaceView), nameof(CommunityView), nameof(SettingsView)
        };

        static ViewsNames()
        {
            MainViewsNames.Sort();
            BottomBarActiveViews.Sort();
        }

        public static bool IsMainView(in string viewName)
        {
            return MainViewsNames.BinarySearch(viewName) >= 0;
        }

        public static bool IsBottomBarActiveView(in string viewName)
        {
            return BottomBarActiveViews.BinarySearch(viewName) >= 0;
        }
    }
}