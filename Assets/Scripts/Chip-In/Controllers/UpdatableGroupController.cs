using System.Collections.Generic;
using ActionsTranslators;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = nameof(UpdatableGroupController), menuName = nameof(Controllers) + "/" + nameof(UpdatableGroupController),
        order = 0)]
    public class UpdatableGroupController : ScriptableObject, IUpdatable
    {
        [SerializeField] private Object[] updatableObjects;
        private IUpdatable[] _updatableInterfaces;

        private void OnEnable()
        {
            var list = new List<IUpdatable>(updatableObjects.Length);

            foreach (var updatableObject in updatableObjects)
            {
                var updatable = (IUpdatable) updatableObject;
                if (updatable == null) continue;

                list.Add(updatable);
            }

            _updatableInterfaces = list.ToArray();
        }

        public void Update()
        {
            for (int i = 0; i < _updatableInterfaces.Length; i++)
            {
                _updatableInterfaces[i].Update();
            }
        }
    }
}