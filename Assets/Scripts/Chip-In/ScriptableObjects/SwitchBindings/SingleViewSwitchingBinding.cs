using System;
using ScriptableObjects.Interfaces;
using UnityEngine;
using Views;

namespace ScriptableObjects.SwitchBindings
{
    [CreateAssetMenu(fileName = nameof(SingleViewSwitchingBinding),
        menuName = nameof(SwitchBindings) + "/" + nameof(SingleViewSwitchingBinding), order = 0)]
    public sealed class SingleViewSwitchingBinding : BaseViewsSwitchingBinding, ISingleViewSwitchingBinding
    {
        public event Action<BaseView> ViewSwitchingRequested;
        public void SwitchViews(in string viewNameToSwitchTo)
        {
            ViewSwitchingRequested?.Invoke( viewsContainer.GetViewByName(viewNameToSwitchTo));
        }
    }
}