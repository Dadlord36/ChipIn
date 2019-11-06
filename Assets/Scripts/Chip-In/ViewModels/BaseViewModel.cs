using ScriptableObjects;
using UnityEngine;
using Views;

namespace ViewModels
{
    /// <summary>
    /// Base view model class
    /// </summary>
    public abstract class BaseViewModel : MonoBehaviour
    {
        [SerializeField] private BaseView view;
        [SerializeField] protected ViewsSwitchingBinding viewsSwitchingBinding;

        public BaseView View => view;
    }
}