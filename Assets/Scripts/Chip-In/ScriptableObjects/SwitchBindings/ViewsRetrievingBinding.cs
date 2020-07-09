using System;
using UnityEngine;
using Views;

namespace ScriptableObjects.SwitchBindings
{
    [CreateAssetMenu(fileName = nameof(ViewsRetrievingBinding),
        menuName = nameof(SwitchBindings) + "/" + nameof(ViewsRetrievingBinding), order = 0)]
    public class ViewsRetrievingBinding : ScriptableObject
    {
        public event Action<BaseView> ViewBeingRetrieved;

        public void RetrieveView(BaseView view)
        {
            ViewBeingRetrieved?.Invoke(view);
        }
    }
}