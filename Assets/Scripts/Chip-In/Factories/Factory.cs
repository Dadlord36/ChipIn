using System;
using Common;
using Common.Timers;
using UnityEngine;
using ViewModels.Interfaces;

namespace Factories
{
    public static class Factory
    {
        public static IViewsSwitchingController CreateSingleViewSwitchingHelper()
        {
            throw new NotImplementedException();
        }

        public static ITimeline AddTimer(GameObject gameObject)
        {
            return gameObject.AddComponent<ParametricBehaviourTimeline>();
        }
    }
}