using ScriptableObjects.SwitchBindings;
using UnityEngine;

namespace Views
{
    public abstract class SingleViewSwitching : ViewsSwitching
    {
        [SerializeField] private SingleViewSwitchingBinding singleViewSwitchingBinding;
        protected override void Awake()
        {
            base.Awake();
            
        }
    }
}