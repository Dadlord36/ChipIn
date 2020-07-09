using Common.Interfaces;
using UnityEngine;
using Views;

namespace Common
{
    [DisallowMultipleComponent]
    public class NameObjectSetterFromViewName : MonoBehaviour, INamedTempObject
    {
        [SerializeField] private BaseView referencedView;

        public string Name => referencedView.ViewName;

        public void Destroy()
        {
            Destroy(this);
        }
    }
}