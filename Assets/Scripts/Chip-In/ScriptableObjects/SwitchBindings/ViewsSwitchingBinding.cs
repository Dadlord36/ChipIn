using System;
using ScriptableObjects.Interfaces;
using UnityEngine;
using Views;

namespace ScriptableObjects.SwitchBindings
{
    [CreateAssetMenu(fileName = nameof(ViewsSwitchingBinding),
        menuName = nameof(SwitchBindings) + "/" + nameof(ViewsSwitchingBinding),
        order = 0)]
    public sealed class ViewsSwitchingBinding : BaseViewsSwitchingBinding, IViewsSwitchingBinding
    {
        public event Action<ViewsSwitchData> ViewSwitchingRequested;

        public void SwitchViews(in string viewNameToSwitchTo, ViewsSwitchData.AppearingSide viewAppearingSide)
        {
            ViewSwitchingRequested?.Invoke(new ViewsSwitchData(viewsContainer.GetViewByName(viewNameToSwitchTo),
                viewAppearingSide));
        }
    }
}