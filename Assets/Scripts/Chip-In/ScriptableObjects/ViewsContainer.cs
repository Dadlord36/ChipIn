using System;
using UnityEngine;
using Views;
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
        private class ViewModelContainerItem : ContainerItem<BaseView>
        {
        }

        [SerializeField] private ViewModelContainerItem[] views;

        public BaseView GetViewById(in string viewId)
        {
            for (var i = 0; i < views.Length; i++)
            {
                var view = views[i].GetInstance;
                if (view.GetViewName == viewId)
                {
                    return view;
                }
            }
            throw new Exception($"There is no view with given ID: {viewId} in {name} views container");
        }
    }
}