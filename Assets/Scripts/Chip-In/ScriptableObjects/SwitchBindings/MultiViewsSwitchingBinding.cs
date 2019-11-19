using System;
using ScriptableObjects.Interfaces;
using UnityEngine;
using Views;

namespace ScriptableObjects.SwitchBindings
{
    [CreateAssetMenu(fileName = nameof(MultiViewsSwitchingBinding),
        menuName = nameof(SwitchBindings) + "/" + nameof(MultiViewsSwitchingBinding),
        order = 0)]
    public sealed class MultiViewsSwitchingBinding : BaseViewsSwitchingBinding, IMultiViewsSwitchingBinding
    {
        public event Action<DualViewsSwitchData> ViewSwitchingRequested;

        public struct DualViewsSwitchData
        {
            public DualViewsSwitchData(BaseView fromView, BaseView toView)
            {
                this.fromView = fromView;
                this.toView = toView;
            }

            public readonly BaseView fromView, toView;
        }

        public void SwitchViews(in string currentViewName, in string viewNameToSwitchTo)
        {
            ViewSwitchingRequested?.Invoke(new DualViewsSwitchData(viewsContainer.GetViewByName(currentViewName),
                viewsContainer.GetViewByName(viewNameToSwitchTo)));
        }
    }
}