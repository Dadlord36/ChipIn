using System;
using UnityEngine;
using ViewModels;
using Views;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(ViewsSwitchingBinding), menuName = "Bindings/" + nameof(ViewsSwitchingBinding),
        order = 0)]
    public class ViewsSwitchingBinding : ScriptableObject
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

        public void SwitchView<T>(BaseView view) where T : BaseViewModel
        {
            ViewSwitchingRequested?.Invoke(new ViewsSwitchData(view, viewsContainer.GetViewOfType<T>().View));
        }
    }
}