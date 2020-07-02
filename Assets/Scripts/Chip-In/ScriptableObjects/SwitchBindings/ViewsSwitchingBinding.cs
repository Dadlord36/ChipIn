using System;
using UnityEngine;
using Views;

namespace ScriptableObjects.SwitchBindings
{
    [CreateAssetMenu(fileName = nameof(ViewsSwitchingBinding),
        menuName = nameof(SwitchBindings) + "/" + nameof(ViewsSwitchingBinding),
        order = 0)]
    public sealed class ViewsSwitchingBinding : ScriptableObject
    {
        [SerializeField] private ViewsContainer viewsContainer;
        public event Action<BaseView> ViewSwitchingRequested;

        public void SwitchViews(in ViewsPairInfo viewsPairInfo)
        {
            ViewSwitchingRequested?.Invoke(viewsContainer.GetViewByName(viewsPairInfo.ViewToSwitchToName));
        }

        public void RemoveExistingViewInstance(in string viewName)
        {
            viewsContainer.RemoveExistingViewInstance(viewName);
        }
    }
}