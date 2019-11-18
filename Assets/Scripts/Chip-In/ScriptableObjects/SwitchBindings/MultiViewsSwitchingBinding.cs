using System;
using ScriptableObjects.Interfaces;
using UnityEngine;
using Views;

namespace ScriptableObjects.SwitchBindings
{
    [CreateAssetMenu(fileName = nameof(MultiViewsSwitchingBinding), menuName = nameof(SwitchBindings)+"/"+ nameof(MultiViewsSwitchingBinding),
        order = 0)]
    public class MultiViewsSwitchingBinding : ScriptableObject, IMultiViewsSwitchingBinding
    {
        public struct ViewsSwitchData
        {
            public ViewsSwitchData(BaseView fromView, BaseView toView)
            {
                this.fromView = fromView;
                this.toView = toView;
            }

            public readonly BaseView fromView, toView;
        }

        public event Action<ViewsSwitchData> ViewSwitchingRequested;
        [SerializeField] private ViewsContainer viewsContainer;

        public void SwitchViews(BaseView currentView, in string viewNameToSwitchTo)
        {
            ViewSwitchingRequested?.Invoke(new ViewsSwitchData(currentView, viewsContainer.GetViewById(viewNameToSwitchTo)));
        }
    }
}