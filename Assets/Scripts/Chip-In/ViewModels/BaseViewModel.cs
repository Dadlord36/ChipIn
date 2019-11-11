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
        public BaseView View => view;
    }
}