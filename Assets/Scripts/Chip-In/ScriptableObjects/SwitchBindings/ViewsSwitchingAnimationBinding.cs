using System;
using UnityEngine;

namespace ScriptableObjects.SwitchBindings
{
    [CreateAssetMenu(fileName = nameof(ViewsSwitchingAnimationBinding),
        menuName = nameof(SwitchBindings) + "/" + nameof(ViewsSwitchingAnimationBinding), order = 0)]
    public class ViewsSwitchingAnimationBinding : ScriptableObject
    {
        public event Action<ViewsSwitchingParameters> ViewsSwitchingAnimationRequested;

        public void RequestViewsSwitchingAnimation(in ViewsSwitchingParameters viewsSwitchingParameters)
        {
            OnViewsSwitchingAnimationRequested(viewsSwitchingParameters);
        }

        private void OnViewsSwitchingAnimationRequested(in ViewsSwitchingParameters viewsSwitchingParameters)
        {
            ViewsSwitchingAnimationRequested?.Invoke(viewsSwitchingParameters);
        }
    }
}