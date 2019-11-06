using System;
using UnityEngine;
using Views;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(ViewsRetrievingBinding), menuName = "Bindings/" + nameof(ViewsRetrievingBinding),
        order = 0)]
    public class ViewsRetrievingBinding : ScriptableObject
    {
        public event Action<BaseView> ViewBeingRetrieved;

        public void RetrieveView(BaseView view)
        {
            ViewBeingRetrieved?.Invoke(view);
        }
    }
}