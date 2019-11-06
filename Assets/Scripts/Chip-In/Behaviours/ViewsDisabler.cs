using ScriptableObjects;
using UnityEngine;
using Views;

namespace Behaviours
{
    public class ViewsDisabler : MonoBehaviour
    {
        [SerializeField] private ViewsRetrievingBinding viewsRetrievingBinding;

        private void OnEnable()
        {
            viewsRetrievingBinding.ViewBeingRetrieved+= DisableAndAttach;
        }

        private void DisableAndAttach(BaseView view)
        {
            view.Hide();
            view.ViewRootRectTransform.SetParent(transform);
        }
    }
}