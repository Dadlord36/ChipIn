using UnityEngine;

namespace Controllers
{
    public interface IResettable
    {
        void Reset();
    }

    public class ResettableObjectsController : MonoBehaviour
    {
        [SerializeField] private GameObject[] resettableGameObjects;
        private IResettable[] _resettableObjects;
        

        public void Initialize()
        {
            CollectResettableObjects();
        }
        
        private void CollectResettableObjects()
        {
            _resettableObjects = new IResettable[resettableGameObjects.Length];
            for (int i = 0; i < resettableGameObjects.Length; i++)
            {
                _resettableObjects[i] = resettableGameObjects[i].GetComponent<IResettable>();
            }
        }

        public void ResetObjects()
        {
            for (int i = 0; i < _resettableObjects.Length; i++)
            {
                _resettableObjects[i].Reset();
            }
        }
    }
}