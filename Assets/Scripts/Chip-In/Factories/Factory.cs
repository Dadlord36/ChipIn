using UnityEngine;
using ViewModels.Helpers;

namespace Factories
{
    public static class Factory
    {
        public static IViewsSwitchingHelper CreateViewSwitchingHelper()
        {
            return ViewsSwitchingHelper.Instance;
        }
    }
}