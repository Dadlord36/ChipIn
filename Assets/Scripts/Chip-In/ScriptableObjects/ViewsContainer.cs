using System;
using UnityEngine;
using Views;
using Object = UnityEngine.Object;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(ViewsContainer), menuName = "Containers/" + nameof(ViewsContainer), order = 0)]
    public class ViewsContainer : ScriptableObject
    {
        private class ContainerItem<T> where T : Object
        {
            protected readonly T Prefab;
            [NonSerialized] protected T Instance;

            protected ContainerItem(T prefab)
            {
                Prefab = prefab;
            }

            protected bool InstanceIsValid => Instance != null;
            public T GetInstance => Instance == null ? Instance = Instantiate(Prefab) : Instance;
            
        }

        [Serializable]
        private class ViewModelContainerItem : ContainerItem<BaseView>
        {
            public string ViewName => Prefab.ViewName;

            public ViewModelContainerItem(BaseView prefab) : base(prefab)
            {
            }
            
            public void RemoveInstance()
            {
                if (!InstanceIsValid) return;
                Destroy(Instance.gameObject);
                Instance = null;
            }
        }
        
        private void OnEnable()
        {
            Initialize();
        }

        void Initialize()
        {
            if (containingViews == null)
            {
                Debug.LogWarning($"There is no views in ViewsContainer {name}");
                return;
            }
            _viewsContainer = new ViewModelContainerItem[containingViews.Length];
            for (int i = 0; i < containingViews.Length; i++)
            {
                _viewsContainer[i] = new ViewModelContainerItem(containingViews[i]);
            }
        }

        [SerializeField] private BaseView[] containingViews;
        private ViewModelContainerItem[] _viewsContainer;

        public BaseView GetViewByName(in string viewName)
        {
            for (var i = 0; i < _viewsContainer.Length; i++)
            {
                if (_viewsContainer[i].ViewName == viewName)
                {
                    return _viewsContainer[i].GetInstance;
                }
            }
            throw new Exception($"There is no view with given ID: {viewName} in {name} views container");
        }

        public void RemoveExistingViewInstance(in string viewName)
        {
            for (var i = 0; i < _viewsContainer.Length; i++)
            {
                if (_viewsContainer[i].ViewName == viewName)
                {
                    _viewsContainer[i].RemoveInstance();
                    return;
                }
            }
            throw new Exception($"There is no view with given ID: {viewName} in {name} views container");
        }
    }
}