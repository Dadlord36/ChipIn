using ActionsTranslators;
using UnityEngine;

namespace Behaviours.Common
{
    public class UpdateTranslationBehaviour : MonoBehaviour
    {
        [SerializeField] private Object[] objectsWithUpdatableInterface;
        private IUpdatable[] _updatableInterfaces;

        private void Start()
        {
            _updatableInterfaces = new IUpdatable[objectsWithUpdatableInterface.Length];
            for (int i = 0; i < objectsWithUpdatableInterface.Length; i++)
            {
                _updatableInterfaces[i] = objectsWithUpdatableInterface[i] as IUpdatable;
            }
        }

        private void Update()
        {
            for (int i = 0; i < _updatableInterfaces.Length; i++)
            {
                _updatableInterfaces[i].Update();
            }
        }
    }
}