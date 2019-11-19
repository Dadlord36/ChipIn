using System;
using Common;
using UnityEngine;
using ViewModels.Helpers;
using ViewModels.Interfaces;

namespace Factories
{
    public static class Factory
    {
        public static IViewsSwitchingHelper CreateMultiViewSwitchingHelper()
        {
            return SingleViewSwitchingHelper.Instance;
        }

        public static IViewsSwitchingHelper CreateSingleViewSwitchingHelper()
        {
            throw new NotImplementedException();
        }

        public static ITimeline AddTimer(GameObject gameObject)
        {
            return gameObject.AddComponent<BehaviourTimeline>();
        }
    }
}