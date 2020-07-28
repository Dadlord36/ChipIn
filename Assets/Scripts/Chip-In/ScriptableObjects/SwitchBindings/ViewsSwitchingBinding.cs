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

        public void SwitchViews(in ViewsPairInfo viewsPairInfo, FormsTransitionBundle formsTransitionBundle)
        {
            var viewToSwitchTo = viewsContainer.GetViewByName(viewsPairInfo.ViewToSwitchToName);
            viewToSwitchTo.FormTransitionBundle = formsTransitionBundle;
            ViewSwitchingRequested?.Invoke(viewToSwitchTo);
        }

        public void RemoveExistingViewInstance(in string viewName)
        {
            viewsContainer.RemoveExistingViewInstance(viewName);
        }
    }
}