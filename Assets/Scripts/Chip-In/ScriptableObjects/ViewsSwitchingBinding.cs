using System;
using ScriptableObjects.Interfaces;
using UnityEngine;
using Views;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(ViewsSwitchingBinding), menuName = "Bindings/" + nameof(ViewsSwitchingBinding),
        order = 0)]
    public class ViewsSwitchingBinding : ScriptableObject, IViewsSwitchingBinding
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

        public BaseView SwitchViews(BaseView currentView, in string viewName)
        {
            var viewToSwitchTo = viewsContainer.GetViewById(viewName);
            ViewSwitchingRequested?.Invoke(new ViewsSwitchData(currentView, viewToSwitchTo));
            return viewToSwitchTo;
        }
    }
}