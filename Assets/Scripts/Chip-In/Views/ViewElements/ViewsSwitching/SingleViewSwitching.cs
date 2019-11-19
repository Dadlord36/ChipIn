using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.Assertions;

namespace Views.ViewElements.ViewsSwitching
{
    public abstract class SingleViewSwitching : ViewsSwitching
    {
        [SerializeField] private SingleViewSwitchingBinding singleViewSwitchingBinding;

        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(singleViewSwitchingBinding, $"There is not views switch binding on: {name}");
            singleViewSwitchingBinding.ViewSwitchingRequested += SwitchTo;
        }

        protected abstract void SwitchTo(BaseView viewToSwitchTo);
    }
}