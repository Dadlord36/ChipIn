using UnityEngine;

namespace ScriptableObjects.SwitchBindings
{
    public abstract class BaseViewsSwitchingBinding : ScriptableObject
    {
        [SerializeField] protected ViewsContainer viewsContainer;
    }
}