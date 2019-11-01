using ScriptableObjects;
using UnityEngine;
using ViewModels;

namespace Behaviours
{
    public class ViewsCreator : MonoBehaviour
    {
        [SerializeField] private ViewsContainer viewsContainer;
        [SerializeField] private ViewsPlacer placer;

        public void PlaceInPreviousContainer<T>() where T : BaseViewModel
        {
            placer.PlaceInPreviousContainer(viewsContainer.GetViewOfType<T>().ViewRootRectTransform);
        }

        public void PlaceInNextContainer<T>() where T : BaseViewModel
        {
            placer.PlaceInNextContainer(viewsContainer.GetViewOfType<T>().ViewRootRectTransform);
        }
    }
}