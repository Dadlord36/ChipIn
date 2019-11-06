using ScriptableObjects;
using UnityEngine;
using ViewModels;

namespace Behaviours
{
    public class ViewsCreator : MonoBehaviour
    {
        [SerializeField] private ViewsContainer viewsContainer;
        [SerializeField] private ViewsSwitchingBinding viewsSwitchingBinding;

        public void PlaceInPreviousContainer<T>() where T : BaseViewModel
        {
            viewsSwitchingBinding.SwitchView<T>(null);
        }
    }
}