using UnityEngine;
using ViewModels.Helpers;
using ViewModels.Interfaces;

namespace Factories
{
    public static class Factory
    {
        public static IViewsSwitchingHelper CreateViewSwitchingHelper()
        {
            return MultiViewsSwitchingHelper.Instance;
        }
    }
}