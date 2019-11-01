using System;
using UnityEngine;
using ViewModels;
using ViewModels.Interfaces;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(ViewsSwitcherBinding), menuName = "Bindings/" + nameof(ViewsSwitcherBinding),
        order = 0)]
    public class ViewsSwitcherBinding : ScriptableObject, IViewSwitcher
    {
        public struct ViewsSwitchData
        {
            public ViewsSwitchData(BaseViewModel fromViewModel, BaseViewModel toViewModel)
            {
                this.fromViewModel = fromViewModel;
                this.toViewModel = toViewModel;
            }

            public readonly BaseViewModel fromViewModel, toViewModel;
        }
        
        public event Action<ViewsSwitchData> ViewSwitchingRequested;
        [SerializeField] private ViewsContainer viewsContainer;

        public void SwitchView<T>(BaseViewModel currentViewModel) where T : BaseViewModel
        {
            ViewSwitchingRequested?.Invoke(new ViewsSwitchData(currentViewModel, viewsContainer.GetViewOfType<T>()));
        }
    }
}