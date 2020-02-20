using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.Assertions;

namespace Views.ViewElements.ViewsSwitching
{
    public abstract class MultiViewsSwitch : ViewsSwitching
    {
        [SerializeField] private ViewsSwitchingBinding viewsSwitchingBinding;

        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(viewsSwitchingBinding, $"There is not views switch binding on: {name}");
            viewsSwitchingBinding.ViewSwitchingRequested += SwitchTo;
        }

        protected abstract void SwitchTo(ViewsSwitchData viewsSwitchData);
    }
}