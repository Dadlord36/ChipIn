using System;
using UnityEngine;
using ViewModels;
using Object = UnityEngine.Object;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(ViewsContainer), menuName = "Containers/" + nameof(ViewsContainer), order = 0)]
    public class ViewsContainer : ScriptableObject
    {
        [Serializable]
        private class ContainerItem<T> where T : Object
        {
            public T prefab;
            [NonSerialized] private T _instance;

            public T GetInstance => _instance == null ? _instance = Instantiate(prefab) : _instance;
        }
        
        [Serializable]
        private class ViewModelContainerItem : ContainerItem<BaseViewModel>{}
        
        [SerializeField] private ViewModelContainerItem[] viewModels;

        public T GetViewOfType<T>() where T : BaseViewModel
        {
            for (var i = 0; i < viewModels.Length; i++)
            {
                if (viewModels[i].prefab is T)
                {
                    return viewModels[i].GetInstance as T;
                }
            }

            return null;
        }
    }
}