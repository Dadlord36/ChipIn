using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.Assertions;

namespace Views.ViewElements.ViewsSwitching
{
    public abstract class MultiViewsSwitch : ViewsSwitching
    {
        [SerializeField] private MultiViewsSwitchingBinding multiViewsSwitchingBinding;
        
        protected  override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(multiViewsSwitchingBinding, $"There is not views switch binding on: {name}");
            multiViewsSwitchingBinding.ViewSwitchingRequested += SwitchTo;
        }
        protected abstract void SwitchTo(MultiViewsSwitchingBinding.DualViewsSwitchData dualViewsSwitchData);
    }
}