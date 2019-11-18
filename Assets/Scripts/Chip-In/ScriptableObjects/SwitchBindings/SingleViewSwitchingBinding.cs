using System;
using ScriptableObjects.Interfaces;
using UnityEngine;

namespace ScriptableObjects.SwitchBindings
{
    [CreateAssetMenu(fileName = nameof(SingleViewSwitchingBinding),
        menuName = nameof(SwitchBindings) + "/" + nameof(SingleViewSwitchingBinding), order = 0)]
    public sealed class SingleViewSwitchingBinding : ScriptableObject, ISingleViewSwitchingBinding
    {
        public event Action<string> ViewSwitchingRequested;

        public void SwitchViews(in string viewNameToSwitchTo)
        {
            ViewSwitchingRequested?.Invoke(viewNameToSwitchTo);
        }
    }
}