using UnityEngine;

namespace Utilities
{
    public static class GameObjectsUtility
    {
        public static T FindOrAttach<T>(Transform rootTransform, in string objectName) where T : Component
        {
            T GetOrAttachComponent(GameObject gameObject)
            {
                return gameObject.TryGetComponent(out T component) ? component : gameObject.gameObject.AddComponent<T>();
            }

            var foundObject = rootTransform.Find(objectName);

            if (foundObject)
            {
                return GetOrAttachComponent(foundObject.gameObject);
            }

            foundObject = new GameObject(objectName).transform;
            foundObject.SetParent(rootTransform);

            return foundObject.gameObject.AddComponent<T>();
        }

        public static GameObject CreateAndAttachToParent(Transform parent,string objectName, Vector3 localPosition)
        {
            var newGameObject = new GameObject(objectName);
            newGameObject.transform.parent = parent;
            newGameObject.transform.localPosition = localPosition;
            return newGameObject;
        }
    }
}